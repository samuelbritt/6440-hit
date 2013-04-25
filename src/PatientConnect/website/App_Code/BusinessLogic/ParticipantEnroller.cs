using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using HealthVaultHelper;
using Microsoft.Health.PatientConnect;
using System.Collections.Generic;
using Microsoft.Health;
using Microsoft.Health.ItemTypes;
using System.Web.Configuration;
using Microsoft.Health.Web;



/// <summary>
/// Summary description for ParticipantEnroller
/// </summary>
public class ParticipantEnroller
{
    private HVParticipantEnroller HvEnroller;
    private ParticipantDAO ParticipantDAO;
    private Random rand;

    public ParticipantEnroller()
    {
        HvEnroller = new HVParticipantEnroller();
        ParticipantDAO = new ParticipantDAO();
        rand = new Random();
    }

    /// <summary>
    /// Enrolls a new participant with HealthVault; that is, gets a participant code
    /// and sends the enrolment email.
    /// </summary>
    /// <param name="participant">New participant to enroll</param>
    public void EnrollNewParticipant(Participant participant)
    {
        participant.HasAuthorized = false;
        participant.IsEligible = false;
        participant.TrialGroup = "";

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
        mysendemail.HotmailEmail em = new mysendemail.HotmailEmail();

        if (String.IsNullOrEmpty(participant.Email))
            throw new Exception("Invalid email");

        string targetUrl = HvEnroller.BuildTargetEnrollmentUrl(participant);
        string msg = String.Format(
            "Dear {0} {1},\n" +
            "Welcome to the Clinical Trial!\n" +
            "Click below to authorize your Microsoft HealthVault account\n" +
            "{2}",
            participant.FirstName,
            participant.LastName,
            targetUrl);
        Debug.WriteLine(msg);
        em.Sender(msg, participant.Email);
    }

    /// <summary>
    /// Updates the database with the newly authenticated participants.
    /// </summary>
    public void UpdateAuthorizedParticipants()
    {
        // TODO: only check since the last update
        Collection<ValidatedPatientConnection> patientConnections = HvEnroller.GetAuthorizedParticipants();
        foreach (var validatedPatient in patientConnections)
        {
            Participant participant = ValidatedPatientConnectionToParticipant(validatedPatient);

            if (participant == null)
                continue;
            participant.HasAuthorized = true;
            participant.HVPersonID = validatedPatient.PersonId;
            participant.HVRecordID = validatedPatient.RecordId;
            ParticipantDAO.UpdateParticipant(participant);
        }
    }

    /// <summary>
    /// Converts a HV Validated Patient Connection to a Participant
    /// </summary>
    /// <param name="validatedPatient">The ValidatedPatientConnection from healthvault</param>
    /// <returns>The corresponding Participant from the database, or null it can't find one</returns>
    private Participant ValidatedPatientConnectionToParticipant(ValidatedPatientConnection validatedPatient)
    {
        Guid participantId = Guid.Empty;
        try
        {
            participantId = Guid.Parse(validatedPatient.ApplicationPatientId);
        }
        catch (FormatException)
        {
            // old integer-style key; ignore
            return null;
        }

        return ParticipantDAO.FindParticipantById(participantId);
    }

    public void UpdateEligbleParticipants()
    {
        ICollection<Participant> authorizedParticipants = ParticipantDAO.GetAuthorizedParticipants();
        foreach (Participant participant in authorizedParticipants)
        {
            if (participant.IsEligible)
            {
                continue;
            }
            else if (isEligible(participant))
            {
                participant.IsEligible = true;
                participant.TrialGroup = GetRandomTrialGroup();
                SendCcdToEhr(participant);
                ParticipantDAO.UpdateParticipant(participant);
            }
        }
    }


    private bool isEligible(Participant participant)
    {
        HVDataAccessor accessor = new HVDataAccessor(participant);
        accessor.AddFilter(Basic.TypeId);
        accessor.AddFilter(Condition.TypeId);
        accessor.AddFilter(Procedure.TypeId);

        Basic basicInfo = (Basic)accessor.GetItem(Basic.TypeId);
        Condition conditionInfo = (Condition)accessor.GetItem(Condition.TypeId);
        Procedure procedureInfo = (Procedure)accessor.GetItem(Procedure.TypeId);

        return (basicInfo != null &&
            conditionInfo != null &&
            procedureInfo != null &&
            basicInfo.Gender == Gender.Female); // pretty low requirements...
    }

    private string GetRandomTrialGroup()
    {
        return (rand.Next(2) == 0) ? "A" : "B";
    }

    private void SendCcdToEhr(Participant participant)
    {
        HVDataAccessor accessor = new HVDataAccessor(participant);
        accessor.AddAllFilters();
        string ccd = accessor.getCcd();
        new mysendemail.HotmailEmail().Sender(ccd, "gtvistaorganicit@gmail.com");
    }
}

