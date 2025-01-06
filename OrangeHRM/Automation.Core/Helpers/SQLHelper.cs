using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Automation.Core.Helpers
{
    public class SQLHelper
    {
        public static class SqlHelper
        {
            public static List<T> ExecuteQuery<T>(string connectionStr, string query, int timeout = 60) where T : new()
            {
                using (IDbConnection connection = new SqlConnection(connectionStr))
                {
                    return connection.Query<T>(query, commandTimeout: timeout).ToList();
                }
            }
            public static void ExecuteMultipleQueries(string connectionStr, params string[] queries)
            {
                using (IDbConnection connection = new SqlConnection(connectionStr))
                {
                    foreach (var query in queries)
                    {
                        connection.Query(query).ToList();
                    }
                }
            }
        }
    }
}
