using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class nurse_AddPcpSuccess : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string username = (string)Session["lastPcpCreatedUsername"];
        string password = (string)Session["lastPcpCreatedPassword"];

        Pcp pcp = (new PcpDAO()).GetPcpByUsername(username);
        if (pcp == null)
        {
            lblInvalidUser.Visible = true;
            return;
        }

        lblUsernameVal.Text = username;
        lblPasswordVal.Text = password;
        lblEmailVal.Text = pcp.Email;
        lblNameVal.Text = pcp.FullName;
        lblPhoneVal.Text = pcp.Phone;
        lblInstitutionVal.Text = pcp.Institution;
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        return;
    }
}