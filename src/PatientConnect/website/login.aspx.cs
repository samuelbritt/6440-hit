using System;
using System.Web.Security;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string email = txtUsername.Text;
        string password = txtPassword.Text;
        bool persistLogin = false;
        if ((new PcpLogin()).LoginIsValid(email, password))
        {
            lblMessage.Text = "Valid Login";
            //Response.Redirect("Dashboard.aspx");
            FormsAuthentication.RedirectFromLoginPage(email, persistLogin);
        }
        else
        {
            lblMessage.Text = "Invalid Login";
        }
    }
}