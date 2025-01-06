using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace OrangeHRM.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        public BasePage(IWebDriver _driver) 
        { 
            driver = _driver;
        }

        // Web Element
        private IWebElement leftMenuItemLeave => driver.FindElement(By.XPath("//span[text() = 'Leave']/.."));


        // Explicit wait common
        public void WaitUntil(Func<IWebDriver, bool> condition, int timeInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeInSeconds));
            wait.Until(condition);
        }
        public void Goto_LeavePage()
        {
            leftMenuItemLeave.Click();
        }
    }
}
