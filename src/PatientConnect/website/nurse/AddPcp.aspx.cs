using System;
using System.Web.Security;

public partial class nurse_AddPcp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnNewPcp_Click(object sender, EventArgs e)
    {
        if (!IsValid)
            return;

        string username = txtEmail.Text;
        string firstName = txtFirstName.Text;
        string lastName = txtLastName.Text;
        string email = txtEmail.Text;
        string password = Membership.GeneratePassword(12, 3);

        if (usernameIsTaken(username) || emailIsTaken(email))
        {
            lblUserExists.Visible = true;
            return;
        }
        else
        {
            MembershipUser newPcp = Membership.CreateUser(username, password, email);
            Roles.AddUserToRole(newPcp.UserName, Logic.Roles.PCP);
        }

        Session["lastPcpCreatedUsername"] = username;
        Session["lastPcpCreatedPassword"] = password;
        Response.Redirect("~/nurse/AddPcpSuccess.aspx");
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