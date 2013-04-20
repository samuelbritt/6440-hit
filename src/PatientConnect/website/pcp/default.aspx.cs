using System;
using System.Collections.Generic;
using Microsoft.Health;
using Microsoft.Health.ItemTypes;
using System.Diagnostics;
using System.Web.Configuration;
using Microsoft.Health.Web;


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
                Guid pid = Guid.Parse(lstPatients.SelectedValue);

                OfflineWebApplicationConnection offlineConn =
                    new OfflineWebApplicationConnection(participant.HVPersonID);
                //Debug.WriteLine("Authentication");
                offlineConn.Authenticate();
 
                HealthRecordAccessor accessor =
                    new HealthRecordAccessor(offlineConn, participant.HVRecordID);
                //Debug.WriteLine("Accessor");
                HealthRecordSearcher search = accessor.CreateSearcher();
                Weight w=new Weight();
                search.Filters.Add(new HealthRecordFilter(Basic.TypeId));
                HealthRecordItemCollection searchResultsGroup = search.GetMatchingItems()[0];
                if (searchResultsGroup.Count > 0)
                {
                    Debug.WriteLine("There is STUFF");
                    // Success - write the user name and default basic demographic info 
                    //   to the console.
                    Basic info = (Basic)searchResultsGroup[0];
                    lblPatientInfo.Text = info.ToString();
                }
                else
                    //Debug.WriteLine("No basic demographic information is available.");
                    lblPatientInfo.Text = "No basic demographic information is available.";
                //BloodPressure bp = (BloodPressure)searchResultsGroup[0];

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
