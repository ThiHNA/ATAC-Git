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

        // Method Interact
        // Check Attendance Chart is displayed
        public bool IsAttendanceChartDisplayed()
        {
            return attendanceChart.Displayed;
        }
    }
}
