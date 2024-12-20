﻿using FluentAssert;
using OpenQA.Selenium;

namespace OrangeHRM.Pages
{
    public class AssignLeavePage : BasePage
    {
        public AssignLeavePage(IWebDriver _driver) : base(_driver)
        {
        }

        // Web Element
        private IWebElement topMenuAssignLeave => driver.FindElement(By.XPath("//a[text() = 'Assign Leave']"));

        private IWebElement assignLeaveTitle => driver.FindElement(By.XPath("//h6[text()= 'Assign Leave']"));

        private IWebElement fieldEmpName => driver.FindElement(By.XPath("//label[text() = 'Employee Name']/../..//input"));

        private IWebElement dropdownLeaveType => driver.FindElement(By.XPath("//label[text() = 'Leave Type']/../..//div[@tabindex = '0']"));

        private IWebElement textLeaveBalance => driver.FindElement(By.XPath("//div[@class='orangehrm-leave-balance']/..//p"));

        private IWebElement fieldFromDate => driver.FindElement(By.XPath("//label[text() = 'From Date']/../..//input"));

        private IWebElement fieldToDate => driver.FindElement(By.XPath("//label[text() = 'To Date']/../..//input"));

        private IWebElement textareaComment => driver.FindElement(By.XPath("//textarea"));


        // Method Interact
        public void Goto_AssignLeavePage() 
        {
            topMenuAssignLeave.Click();
        }

        public bool IsAssignLeaveTitleDisplayed()
        {
            return assignLeaveTitle.Displayed;
        }

        public void GetDefaultValue(string def_EmpName, string def_LeaveType, string def_LeaveBalance, string def_DateTime, string def_Comment)
        {
            string def_PlaceholderValue = fieldEmpName.GetDomAttribute("placeholder");
            def_PlaceholderValue.ShouldContain(def_EmpName);

            string def_DropdownValue = dropdownLeaveType.Text;
            def_DropdownValue.ShouldContain(def_LeaveType);

            string def_TextValue = textLeaveBalance.Text;
            def_TextValue.ShouldContain(def_LeaveBalance);

            string def_FromDateValue = fieldFromDate.GetDomAttribute("placeholder");
            def_FromDateValue.ShouldContain(def_DateTime);

            string def_ToDateValue = fieldToDate.GetDomAttribute("placeholder");
            def_ToDateValue.ShouldContain(def_DateTime);

            string def_CommentValue = textareaComment.Text;
            def_CommentValue.ShouldContain(def_Comment);
        }

        //public void GetDefaultValue_
    }
}