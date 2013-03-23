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
    public class Patient
    {
        public string ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string securityQuestion { get; set; }
        public string securityAnswer { get; set; }
        public string fullName
        {
            get
            {
                return lastName + ", " + firstName;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LastUpdateLabel.Text = getLastAuthUpdate();
        bindAuthorizedParticipantsListBox();
    }

    private void bindAuthorizedParticipantsListBox()
    {
        String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            String query = "select * from Patient where hasAuthorized=1";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count > 0)
                {
                    AuthParticipantsListBox.DataSource = dataTable;
                    AuthParticipantsListBox.DataTextField = "FirstName";
                    AuthParticipantsListBox.DataValueField = "PatientID";
                    AuthParticipantsListBox.DataBind();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
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
        String connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            String query = "INSERT INTO Patient ("
                + "FirstName, LastName, Email, SecurityQuestion, SecurityAnswer"
                + ") VALUES ("
                + "@firstName, @lastName, @email, @securityQuestion, @securityAnswer"
                + ")";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@firstName", firstName));
            command.Parameters.Add(new SqlParameter("@lastName", lastName));
            command.Parameters.Add(new SqlParameter("@email", email));
            command.Parameters.Add(new SqlParameter("@securityQuestion", securityQuestion));
            command.Parameters.Add(new SqlParameter("@securityAnswer", securityAnswer));

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected < 1)
                {
                    throw new Exception("Could not insert new record");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
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