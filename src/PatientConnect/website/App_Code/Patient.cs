using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Patient
/// </summary>
public class Patient
{
    public int ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Guid HVPatientID { get; set; }
    public Guid HVRecordID { get; set; }
    public Guid HVParticipantCode { get; set; }
    public string SecurityQuestion { get; set; }
    public string SecurityAnswer { get; set; }
    public bool HasAuthorized { get; set; }

    public string FullName
    {
        get
        {
            return LastName + ", " + FirstName;
        }
    }

    public Patient()
    {
    }
}