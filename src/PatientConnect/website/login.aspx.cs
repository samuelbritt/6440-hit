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

        // hide option to log in, since this is the login page.
        if (!Request.IsAuthenticated)
        {
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
            Request.QueryString["ReturnUrl"] = "~/default.aspx";
            FormsAuthentication.RedirectFromLoginPage(username, persistLogin);
        }
        else
        {
            lblMessage.Text = "Invalid Login";
        }
    }
}