using System.Reflection;
using Automation.Core.Helpers;
using Automation.WebDriver;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OrangeHRM.Test
{
    [TestClass]
    public class BaseTest : IDisposable
    {
        protected IWebDriver driver;
        protected ReportHelper reportHelper;

        public TestContext TestContext { get; set; }

        public BaseTest()
        {
            // Read configuration and init browser
            string browserType = ConfigurationHelper.GetValue<string>("browser");
            int timeout = ConfigurationHelper.GetValue<int>("timeout");
            driver = DriverFactory.InitBrowser(browserType, timeout);

            //Init report helper
            reportHelper = new ReportHelper();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            // Get description of testcase
            var testMethod = TestContext?.TestName;
            var method = GetType().GetMethods().FirstOrDefault(m => m.GetCustomAttributes(typeof(TestMethodAttribute), false)
                            .Any() && m.Name == testMethod);

            var testMethodAttribute = method.GetCustomAttributes(typeof(TestMethodAttribute), false)
                                                .Cast<TestMethodAttribute>()
                                                .FirstOrDefault();

            string testDescription = testMethodAttribute?.DisplayName ?? method.Name;
            
            reportHelper.CreateTestCase(TestContext.TestName, testDescription);
        }

        public void Dispose()
        {
            if (TestContext.CurrentTestOutcome == UnitTestOutcome.Failed)
            {
                string imgBase = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;
                reportHelper.LogMessage("Fail", "Test case failed", imgBase);
            }
            else
            {
                reportHelper.LogMessage("Pass", "Test case passed");
            }
            // Close Browser
            driver.Quit();
            reportHelper.ExportReport();
        }
    }
}
