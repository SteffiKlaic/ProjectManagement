using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projektverwaltung.Models;

namespace Projektverwaltung.Data
{
    public class ProjektverwaltungContext : IdentityDbContext<User>
    {
        public ProjektverwaltungContext (DbContextOptions<ProjektverwaltungContext> options)
            : base(options)
        {
        }
        public DbSet<Projektverwaltung.Models.Project> Project { get; set; } = default!;
        public DbSet<Projektverwaltung.Models.ProjectTask> ProjectTask { get; set; } = default!;
    }
}
