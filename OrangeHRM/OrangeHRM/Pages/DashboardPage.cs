using OpenQA.Selenium;

namespace OrangeHRM.Pages
{
    public class DashboardPage : BasePage
    {
        public DashboardPage(IWebDriver _driver) : base(_driver)
        {
        }

        // Web Element
        private IWebElement attendanceChart => driver.FindElement(By.XPath("//div[@class = 'emp-attendance-chart']"));
        private IWebElement buttonAssignLeave => driver.FindElement(By.XPath("//button[@title = 'Assign Leave']"));
        private IWebElement buttonLeaveList => driver.FindElement(By.XPath("//button[@title = 'Leave List']"));

        // Method Interact
        // Check Attendance Chart is displayed
        public bool IsAttendanceChartDisplayed()
        {
            return attendanceChart.Displayed;
        }

        public void Goto_AssignLeave_FromDashboard()
        {
            buttonAssignLeave.Click();
        }
        public void Goto_LeaveList_FromDashboard()
        {
            buttonLeaveList.Click();
        }
    }

}
