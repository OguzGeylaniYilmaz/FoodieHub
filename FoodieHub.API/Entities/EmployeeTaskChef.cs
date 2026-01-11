using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodieHub.API.Entities
{
    public class EmployeeTaskChef
    {
        [Key]
        public int Id { get; set; } // Primary key

        [ForeignKey("EmployeeTask")]
        public int EmployeeTaskId { get; set; } // Foreign key to EmployeeTask
        public EmployeeTask EmployeeTask { get; set; } // Foreign key to EmployeeTask

        [ForeignKey("Chef")]
        public int ChefId { get; set; } // Foreign key to Chef
        public Chef Chef { get; set; } // Foreign key to Chef
    }
}
