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
        LastUpdateLabel.Text = getLastAuthUpdate();
        bindAuthorizedParticipantsListBox();
    }

    private void bindAuthorizedParticipantsListBox()
    {
        PatientDAO dao = new PatientDAO();
        ICollection<Patient> patientList = dao.getAuthorizedPatients();

        AuthParticipantsListBox.DataSource = patientList;
        AuthParticipantsListBox.DataTextField = "FullName";
        AuthParticipantsListBox.DataValueField = "ID";
        AuthParticipantsListBox.DataBind();
    }

    protected void btnEnroll_Click(object sender, EventArgs e)
    {
        // TODO: validate input
        String firstName = PatientFirstNameTextBox.Text;
        String lastName = PatientLastNameTextBox.Text;
        String email = PatientEmailTextBox.Text;
        String securityQuestion = SecurityQuestionTextBox.Text;
        String securityAnswer = SecurityAnswerTextBox.Text;
        enrollNewParticipant(firstName, lastName, email, securityQuestion, securityAnswer);
    }

    private void enrollNewParticipant(string firstName, string lastName, string email,
                                      string securityQuestion, string securityAnswer)
    {
        Patient patient = new Patient();
        patient.FirstName = firstName;
        patient.LastName = lastName;
        patient.Email = email;
        patient.SecurityQuestion = securityQuestion;
        patient.SecurityAnswer = securityAnswer;
        patient.HasAuthorized = true;

        PatientDAO dao = new PatientDAO();
        patient.ID = dao.InsertPatient(patient);
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