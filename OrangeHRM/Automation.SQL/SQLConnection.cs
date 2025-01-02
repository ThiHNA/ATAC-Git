using Microsoft.Data.SqlClient;

namespace Automation.SQL
{
    public class SQLConnection
    {
        public SQLConnection()
        {
            string connectionString = @"Server=DESKTOP-OL3USMA\SQLEXPRESS01;Database=Classroom;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
            }
        }
    }
}
