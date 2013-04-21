﻿using System;
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
        participant.IsEligible = false;
        // update data store for participant
        ParticipantDAO.UpdateParticipant(participant);
    }

    /// <summary>
    /// Sends the email to the new participant with a link to authenticate the app.
    /// </summary>
    /// <param name="participant">Participant to enroll</param>
    private void SendEnrollmentEmail(Participant participant)
    {
        Debug.WriteLine("OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO "+participant.IsEligible);
        //Debug.WriteLine("Arriba");
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
    public String UpdateAuthorizedParticipants()
    {
        
        String UnauthorizedPatients = "";
        // TODO: only check since the last update
        Collection<ValidatedPatientConnection> patientConnections = HvEnroller.GetAuthorizedParticipants(0);
        foreach (var validatedPatient in patientConnections)
        {
            Participant participant = ValidatedPatientConnectionToParticipant(validatedPatient);
            if (participant != null && (participant.HasAuthorized && !participant.IsEligible)) UnauthorizedPatients += participant.FullName + " || "; 

            if (participant == null)
                continue;
            participant.HasAuthorized = true;
            participant.HVPersonID = validatedPatient.PersonId;
            participant.HVRecordID = validatedPatient.RecordId;
            OfflineWebApplicationConnection conn = HVConnectionManager.CreateConnection(participant.HVPersonID);
            HealthRecordAccessor accessor = new HealthRecordAccessor(conn, participant.HVRecordID);
            HealthRecordSearcher search = accessor.CreateSearcher();
            search.Filters.Add(new HealthRecordFilter(Basic.TypeId));
            search.Filters.Add(new HealthRecordFilter(Condition.TypeId));
            search.Filters.Add(new HealthRecordFilter(Procedure.TypeId));



            if (search.GetMatchingItems()[0].Count > 0 && search.GetMatchingItems()[1].Count > 0 && search.GetMatchingItems()[2].Count > 0)
            {
                Debug.WriteLine("ENTERED!!!");
               
                participant.IsEligible = true;
                Debug.WriteLine(participant.HasAuthorized.ToString(), " ", participant.IsEligible.ToString());
                ParticipantDAO.UpdateParticipant(participant);
                //return "AllAuthorized";
            }
            else
            {
                Debug.WriteLine("NOT ENTERED!!!");

                participant.IsEligible = false;
                ParticipantDAO.UpdateParticipant(participant);
                UnauthorizedPatients += participant.FullName + " || ";
                //return UnauthorizedPatients;
            }


            
        }

        if (UnauthorizedPatients.Length > 0) return UnauthorizedPatients;
        else return "AllAuthorized";
        
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
}

