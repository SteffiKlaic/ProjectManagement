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
            InProgress,
            Done
        }
        [Required]
        public Status CurrentStatus { get; set; }


        [Required]
        public DateTime CreatedOn { get; set; }

        // FK
        [Required]
        public int ProjectId { get; set; }
        // Navigation
        public Project Project { get; set; }
    }
}
