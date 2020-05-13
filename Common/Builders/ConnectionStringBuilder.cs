using System;
using System.Collections.Generic;
using System.Text;
using Common.Settings;

namespace Common.Builders
{
    public static class ConnectionStringBuilder
    {

        public static string BuildSQLConnectionString(SqlSettings sqlSettings)
        {
            //return $"Server=tcp:{sqlSettings.Server},1433;Initial Catalog={sqlSettings.Catalog};Persist Security Info=False;User ID={sqlSettings.User};Password={sqlSettings.Password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return $"Server={sqlSettings.Server};Initial Catalog={sqlSettings.Catalog};Trusted_Connection=True;MultipleActiveResultSets=true";
        }

    }
}
