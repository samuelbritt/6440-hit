using System;
using System.Collections.Generic;
using Microsoft.Health;
using Microsoft.Health.ItemTypes;
using System.Diagnostics;
using System.Web.Configuration;
using Microsoft.Health.Web;
using HealthVaultHelper;


public partial class statistician_default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblWtGroupA.Text = getGroupAverage("A", "Weight");
            lblAgeGroupA.Text = getGroupAverage("A", "Age");
            lblWtGroupB.Text = getGroupAverage("B", "Weight");
            lblAgeGroupB.Text = getGroupAverage("B", "Age");
        }
    }

    private string getGroupAverage(string tGroup, string thing)
    {
        int sumWt = 0;
        int sumAge = 0;
        ParticipantDAO dao = new ParticipantDAO();
        ICollection<Participant> participantList = dao.GetTrialGroup(tGroup);

        foreach (var participant in participantList)
        {
            try
            {
                OfflineWebApplicationConnection conn = HVConnectionManager.CreateConnection(participant.HVPersonID);
                HealthRecordAccessor accessor = new HealthRecordAccessor(conn, participant.HVRecordID);
                HealthRecordSearcher search = accessor.CreateSearcher();

                search.Filters.Add(new HealthRecordFilter(Weight.TypeId));
                search.Filters.Add(new HealthRecordFilter(Basic.TypeId));
                HealthRecordItemCollection items = null;

                items = search.GetMatchingItems()[0];
                var firstStr = items[0].ToString();
                var secStr = firstStr.Substring(0, firstStr.IndexOf(" "));
                int intVal = Convert.ToInt32(secStr);
                sumWt = sumWt + intVal;

                items = search.GetMatchingItems()[1];
                var age = items[0].ToString();
                age = age.Substring(age.Length-4, 4);
                intVal = Convert.ToInt32(age);
                sumAge = sumAge + intVal;
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
        if (participantList.Count > 0)
        {
            if (thing == "Weight") return (sumWt / participantList.Count).ToString();
            else if (thing == "Age") return (2013 - (sumAge / participantList.Count)).ToString();
        }
        return "No participants in Group " + tGroup;
    }
}
