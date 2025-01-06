using Automation.Core.Helpers;
using OrangeHRM.Model;
using OrangeHRM.SQLQueries;
using static Automation.Core.Helpers.SQLHelper;

namespace OrangeHRM.Services
{
    public class DatabaseService
    {
        string connectionString = ConfigurationHelper.GetValue<string>("connectionDB");

        public Course GetInformation()
        {
            string queryTemp = Queries.queryTemp;
            var result = SqlHelper.ExecuteQuery<Course>(connectionString, queryTemp);
            return result.FirstOrDefault();
        }
}
    }
