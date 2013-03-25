using System.Diagnostics;
using HealthVaultHelper;
using System;

/// <summary>
/// Summary description for ParticipantEnroller
/// </summary>
public class ParticipantEnroller
{
    public ParticipantEnroller()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void EnrollNewParticipant(Participant participant)
    {
        participant.HasAuthorized = false;

        // get app-specific participant ID from database
        ParticipantDAO dao = new ParticipantDAO();
        participant.ID = dao.InsertParticipant(participant);

        // enroll with healthvault
        HVParticipantEnroller enroller = new HVParticipantEnroller();
        participant.HVParticipantCode = enroller.EnrollNewParticipant(participant);
        SendEnrollmentEmail(participant);

        // update data store for participant
        dao.UpdateParticipant(participant);
    }

    /// <summary>
    /// Sends the email to the new participant with a link to authenticate the app.
    /// </summary>
    /// <param name="participant">Participant to enroll</param>
    private static void SendEnrollmentEmail(Participant participant)
    {
        // TODO: actually send an email
        if (String.IsNullOrEmpty(participant.HVParticipantCode) ||
            String.IsNullOrEmpty(participant.Email))
        {
            throw new Exception("Invalid participant to enroll");
        }

        string targetUrl = BuildTargetUrl(participant);
        string msg = String.Format("Send email to {0} with link to {1}", participant.Email, targetUrl);
        Debug.WriteLine(msg);
    }

    private static string BuildTargetUrl(Participant participant)
    {
        // See http://msdn.microsoft.com/en-us/library/ff803620.aspx
        return String.Format(
            "{0}/redirect.aspx?target=CONNECT&targetqs=packageid%3d{1}",
            HVConnectionManager.ShellUrl, participant.HVParticipantCode
        );
    }
}