using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace OrangeHRM.Test
{
    [TestClass]
    public class BaseTest : IDisposable
    {
        protected IWebDriver driver;

        //[ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        //public static void SetupBrowser(TestContext testContext)
        public BaseTest()
        {
            // Open Chrome driver
            driver = new ChromeDriver();

            // Set implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(15);
        }

        //[ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass)]
        //public static void CleanupBrowser()
        //{
        //    // Close browser
        //    driver.Quit();
        //}

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
