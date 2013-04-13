using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

/// <summary>
/// Summary description for PcpDAO
/// </summary>
public class PcpDAO
{
    private static class Table
    {
        public static string TABLE_NAME = "PCP";
    }

	public PcpDAO()
	{
	}

    public bool isValidPcp(string email, string password)
    {
        SqlConnection conn = Database.NewConnection();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "[dbo].[PcpValidLogin]";
        cmd.Parameters.Add("@Email", SqlDbType.VarChar, 256).Value = email;
        cmd.Parameters.Add("@Password", SqlDbType.VarChar, 256).Value = password;
        cmd.Parameters.Add("@isValid", SqlDbType.Int, 4);
        cmd.Parameters["@isValid"].Direction = ParameterDirection.Output;
        cmd.Connection = conn;
        int results = 0;
        try
        {
            conn.Open();
            cmd.ExecuteNonQuery();
            results = (int)cmd.Parameters["@isValid"].Value;
        }
        catch (SqlException ex)
        {
            Debug.WriteLine(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            if (conn != null)
            {
                conn.Close();
            }
        }
        return results == 1;
    }
}