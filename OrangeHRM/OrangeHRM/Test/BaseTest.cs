﻿using System.Configuration;
using Automation.Core.Helpers;
using Automation.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace OrangeHRM.Test
{
    [TestClass]
    public class BaseTest : IDisposable
    {
        protected IWebDriver driver;
        public BaseTest()
        {
            // Read configuration and init browser
            string browserType = ConfigurationHelper.GetValue<string>("browser");
            int timeout = ConfigurationHelper.GetValue<int>("timeout");
            driver = DriverFactory.InitBrowser(browserType, timeout);


        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
