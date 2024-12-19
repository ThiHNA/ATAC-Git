﻿using OpenQA.Selenium;

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

        // Method interact
        public void EnterUsernamePassword(string username, string password)
        {
            fieldUsername.SendKeys(username);
            fieldPassword.SendKeys(password);
        }

        public void ClickButtonLogin() 
        {
            buttonLogin.Click();
        }

        public void Login_Successful()
        {
            string username = "Admin";
            string password = "admin123";

            EnterUsernamePassword(username, password);
            ClickButtonLogin();
        }

        public bool IsMessErrorDisplayed()
        {
            return messError.Displayed;
        }
    }
}