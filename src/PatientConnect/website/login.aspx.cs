using System;
using System.Web.Security;
using System.Web.UI;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Control loginStatus = Master.FindControl("LoginStatus");
        loginStatus.Visible = false;
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string username = txtUsername.Text;
        string password = txtPassword.Text;
        bool persistLogin = false;

        if (Membership.ValidateUser(username, password))
        {
            FormsAuthentication.RedirectFromLoginPage(username, persistLogin);
        }
        else
        {
            lblMessage.Text = "Invalid Login";
        }
    }
}