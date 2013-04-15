using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

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
        ICollection<Participant> participantList = dao.getAuthorizedParticipants();

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

        drpPcpList.Items.Insert(0, new ListItem("Create New PCP", CREATE_NEW_PCP));
        drpPcpList.SelectedIndex = 0;
    }

    protected void btnEnroll_Click(object sender, EventArgs e)
    {
        if (!IsValid)
            return;
        if (drpPcpList.SelectedValue == CREATE_NEW_PCP)
        {
            Response.Redirect("~/nurse/AddPcp.aspx");
        }

        String firstName = txtParticipantFirstName.Text;
        String lastName = txtParticipantLastName.Text;
        String email = txtParticipantEmail.Text;
        String securityQuestion = txtSecurityQuestion.Text;
        String securityAnswer = txtSecurityAnswer.Text;
        String pcpUsername = drpPcpList.SelectedValue;

        enrollNewParticipant(firstName, lastName, email, securityQuestion, securityAnswer);
        ClearTextBoxes();
    }

    private void enrollNewParticipant(string firstName, string lastName, string email,
                                      string securityQuestion, string securityAnswer)
    {
        Participant participant = new Participant();
        participant.FirstName = firstName;
        participant.LastName = lastName;
        participant.Email = email;
        participant.SecurityQuestion = securityQuestion;
        participant.SecurityAnswer = securityAnswer;

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
}