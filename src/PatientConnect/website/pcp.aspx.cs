using System;

public partial class pcp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string email = txtUsername.Text;
        string password = txtPassword.Text;
        if (PcpLogin.LoginIsValid(email, password))
        {
            lblMessage.Text = "Valid Login";
            Response.Redirect("pcp-dashboard.aspx");
        }
        else
        {
            lblMessage.Text = "Invalid Login";
        }
    }
}