using System;
using System.Web.Security;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LoginStatus.LogoutPageUrl = "~/default.aspx";
        LoginStatus.LogoutAction = LogoutAction.RedirectToLoginPage;
        string FullName = String.Join(" ", new String[] {Profile.FirstName, Profile.LastName});
        LoginName.FormatString = FullName + " ({0})";
    }
}
