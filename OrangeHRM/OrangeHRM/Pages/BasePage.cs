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

        public void WaitUntil(string xpath, int time)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));
            wait.Until(d => driver.FindElement(By.XPath(xpath)));
        }

    }
}
