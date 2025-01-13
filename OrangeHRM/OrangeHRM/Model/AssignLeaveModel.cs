namespace OrangeHRM.Model
{
    public class AssignLeaveModel
    {
        public string EmployeeName { get; set; }
        public string LeaveType { get; set; }
        public string LeaveBalance { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PartialDays { get; set; }
        public string Duration { get; set; }
        public string Comment { get; set; }
    }
}
