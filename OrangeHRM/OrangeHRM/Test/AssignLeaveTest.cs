using OpenQA.Selenium.Support.UI;
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

            //Verify this is AssignLeave Page
            assignLeavePage.IsAssignLeaveTitleDisplayed();
        }

        [TestMethod("TC01: Verify default value")]
        public void Verify_DefaultValue_AssignLeave()
        {
            assignLeavePage.GetDefaultValue("Type for hints...", "-- Select --", "0.00 Day(s)", "yyyy-dd-mm", "");
        }

        [TestMethod("TC02: Verify assign leave for emp success")]
        public void Verify_Positive_AssignLeaveTest()
        {
            assignLeavePage.EnterEmployeeName("Ranga");
            assignLeavePage.ChooseDropDownLeaveType("CAN - FMLA");
            assignLeavePage.EnterFromDate_ToDate("2024-30-12", "");
            //assignLeavePage.ChooseDuration("Full Day");
            assignLeavePage.EnterComment("Leave to takecare child");
            assignLeavePage.ClickButtonAssign();
            assignLeavePage.AcceptAssignLeave();

            // Verify message success is display
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));
            wait.Until(driver => assignLeavePage.isMessageSuccessDisplay());
        }
    }
}
