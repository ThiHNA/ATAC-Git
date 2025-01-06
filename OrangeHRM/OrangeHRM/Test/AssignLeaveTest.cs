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
            
            // Step 1: Verify default value of Employee Name
            defaultValueDict["def_PlaceholderValue"].ShouldBeEqualTo("Type for hints...");

            // Step 2: Verify default value of Leave Type
            defaultValueDict["def_DropdownValue"].ShouldBeEqualTo("-- Select --");

            // Step 3: Verify default value of Leave Balance
            defaultValueDict["def_TextValue"].ShouldBeEqualTo("0.00 Day(s)");

            // Step 4: Verify default value of From Date
            defaultValueDict["def_FromDateValue"].ShouldBeEqualTo("yyyy-dd-mm");

            // Step 5: Verify default value of To Date
            defaultValueDict["def_ToDateValue"].ShouldBeEqualTo("yyyy-dd-mm");

            // Step 6: Verify default value of Comment
            defaultValueDict["def_CommentValue"].ShouldBeEqualTo("");
        }

        [TestMethod("TC02: Verify Employee Name, Leave Type, From Date, To Date is required")]
        public void Verify_Required_Fields_Validation()
        {
            // Step 1: Not input any fields
            // Step 2: Click button Assign
            assignLeavePage.ClickButtonAssign();

            // Step 3: Verify message required is display
            assignLeavePage.isRequiredMessageDisplayed_EmpName();
            assignLeavePage.isRequiredMessageDisplayed_LeaveType();
            assignLeavePage.isRequiredMessageDisplayed_FromDate();
            assignLeavePage.isRequiredMessageDisplayed_ToDate();
        }

        [TestMethod("TC03: Verify assign 1 day leave for employee success")]
        public void Verify_Positive_AssignOneDayLeaveTest()
        {
            // Step 1: Input valid username into Employee Name
            assignLeavePage.EnterEmployeeName("Ranga");

            // Step 2: Chose 1 value from Leave Type
            assignLeavePage.ChooseDropDownLeaveType("CAN - Personal");

            // Step 3: Input the same day into From Date and To Date
            assignLeavePage.EnterFromDate_ToDate("2024-30-12", "2024-30-12");

            // Step 4: Input leave reason into Comment
            assignLeavePage.EnterComment("Leave to takecare child");

            // Step 5: Click button Assign
            assignLeavePage.ClickButtonAssign();

            // Step 6: Click button Ok on alert Confirm
            assignLeavePage.AcceptAssignLeave();

            // Step 7: Verify message success is display
            basePage.WaitUntil(driver => assignLeavePage.isMessageSuccessDisplay(), 100);
        }

        [TestMethod("TC04: Verify assign period leave for employee success")]
        public void Verify_Positive_AssignPeriodLeaveTest()
        {
            // Step 1: Input valid username into Employee Name
            assignLeavePage.EnterEmployeeName("Ranga");

            // Step 2: Chose 1 value from Leave Type
            assignLeavePage.ChooseDropDownLeaveType("CAN - Personal");

            // Step 3: Input period days into From Date and To Date
            assignLeavePage.EnterFromDate_ToDate("2025-06-01", "2025-10-01");

            // Step 4: Chose All Days from Partial Days
            assignLeavePage.ChooseDropDownPartialDays("All Days");

            // Step 5: Chose Specify Time from Duration
            assignLeavePage.ChooseDropDownDuration("Specify Time");

            // Step 6: Input leave reason into Comment
            assignLeavePage.EnterComment("Take a period leave");

            // Step 7: Click button Assign
            assignLeavePage.ClickButtonAssign();

            // Step 8: Click button Ok on alert Confirm
            assignLeavePage.AcceptAssignLeave();

            // Step 9: Verify message success is display
            basePage.WaitUntil(driver => assignLeavePage.isMessageSuccessDisplay(), 100);
        }
    }
}
