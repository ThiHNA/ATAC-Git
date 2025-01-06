using Automation.Core.Helpers;
using OpenQA.Selenium;

namespace OrangeHRM.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver _driver) : base(_driver)
        {
        }

        // Web Element
        private IWebElement fieldUsername => driver.FindElement(By.XPath("//input[@name = 'username']"));

        private IWebElement fieldPassword => driver.FindElement(By.XPath("//input[@name = 'password']"));

        private IWebElement buttonLogin => driver.FindElement(By.XPath("//button[@type = 'submit']"));

        private IWebElement messError => driver.FindElement(By.XPath("//div[@role = 'alert']"));

        private IWebElement textUserNameRequired => driver.FindElement(By.XPath("//div[@class='oxd-form-row'][1]//span"));

        private IWebElement textPasswordRequired => driver.FindElement(By.XPath("//div[@class='oxd-form-row'][2]//span"));

        // Method interact
        // Navigate to url page
        public void Goto_LoginPage()
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(ConfigurationHelper.GetValue<string>("url"));
        }

        // Input value into Username and Password
        public void EnterUsernamePassword(string username, string password)
        {
            fieldUsername.SendKeys(username);
            fieldPassword.SendKeys(password);
        }

        // Click on Login button
        public void ClickButtonLogin() 
        {
            buttonLogin.Click();
        }

        // Login to Orange page
        public void Login_Successful()
        {
            // Read configuration to get username and password
            string username = ConfigurationHelper.GetValue<string>("username");
            string password = ConfigurationHelper.GetValue<string>("password");

            // Type username and password, then click Login
            EnterUsernamePassword(username, password);
            ClickButtonLogin();
        }

        // Check error message is displayed
        public bool IsMessErrorDisplayed()
        {
            return messError.Displayed;
        }

        // Check message Required of Username is displayed
        public bool IstextUserNameRequiredDisplayed()
        {
            return textUserNameRequired.Displayed;
        }

        // Check message Required of Password is displayed
        public bool IstextPasswordRequiredDisplayed()
        {
            return textPasswordRequired.Displayed;
        }
    }
}
