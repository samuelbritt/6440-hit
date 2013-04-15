using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Pcp
/// </summary>
public class Pcp
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Institution { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public string FullName
    {
        get
        {
            return LastName + ", " + FirstName;
        }
    }

    public String Display
    {
        get
        {
            return String.Format("{0} ({1})", FullName, Institution);
        }
    }

    public Pcp()
    {
        UserId = Guid.Empty;
    }
}