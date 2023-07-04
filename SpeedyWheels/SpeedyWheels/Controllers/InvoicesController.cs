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
    public class InvoicesController : Controller
    {
        private readonly RentalDataContext _context;

        public InvoicesController(RentalDataContext context)
        {
            _context = context;
        }

        // GET: Invoices
        [Authorize(Policy = "moderatorsOnly")]
        public async Task<IActionResult> Index()
        {
            var rentalDataContext = _context.Invoices.Include(i => i.Client).Include(i => i.Rental).OrderBy(o => o.Id);
            return View(await rentalDataContext.ToListAsync());
        }

        // GET: Invoices/Details/5
        [Authorize(Policy = "moderatorsOnly")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Client)
                .Include(i => i.Rental)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        [Authorize(Policy = "moderatorsOnly")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Invoices == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.Include(i => i.Client)
                .Include(i => i.Rental).FirstOrDefaultAsync(m => m.Id == id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", invoice.ClientId);
            ViewData["RentalId"] = new SelectList(_context.Rentals, "Id", "Id", invoice.RentalId);
            ViewData["Client"] = invoice.Client;
            ViewData["Rental"] = invoice.Rental;
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "moderatorsOnly")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RentalId,ClientId,IssueDate,amount,PaymentStatus")] Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return NotFound();
            }
            invoice.Client = _context.Clients.FirstOrDefault(i => i.Id == invoice.ClientId);
            invoice.Rental = _context.Rentals.FirstOrDefault(i => i.Id == invoice.RentalId);

            try
            {
                _context.Update(invoice);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(invoice.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", invoice.ClientId);
            ViewData["RentalId"] = new SelectList(_context.Rentals, "Id", "Id", invoice.RentalId);
            return View(invoice);
        }  

        private bool InvoiceExists(int id)
        {
          return (_context.Invoices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
