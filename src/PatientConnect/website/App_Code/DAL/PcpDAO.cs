using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.Security;
using System.Web;
using System.Web.Profile;

/// <summary>
/// Summary description for PcpDAO
/// </summary>
public class PcpDAO
{
    public PcpDAO()
    {
    }

    public void InsertNewPcp(Pcp pcp)
    {

        if (usernameIsTaken(pcp.Username) || emailIsTaken(pcp.Email))
        {
            throw new MembershipCreateUserException("Username or Email is taken");
        }
        else
        {
            MembershipUser pcpMember = Membership.CreateUser(pcp.Username, pcp.Password, pcp.Email);
            Roles.AddUserToRole(pcpMember.UserName, Logic.Roles.PCP);
            ProfileCommon profile = (ProfileCommon)ProfileCommon.Create(pcpMember.UserName, true);
            profile.Institution = pcp.Institution;
            profile.Phone = pcp.Phone;
            profile.FirstName = pcp.FirstName;
            profile.LastName = pcp.LastName;
            profile.Save();
        }
    }

    public Pcp GetPcpByUsername(string username)
    {
        MembershipUser member = Membership.GetUser(username, false);
        if (member == null)
        {
            return null;
        }
        ProfileCommon profile = (ProfileCommon)ProfileCommon.Create(username, true);
        Pcp pcp = new Pcp();
        pcp.Username = member.UserName;
        pcp.Email = member.Email;
        pcp.FirstName = profile.FirstName;
        pcp.LastName = profile.LastName;
        pcp.Phone = profile.Phone;
        pcp.Institution = profile.Institution;

        return pcp;
    }

    private bool usernameIsTaken(string username)
    {
        return Membership.GetUser(username) != null;
    }

    private bool emailIsTaken(string email)
    {
        return Membership.GetUserNameByEmail(email) != null;
    }
}