using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoundationWebApp.Data;
using FoundationWebApp.Models;

namespace FoundationWebApp.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly AppDbContext _context;

        public AssignmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Assignments
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Assignments.Include(a => a.Disaster).Include(a => a.Volunteer);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Assignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .Include(a => a.Disaster)
                .Include(a => a.Volunteer)
                .FirstOrDefaultAsync(m => m.Assignment_ID == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // GET: Assignments/Create
        public IActionResult Create()
        {
            ViewData["Disaster_ID"] = new SelectList(_context.Disasters, "Disaster_ID", "Name");
            ViewData["Volunteer_ID"] = new SelectList(_context.Volunteers, "Volunteer_ID", "FirstName");
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Assignment_ID,Role,AssignedDate,Status,Volunteer_ID,Disaster_ID")] Assignment assignment)
        {
           
            Disaster disaster = _context.Disasters.Find(assignment.Disaster_ID);
            Volunteer volunteer = _context.Volunteers.Find(assignment.Volunteer_ID);

            assignment.Disaster = disaster;
            assignment.Volunteer = volunteer;
            _context.Add(assignment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
            
        }

        // GET: Assignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null)
            {
                return NotFound();
            }
            ViewData["Disaster_ID"] = new SelectList(_context.Disasters, "Disaster_ID", "Name", assignment.Disaster_ID);
            ViewData["Volunteer_ID"] = new SelectList(_context.Volunteers, "Volunteer_ID", "FirstName", assignment.Volunteer_ID);
            return View(assignment);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Assignment_ID,Role,AssignedDate,Status,Volunteer_ID,Disaster_ID")] Assignment assignment)
        {
            if (id != assignment.Assignment_ID)
            {
                return NotFound();
            }

            Disaster disaster = _context.Disasters.Find(assignment.Disaster_ID);
            Volunteer volunteer = _context.Volunteers.Find(assignment.Volunteer_ID);

            assignment.Disaster = disaster;
            assignment.Volunteer = volunteer;

            try
                {
                    _context.Update(assignment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentExists(assignment.Assignment_ID))
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

        // GET: Assignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.Assignments
                .Include(a => a.Disaster)
                .Include(a => a.Volunteer)
                .FirstOrDefaultAsync(m => m.Assignment_ID == id);
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment != null)
            {
                _context.Assignments.Remove(assignment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentExists(int id)
        {
            return _context.Assignments.Any(e => e.Assignment_ID == id);
        }
    }
}
