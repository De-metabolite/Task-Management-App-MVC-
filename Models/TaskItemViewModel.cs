using System.ComponentModel.DataAnnotations;
using TaskManagementApp_MVC_.Entities;

namespace TaskManagementApp_MVC_.Models
{
    public class TaskItemViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The title should be between 3-100 characters")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "The description should be between 10-500 characters")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DueDate { get; set; }
        [Required(ErrorMessage ="This field is required")]
        public Priority Priority { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsOverdue => Status != Status.Completed && DueDate < DateTime.Now;

        
    }
}
