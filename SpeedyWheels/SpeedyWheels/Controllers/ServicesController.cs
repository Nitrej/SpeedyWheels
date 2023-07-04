using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SpeedyWheels.Models;

namespace SpeedyWheels.Controllers
{
    public class ServicesController : Controller
    {
        private readonly RentalDataContext _context;

        public ServicesController(RentalDataContext context)
        {
            _context = context;
        }

        // GET: Services
        [Authorize(Policy = "moderatorsOnly")]
        public async Task<IActionResult> Index()
        {
            var rentalDataContext = _context.Services.Include(s => s.Car).OrderBy(o => o.Id);
            return View(await rentalDataContext.ToListAsync());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var services = await _context.Services
                .Include(s => s.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (services == null)
            {
                return NotFound();
            }

            return View(services);
        }

        // GET: Services/Create
        public IActionResult Create(int id)
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", id);
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarId,StartDate,EndDate,Cost,Description")] Services services)
        {
            services.CarId = services.Id;
            services.Id = 0;
            services.Car = _context.Cars.FirstOrDefault(i => i.Id == services.CarId);

            _context.Add(services);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", services.CarId);
            return View(services);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var services = await _context.Services.Include(i => i.Car).FirstOrDefaultAsync(m => m.Id == id);
            if (services == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", services.CarId);
            return View(services);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarId,StartDate,EndDate,Cost,Description")] Services services)
        {
            if (id != services.Id)
            {
                return NotFound();
            }

            services.Car = _context.Cars.FirstOrDefault(i => i.Id == services.CarId);

            try
            {
                _context.Update(services);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicesExists(services.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
            
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Brand", services.CarId);
            return View(services);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var services = await _context.Services
                .Include(s => s.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (services == null)
            {
                return NotFound();
            }

            return View(services);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Services == null)
            {
                return Problem("Entity set 'RentalDataContext.Services'  is null.");
            }
            var services = await _context.Services.FindAsync(id);
            if (services != null)
            {
                _context.Services.Remove(services);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicesExists(int id)
        {
          return (_context.Services?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
