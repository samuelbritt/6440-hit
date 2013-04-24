using System;
using Logic;

public partial class redirect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string redirect = "~/login.aspx";
        if (Context.User.IsInRole(Roles.ONCOLOGIST))
        {
            redirect = "~/oncologist";
        }
        else if (Context.User.IsInRole(Roles.PCP))
        {
            redirect = "~/pcp";
        }
        else if (Context.User.IsInRole(Roles.NURSE))
        {
            redirect = "~/nurse";
        }
        else if (Context.User.IsInRole(Roles.STATISTICIAN))
        {
            redirect = "~/statistician";
        }
        else if (Context.User.IsInRole(Roles.IT_OFFICER))
        {
            redirect = "~/it_officer";
        }
        else if (Context.User.IsInRole(Roles.ADMIN))
        {
            redirect = "~/admin";
        }

        Response.Redirect(redirect);
    }
}