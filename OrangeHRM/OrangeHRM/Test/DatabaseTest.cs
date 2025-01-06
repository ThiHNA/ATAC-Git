using FluentAssert;
using OrangeHRM.Services;

namespace OrangeHRM.Test
{
    [TestClass]
    public class DatabaseTest : BaseTest
    {
        DatabaseService databaseService;

        public DatabaseTest()
        {
            databaseService = new DatabaseService();
        }

        [TestMethod]
        public void TestDatabase()
        {
            var result = databaseService.GetInformation();
            result.Name.ShouldBeEqualTo("SQL Basic");
            result.IdCourse.ShouldBeEqualTo(1);
        }
    }
}
