using System.ComponentModel.DataAnnotations;
using TaskManagementApp_MVC_.Entities;

namespace TaskManagementApp_MVC_.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="This field is required")]
        [StringLength(255)]
        public string? Name { get; set; }
        [Required(ErrorMessage ="This field is required")]
        [StringLength(255)]
        public string? Username {  get; set; }
        [EmailAddress]
        [StringLength(255)]
        public string? Email { get; set; }
        [Required(ErrorMessage ="This field is required")]
        [DataType(DataType.Password)]
        [StringLength(255)]
        public string? Password { get; set; }
        public List<TaskItem> taskItems { get; set; } = new List<TaskItem>();
    }
}
