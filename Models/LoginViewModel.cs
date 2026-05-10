using System.ComponentModel.DataAnnotations;

namespace TaskManagementApp_MVC_.Models
{
    public class LoginViewModel
    {
        [EmailAddress]
        [StringLength(255)]
        public string? Email { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [StringLength(255)]
        public string? Password { get; set; }
    }
}
