using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Web.Security;

public partial class Default : System.Web.UI.Page
{
    private static string CREATE_NEW_PCP = "Create New";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblLastUpdateDate.Text = getLastAuthUpdate();
            bindLstAuthorizedParticipants();
            bindDrpPcps();
            ResetPcpDropdown();
        }
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

    private void bindLstAuthorizedParticipants()
    {
        ParticipantDAO dao = new ParticipantDAO();
        ICollection<Participant> participantList = dao.GetAuthorizedParticipants();

        lstAuthParticipants.DataSource = participantList;
        lstAuthParticipants.DataTextField = "FullName";
        lstAuthParticipants.DataValueField = "ID";
        lstAuthParticipants.DataBind();
    }

    private void bindDrpPcps()
    {
        ICollection<Pcp> pcpList = (new PcpDAO()).GetAllPcps();
        drpPcpList.DataSource = pcpList;
        drpPcpList.DataTextField = "Display";
        drpPcpList.DataValueField = "Username";
        drpPcpList.DataBind();
        drpPcpList.Items.Insert(0, new ListItem("", ""));
        drpPcpList.Items.Insert(1, new ListItem("Add New...", CREATE_NEW_PCP));
    }

    private void ResetPcpDropdown()
    {
        drpPcpList.SelectedIndex = 0;
        liAddPcp.Visible = false;
    }

    private bool willCreatePCP()
    {
        return drpPcpList.SelectedValue.Equals(CREATE_NEW_PCP);
    }

    protected void btnEnroll_Click(object sender, EventArgs e)
    {
        if (willCreatePCP())
            Page.Validate("newPcp");

        if (!IsValid)
        {
            newPcpValidationSummary.Visible = true;
            return;
        }


        String firstName = txtParticipantFirstName.Text;
        String lastName = txtParticipantLastName.Text;
        String email = txtParticipantEmail.Text;
        String securityQuestion = txtSecurityQuestion.Text;
        String securityAnswer = txtSecurityAnswer.Text;
        String pcpUsername;
        try
        {
            pcpUsername = getOrCreatePcp();
        }
        catch (MembershipCreateUserException ex)
        {
            lblUserExists.Text = GetErrorMessage(ex.StatusCode);
            lblUserExists.Visible = true;
            return;
        }

        enrollNewParticipant(firstName, lastName, email, securityQuestion, securityAnswer, pcpUsername);
        ClearTextBoxes();
        ResetPcpDropdown();
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

    private string getOrCreatePcp()
    {
        string pcpUsername;
        if (willCreatePCP())
            pcpUsername = (createNewPcp()).Username;
        else
            pcpUsername = drpPcpList.SelectedValue;
        return pcpUsername;
    }

    private Pcp createNewPcp()
    {
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
            throw;
        }

        return pcp;
    }

    private void enrollNewParticipant(string firstName, string lastName, string email,
                                      string securityQuestion, string securityAnswer, string pcpUsername)
    {
        Participant participant = new Participant();
        participant.FirstName = firstName;
        participant.LastName = lastName;
        participant.Email = email;
        participant.SecurityQuestion = securityQuestion;
        participant.SecurityAnswer = securityAnswer;
        participant.PcpUsername = pcpUsername;

        ParticipantEnroller enroller = new ParticipantEnroller();
        enroller.EnrollNewParticipant(participant);
    }

    protected void btnCheckAuth_Click(object sender, EventArgs e)
    {
        ParticipantEnroller enroller = new ParticipantEnroller();
        enroller.UpdateAuthorizedParticipants();
        setLastAuthUpdate(DateTime.Now);
        bindLstAuthorizedParticipants();
        lblLastUpdateDate.Text = getLastAuthUpdate();
    }

    private String getLastAuthUpdate()
    {
        Application.Lock();
        String lastUpdate = Application["lastAuthUpdate"].ToString();
        Application.UnLock();
        return lastUpdate;
    }

    private void setLastAuthUpdate(DateTime dateTime)
    {
        Application.Lock();
        Application["lastAuthUpdate"] = dateTime;
        Application.UnLock();
    }

    protected void AuthParticipantsListBox_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void drpPcpList_SelectedIndexChange(object sender, EventArgs e)
    {
        liAddPcp.Visible = willCreatePCP();
    }

    protected void btnGenPassword_Click(object sender, EventArgs e)
    {
        int totalLength = 12;
        int specialChars = 3;
        txtPassword.Text = Membership.GeneratePassword(totalLength, specialChars);
    }

}