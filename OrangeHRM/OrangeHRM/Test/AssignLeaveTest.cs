using Automation.Core.Helpers;
using FluentAssert;
using OrangeHRM.Model;
using OrangeHRM.Pages;

namespace OrangeHRM.Test
{
    [TestClass]
    public class AssignLeaveTest : BaseTest
    {
        private LoginPage loginPage;
        private DashboardPage dashboardPage;
        private AssignLeavePage assignLeavePage;
        private LeaveListPage leaveListPage;

        [TestInitialize]
        public void SetupAssignLeave()
        {
            loginPage = new LoginPage(driver);
            dashboardPage = new DashboardPage(driver);
            assignLeavePage = new AssignLeavePage(driver);
            leaveListPage = new LeaveListPage(driver);

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
            AssignLeaveModel defaultValueData = JsonHelper.ReadJsonFile<AssignLeaveModel>("Data/AssignLeave_TC01.json");

            // Step 1: Verify default value of Employee Name
            defaultValueDict["def_PlaceholderValue"].ShouldBeEqualTo(defaultValueData.EmployeeName);

            // Step 2: Verify default value of Leave Type
            defaultValueDict["def_DropdownValue"].ShouldBeEqualTo(defaultValueData.LeaveType);

            // Step 3: Verify default value of Leave Balance
            defaultValueDict["def_TextValue"].ShouldBeEqualTo(defaultValueData.LeaveBalance);

            // Step 4: Verify default value of From Date
            defaultValueDict["def_FromDateValue"].ShouldBeEqualTo(defaultValueData.FromDate);

            // Step 5: Verify default value of To Date
            defaultValueDict["def_ToDateValue"].ShouldBeEqualTo(defaultValueData.ToDate);

            // Step 6: Verify default value of Comment
            defaultValueDict["def_CommentValue"].ShouldBeEqualTo(defaultValueData.Comment);
        }

        [TestMethod("TC02: Verify Employee Name, Leave Type, From Date, To Date is required")]
        public void Verify_Required_Fields_Validation()
        {
            // Step 1: Not input any fields
            // Step 2: Click button Assign
            assignLeavePage.ClickButtonAssign();

            // Step 3: Verify message required is display
            assignLeavePage.isRequiredMessageDisplayed_EmpName().ShouldBeTrue();
            assignLeavePage.isRequiredMessageDisplayed_LeaveType().ShouldBeTrue();
            assignLeavePage.isRequiredMessageDisplayed_FromDate().ShouldBeTrue();
            assignLeavePage.isRequiredMessageDisplayed_ToDate().ShouldBeTrue();
        }

        [TestMethod("TC03: Verify ToDate must be greater than FromDate")]
        public void Verify_ToDateGreaterThanFromDate()
        {
            // Pre-condition
            AssignLeaveModel dataTest = JsonHelper.ReadJsonFile<AssignLeaveModel>("Data/AssignLeave_TC03.json");

            // Step 1 & Step 2: Input From Date greater than To Date
            assignLeavePage.EnterFromDate_ToDate(dataTest.FromDate, dataTest.ToDate);

            // Step 3: Verify message ToDate must be greater than FromDate is displayed
            assignLeavePage.isCheckDateMessageDisplayed().ShouldBeTrue();
        }

        [TestMethod("TC04: Verify assign 1 day leave for employee success")]
        public void Verify_Positive_AssignLeave_OneDayTest()
        {
            // Step 1 -> Step 6: Input Leave Infomation, submit and verify assign leave successful
            ExcuteAssignLeave_Success("Data/AssignLeave_TC04.json");
        }

        [TestMethod("TC05: Verify assign period leave for employee success")]
        public void Verify_Positive_AssignLeave_PeriodTest()
        {
            // Step 1 -> Step 8: Input Leave Infomation, submit and verify assign leave successful
            ExcuteAssignLeave_Success("Data/AssignLeave_TC05.json");
        }

        [TestMethod("TC06: Verify failed to assign existing leave date")]
        public void Verify_Negative_AssignLeave_ExistedTest()
        {
            AssignLeaveModel dataTest = JsonHelper.ReadJsonFile<AssignLeaveModel>("Data/AssignLeave_TC06.json");

            // Pre-condition
            leaveListPage.Goto_LeaveList();
            if (leaveListPage.CheckIfLeaveAssigned(dataTest) is false)
            {
                assignLeavePage.Goto_AssignLeavePage();
                assignLeavePage.ExcuteAssignLeave(dataTest);
            }
            assignLeavePage.Goto_AssignLeavePage();

            // Step 1: Input Employee Name
            assignLeavePage.EnterEmployeeName(dataTest.EmployeeName);

            // Step 2: Input Leave Type
            assignLeavePage.ChooseDropDownLeaveType(dataTest.LeaveType);

            // Step 3: Input From Date and To Date existing of employee
            assignLeavePage.EnterFromDate_ToDate(dataTest.FromDate, dataTest.ToDate);

            // Step 4: Input Leave Reason
            assignLeavePage.EnterComment(dataTest.Comment);

            // Step 5: Click button Assign to submit
            assignLeavePage.ClickButtonAssign();

            // Step 6: Click button OK on alert to confirm
            assignLeavePage.AcceptAssignLeave();

            // Step 7: Verify that warning message is displayed
            assignLeavePage.isMessageWarnDisplay().ShouldBeTrue();
        }

        [TestMethod("TC07: Verify failed to assign a leave date on a non-working day")]
        public void Verify_Negative_AssignLeave_NonworkingTest()
        {
            // Pre-condition
            AssignLeaveModel dataTest = JsonHelper.ReadJsonFile<AssignLeaveModel>("Data/AssignLeave_TC07.json");

            // Input Employee Name
            assignLeavePage.EnterEmployeeName(dataTest.EmployeeName);

            // Step 2: Input Leave Type
            assignLeavePage.ChooseDropDownLeaveType(dataTest.LeaveType);

            // Step 3: Input From Date and To Date is weekend day
            assignLeavePage.EnterFromDate_ToDate(dataTest.FromDate, dataTest.ToDate);

            // Step 4: Input Leave Reason
            assignLeavePage.EnterComment(dataTest.Comment);

            // Step 5: Click button Assign to submit
            assignLeavePage.ClickButtonAssign();

            // Step 6: Click button OK on alert to confirm
            assignLeavePage.AcceptAssignLeave();

            // Step 7: Verify that error message is displayed
            assignLeavePage.isMessageErrorDisplay().ShouldBeTrue();
        }

        public void ExcuteAssignLeave_Success(string dataTestFilePath)
        {
            AssignLeaveModel dataTest = JsonHelper.ReadJsonFile<AssignLeaveModel>(dataTestFilePath);

            // Verify if leave is not assigned to employee in Leave List.
            leaveListPage.Goto_LeaveList();
            if (leaveListPage.CheckIfLeaveAssigned(dataTest) is true)
            {
                throw new Exception("Leave has already been assigned.");
            }
            assignLeavePage.Goto_AssignLeavePage();

            // Input Leave Infomation and submit leave
            assignLeavePage.ExcuteAssignLeave(dataTest);

            // Verify message Success is display
            assignLeavePage.isMessageSuccDisplay().ShouldBeTrue();

            // Verify if leave is assigned for employee in Leave List.
            leaveListPage.Goto_LeaveList();
            leaveListPage.CheckIfLeaveAssigned(dataTest).ShouldBeTrue();
        }
    }
}
