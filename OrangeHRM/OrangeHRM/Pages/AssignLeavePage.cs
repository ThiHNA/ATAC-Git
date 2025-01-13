using FluentAssert;
using Microsoft.IdentityModel.Tokens;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OrangeHRM.Model;

namespace OrangeHRM.Pages
{
    public class AssignLeavePage : BasePage
    {
        public AssignLeavePage(IWebDriver _driver) : base(_driver)
        {
        }

        // Web Element
        // Title Assign Leave
        private IWebElement assignLeaveTitle => driver.FindElement(By.XPath("//h6[text()= 'Assign Leave']"));

        // Textbox Employee Name
        private IWebElement fieldEmpName => driver.FindElement(By.XPath("//label[text() = 'Employee Name']/../..//input"));

        // Listbox Employee Name
        private Func<string, IWebElement> OptionEmpName => empNameValue => driver.FindElement(By.XPath($"//div[@role='listbox']//span[contains(text(), '{empNameValue}')]"));

        // Message required Employee Name
        private IWebElement messRequiredEmpName => driver.FindElement(By.XPath("//label[text() = 'Employee Name']/../../span[text() = 'Required']"));

        // Dropdown Leave Type
        private IWebElement dropdownLeaveType => driver.FindElement(By.XPath("//label[text() = 'Leave Type']/../..//div[@tabindex = '0']"));

        // Listbox Leave Type
        private Func<string, IWebElement> OptionLeaveType => leaveTypeValue => driver.FindElement(By.XPath($"//div[@role='listbox']//span[text()='{leaveTypeValue}']"));

        // Message required Leave Type
        private IWebElement messRequiredLeaveType => driver.FindElement(By.XPath("//label[text() = 'Leave Type']/../../span[text() = 'Required']"));

        // Text Leave Balance
        private IWebElement textLeaveBalance => driver.FindElement(By.XPath("//div[@class='orangehrm-leave-balance']/..//p"));

        // Textbox From Date
        private IWebElement fieldFromDate => driver.FindElement(By.XPath("//label[text() = 'From Date']/../..//input"));

        // Message Required From Date
        private IWebElement messRequiredFromDate => driver.FindElement(By.XPath("//label[text() = 'From Date']/../../span[text() = 'Required']"));

        // Textbox To Date
        private IWebElement fieldToDate => driver.FindElement(By.XPath("//label[text() = 'To Date']/../..//input"));

        // Title To Date
        private IWebElement textToDate => driver.FindElement(By.XPath("//label[text() = 'To Date']"));

        // Message Required To Date
        private IWebElement messRequiredToDate => driver.FindElement(By.XPath("//label[text() = 'To Date']/../../span[text() = 'Required']"));

        // Dropdown Partical Days
        private IWebElement dropdownPartialDays => driver.FindElement(By.XPath("//label[text() = 'Partial Days']/../..//div[@tabindex = '0']"));

        // Listbox Partical Days
        private Func<string, IWebElement> OptionPartialDays => partialDayValue => driver.FindElement(By.XPath($"//div[@role='listbox']//span[contains(text(), '{partialDayValue}')]"));

        // Dropdown Duration
        private IWebElement dropdownDuration => driver.FindElement(By.XPath("//label[text() = 'Duration']/../..//div[@class='oxd-select-text-input']"));

        // Listbox Duration
        private Func<string, IWebElement> OptionDuration => durationValue => driver.FindElement(By.XPath($"//div[@role='listbox']//span[contains(text(), '{durationValue}')]"));

        // Textarea Comment
        private IWebElement textareaComment => driver.FindElement(By.XPath("//textarea"));

        // Button Assign
        private IWebElement buttonAssign => driver.FindElement(By.XPath("//button[@type='submit']"));

        // Button OK
        private IWebElement buttonConfirmOk => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary orangehrm-button-margin']"));

        // Message Success
        private IWebElement messSuccess => driver.FindElement(By.XPath("//div[@class='oxd-toast oxd-toast--success oxd-toast-container--toast']"));

        // Message Warning
        private IWebElement messWarning => driver.FindElement(By.XPath("//div[@class='oxd-toast oxd-toast--warn oxd-toast-container--toast']"));

        // Message Error
        private IWebElement messError => driver.FindElement(By.XPath("//div[@class='oxd-toast oxd-toast--error oxd-toast-container--toast']"));

        // Message check To Date
        private IWebElement messToDate => driver.FindElement(By.XPath("//span[text() = 'To date should be after from date']"));

        // Method Interact
        // Navigate to Assign Page
        public void Goto_AssignLeavePage() 
        {
            Goto_SubLeavePage("Assign Leave");
        }

        // Check Assign Leave title is displayed
        public bool IsAssignLeaveTitleDisplayed()
        {
            return assignLeaveTitle.Displayed;
        }
       
