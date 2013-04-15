using System;
using System.Web.Security;

public partial class nurse_AddPcp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnGenPassword_Click(object sender, EventArgs e)
    {
        txtPassword.Text = Membership.GeneratePassword(12, 3);
    }

    protected void btnNewPcp_Click(object sender, EventArgs e)
    {
        if (!IsValid)
            return;

        Pcp pcp = new Pcp();
        pcp.FirstName = txtFirstName.Text;
        pcp.LastName = txtLastName.Text;
        pcp.Institution = txtInstitution.Text;
        pcp.Email = txtEmail.Text;
        pcp.Phone = txtPhone.Text;
        pcp.Username = txtEmail.Text;
        pcp.Password = txtPassword.Text;

        PcpDAO dao = new PcpDAO();
        try
        {
            dao.InsertNewPcp(pcp);
        }
        catch (MembershipCreateUserException)
        {
            lblUserExists.Visible = true;
            return;
        }

        Session["lastPcpCreatedUsername"] = pcp.Username;
        Session["lastPcpCreatedPassword"] = pcp.Password;
        Response.Redirect("~/nurse/AddPcpSuccess.aspx");
    }

}