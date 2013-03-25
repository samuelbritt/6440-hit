using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

using System.Data;
using System.Diagnostics;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LastUpdateLabel.Text = getLastAuthUpdate();
            bindAuthorizedParticipantsListBox();
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

    private void bindAuthorizedParticipantsListBox()
    {
        ParticipantDAO dao = new ParticipantDAO();
        ICollection<Participant> participantList = dao.getAuthorizedParticipants();

        AuthParticipantsListBox.DataSource = participantList;
        AuthParticipantsListBox.DataTextField = "FullName";
        AuthParticipantsListBox.DataValueField = "ID";
        AuthParticipantsListBox.DataBind();
    }

    protected void btnEnroll_Click(object sender, EventArgs e)
    {
        if (!IsValid)
            return;

        String firstName = ParticipantFirstNameTextBox.Text;
        String lastName = ParticipantLastNameTextBox.Text;
        String email = ParticipantEmailTextBox.Text;
        String securityQuestion = SecurityQuestionTextBox.Text;
        String securityAnswer = SecurityAnswerTextBox.Text;

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

        ParticipantEnroller.EnrollNewParticipant(participant);
    }

    protected void btnCheckAuth_Click(object sender, EventArgs e)
    {
        bindAuthorizedParticipantsListBox();
        setLastAuthUpdate(DateTime.Now);
        LastUpdateLabel.Text = getLastAuthUpdate();
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