using OpenQA.Selenium;

namespace OrangeHRM.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        public BasePage(IWebDriver _driver) 
        { 
            driver = _driver;
        }
    }
}
