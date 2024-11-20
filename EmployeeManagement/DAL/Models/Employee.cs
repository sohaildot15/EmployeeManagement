using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.DAL.Models
{
    public class Employee
    {
        public int EmployeeCode { get; set; }


        [Required(ErrorMessage = "Vendor is required.")]
        public int VendorCode { get; set; }


        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string EmployeeName { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; }

        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Work mode is required.")]
        public string WorkMode { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public int StateID { get; set; }
        
        [Required(ErrorMessage = "City is required.")]
        public int CityID { get; set; }

        public string StateName { get; set; }

        public string CityName { get; set; }
        public string TaskStatus { get; set; } = "Pending"; // Default value

    }
}
