using OpenQA.Selenium;

namespace OrangeHRM.Pages
{
    public class LeaveListPage : BasePage
    {
        public LeaveListPage(IWebDriver _driver) : base(_driver)
        {
        }

        // Web Element
        // Tab Assign Leave on top menu
        private IWebElement topMenuLeaveList => driver.FindElement(By.XPath("//a[text() = 'Leave List']"));

        // Method Interact
        // Navigate to Leave List
        public void Goto_LeaveList()
        {
            topMenuLeaveList.Click();
        }
    }
}
