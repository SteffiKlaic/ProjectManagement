using System.ComponentModel.DataAnnotations;

namespace Projektverwaltung.Models
{
    public class ProjectTask
    {
        public int ProjectTaskId { get; set; }

        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }

        public enum Status
        {
            Open,
            [Display(Name = "In Progress")] InProgress,
            Done
        }
        [Required]
        [Display(Name = "Current Status")]
        public Status CurrentStatus { get; set; }   


        [Required]
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }

        // FK

        public int? ProjectId { get; set; }
        // Navigation
        public Project? Project { get; set; }
    }
}
