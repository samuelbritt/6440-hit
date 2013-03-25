using System;
using System.Web.Configuration;

using Microsoft.Health.Web;

namespace HealthVaultHelper
{
    /// <summary>
    /// Summary description for HVConnectionManager
    /// </summary>
    public class HVConnectionManager
    {
        public static Guid ApplicationID
        {
            get
            {
                return Guid.Parse(WebConfigurationManager.AppSettings["ApplicationID"].ToString());
            }
        }

        public static string ShellUrl
        {
            get
            {
                return WebConfigurationManager.AppSettings["ShellUrl"].ToString();
            }
        }

        public HVConnectionManager()
        {
        }

        /// <summary>
        /// Create Offline Web Application Connection
        /// </summary>
        /// <returns>reference to OfflineWebApplicationConnection </returns>
        public static OfflineWebApplicationConnection CreateConnection()
        {
            return CreateConnection(Guid.Empty);
        }

        /// <summary>
        /// Creates Offline Web Application Connection
        /// </summary>
        /// <param name="personId">Person Id who granted permission to perform operation</param>
        /// <returns>Returns authentication connection</returns>
        public static OfflineWebApplicationConnection CreateConnection(Guid personId)
        {
            OfflineWebApplicationConnection offlineConn = new OfflineWebApplicationConnection(ApplicationID,
                WebApplicationConfiguration.HealthServiceUrl, personId);
            offlineConn.Authenticate();
            return offlineConn;
        }
    }
}