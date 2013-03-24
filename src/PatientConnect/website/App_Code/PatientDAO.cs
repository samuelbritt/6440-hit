using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections;
using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for PatientDAO
/// </summary>
public class PatientDAO
{
    private static class Table
    {
        public static string TABLE_NAME = "Patient";
        // Table column names
        public static string PATIENT_ID = "PatientID";
        public static string FIRST_NAME = "FirstName";
        public static string LAST_NAME = "LastName";
        public static string EMAIL = "Email";
        public static string HV_PATIENT_ID = "HVPatientID";
        public static string HV_RECORD_ID = "HVRecordID";
        public static string HV_PARTICIPANT_CODE = "HVParticipantCode";
        public static string SECURITY_QUESTION = "SecurityQuestion";
        public static string SECURITY_ANSWER = "SecurityAnswer";
        public static string HAS_AUTHORIZED = "HasAuthorized";
    }

    public PatientDAO()
    {
    }

    /// <summary>
    /// Inserts a new patient into the database.
    /// </summary>
    /// <param name="patient">A new patient, with appropriate fields filled out</param>
    /// <returns>The PatientID of the newly inserted patient.</returns>
    public int InsertPatient(Patient patient)
    {
        String query =
            String.Format("INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}) ",
                          Table.TABLE_NAME,
                          Table.FIRST_NAME,
                          Table.LAST_NAME,
                          Table.EMAIL,
                          Table.HV_PATIENT_ID,
                          Table.HV_RECORD_ID,
                          Table.HV_PARTICIPANT_CODE,
                          Table.SECURITY_QUESTION,
                          Table.SECURITY_ANSWER,
                          Table.HAS_AUTHORIZED
                          )
            + String.Format("VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8});",
                            "@firstName",
                            "@lastName",
                            "@email",
                            "@HVPatientID",
                            "@HVRecordID",
                            "@HVParticipantCode",
                            "@securityQuestion",
                            "@securityAnswer",
                            "@hasAuthorized"
                            )
              + "SELECT CAST(scope_identity() AS int)"; // returns the last value of the autoincrement column

        SqlCommand command = new SqlCommand(query);
        command.Parameters.AddWithValue("@firstName", patient.FirstName);
        command.Parameters.AddWithValue("@lastName", patient.LastName);
        command.Parameters.AddWithValue("@email", patient.Email);
        command.Parameters.AddWithValue("@HVPatientID", patient.HVPatientID);
        command.Parameters.AddWithValue("@HVRecordID", patient.HVRecordID);
        command.Parameters.AddWithValue("@HVParticipantCode", patient.HVParticipantCode);
        command.Parameters.AddWithValue("@securityQuestion", patient.SecurityQuestion);
        command.Parameters.AddWithValue("@securityAnswer", patient.SecurityAnswer);
        command.Parameters.AddWithValue("@hasAuthorized", patient.HasAuthorized);

        int newID = 0;
        using (SqlConnection conn = Database.NewConnection())
        {
            command.Connection = conn;
            try
            {
                conn.Open();
                newID = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        return newID;
    }

    /// <summary>
    /// Gets all patients that have authorized the application.
    /// </summary>
    /// <returns>List of authorized Patient objects</returns>
    public ICollection<Patient> getAuthorizedPatients()
    {
        List<Patient> patientList = new List<Patient>();
        string query = String.Format("SELECT * FROM {0} WHERE {1}=1 ORDER BY {2}",
                                     Table.TABLE_NAME, Table.HAS_AUTHORIZED, Table.LAST_NAME);
        SqlCommand command = new SqlCommand(query);

        using (SqlConnection conn = Database.NewConnection())
        {
            command.Connection = conn;
            try
            {
                conn.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    Patient patient = new Patient();
                    patient.ID = (int)row[Table.PATIENT_ID];
                    patient.FirstName = row[Table.FIRST_NAME].ToString();
                    patient.LastName = row[Table.LAST_NAME].ToString();
                    patient.Email = row[Table.EMAIL].ToString();
                    patient.HasAuthorized = (bool)row[Table.HAS_AUTHORIZED];
                    patient.HVPatientID = Guid.Parse(row[Table.HV_PATIENT_ID].ToString());
                    patient.HVRecordID = Guid.Parse(row[Table.HV_RECORD_ID].ToString());
                    patient.HVParticipantCode = Guid.Parse(row[Table.HV_PARTICIPANT_CODE].ToString());

                    patientList.Add(patient);
                }
                dataTable.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        return patientList;
    }
}