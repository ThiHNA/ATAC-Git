using FluentAssert;
using OpenQA.Selenium.Support.UI;
using OrangeHRM.Pages;

namespace OrangeHRM.Test
{
    [TestClass]
    public class LoginTest : BaseTest
    {
        private LoginPage loginPage;

        [TestInitialize]
        public void SetupLogin()
        {
            loginPage = new LoginPage(driver);

            // Go to Url
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/auth/login");
        }

        [TestMethod("TC01: Verify succesful login with valid username and password")]
        public void Verify_Positive_LoginTest()
        {
            loginPage.Login_Successful();

            // Verify go to Dashboard Page
            driver.Url.ShouldContain("/dashboard/index");
        }

        [TestMethod("TC02: Verify unsuccesful login with invalid username and valid password")]
        public void Verif_Negative_UsernameTest()
        {
            // Type incorrect username and correct password -> click Login
            loginPage.EnterUsernamePassword("IncorrectUser", "admin123");
            loginPage.ClickButtonLogin();

            // Verify message can't login is displayed
            loginPage.IsMessErrorDisplayed();
        }
    }
}
