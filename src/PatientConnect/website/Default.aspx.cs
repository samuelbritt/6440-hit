using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

public partial class Default : System.Web.UI.Page
{
    [Serializable]
    public class Patient
    {
        public string PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LastUpdateLabel.Text = getLastAuthUpdate();

        //ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"];
        ////String connectionString = "Data Source=.\SQLEXPRESS;"
        ////+ "AttachDbFilename=|DataDirectory|\Database.mdf;"
        ////+ "Integrated Security=True;"
        ////+ "User Instance=True";
        //using (SqlConnection connection = new SqlConnection(settings.ConnectionString))
        //{
        //    connection.Open();
        //}
    }

    protected void btnEnroll_Click(object sender, EventArgs e)
    {
        // TODO: validate input
        String firstName = PatientFirstNameTextBox.Text;
        String lastName = PatientLastNameTextBox.Text;
        String email = PatientEmailTextBox.Text;
        String securityQuestion = SecurityQuestionTextBox.Text;
        String securityAnswer = SecurityAnswerTextBox.Text;
    }

    protected void btnCheckAuth_Click(object sender, EventArgs e)
    {
        setLastAuthUpdate(DateTime.Now);
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
}