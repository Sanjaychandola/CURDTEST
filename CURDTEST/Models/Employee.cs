using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CURDTEST.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }
        [DisplayName("Date Of Birth")]
        [Required]
        public DateTime DateOfBirth { get; set; }
        [DisplayName("E-Mail")]
        [Required]
        public string Email { get; set; }

        [Required]
        public double Salary { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return FirstName + " " + LastName; }

        }

    }
}