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
    public class ClientOpinionsController : Controller
    {
        private readonly RentalDataContext _context;

        public ClientOpinionsController(RentalDataContext context)
        {
            _context = context;
        }

        // GET: ClientOpinions
        [Authorize(Policy = "moderatorsOnly")]
        public async Task<IActionResult> Index()
        {
            var rentalDataContext = _context.ClientOpinions.Include(c => c.Client).Include(c => c.Rental).OrderBy(o => o.Id);
            return View(await rentalDataContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClientOpinions == null)
            {
                return NotFound();
            }

            var clientOpinion = await _context.ClientOpinions
                .Include(c => c.Client)
                .Include(c => c.Rental)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientOpinion == null)
            {
                return NotFound();
            }

            return View(clientOpinion);
        }


        private bool ClientOpinionExists(int id)
        {
          return (_context.ClientOpinions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
