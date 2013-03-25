using Microsoft.Health.PatientConnect;
using Microsoft.Health.Web;
using System.Collections.ObjectModel;
using System;

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

        public string GetParticipantCode(Participant participant)
        {
            OfflineWebApplicationConnection conn = HVConnectionManager.CreateConnection();
            string participantCode = PatientConnection.Create(conn, participant.FirstName, participant.SecurityQuestion,
                participant.SecurityAnswer, null, participant.ID.ToString());
            return participantCode;
        }

        public string BuildTargetEnrollmentUrl(Participant participant)
        {
            if (String.IsNullOrEmpty(participant.HVParticipantCode))
                throw new Exception("Invalid participant to enroll: bad participant code");

            // See http://msdn.microsoft.com/en-us/library/ff803620.aspx
            return String.Format(
                "{0}/redirect.aspx?target=CONNECT&targetqs=packageid%3d{1}",
                HVConnectionManager.ShellUrl, participant.HVParticipantCode
            );
        }

        public Collection<ValidatedPatientConnection> GetAuthorizedParticipants(int pastDays)
        {
            OfflineWebApplicationConnection conn = HVConnectionManager.CreateConnection();
            if (pastDays > 0)
            {
                DateTime dtSince = DateTime.Now.AddDays(-1 * pastDays);
                return PatientConnection.GetValidatedConnections(conn, dtSince);
            }
            return PatientConnection.GetValidatedConnections(conn);
        }
    }
}
