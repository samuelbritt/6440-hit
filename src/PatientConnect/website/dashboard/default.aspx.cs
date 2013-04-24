using System;
using System.Collections.Generic;
using Microsoft.Health;
using Microsoft.Health.ItemTypes;
using System.Diagnostics;
using System.Web.Configuration;
using Microsoft.Health.Web;
using HealthVaultHelper;
using System.Xml.XPath;
using System.Xml;
using System.Collections.ObjectModel;
using Logic;


public partial class dashboard_default : System.Web.UI.Page
{
    Participant selectedParticipant;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLstPatients();
            frmSelectedPatient.Visible = false;
        }
        selectedParticipant = GetSelectedParticpant();
    }

    private void BindLstPatients()
    {
        lstPatients.DataSource = GetParticipantList();
        lstPatients.DataValueField = "ID";
        lstPatients.DataTextField = "FullName";
        lstPatients.DataBind();
    }

    private ICollection<Participant> GetParticipantList()
    {
        if (User.IsInRole(Roles.ONCOLOGIST))
        {
            return GetOncologistPatients();
        }
        else
        {
            return GetPcpPatients();
        }
    }


    private ICollection<Participant> GetOncologistPatients()
    {
        ParticipantDAO dao = new ParticipantDAO();
        return dao.GetEligibleParticipants();
    }

    private ICollection<Participant> GetPcpPatients()
    {
        string username = Profile.UserName;
        ParticipantDAO dao = new ParticipantDAO();
        return dao.GetParticipantsForPcp(username);
    }

    private Participant GetSelectedParticpant()
    {
        String selection = lstPatients.SelectedValue;
        if (string.IsNullOrEmpty(selection))
            return null;
        Guid pid = Guid.Parse(selection);
        return GetParticipant(pid);
    }

    private Participant GetParticipant(Guid pid)
    {
        ParticipantDAO dao = new ParticipantDAO();
        return dao.FindParticipantById(pid);
    }


    protected void lstPatients_SelectedIndexChanged(object sender, EventArgs e)
    {
        DisplaySelectedParticipant(selectedParticipant);
    }

    private void DisplaySelectedParticipant(Participant participant)
    {
        frmSelectedPatient.Visible = true;
        lblPatientHeader.Text = participant.FullName;

        if (participant.HasAuthorized)
        {
            SelectedPatientData.Visible = true;
            lblPatientBasicInfo.Visible = true;
            UnauthPatientMessage.Visible = false;
            GetParticipantData(participant);
        }
        else
        {
            SelectedPatientData.Visible = false;
            lblPatientBasicInfo.Visible = false;
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
            search.Filters.Add(new HealthRecordFilter(Personal.TypeId));
            search.Filters.Add(new HealthRecordFilter(Allergy.TypeId));
            search.Filters.Add(new HealthRecordFilter(Height.TypeId));
            search.Filters.Add(new HealthRecordFilter(BloodGlucose.TypeId));
            search.Filters.Add(new HealthRecordFilter(BloodPressure.TypeId));
            search.Filters.Add(new HealthRecordFilter(Condition.TypeId));
            search.Filters.Add(new HealthRecordFilter(Procedure.TypeId));
            search.Filters.Add(new HealthRecordFilter(Medication.TypeId));
            search.Filters.Add(new HealthRecordFilter(Weight.TypeId));
            HealthRecordItemCollection items = null;

            ICollection<HealthRecordItemCollection> allItems = search.GetMatchingItems();
            String ccdData = search.GetTransformedItems("toccd");
            Debug.WriteLine(ccdData);

            
            Personal personalInfo;
            Basic basicInfo;
            int age = 0;
            String genderStr = "";
            //String allergies = "";
            //double height = 0;
            //double weight = 0;
            //double bloodGlucose = 0;
            //double bloodPressure = 0;
            //string condition;
            //string procedure;
            //string medications;

            //Basic info = (Basic)items[0];
            items = search.GetMatchingItems()[0];
            //Debug.WriteLine(search.GetMatchingItemsRaw());
            if (items.Count > 0)
            {
                basicInfo = (Basic)items[0];
                if (basicInfo != null)
                {
                    switch (basicInfo.Gender)
                    {
                        case Gender.Male:
                            genderStr = "M";
                            break;
                        case Gender.Female:
                            genderStr = "F";
                            break;
                    }
                }
            }

            items = search.GetMatchingItems()[1];
            if (items.Count > 0)
            {
                personalInfo = (Personal)items[0];
                HealthServiceDateTime dob = personalInfo.BirthDate;
                if (dob != null)
                    age = GetAge(dob.ToDateTime());
            }

            lblPatientBasicInfo.Text = "";
            if (age > 0)
                lblPatientBasicInfo.Text = "Age " + age;
            if (!String.IsNullOrEmpty(genderStr))
                lblPatientBasicInfo.Text += " (" + genderStr + ")";

            DisplayItem(search, 2, lblPatientAllergy);
            DisplayItem(search, 3, lblPatientHeight);
            DisplayItem(search, 4, lblPatientGlucose);
            DisplayItem(search, 5, lblPatientPressure);
            DisplayItem(search, 6, lblPatientCondition);
            DisplayItem(search, 7, lblPatientProcedure);
            //DisplayItem(search, 8, lblPatientMedication);
            DisplayItem(search, 9, lblPatientWeight);

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

    private void DisplayItem(HealthRecordSearcher searcher,
        int filterNumber, System.Web.UI.WebControls.Label lbl)
    {
        HealthRecordItemCollection items = searcher.GetMatchingItems()[filterNumber];
        if (items.Count > 0)
            lbl.Text = items[0].ToString();
        else
            lbl.Text = "No Information Available";
    }

    private int GetAge(DateTime birthDate)
    {
        // definitely not accurate...
        return (int)DateTime.Now.Subtract(birthDate).TotalDays / 365;
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

        List<HealthRecordItem> ccdItems = GetValues<HealthRecordItem>(selectedParticipant, CCD_THING_GUID);

        int ccrItemCount = 0;
        string ccrFileName = "";
        foreach (HealthRecordItem ccd in ccdItems)
        {
            // ccd xml data is in ccd.TypeSpecificData
            Debug.WriteLine(ccd.TypeSpecificData.ToString());
            ccrItemCount = ccrItemCount + 1;
            ccrFileName = "CCR_" + ccrItemCount.ToString() + ".xml";
            XPathDocument document = (XPathDocument)ccd.TypeSpecificData;
            XPathNavigator documentNav = document.CreateNavigator();
            XmlTextWriter writer = new XmlTextWriter(Server.MapPath(ccrFileName), System.Text.Encoding.UTF8);
            writer.WriteStartDocument();
            writer.WriteNode(documentNav, true);
            writer.WriteEndDocument();
            writer.Close();  

        }

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
