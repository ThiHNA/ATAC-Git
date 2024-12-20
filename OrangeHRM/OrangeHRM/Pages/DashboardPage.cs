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

        private IWebElement leftMenuItemLeave => driver.FindElement(By.XPath("//span[text() = 'Leave']/.."));

        // Method Interact
        public bool IsAttendanceChartDisplayed()
        {
            return attendanceChart.Displayed;
        }

        public void Goto_LeavePage()
        {
            leftMenuItemLeave.Click();
        }
    }
}
