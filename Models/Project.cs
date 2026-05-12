using System.ComponentModel.DataAnnotations;

namespace Projektverwaltung.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }

        public enum Status
        {
            Planned,
            Active,
            Completed
        }
        [Required]
        [Display(Name = "Current Status")]
        public Status CurrentStatus { get; set; }


        // FK 

        public string? UserId { get; set; }
        // Navigation
        public User? User { get; set; }

        // 1:M Relationship
        public List<ProjectTask> ProjectTasks { get; set; } = new();

    }
}
