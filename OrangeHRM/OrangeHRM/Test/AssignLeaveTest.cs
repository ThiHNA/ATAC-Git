using OrangeHRM.Pages;

namespace OrangeHRM.Test
{
    [TestClass]
    public class AssignLeaveTest : BaseTest
    {
        private LoginPage loginPage;
        private DashboardPage dashboardPage;
        private AssignLeavePage assignLeavePage;

        [TestInitialize]
        public void SetupAssignLeave()
        {
            loginPage = new LoginPage(driver);
            dashboardPage = new DashboardPage(driver);
            assignLeavePage = new AssignLeavePage(driver);

            // Login
            loginPage.Goto_LoginPage();
            loginPage.Login_Successful();

            // Go to Assign Leave Page
            dashboardPage.Goto_LeavePage();
            assignLeavePage.Goto_AssignLeavePage();
        }

        [TestMethod]
        public void Verify_DefaultValue_AssignLeave()
        {
            //Verify this is AssignLeave Page
            assignLeavePage.IsAssignLeaveTitleDisplayed();

            assignLeavePage.GetDefaultValue("Type for hints...", "-- Select --", "0.00 Day(s)", "yyyy-dd-mm", "");

        }
    }
}
