using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

/// <summary>
/// Data Access Object---handles all connections and queries to database.
/// Methods take data transfer objects as parameters and return them as outputs.
/// </summary>
public class ParticipantDAO
{
    /// <summary>
    /// Constants for the table name and column names
    /// </summary>
    private static class Table
    {
        public static string TABLE_NAME = "Participant";
        // Table column names
        public static string PARTICIPANT_ID = "ParticipantID";
        public static string FIRST_NAME = "FirstName";
        public static string LAST_NAME = "LastName";
        public static string EMAIL = "Email";
        public static string HV_PERSON_ID = "HVPersonID";
        public static string HV_RECORD_ID = "HVRecordID";
        public static string HV_PARTICIPANT_CODE = "HVParticipantCode";
        public static string SECURITY_QUESTION = "SecurityQuestion";
        public static string SECURITY_ANSWER = "SecurityAnswer";
        public static string HAS_AUTHORIZED = "HasAuthorized";
        public static string ALL_COLUMNS_EXCEPT_ID = String.Format(
            "{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}",
            FIRST_NAME,
            LAST_NAME,
            EMAIL,
            HV_PERSON_ID,
            HV_RECORD_ID,
            HV_PARTICIPANT_CODE,
            SECURITY_QUESTION,
            SECURITY_ANSWER,
            HAS_AUTHORIZED);
    }

    /// <summary>
    /// Use for parameters in Command objects
    /// </summary>
    private static class Params
    {
        public static string PARTICIPANT_ID = "@ParticipantID";
        public static string FIRST_NAME = "@FirstName";
        public static string LAST_NAME = "@LastName";
        public static string EMAIL = "@Email";
        public static string HV_PERSON_ID = "@HVPersonID";
        public static string HV_RECORD_ID = "@HVRecordID";
        public static string HV_PARTICIPANT_CODE = "@HVParticipantCode";
        public static string SECURITY_QUESTION = "@SecurityQuestion";
        public static string SECURITY_ANSWER = "@SecurityAnswer";
        public static string HAS_AUTHORIZED = "@HasAuthorized";
        public static string ALL_COLUMNS_EXCEPT_ID = String.Format(
            "{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}",
            FIRST_NAME,
            LAST_NAME,
            EMAIL,
            HV_PERSON_ID,
            HV_RECORD_ID,
            HV_PARTICIPANT_CODE,
            SECURITY_QUESTION,
            SECURITY_ANSWER,
            HAS_AUTHORIZED);
    }

    public ParticipantDAO()
    {
    }

    /// <summary>
    /// Inserts a new participant into the database.
    /// </summary>
    /// <param name="participant">A new participant, with appropriate fields filled out</param>
    /// <returns>The ID of the newly inserted participant.</returns>
    public int InsertParticipant(Participant participant)
    {
        String query = String.Format("INSERT INTO {0} ({1}) ", Table.TABLE_NAME, Table.ALL_COLUMNS_EXCEPT_ID)
            + String.Format("VALUES ({0});", Params.ALL_COLUMNS_EXCEPT_ID)
            + "SELECT CAST(scope_identity() AS int)"; // returns the last value of the autoincrement column

        SqlCommand command = new SqlCommand(query);
        AddAllParamsExceptIDToCommand(command, participant);
        int newID = ExecuteScalar(command);
        return newID;
    }

    public void UpdateParticipant(Participant participant)
    {
        String query = String.Format("Update {0} SET ", Table.TABLE_NAME)
            + String.Format("{0}={1}, ", Table.FIRST_NAME, Params.FIRST_NAME)
            + String.Format("{0}={1}, ", Table.LAST_NAME, Params.LAST_NAME)
            + String.Format("{0}={1}, ", Table.EMAIL, Params.EMAIL)
            + String.Format("{0}={1}, ", Table.HV_PERSON_ID, Params.HV_PERSON_ID)
            + String.Format("{0}={1}, ", Table.HV_RECORD_ID, Params.HV_RECORD_ID)
            + String.Format("{0}={1}, ", Table.HV_PARTICIPANT_CODE, Params.HV_PARTICIPANT_CODE)
            + String.Format("{0}={1}, ", Table.SECURITY_QUESTION, Params.SECURITY_QUESTION)
            + String.Format("{0}={1}, ", Table.SECURITY_ANSWER, Params.SECURITY_ANSWER)
            + String.Format("{0}={1} ", Table.HAS_AUTHORIZED, Params.HAS_AUTHORIZED)
            + String.Format("WHERE {0}={1}", Table.PARTICIPANT_ID, Params.PARTICIPANT_ID);

        SqlCommand command = new SqlCommand(query);
        AddAllParamsToCommand(command, participant);
        int rowsAffected = ExecuteNonQuery(command);
        if (rowsAffected < 1)
        {
            Debug.WriteLine("Error updating Participant");
        }
    }

