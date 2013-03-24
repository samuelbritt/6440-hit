using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;

/// <summary>
/// Encapsulates the database itself. Wil give the connection string and 
/// will proved a new database connection.
/// </summary>
public class Database
{

    public static String ConnectionString
    {
        get
        {
            return ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }
    }

    public Database()
    {
    }

    public static SqlConnection NewConnection()
    {
        return new SqlConnection(ConnectionString);
    }
}