using System;

/// <summary>
/// Data Transfer Object for the Participant table. A plain old data object useful
/// for logic layers.
/// </summary>
public class Participant
{
    public Guid ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Guid HVPersonID { get; set; }
    public Guid HVRecordID { get; set; }
    public string HVParticipantCode { get; set; }
    public string SecurityQuestion { get; set; }
    public string SecurityAnswer { get; set; }
    public bool HasAuthorized { get; set; }
    public string PcpUsername { get; set; }
    public bool IsEligible { get; set; }

    public string FullName
    {
        get
        {
            return LastName + ", " + FirstName;
        }
    }

    public Participant()
    {
        ID = Guid.Empty;
        HVPersonID = Guid.Empty;
        HVRecordID = Guid.Empty;
        HVParticipantCode = String.Empty;
    }
}