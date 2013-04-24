using System;
using System.Collections.Generic;
using Microsoft.Health;
using Microsoft.Health.ItemTypes;
using System.Diagnostics;
using System.Web.Configuration;
using Microsoft.Health.Web;
using HealthVaultHelper;
using System.Xml.XPath;


public partial class pcp_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLstPatients();
            frmSelectedPatient.Visible = false;
        }
    }

    private void BindLstPatients()
    {
        string username = Profile.UserName;
        ParticipantDAO dao = new ParticipantDAO();
        ICollection<Participant> myPatients = dao.GetParticipantsForPcp(username);

        lstPatients.DataSource = myPatients;
        lstPatients.DataValueField = "ID";
        lstPatients.DataTextField = "FullName";
        lstPatients.DataBind();
    }


    protected void lstPatients_SelectedIndexChanged(object sender, EventArgs e)
    {
        Guid pid = Guid.Parse(lstPatients.SelectedValue);
        Participant participant = (new ParticipantDAO()).FindParticipantById(pid);
        DisplaySelectedParticipant(participant);
    }

    private void DisplaySelectedParticipant(Participant participant)
    {
        frmSelectedPatient.Visible = true;
        lblPatientHeader.Text = participant.FullName;

        if (participant.HasAuthorized)
        {
            SelectedPatientData.Visible = true;
            UnauthPatientMessage.Visible = false;
            GetParticipantData(participant);
        }
        else
        {
            SelectedPatientData.Visible = false;
            UnauthPatientMessage.Visible = true;
        }
    }

    private void GetParticipantData(Participant participant)
    {
        try
        {

            OfflineWebApplicationConnection conn = HVConnectionManager.CreateConnection(participant.HVPersonID);
            HealthRecordAccessor accessor = new HealthRecordAccessor(conn, participant.HVRecordID);
            HealthRecordSearcher search = accessor.CreateSearcher();

            search.Filters.Add(new HealthRecordFilter(Basic.TypeId));
            search.Filters.Add(new HealthRecordFilter(Allergy.TypeId));
            search.Filters.Add(new HealthRecordFilter(Height.TypeId));
            search.Filters.Add(new HealthRecordFilter(BloodGlucose.TypeId));
            search.Filters.Add(new HealthRecordFilter(BloodPressure.TypeId));
            search.Filters.Add(new HealthRecordFilter(Condition.TypeId));
            search.Filters.Add(new HealthRecordFilter(Procedure.TypeId));
            search.Filters.Add(new HealthRecordFilter(Medication.TypeId));
            search.Filters.Add(new HealthRecordFilter(Weight.TypeId));
            HealthRecordItemCollection items = null;


            //Basic info = (Basic)items[0];
            items = search.GetMatchingItems()[0];
            //Debug.WriteLine(search.GetMatchingItemsRaw());
            if (items.Count > 0)
                lblPatientBasic.Text = "Gender, Birth Year: " + items[0].ToString();
            else
                lblPatientBasic.Text = "No Basic Info Available";
            items = search.GetMatchingItems()[1];
            if (items.Count > 0)
                lblPatientAllergy.Text = "Allergy: " + items[0].ToString();
            else
                lblPatientAllergy.Text = "No Allergy Info Available";
            items = search.GetMatchingItems()[2];
            if (items.Count > 0)
                lblPatientHeight.Text = "Height: " + items[0].ToString();
            else
                lblPatientHeight.Text = "No Height Info Available";
            items = search.GetMatchingItems()[3];
            if (items.Count > 0)
                lblPatientGlucose.Text = "Blood Glucose: " + items[0].ToString();
            else
                lblPatientGlucose.Text = "No Blood Glucose Info Available";
            items = search.GetMatchingItems()[4];
            if (items.Count > 0)
                lblPatientPressure.Text = "Blood Pressure: " + items[0].ToString();
            else
                lblPatientPressure.Text = "No Blood Pressure Info Available";
            items = search.GetMatchingItems()[5];
            if (items.Count > 0)
                lblPatientCondition.Text = "Condition: " + items[0].ToString();
            else
                lblPatientCondition.Text = "No Condition Info Available";
            items = search.GetMatchingItems()[6];
            if (items.Count > 0)
                lblPatientProcedure.Text = "Procedure: " + items[0].ToString();
            else
                lblPatientProcedure.Text = "No Procedure Info Available";
            items = search.GetMatchingItems()[7];
            if (items.Count > 0)
                lblPatientMedication.Text = "Medications Prescribed: " + items[0].ToString();
            else
                lblPatientMedication.Text = "No Medication Info Available";
            items = search.GetMatchingItems()[8];
            if (items.Count > 0)
                lblPatientWeight.Text = "Weight: " + items[0].ToString();
            else
                lblPatientWeight.Text = "No Weight Info Available";
        }
        catch (HealthServiceException ex)
        {
            String msg = String.Empty;
            if (ex is HealthServiceAccessDeniedException)
                msg = "The application may be trying to read user data from an anonymous connection.\n";
            msg += ex.Error.ErrorInfo + "\n" + ex.Message.ToString();
            Debug.WriteLine(msg);
        }
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        Guid pid = Guid.Parse(lstPatients.SelectedValue);
        Session["selected_patient_pid"] = pid;
        Response.Redirect("~/pcp/edit.aspx");
    }
    protected void btnGetCcd_Click(object sender, EventArgs e)
    {
        Guid CCR_THING_GUID = new Guid("1e1ccbfc-a55d-4d91-8940-fa2fbf73c195");
        Guid CCD_THING_GUID = new Guid("9c48a2b8-952c-4f5a-935d-f3292326bf54");

        Guid pid = Guid.Parse(lstPatients.SelectedValue);
        Participant participant = GetParticipant(pid);

        List<HealthRecordItem> ccdItems = GetValues<HealthRecordItem>(participant, CCD_THING_GUID);

        foreach (HealthRecordItem ccd in ccdItems)
        {
            Debug.WriteLine(ccd.TypeSpecificData.ToString());
            XPathNavigator nav = ccd.TypeSpecificData.CreateNavigator();
            // ccd xml data is in ccd.TypeSpecificData
        }

    }

    private Participant GetParticipant(Guid pid)
    {
        ParticipantDAO dao = new ParticipantDAO();
        return dao.FindParticipantById(pid);
    }

    private HealthRecordSearcher GetSearcher(Participant participant)
    {
        OfflineWebApplicationConnection conn = HVConnectionManager.CreateConnection(participant.HVPersonID);
        HealthRecordAccessor accessor = new HealthRecordAccessor(conn, participant.HVRecordID);
        return accessor.CreateSearcher();
    }

    T GetSingleValue<T>(Participant participant, Guid typeID) where T : class
    {
        HealthRecordSearcher searcher = GetSearcher(participant);
        HealthRecordFilter filter = new HealthRecordFilter(typeID);
        searcher.Filters.Add(filter);
        HealthRecordItemCollection items = searcher.GetMatchingItems()[0];
        if (items != null && items.Count > 0)
        {
            return items[0] as T;
        }
        else
        {
            return null;
        }
    }

    List<T> GetValues<T>(Participant participant, Guid typeID) where T : HealthRecordItem
    {
        HealthRecordSearcher searcher = GetSearcher(participant);
        HealthRecordFilter filter = new HealthRecordFilter(typeID);
        searcher.Filters.Add(filter);
        HealthRecordItemCollection items = searcher.GetMatchingItems()[0];
        List<T> typedList = new List<T>();
        foreach (HealthRecordItem item in items)
        {
            typedList.Add((T)item);
        }

        return typedList;
    }
}
