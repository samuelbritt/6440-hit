using System;
using System.Web.Security;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Context.User.Identity.IsAuthenticated)
        {
            msgLoginState.Text = "Not Logged In";
            btnLogout.Visible = false;
        }
        else
        {
            msgLoginState.Text = Context.User.Identity.Name;
            btnLogout.Visible = true;
        }
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("login.aspx");
    }
}