    public Participant FindParticipantById(int id)
    {
        string query = String.Format("SELECT * FROM {0} WHERE {1}={2}",
            Table.TABLE_NAME, Table.PARTICIPANT_ID, Params.PARTICIPANT_ID);
        SqlCommand command = new SqlCommand(query);
        command.Parameters.AddWithValue(Params.PARTICIPANT_ID, id);
        DataTable dataTable = new DataTable();
        ExecuteFillDataTable(command, dataTable);
        Participant p = null;
        if (dataTable.Rows.Count > 0)
        {
            p = ParticipantFromRow(dataTable.Rows[0]);
        }
        dataTable.Dispose();
        return p;
    }

    /// <summary>
    /// Gets all participants that have authorized the application.
    /// </summary>
    /// <returns>List of authorized Participant objects</returns>
    public ICollection<Participant> getAuthorizedParticipants()
    {
        string query = String.Format("SELECT * FROM {0} WHERE {1}=1 ORDER BY {2}",
            Table.TABLE_NAME, Table.HAS_AUTHORIZED, Table.LAST_NAME);
        SqlCommand command = new SqlCommand(query);
        DataTable dataTable = new DataTable();
        ExecuteFillDataTable(command, dataTable);
        ICollection<Participant> participantList = ParticipantCollectionFromDataTable(dataTable);
        dataTable.Dispose();
        return participantList;
    }

    private void AddAllParamsExceptIDToCommand(SqlCommand command, Participant participant)
    {
        command.Parameters.AddWithValue(Params.FIRST_NAME, participant.FirstName);
        command.Parameters.AddWithValue(Params.LAST_NAME, participant.LastName);
        command.Parameters.AddWithValue(Params.EMAIL, participant.Email);
        command.Parameters.AddWithValue(Params.HV_PERSON_ID, participant.HVPersonID);
        command.Parameters.AddWithValue(Params.HV_RECORD_ID, participant.HVRecordID);
        command.Parameters.AddWithValue(Params.HV_PARTICIPANT_CODE, participant.HVParticipantCode);
        command.Parameters.AddWithValue(Params.SECURITY_QUESTION, participant.SecurityQuestion);
        command.Parameters.AddWithValue(Params.SECURITY_ANSWER, participant.SecurityAnswer);
        command.Parameters.AddWithValue(Params.HAS_AUTHORIZED, participant.HasAuthorized);
    }

    private void AddAllParamsToCommand(SqlCommand command, Participant participant)
    {
        command.Parameters.AddWithValue(Params.PARTICIPANT_ID, participant.ID);
        AddAllParamsExceptIDToCommand(command, participant);
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
    /// Runs the <paramref name="comand"/>'s ExecuteNonQuery method and 
    /// returns the result (number of rows affected).
    /// </summary>
    /// <param name="command">Command to execute. Should result in a SELECT.</param>
    /// <returns>Number of rows affected by <paramref name="command"/></returns>
    private static int ExecuteNonQuery(SqlCommand command)
    {
        int result = 0;
        using (SqlConnection conn = Database.NewConnection())
        {
            command.Connection = conn;
            try
            {
                conn.Open();
                result = (int)command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        return result;
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
    /// Returns a list of Participant objects, one for each row in the DataTable
    /// </summary>
    /// <param name="dataTable">Table where each row represnts a Participant.</param>
    /// <returns>List of Participant objects, one for each row in the DataTable</returns>
    private static ICollection<Participant> ParticipantCollectionFromDataTable(DataTable dataTable)
    {
        List<Participant> participantList = new List<Participant>();
        foreach (DataRow row in dataTable.Rows)
        {
            Participant participant = ParticipantFromRow(row);
            participantList.Add(participant);
        }
        return participantList;
    }

    /// <summary>
    /// Given a DataRow representing a participant, returns a corresponding Participant DTO
    /// </summary>
    /// <param name="row">Row representing a participant</param>
    /// <returns>a corresponding Participant DTO</returns>
    private static Participant ParticipantFromRow(DataRow row)
    {
        Participant participant = new Participant();
        participant.ID = (int)row[Table.PARTICIPANT_ID];
        participant.FirstName = row[Table.FIRST_NAME].ToString();
        participant.LastName = row[Table.LAST_NAME].ToString();
        participant.Email = row[Table.EMAIL].ToString();
        participant.HasAuthorized = (bool)row[Table.HAS_AUTHORIZED];
        participant.HVPersonID = Guid.Parse(row[Table.HV_PERSON_ID].ToString());
        participant.HVRecordID = Guid.Parse(row[Table.HV_RECORD_ID].ToString());
        participant.HVParticipantCode = row[Table.HV_PARTICIPANT_CODE].ToString();
        participant.SecurityQuestion = row[Table.SECURITY_QUESTION].ToString();
        participant.SecurityAnswer = row[Table.SECURITY_ANSWER].ToString();
        participant.HasAuthorized = (bool)row[Table.HAS_AUTHORIZED];
        return participant;
    }
}