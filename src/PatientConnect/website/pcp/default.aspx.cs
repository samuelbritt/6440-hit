using System;
using System.Collections.Generic;
using Microsoft.Health;
using Microsoft.Health.ItemTypes;
using System.Diagnostics;
using System.Web.Configuration;
using Microsoft.Health.Web;
using HealthVaultHelper;


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
        frmSelectedPatient.Visible = true;
    }

    private void DisplaySelectedParticipant(Participant participant)
    {
        lblPatientHeader.Text = participant.FullName;

        try
        {
            OfflineWebApplicationConnection conn = HVConnectionManager.CreateConnection(participant.HVPersonID);
            HealthRecordAccessor accessor = new HealthRecordAccessor(conn, participant.HVRecordID);
            HealthRecordSearcher search = accessor.CreateSearcher();

            search.Filters.Add(new HealthRecordFilter(Basic.TypeId));
            search.Filters.Add(new HealthRecordFilter(Allergy.TypeId));
            search.Filters.Add(new HealthRecordFilter(Height.TypeId));
            search.Filters.Add(new HealthRecordFilter(BloodPressure.TypeId));
            search.Filters.Add(new HealthRecordFilter(Weight.TypeId));
            HealthRecordItemCollection items = null;

            //Basic info = (Basic)items[0];
            items = search.GetMatchingItems()[0];
            //Debug.WriteLine(search.GetMatchingItemsRaw());
            lblPatientInfo.Text = items[0].ToString();
            Debug.WriteLine(items[0].ToString());
            items = search.GetMatchingItems()[1];
            lblPatientAllergy.Text = items[0].ToString();
            Debug.WriteLine(items[0].ToString());
            items = search.GetMatchingItems()[2];
            lblPatientHeight.Text = items[0].ToString();
            Debug.WriteLine(items[0].ToString());
            //items = search.GetMatchingItems()[3];
            //lblPatientGlucose.Text = items[0].ToString();
            Debug.WriteLine(items[0].ToString());
            items = search.GetMatchingItems()[4];
            lblPatientWeight.Text = items[0].ToString();
            Debug.WriteLine(items[0].ToString());
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
}
