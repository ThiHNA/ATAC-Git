using FluentAssert;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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

        private IWebElement dropdownDuration => driver.FindElement(By.XPath("//label[text() = 'Duration']/../..//div[@class='oxd-select-text-input']"));

        private IWebElement buttonAssign => driver.FindElement(By.XPath("//button[@type='submit']"));

        private IWebElement buttonConfirmOk => driver.FindElement(By.XPath("//button[@class='oxd-button oxd-button--medium oxd-button--secondary orangehrm-button-margin']"));

        private IWebElement messSuccess => driver.FindElement(By.XPath("//div[@class='oxd-toast oxd-toast--success oxd-toast-container--toast']"));

        private Func<string, IWebElement> OptionLeaveType => leaveTypeValue => driver.FindElement(By.XPath($"//div[@role='listbox']//span[text()='{leaveTypeValue}']"));

        private Func<string, IWebElement> OptionEmpName => empNameValue => driver.FindElement(By.XPath($"//div[@role='listbox']//span[contains(text(), '{empNameValue}')]"));

        // Method Interact
        // Navigate to Assign Page
        public void Goto_AssignLeavePage() 
        {
            topMenuAssignLeave.Click();
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
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));
            IWebElement option = wait.Until(driver => OptionEmpName(empName));
            option.Click();
        }

        // Input value into field From Date and To Date
        public void EnterFromDate_ToDate(string fromDate, string toDate)
        {
            fieldFromDate.SendKeys(fromDate);
            if (toDate != null)
            {
                fieldToDate.Clear();
            }
            fieldToDate.SendKeys(toDate);
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

        // Check Message Success is displayed
        public bool isMessageSuccessDisplay()
        {
            return messSuccess.Displayed;
        }
    }
}
