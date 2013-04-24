using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Web.Security;

public partial class Default : System.Web.UI.Page
{
    String selectedUsername;
    MembershipUser selectedUser;
    ProfileCommon selectedProfile;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindLstUsers();
        }
        selectedUsername = lstUsers.SelectedValue;
        selectedUser = Membership.GetUser(selectedUsername);
        selectedProfile = (ProfileCommon)ProfileCommon.Create(selectedUsername, true);
    }

    /// <summary>
    /// clear all textboxes
    /// </summary>
    private void ClearTextBoxes()
    {
        foreach (Control ctrl in Page.Controls)
        {
            ClearChildControl(ctrl);
        }
    }

    /// <summary>
    /// Recursively clear textbox controls
    /// </summary>
    /// <param name="control"></param>
    private void ClearChildControl(Control control)
    {
        foreach (Control ctrl in control.Controls)
        {
            if (ctrl is TextBox)
            {
                ((TextBox)ctrl).Text = String.Empty;
            }
            ClearChildControl(ctrl);
        }
    }

    private void bindLstUsers()
    {
        MembershipUserCollection Users = Membership.GetAllUsers();
        lstUsers.DataSource = Users;
        lstUsers.DataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        selectedProfile.FirstName = txtFirstName.Text;
        selectedProfile.LastName = txtLastName.Text;
        selectedProfile.Save();
        selectedUser.Email = txtEmail.Text;
        Membership.UpdateUser(selectedUser);
    }

    private string GetErrorMessage(MembershipCreateStatus status)
    {
        switch (status)
        {
            case MembershipCreateStatus.DuplicateUserName:
                return "Username already exists. Please enter a different user name.";

            case MembershipCreateStatus.DuplicateEmail:
                return "A username for that e-mail address already exists. Please enter a different e-mail address.";

            case MembershipCreateStatus.InvalidPassword:
                return "The password provided is invalid. Please enter a valid password value.";

            case MembershipCreateStatus.InvalidEmail:
                return "The e-mail address provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.InvalidAnswer:
                return "The password retrieval answer provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.InvalidQuestion:
                return "The password retrieval question provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.InvalidUserName:
                return "The user name provided is invalid. Please check the value and try again.";

            case MembershipCreateStatus.ProviderError:
                return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

            case MembershipCreateStatus.UserRejected:
                return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

            default:
                return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
        }
    }
    protected void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
    {


        txtFirstName.Text = selectedProfile.FirstName;
        txtLastName.Text = selectedProfile.LastName;
        txtEmail.Text = selectedUser.Email;
    }
}