using System;
using System.Web.Security;
using System.Web.UI;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.IsAuthenticated &&
                !String.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
            {
                // We were redirected here becuase an authenticated user is 
                // trying to reach something he's not allowed to.
                Response.Redirect("~/UnauthorizedAccess.aspx");
            }
        }

        if (!Request.IsAuthenticated)
        {
            // We are anonymous. Hide option to log in, since this is the login page.
            Control loginStatus = Master.FindControl("LoginStatus");
            loginStatus.Visible = false;
        }

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