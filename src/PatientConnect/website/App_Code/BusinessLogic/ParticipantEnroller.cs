using System.Diagnostics;
using HealthVaultHelper;
using System;
using Microsoft.Health.PatientConnect;
using System.Collections.ObjectModel;




/// <summary>
/// Summary description for ParticipantEnroller
/// </summary>
public class ParticipantEnroller
{
    private HVParticipantEnroller HvEnroller;
    private ParticipantDAO ParticipantDAO;

    public ParticipantEnroller()
    {
        HvEnroller = new HVParticipantEnroller();
        ParticipantDAO = new ParticipantDAO();
    }

    /// <summary>
    /// Enrolls a new participant with HealthVault; that is, gets a participant code
    /// and sends the enrolment email.
    /// </summary>
    /// <param name="participant">New participant to enroll</param>
    public void EnrollNewParticipant(Participant participant)
    {
        participant.HasAuthorized = false;

        // get app-specific participant ID from database
        participant.ID = ParticipantDAO.InsertParticipant(participant);

        // enroll with healthvault
        participant.HVParticipantCode = HvEnroller.GetParticipantCode(participant);
        SendEnrollmentEmail(participant);

        // update data store for participant
        ParticipantDAO.UpdateParticipant(participant);
    }

    /// <summary>
    /// Sends the email to the new participant with a link to authenticate the app.
    /// </summary>
    /// <param name="participant">Participant to enroll</param>
    private void SendEnrollmentEmail(Participant participant)
    {
        Debug.WriteLine("Arriba");
        // TODO: actually send an email
        mysendemail.HotmailEmail em = new mysendemail.HotmailEmail();
        


        if (String.IsNullOrEmpty(participant.Email))
            throw new Exception("Invalid email");

        string targetUrl = HvEnroller.BuildTargetEnrollmentUrl(participant);
        string msg = String.Format("Send email to {0} with link to {1}", participant.Email, targetUrl);
        em.Sender(msg);
        Debug.WriteLine(msg);
    }

    /// <summary>
    /// Updates the database with the newly authenticated participants.
    /// </summary>
    public void UpdateAuthorizedParticipants()
    {
        // TODO: only check since the last update
        Collection<ValidatedPatientConnection> patientConnections = HvEnroller.GetAuthorizedParticipants(0);
        foreach (var validatedPatient in patientConnections)
        {
            int participantId = int.Parse(validatedPatient.ApplicationPatientId);
            Participant participant = ParticipantDAO.FindParticipantById(participantId);
            if (participant == null || participant.HasAuthorized)
                continue;

            participant.HVPersonID = validatedPatient.PersonId;
            participant.HVRecordID = validatedPatient.RecordId;
            participant.HasAuthorized = true;
            ParticipantDAO.UpdateParticipant(participant);
        }
    }
}

