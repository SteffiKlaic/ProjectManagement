using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projektverwaltung.Data;
using Projektverwaltung.Models;

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
        public async Task<IActionResult> Index()
        {
            var projektverwaltungContext = _context.ProjectTask.Include(p => p.Project);
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
                .FirstOrDefaultAsync(m => m.ProjectTaskId == id);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // GET: ProjectTask/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectName");
            return View();
        }

        // POST: ProjectTask/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectTaskId,Title,Description,CurrentStatus,CreatedOn,ProjectId")] ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectName", projectTask.ProjectId);
            return View(projectTask);
        }

        // GET: ProjectTask/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = await _context.ProjectTask.FindAsync(id);
            if (projectTask == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectName", projectTask.ProjectId);
            return View(projectTask);
        }

        // POST: ProjectTask/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectTaskId,Title,Description,CurrentStatus,CreatedOn,ProjectId")] ProjectTask projectTask)
        {
            if (id != projectTask.ProjectTaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectTask);
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Project, "ProjectId", "ProjectName", projectTask.ProjectId);
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
                .FirstOrDefaultAsync(m => m.ProjectTaskId == id);
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
            var projectTask = await _context.ProjectTask.FindAsync(id);
            if (projectTask != null)
            {
                _context.ProjectTask.Remove(projectTask);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectTaskExists(int id)
        {
            return _context.ProjectTask.Any(e => e.ProjectTaskId == id);
        }
    }
}
