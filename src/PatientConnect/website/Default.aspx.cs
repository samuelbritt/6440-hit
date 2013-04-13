using System;

public partial class redirect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string redirect = "~/login.aspx";
        if (Context.User.IsInRole("physician"))
        {
            redirect = "~/physician";
        }
        else if (Context.User.IsInRole("pcp"))
        {
            redirect = "~/pcp";
        }
        else if (Context.User.IsInRole("nurse"))
        {
            redirect = "~/nurse";
        }
        else if (Context.User.IsInRole("statistician"))
        {
            redirect = "~/statistician";
        }
        else if (Context.User.IsInRole("it_officer"))
        {
            redirect = "~/it_officer";
        }

        Response.Redirect(redirect);
    }
}