        // Get list of default value 
        public Dictionary<string, string> GetDefaultValue()
        {
            // Get default value of Employee Name
            string def_PlaceholderValue = fieldEmpName.GetDomAttribute("placeholder");
            // Get default value of Leave Type
            string def_DropdownValue = dropdownLeaveType.Text;
            // Get default value of Leave Balance
            string def_TextValue = textLeaveBalance.Text;
            // Get default value of From Date
            string def_FromDateValue = fieldFromDate.GetDomAttribute("placeholder");
            // Get default value of To Date
            string def_ToDateValue = fieldToDate.GetDomAttribute("placeholder");
            // Get default value of Comment
            string def_CommentValue = textareaComment.Text;

            // Add default value to list
            Dictionary<string, string> def_List = new Dictionary<string, string>();
            def_List["def_PlaceholderValue"] = def_PlaceholderValue;
            def_List["def_DropdownValue"] = def_DropdownValue;
            def_List["def_TextValue"] = def_TextValue;
            def_List["def_FromDateValue"] = def_FromDateValue;
            def_List["def_ToDateValue"] = def_ToDateValue;
            def_List["def_CommentValue"] = def_CommentValue;

            return def_List;
        }

        // Input value into field Employee Name
        public void EnterEmployeeName(string empName)
        {
            // Enter Employee Name
            fieldEmpName.SendKeys(empName);

            // Wait until suggest Employee is displayed and click
            WaitUntil(driver => OptionEmpName(empName).Displayed, 100);
            OptionEmpName(empName).Click();
        }

        // Input value into field From Date and To Date
        public void EnterFromDate_ToDate(string fromDate, string toDate)
        {
            fieldFromDate.SendKeys(fromDate);
            if (toDate != null)
            {
                fieldToDate.SendKeys(Keys.Control + "a");
                fieldToDate.SendKeys(Keys.Delete);
                fieldToDate.SendKeys(toDate);
            }
            
            // Click to apply date
            textToDate.Click();
        }

        // Input value into textarea Comment
        public void EnterComment(string comment)
        {
            textareaComment.SendKeys(comment);
        }

        // Chose value from Leave Type
        public void ChooseDropDownLeaveType(string leaveTypeValue)
        {
            // Click to show value of Leave Type
            dropdownLeaveType.Click();
            // Click value option match
            OptionLeaveType(leaveTypeValue).Click();
        }

        // Chose value from Partial Days
        public void ChooseDropDownPartialDays(string partialDayValue)
        {
            // Click to show value of Leave Type
            dropdownPartialDays.Click();
            // Click value option match
            OptionPartialDays(partialDayValue).Click();
        }

        // Chose value from Partial Days
        public void ChooseDropDownDuration(string durationValue)
        {
            // Click to show value of Leave Type
            dropdownDuration.Click();
            // Click value option match
            OptionDuration(durationValue).Click();
        }

        // Click on button Assign
        public void ClickButtonAssign()
        {
            buttonAssign.Click();
        }

        // Click on button Ok of Alert Confirm
        public void AcceptAssignLeave()
        {
            buttonConfirmOk.Click();
        }

        // Check Message Result is displayed
        public bool isMessageResultDisplay(IWebElement messElement)
        {
            try
            {
                WaitUntil(drive => messElement.Displayed, 100);
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        // Check Message Success is displayed
        public bool isMessageSuccDisplay()
        {
            return isMessageResultDisplay(messSuccess);
        }

        public bool isMessageWarnDisplay()
        {
            return isMessageResultDisplay(messWarning);
        }

        public bool isMessageErrorDisplay()
        {
            return isMessageResultDisplay(messError);
        }

        // Check Message Required is displayed
        public bool isRequiredMessageDisplayed_EmpName()
        {
            return messRequiredEmpName.Displayed;
        }
        public bool isRequiredMessageDisplayed_LeaveType()
        {
            return messRequiredLeaveType.Displayed;
        }

        public bool isRequiredMessageDisplayed_FromDate()
        {
            return messRequiredFromDate.Displayed;
        }

        public bool isRequiredMessageDisplayed_ToDate()
        {
            return messRequiredToDate.Displayed;
        }

        // Check message To Date greater than From Date is displayed
        public bool isCheckDateMessageDisplayed()
        {
            return messToDate.Displayed;
        }

        public void ExcuteAssignLeave(AssignLeaveModel leaveInfo)
        {
            EnterEmployeeName(leaveInfo.EmployeeName); 
            ChooseDropDownLeaveType(leaveInfo.LeaveType);
            EnterFromDate_ToDate(leaveInfo.FromDate, leaveInfo.ToDate);
            if (!leaveInfo.PartialDays.IsNullOrEmpty())
            {
                ChooseDropDownPartialDays(leaveInfo.PartialDays);
            }

            if (!leaveInfo.Duration.IsNullOrEmpty())
            {
                ChooseDropDownDuration(leaveInfo.Duration);
            }

            if (!leaveInfo.Comment.IsNullOrEmpty())
            {
                EnterComment(leaveInfo.Comment);
            }

            ClickButtonAssign();

            AcceptAssignLeave();
        }
    }
}
