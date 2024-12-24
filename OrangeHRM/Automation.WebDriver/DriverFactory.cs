using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Automation.WebDriver
{
    public class DriverFactory
    {
        public static IWebDriver InitBrowser(string browserType, int timeout)
        {
            IWebDriver driver;

            switch (browserType)
            {
                case "CHROME":
                    driver = new ChromeDriver();
                    break;
                case "FIREFOX":
                    driver = new FirefoxDriver();
                    break;
                case "EDGE":
                    driver = new EdgeDriver();
                    break;
                default:
                    throw new Exception("Can not support this driver");
            }

            // Set implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeout);

            return driver;
        }
    }
}
