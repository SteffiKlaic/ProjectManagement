using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Projektverwaltung.Models
{
    public class User : IdentityUser
    {

        // 1:N relationship
        public List<Project> Projects { get; set; } = new();

    }
}
