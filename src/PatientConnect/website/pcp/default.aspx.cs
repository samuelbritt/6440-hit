using System;
using System.Collections.Generic;
using Microsoft.Health;
<<<<<<< HEAD
using Microsoft.Health.ItemTypes;
using System.Diagnostics;
using System.Web.Configuration;
using Microsoft.Health.Web;

=======

using Microsoft.Health.ItemTypes;
using System.Diagnostics;
using System.Web.Configuration;
using Microsoft.Health.Web;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using HealthVaultHelper;

>>>>>>> Pull Patient Info

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
<<<<<<< HEAD

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

=======
                OfflineWebApplicationConnection conn = HVConnectionManager.CreateConnection(participant.HVPersonID);
                HealthRecordAccessor accessor = new HealthRecordAccessor(conn, participant.HVRecordID);

                /*
                 OfflineWebApplicationConnection offlineConn =
                     new OfflineWebApplicationConnection(participant.HVPersonID);
                 //Debug.WriteLine("Authentication");
                 //offlineConn.Authenticate();
                 //Debug.WriteLine(offlineConn.GetAuthorizedPeople());
                 HealthRecordAccessor accessor =
                     new HealthRecordAccessor(offlineConn, participant.HVRecordID);
                 */
                //Debug.WriteLine("Accessor");
                HealthRecordSearcher search = accessor.CreateSearcher();

                search.Filters.Add(new HealthRecordFilter(Basic.TypeId));
                search.Filters.Add(new HealthRecordFilter(Allergy.TypeId));
                search.Filters.Add(new HealthRecordFilter(Height.TypeId));
                search.Filters.Add(new HealthRecordFilter(BloodPressure.TypeId));
                search.Filters.Add(new HealthRecordFilter(Weight.TypeId));
                 HealthRecordItemCollection items = null;



                    //Debug.WriteLine(items[0].ToString());


                
                /*
                        List<Basic> typedList = new List<Basic>();

                        foreach (HealthRecordItem item in items)
                        {
                            typedList.Add((Basic)item);

                        }

                        foreach (Basic item in typedList)
                        {

                            Debug.WriteLine("VALUE"+item+" "+typedList.Count);
                        }

                */
                        
                       // if (items.Count > 0)
                     //   {

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
                        
                    //    }
                   //     else
                   //     {
                            Debug.WriteLine("No basic demographic information is available.");
                  //          lblPatientInfo.Text = "No basic demographic information is available.";
                            //BloodPressure bp = (BloodPressure)searchResultsGroup[0];
                    //    }
>>>>>>> Pull Patient Info
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
