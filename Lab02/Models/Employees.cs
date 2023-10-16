using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab02.Models
{
    [Table("tbEmployee")]
    public class Employees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }
        [Required(ErrorMessage = "LastName is required...")]
        [StringLength(20, MinimumLength =2,ErrorMessage ="Last name from 2 to 20 characters.")]
        public string? LastName { get; set; }
        public string? FirstName { get; set;}
        public DateTime BirthDate { get; set; }
        public string? Photo { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Skills { get; set; }

    }
}
