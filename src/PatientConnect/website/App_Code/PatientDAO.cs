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
/// Data Access Object---handles all connections and queries to database.
/// Methods take data transfer objects as parameters and return them as outputs.
/// </summary>
public class PatientDAO
{
    /// <summary>
    /// Constants for the table name and column names
    /// </summary>
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

        int newID = ExecuteScalar(command);
        return newID;
    }

    /// <summary>
    /// Runs the <paramref name="comand"/>'s ExecuteScalar method and returns the result.
    /// </summary>
    /// <param name="command">Command to execute. Should result in a SELECT.</param>
    /// <returns>Scalar (first column) result of <paramref name="command"/></returns>
    private static int ExecuteScalar(SqlCommand command)
    {
        int result = 0;
        using (SqlConnection conn = Database.NewConnection())
        {
            command.Connection = conn;
            try
            {
                conn.Open();
                result = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        return result;
    }

    /// <summary>
    /// Gets all patients that have authorized the application.
    /// </summary>
    /// <returns>List of authorized Patient objects</returns>
    public ICollection<Patient> getAuthorizedPatients()
    {
        string query = String.Format("SELECT * FROM {0} WHERE {1}=1 ORDER BY {2}",
                                     Table.TABLE_NAME, Table.HAS_AUTHORIZED, Table.LAST_NAME);
        SqlCommand command = new SqlCommand(query);
        DataTable dataTable = new DataTable();
        ExecuteFillDataTable(command, dataTable);
        ICollection<Patient> patientList = PatientCollectionFromDataTable(dataTable);
        dataTable.Dispose();
        return patientList;
    }

    /// <summary>
    /// Fills the DataTable with the result of the command
    /// </summary>
    /// <param name="command">Command to run. Should resutl in a SELECT.</param>
    /// <param name="dataTable">DataTable to fill</param>
    private static void ExecuteFillDataTable(SqlCommand command, DataTable dataTable)
    {
        using (SqlConnection conn = Database.NewConnection())
        {
            command.Connection = conn;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            try
            {
                conn.Open();
                adapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

    /// <summary>
    /// Returns a list of Patient objects, one for each row in the DataTable
    /// </summary>
    /// <param name="dataTable">Table where each row represnts a Patient.</param>
    /// <returns>List of Patient objects, one for each row in the DataTable</returns>
    private static ICollection<Patient> PatientCollectionFromDataTable(DataTable dataTable)
    {
        List<Patient> patientList = new List<Patient>();
        foreach (DataRow row in dataTable.Rows)
        {
            Patient patient = PatientFromRow(row);
            patientList.Add(patient);
        }
        return patientList;
    }

    /// <summary>
    /// Given a DataRow representing a patient, returns a corresponding Patient DTO
    /// </summary>
    /// <param name="row">Row representing a patient</param>
    /// <returns>a corresponding Patient DTO</returns>
    private static Patient PatientFromRow(DataRow row)
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
        return patient;
    }
}