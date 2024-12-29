using Automation.Core.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OrangeHRM.Pages;

namespace OrangeHRM.Test
{
    [TestClass]
    public class LoginTest : BaseTest
    {
        private LoginPage loginPage;
        private DashboardPage dashboardPage;
        private BasePage basePage;

        [TestInitialize]
        public void SetupLogin()
        {
            loginPage = new LoginPage(driver);
            dashboardPage = new DashboardPage(driver);
            basePage = new BasePage(driver);

            // Go to OrangeHRM Page
            loginPage.Goto_LoginPage();
        }

        [TestMethod("TC01: Verify succesful login with valid username and password")]
        public void Verify_Positive_LoginTest()
        {
            // Login with correct username and password
            loginPage.Login_Successful();

            // Log info
            reportHelper.LogMessage("Info", "Login with username: " + ConfigurationHelper.GetValue<string>("username"));
            reportHelper.LogMessage("Info", "Login with password: " + ConfigurationHelper.GetValue<string>("password"));

            // Set explicit wait and verify Dashboard Page is loaded
            basePage.WaitUntil(driver => dashboardPage.IsAttendanceChartDisplayed(), 15);
        }

        [TestMethod("TC02: Verify unsuccesful login with invalid username and valid password")]
        public void Verify_Negative_UsernameTest()
        {
            // Generate a random invalid username
            string invalidUsername = "User" + new Random().Next(1000, 9999);

            // Read configuration to get valid password
            string password = ConfigurationHelper.GetValue<string>("password");

            // Log info
            reportHelper.LogMessage("Info", "Login with username: " + invalidUsername);
            reportHelper.LogMessage("Info", "Login with password: " + password);

            // Type incorrect username and correct password -> click Login
            loginPage.EnterUsernamePassword(invalidUsername, password);
            loginPage.ClickButtonLogin();

            // Verify message can't login is displayed
            loginPage.IsMessErrorDisplayed();
        }

        [TestMethod("TC02: Verify unsuccesful login with valid username and invalid password")]
        public void Verif_Negative_PasswordTest()
        {
            // Read configuration to get valid username
            string username = ConfigurationHelper.GetValue<string>("username");

            // Generate a random invalid password
            string invalidPassword = "admin" + new Random().Next(1000, 9999);

            // Log info
            reportHelper.LogMessage("Info", "Login with username: " + username);
            reportHelper.LogMessage("Info", "Login with password: " + invalidPassword);

            // Type incorrect username and correct password -> click Login
            loginPage.EnterUsernamePassword(username, invalidPassword);
            loginPage.ClickButtonLogin();

            // Verify message can't login is displayed
            loginPage.IsMessErrorDisplayed();
        }

        [TestMethod("TC03: Verify username and password is required")]
        public void Verify_UsernamePassword_Required()
        {
            // Do not input username and password, then click Login
            loginPage.ClickButtonLogin();

            // Verify text "Required" of username and password is displayed
            loginPage.IstextUserNameRequiredDisplayed();
            loginPage.IstextPasswordRequiredDisplayed();
        }
    }
}
