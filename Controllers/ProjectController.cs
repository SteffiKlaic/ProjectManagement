using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projektverwaltung.Data;
using Projektverwaltung.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Projektverwaltung.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly ProjektverwaltungContext _context;

        public ProjectController(ProjektverwaltungContext context)
        {
            _context = context;
        }

        // GET: Project
        public async Task<IActionResult> Index()
        {
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userProjects = await _context.Project
                .Where(p => p.UserId == currentUser && !p.IsDeleted)
                .ToListAsync();

            return View(userProjects);
        }

        // GET: Project/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.ProjectId == id && !m.IsDeleted);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Project/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectName,Description, CurrentStatus")] Project project)
        {
            if (ModelState.IsValid)
            {
                var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
                project.UserId = currentUser;
                
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Project/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.FirstOrDefaultAsync(p => p.ProjectId == id && !p.IsDeleted);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,ProjectName,Description, CurrentStatus")] Project project)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }

            var existingProject = await _context.Project.FirstOrDefaultAsync(p => p.ProjectId == id && !p.IsDeleted);

            if (existingProject == null)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    existingProject.ProjectName = project.ProjectName;
                    existingProject.Description = project.Description;
                    existingProject.CurrentStatus = project.CurrentStatus;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Project/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project
                .FirstOrDefaultAsync(m => m.ProjectId == id && !m.IsDeleted);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Project
            .Include(p => p.ProjectTasks)
            .FirstOrDefaultAsync(p => p.ProjectId == id && !p.IsDeleted);
            
            if (project != null)
            {
                project.IsDeleted = true;

                foreach (var task in project.ProjectTasks)
                {
                    task.IsDeleted = true;
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.ProjectId == id);
        }
    }
}
