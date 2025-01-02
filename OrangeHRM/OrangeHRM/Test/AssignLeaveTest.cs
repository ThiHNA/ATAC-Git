using FluentAssert;
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
        private BasePage basePage;

        [TestInitialize]
        public void SetupAssignLeave()
        {
            loginPage = new LoginPage(driver);
            dashboardPage = new DashboardPage(driver);
            assignLeavePage = new AssignLeavePage(driver);
            basePage = new BasePage(driver);

            // Navigate to Login Page
            loginPage.Goto_LoginPage();
            // Login into Page
            loginPage.Login_Successful();

            // Navigate to Leave from Left Menu
            dashboardPage.Goto_LeavePage();
            // Navigate to Assign Leave Page from Top Menu
            assignLeavePage.Goto_AssignLeavePage();

            //Verify this is AssignLeave Page
            assignLeavePage.IsAssignLeaveTitleDisplayed();
        }

        [TestMethod("TC01: Verify default value")]
        public void Verify_DefaultValue_AssignLeave()
        {
            Dictionary<string, string> defaultValueDict = assignLeavePage.GetDefaultValue();
            // Verify default value of Employee Name
            defaultValueDict["def_PlaceholderValue"].ShouldBeEqualTo("Type for hints...");

            // Verify default value of Leave Type
            defaultValueDict["def_DropdownValue"].ShouldBeEqualTo("-- Select --");

            // Verify default value of Leave Balance
            defaultValueDict["def_TextValue"].ShouldBeEqualTo("0.00 Day(s)");

            // Verify default value of From Date
            defaultValueDict["def_FromDateValue"].ShouldBeEqualTo("yyyy-dd-mm");

            // Verify default value of To Date
            defaultValueDict["def_ToDateValue"].ShouldBeEqualTo("yyyy-dd-mm");

            // Verify default value of Comment
            defaultValueDict["def_CommentValue"].ShouldBeEqualTo("");
        }

        [TestMethod("TC02: Verify assign 1 day leave for employee success")]
        public void Verify_Positive_AssignOneDayLeaveTest()
        {
            // Input value into Employee Name
            assignLeavePage.EnterEmployeeName("Ranga");

            // Chose value option of Leave Type
            assignLeavePage.ChooseDropDownLeaveType("CAN - Personal");

            // Input value into From Date and To Date
            assignLeavePage.EnterFromDate_ToDate("2024-30-12", "");

            // Input value into Comment
            assignLeavePage.EnterComment("Leave to takecare child");

            // Click button Assign
            assignLeavePage.ClickButtonAssign();

            // Click button Ok on alert Confirm
            assignLeavePage.AcceptAssignLeave();

            // Verify message success is display
            basePage.WaitUntil(driver => assignLeavePage.isMessageSuccessDisplay(), 100);
        }

        [TestMethod("TC03: Verify assign period leave for employee success")]
        public void Verify_Positive_AssignPeriodLeaveTest()
        {
            // Input value into Employee Name
            assignLeavePage.EnterEmployeeName("Ranga");

            // Chose value option of Leave Type
            assignLeavePage.ChooseDropDownLeaveType("CAN - Personal");

            // Input value into From Date and To Date
            assignLeavePage.EnterFromDate_ToDate("2025-06-01", "2025-10-01");
        }
    }
}
