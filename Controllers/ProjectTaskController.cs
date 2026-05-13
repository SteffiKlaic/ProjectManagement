using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
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
    public class ProjectTaskController : Controller
    {
        private readonly ProjektverwaltungContext _context;

        public ProjectTaskController(ProjektverwaltungContext context)
        {
            _context = context;
        }

        // GET: ProjectTask
        public async Task<IActionResult> Index(int projectId)
        {
            ViewBag.projectId = projectId;

            var projektverwaltungContext = _context.ProjectTask
                .Include(p => p.Project)
                .Where(p => p.ProjectId == projectId && !p.IsDeleted);
               
            return View(await projektverwaltungContext.ToListAsync());
        }

        // GET: ProjectTask/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTask
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.ProjectTaskId == id && !m.IsDeleted);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // GET: ProjectTask/Create
        public IActionResult Create(int id)
        {
            var model = new ProjectTask { ProjectId = id };
            return View(model);
        }

        // POST: ProjectTask/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,CurrentStatus, ProjectId")] ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {

                _context.Add(projectTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { projectId = projectTask.ProjectId });
            }

            return View(projectTask);
        }

        // GET: ProjectTask/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTask.FirstOrDefaultAsync(p => p.ProjectTaskId == id && !p.IsDeleted);
            if (projectTask == null)
            {
                return NotFound();
            }
        
            return View(projectTask);
        }

        // POST: ProjectTask/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectTaskId,Title,Description,CurrentStatus,ProjectId")] ProjectTask projectTask)
        {

            if (id != projectTask.ProjectTaskId)
            {
                return NotFound();
            }

            var existingTask = await _context.ProjectTask.FirstOrDefaultAsync(p => p.ProjectTaskId == id && !p.IsDeleted);

            if (existingTask == null)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    existingTask.Title = projectTask.Title;
                    existingTask.Description = projectTask.Description;
                    existingTask.CurrentStatus = projectTask.CurrentStatus;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectTaskExists(projectTask.ProjectTaskId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new {projectId = projectTask.ProjectId});
            }
            return View(projectTask);
        }

        // GET: ProjectTask/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTask
                .Include(p => p.Project)
                .FirstOrDefaultAsync(m => m.ProjectTaskId == id && !m.IsDeleted);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // POST: ProjectTask/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectTask = await _context.ProjectTask.FirstOrDefaultAsync(p => p.ProjectTaskId == id && !p.IsDeleted);

            if (projectTask != null)
            {
                projectTask.IsDeleted = true;
               
            }
            
            else
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new {projectId = projectTask.ProjectId});
        }

        [HttpGet]
        public async Task<IActionResult> ByTask(string searchTerm, int projectId, ProjectTask projectTask)
        {

            if (string.IsNullOrWhiteSpace(searchTerm))
                return RedirectToAction(nameof(Index), new { projectId = projectTask.ProjectId });


            searchTerm = searchTerm.Trim();


            var tasks = await _context.ProjectTask
                .Include(p => p.Project)
                .Where(p => p.Title.Contains(searchTerm) || p.Description.Contains(searchTerm))
                .Where(p => !p.IsDeleted)
                .Where(p => p.ProjectId == projectId)
                .OrderByDescending(c => c.CreatedOn)
                .ToListAsync();


            ViewBag.FilterTask = searchTerm;
            @ViewBag.projectId = projectId;


            return View("Index", tasks);
        }

        private bool ProjectTaskExists(int id)
        {
            return _context.ProjectTask.Any(e => e.ProjectTaskId == id);
        }
    }
}
