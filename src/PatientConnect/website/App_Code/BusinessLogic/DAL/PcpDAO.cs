using System.Collections.Generic;
using System.Web.Security;

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
            UpdatePcpProfile(pcp);
        }
    }

    public void UpdatePcp(Pcp pcp)
    {
        UpdatePcpProfile(pcp);
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

    public ICollection<Pcp> GetAllPcps()
    {
        List<Pcp> list = new List<Pcp>();
        string[] usernames = Roles.GetUsersInRole(Logic.Roles.PCP);
        foreach (var uname in usernames)
        {
            list.Add(GetPcpByUsername(uname));
        }
        return list;
    }

    private void UpdatePcpProfile(Pcp pcp)
    {
        ProfileCommon profile = (ProfileCommon)ProfileCommon.Create(pcp.Username, true);
        profile.FirstName = pcp.FirstName;
        profile.LastName = pcp.LastName;
        profile.Institution = pcp.Institution;
        profile.Phone = pcp.Phone;
        profile.Save();
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