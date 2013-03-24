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
        String query =
            String.Format("INSERT INTO {0} ({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}) ",
                          Table.TABLE_NAME,
                          Table.FIRST_NAME,
                          Table.LAST_NAME,
                          Table.EMAIL,
                          Table.HV_PERSON_ID,
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
                            "@HVPersonID",
                            "@HVRecordID",
                            "@HVParticipantCode",
                            "@securityQuestion",
                            "@securityAnswer",
                            "@hasAuthorized"
                            )
              + "SELECT CAST(scope_identity() AS int)"; // returns the last value of the autoincrement column

        SqlCommand command = new SqlCommand(query);
        command.Parameters.AddWithValue("@firstName", participant.FirstName);
        command.Parameters.AddWithValue("@lastName", participant.LastName);
        command.Parameters.AddWithValue("@email", participant.Email);
        command.Parameters.AddWithValue("@HVPersonID", participant.HVPersonID);
        command.Parameters.AddWithValue("@HVRecordID", participant.HVRecordID);
        command.Parameters.AddWithValue("@HVParticipantCode", participant.HVParticipantCode);
        command.Parameters.AddWithValue("@securityQuestion", participant.SecurityQuestion);
        command.Parameters.AddWithValue("@securityAnswer", participant.SecurityAnswer);
        command.Parameters.AddWithValue("@hasAuthorized", participant.HasAuthorized);

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
        participant.HVParticipantCode = Guid.Parse(row[Table.HV_PARTICIPANT_CODE].ToString());
        return participant;
    }
}