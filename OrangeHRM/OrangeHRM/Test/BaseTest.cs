using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace OrangeHRM.Test
{
    [TestClass]
    public class BaseTest
    {
        protected static IWebDriver driver;

        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void SetupBrowser(TestContext testContext)
        {
            // Open Chrome driver
            driver = new ChromeDriver();

            // Set implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void CleanupBrowser()
        {
            driver.Quit();
        }
    }
}
