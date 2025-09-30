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
    public class DonationsController : Controller
    {
        private readonly AppDbContext _context;

        public DonationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Donations
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Donations.Include(d => d.Disaster).Include(d => d.Resource);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Donations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations
                .Include(d => d.Disaster)
                .Include(d => d.Resource)
                .FirstOrDefaultAsync(m => m.Donation_ID == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // GET: Donations/Create
        public IActionResult Create()
        {
            ViewData["Disaster_ID"] = new SelectList(_context.Disasters, "Disaster_ID", "Name");
            ViewData["Resource_ID"] = new SelectList(_context.Resources, "Resource_ID", "Name");
            return View();
        }

        // POST: Donations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Donation_ID,DonorName,Amount,DonationDate,Disaster_ID")] Donation donation)
        {

            FoundationWebApp.Models.Resource resource = new Resource
            {
                Resource_ID = donation.Resource_ID,
                Name = "Monetary Donation",
                Quantity = 0,
                Type = "Monetary",
                StorageLocation = "N/A",
                LastUpdated = DateTime.Now,
                UserID = 1 // Assuming a default user ID for system-generated resources
            };

            donation.Resource = resource;
            _context.Add(donation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Donations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            ViewData["Disaster_ID"] = new SelectList(_context.Disasters, "Disaster_ID", "Name", donation.Disaster_ID);
            ViewData["Resource_ID"] = new SelectList(_context.Resources, "Resource_ID", "Name", donation.Resource_ID);
            return View(donation);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Donation_ID,DonorName,Amount,DonationDate,Disaster_ID")] Donation donation)
        {
            if (id != donation.Donation_ID)
            {
                return NotFound();
            }
            FoundationWebApp.Models.Resource resource = new Resource
            {
                Resource_ID = donation.Resource_ID,
                Name = "Monetary Donation",
                Quantity = 0,
                Type = "Monetary",
                StorageLocation = "N/A",
                LastUpdated = DateTime.Now,
                UserID = 1 // Assuming a default user ID for system-generated resources
            };

            donation.Resource = resource;

            try
                {
                    _context.Update(donation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationExists(donation.Donation_ID))
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

        // GET: Donations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donation = await _context.Donations
                .Include(d => d.Disaster)
                .Include(d => d.Resource)
                .FirstOrDefaultAsync(m => m.Donation_ID == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donation = await _context.Donations.FindAsync(id);
            if (donation != null)
            {
                _context.Donations.Remove(donation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonationExists(int id)
        {
            return _context.Donations.Any(e => e.Donation_ID == id);
        }
    }
}
