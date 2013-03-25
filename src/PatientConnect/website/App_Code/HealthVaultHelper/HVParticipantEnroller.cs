using Microsoft.Health.PatientConnect;
using Microsoft.Health.Web;

namespace HealthVaultHelper
{
    /// <summary>
    /// Summary description for HVParticipantEnroller
    /// </summary>
    public class HVParticipantEnroller
    {
        public HVParticipantEnroller()
        {
        }

        public string EnrollNewParticipant(Participant participant)
        {
            return GetParticipantCode(participant);
        }

        private string GetParticipantCode(Participant participant)
        {
            OfflineWebApplicationConnection conn = HVConnectionManager.CreateConnection();
            string participantCode = PatientConnection.Create(conn, participant.FirstName, participant.SecurityQuestion,
                participant.SecurityAnswer, null, participant.ID.ToString());
            return participantCode;
        }
    }
}
