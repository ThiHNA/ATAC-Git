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
            // Step 1: Input valid username into Username field
            // Step 2: Input valid password into Password field
            // Step 3: Click button Login
            loginPage.Login_Successful();

            // Log info
            reportHelper.LogMessage("Info", "Login with username: " + ConfigurationHelper.GetValue<string>("username"));
            reportHelper.LogMessage("Info", "Login with password: " + ConfigurationHelper.GetValue<string>("password"));

            // Step 4: Verify Dashboard Page is loaded
            basePage.WaitUntil(driver => dashboardPage.IsAttendanceChartDisplayed(), 15);
        }

        [TestMethod("TC02: Verify unsuccesful login with invalid username and valid password")]
        public void Verify_Negative_UsernameTest()
        {
            // Pre-condition
            string invalidUsername = "User" + new Random().Next(1000, 9999);
            string password = ConfigurationHelper.GetValue<string>("password");

            // Log info
            reportHelper.LogMessage("Info", "Login with username: " + invalidUsername);
            reportHelper.LogMessage("Info", "Login with password: " + password);

            // Step 1: Input invalid username into Username field
            // Step 2: Input valid password into Password field
            loginPage.EnterUsernamePassword(invalidUsername, password);

            // Step 3: Click button Login
            loginPage.ClickButtonLogin();

            // Step 4: Verify message can't login is displayed
            loginPage.IsMessErrorDisplayed();
        }

        [TestMethod("TC03: Verify unsuccesful login with valid username and invalid password")]
        public void Verif_Negative_PasswordTest()
        {
            // Pre-condition
            string username = ConfigurationHelper.GetValue<string>("username");
            string invalidPassword = "admin" + new Random().Next(1000, 9999);

            // Log info
            reportHelper.LogMessage("Info", "Login with username: " + username);
            reportHelper.LogMessage("Info", "Login with password: " + invalidPassword);

            // Step 1: Input valid username into Username field
            // Step 2: Input invalid password into Password field
            loginPage.EnterUsernamePassword(username, invalidPassword);

            // Step 3: Click button Login
            loginPage.ClickButtonLogin();

            // Step 4: Verify message can't login is displayed
            loginPage.IsMessErrorDisplayed();
        }

        [TestMethod("TC03: Verify username and password is required")]
        public void Verify_UsernamePassword_Required()
        {
            // Step 1: Not input username and password
            // Step 2: Click button Login
            loginPage.ClickButtonLogin();

            // Step 3: Verify text "Required" of username and password is displayed
            loginPage.IstextUserNameRequiredDisplayed();
            loginPage.IstextPasswordRequiredDisplayed();
        }
    }
}
