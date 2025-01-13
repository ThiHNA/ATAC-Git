using System.Data;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OrangeHRM.Model;

namespace OrangeHRM.Pages
{
    public class LeaveListPage : BasePage
    {
        public LeaveListPage(IWebDriver _driver) : base(_driver)
        {
        }

        // Web Element
        // Leave List 
        // Field From Date
        private IWebElement fieldFromDate => driver.FindElement(By.XPath("//label[text() = 'From Date']/../..//input"));
        
        // Field To Date
        private IWebElement fieldToDate => driver.FindElement(By.XPath("//label[text() = 'To Date']/../..//input"));
        
        // Title To Date
        private IWebElement textToDate => driver.FindElement(By.XPath("//label[text() = 'To Date']"));
        
        // Dropdown Leave Type
        private IWebElement dropdownLeaveType => driver.FindElement(By.XPath("//label[text() = 'Leave Type']/../..//div[@tabindex = '0']"));

        // Listbox Leave Type
        private Func<string, IWebElement> OptionLeaveType => leaveTypeValue => driver.FindElement(By.XPath($"//div[@role='listbox']//span[text()='{leaveTypeValue}']"));
        private IWebElement fieldEmpName => driver.FindElement(By.XPath("//label[text() = 'Employee Name']/../..//input"));
        private Func<string, IWebElement> OptionEmpName => empNameValue => driver.FindElement(By.XPath($"//div[@role='listbox']//span[contains(text(), '{empNameValue}')]"));
        private IWebElement buttonSearch => driver.FindElement(By.XPath("//button[@type='submit']"));
        private IWebElement dropdownLeaveStatus => driver.FindElement(By.XPath("//label[text() = 'Show Leave with Status']/../..//div[@tabindex = '0']"));
        private Func<string, IWebElement> OptionLeaveStatus => leaveStatusValue => driver.FindElement(By.XPath($"//div[@role='listbox']//span[text()='{leaveStatusValue}']"));
        private IReadOnlyCollection<IWebElement> rowsLeaveList => driver.FindElements(By.XPath("//div[@role='row']"));
        private IWebElement buttonDetail => driver.FindElement(By.XPath("//i[@class = 'oxd-icon bi-three-dots-vertical']/.."));
        private Func<string, IWebElement> OptionDetailsLeave => leaveDetailsValue => driver.FindElement(By.XPath($"//ul[@role = 'menu']//p[text() = '{leaveDetailsValue}']"));
        // Method Interact
        // Navigate to Leave List
        public void Goto_LeaveList()
        {
            Goto_SubLeavePage("Leave List");
        }

        // Input value into field From Date and To Date
        public void EnterFromDate_ToDate(string fromDate, string toDate)
        {
            if (fromDate != null)
            {
                fieldFromDate.SendKeys(Keys.Control + "a");
                fieldFromDate.SendKeys(Keys.Delete);
                fieldFromDate.SendKeys(fromDate);
                // Click to apply date
                textToDate.Click();
            }
            if (toDate != null)
            {
                fieldToDate.SendKeys(Keys.Control + "a");
                fieldToDate.SendKeys(Keys.Delete);
                fieldToDate.SendKeys(toDate);
                // Click to apply date
                textToDate.Click();
            }
        }

        // Chose value from Leave Type
        public void ChooseDropDownLeaveType(string leaveTypeValue)
        {
            // Click to show value of Leave Type
            dropdownLeaveType.Click();
            // Click value option match
            OptionLeaveType(leaveTypeValue).Click();
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

        public string[] CheckLeaveStatus(string fromDate, string toDate)
        {
            // Change fromDate, toDate from string to Date
            DateTime startLeaveDay = DateTime.Parse(fromDate);
            DateTime endLeaveDay = DateTime.Parse(toDate);

            // Get Current Day
            DateTime currentDay = DateTime.Now;

            string[] leaveStatus = null;

            // Check if Current Day in Leave Period
            if (startLeaveDay <= currentDay && endLeaveDay >= currentDay)
            {
                leaveStatus = new string[] { "Taken" };
            }
            else if (currentDay <= startLeaveDay)
            {
                leaveStatus = new string[] { "Scheduled" };
            }

            return leaveStatus;
        }

        public void ChooseDropDownLeaveStatus(string[] leaveStatusValue)
        {
            foreach (string value in leaveStatusValue)
            {
                dropdownLeaveStatus.Click();
                OptionLeaveStatus(value).Click();
            }
        }

        public void ClickSearchButton()
        {
            buttonSearch.Click();
        }

        public void ViewLeaveDetails()
        {
            WaitUntil(driver => buttonDetail.Displayed, 100);
            buttonDetail.Click();  
            OptionDetailsLeave("View Leave Details").Click();
        }

        public bool CheckIfLeaveAssigned(AssignLeaveModel leaveInfo)
        {
            EnterFromDate_ToDate(leaveInfo.FromDate, leaveInfo.ToDate);

            string[] leaveStatus = { "Scheduled", "Taken" };
            ChooseDropDownLeaveStatus(leaveStatus);

            ChooseDropDownLeaveType(leaveInfo.LeaveType);

            EnterEmployeeName(leaveInfo.EmployeeName);
            ClickSearchButton();

            if (rowsLeaveList.Count > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
