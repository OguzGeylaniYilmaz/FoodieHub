namespace FoodieHub.API.Entities
{
    public class EmployeeTask
    {
        public int EmployeeTaskID { get; set; }
        public string TaskName { get; set; }
        public byte TaskStatusValue { get; set; }
        public DateTime AssignDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
        public string TaskStatus { get; set; }

        //Many to Many Relationship
        public List<EmployeeTaskChef> EmployeeTaskChefs { get; set; }
    }
}
