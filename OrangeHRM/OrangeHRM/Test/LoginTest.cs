using FluentAssert;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OrangeHRM.Pages;

namespace OrangeHRM.Test
{
    [TestClass]
    public class LoginTest : BaseTest
    {
        private LoginPage loginPage;
        private DashboardPage dashboardPage;

        [TestInitialize]
        public void SetupLogin()
        {
            loginPage = new LoginPage(driver);
            dashboardPage = new DashboardPage(driver);

            // Go to OrangeHRM Page
            loginPage.Goto_LoginPage();
        }

        [TestMethod("TC01: Verify succesful login with valid username and password")]
        public void Verify_Positive_LoginTest()
        {
            loginPage.Login_Successful();

            // Verify go to Dashboard Page
            driver.Url.ShouldContain("/dashboard/index");

            // Set explicit wait
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));
            wait.Until(d => dashboardPage.IsAttendanceChartDisplayed());
        }

        [TestMethod("TC02: Verify unsuccesful login with invalid username and valid password")]
        public void Verify_Negative_UsernameTest()
        {
            // Type incorrect username and correct password -> click Login
            loginPage.EnterUsernamePassword("IncorrectUser", "admin123");
            loginPage.ClickButtonLogin();

            // Verify message can't login is displayed
            loginPage.IsMessErrorDisplayed();
        }

        [TestMethod("TC02: Verify unsuccesful login with valid username and invalid password")]
        public void Verif_Negative_PasswordTest()
        {
            // Type incorrect username and correct password -> click Login
            loginPage.EnterUsernamePassword("Admin", "incorrectPass");
            loginPage.ClickButtonLogin();

            // Verify message can't login is displayed
            loginPage.IsMessErrorDisplayed();
        }

        [TestMethod("TC03: Verify username and password is required")]
        public void Verify_UsernamePassword_Required()
        {
            // Click Login
            loginPage.ClickButtonLogin();

            // Verify warning username and password is required
            loginPage.IstextUserNameRequiredDisplayed();
            loginPage.IstextPasswordRequiredDisplayed();
        }
    }
}
