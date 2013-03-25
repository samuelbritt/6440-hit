using Microsoft.Health.PatientConnect;
using Microsoft.Health.Web;
using System.Collections.ObjectModel;
using System;

namespace HealthVaultHelper
{
    /// <summary>
    /// Handles talking to HealthVault in matters regarding new patients.
    /// Gets participation codes, retrieves new authorized patients, etc.
    /// </summary>
    public class HVParticipantEnroller
    {
        public HVParticipantEnroller()
        {
        }

        /// <summary>
        /// Connects with HealthVault to get a unique code for the participant
        /// </summary>
        /// <param name="participant">New participant that needs a HV authentication code</param>
        /// <returns>Authentication code from HealthVault</returns>
        public string GetParticipantCode(Participant participant)
        {
            OfflineWebApplicationConnection conn = HVConnectionManager.CreateConnection();
            string participantCode = PatientConnection.Create(conn, participant.FirstName, participant.SecurityQuestion,
                participant.SecurityAnswer, null, participant.ID.ToString());
            return participantCode;
        }

        /// <summary>
        /// Returns the appropriate enrollment redirect URL for the provided participant
        /// </summary>
        /// <param name="participant">Participant containing the HvParticipantCode needed to form the URL</param>
        /// <returns>The enrollment URL to send to the participant</returns>
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

        /// <summary>
        /// Get new participants that have authenticated in the past <paramref name="pastDays"/> days
        /// </summary>
        /// <param name="pastDays"></param>
        /// <returns>A collection of <typeparamref name="ValidatedPatientConnection"/>s, which contain the PersonId, RecordId, etc.</returns>
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
