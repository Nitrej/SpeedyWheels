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
    public class CarsController : Controller
    {
        private readonly RentalDataContext _context;

        public CarsController(RentalDataContext context)
        {
            _context = context;
        }

        // GET: Cars
        [Authorize(Policy = "moderatorsOnly")]
        public async Task<IActionResult> Index()
        {
              return _context.Cars != null ? 
                          View(await _context.Cars.OrderBy(o => o.Id).ToListAsync()) :
                          Problem("Entity set 'RentalDataContext.Cars'  is null.");
        }

        // GET: Cars/Details/5
        [Authorize(Policy = "moderatorsOnly")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        [Authorize(Policy = "moderatorsOnly")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "moderatorsOnly")]
        public async Task<IActionResult> Create([Bind("Id,Name,CostPerHour,ProductionYear,Mileage,DoorCount,GearBox,SeatsCount,IsRented,Brand,RegistrationNumber,ImageAddress,IsActive")] Car car)
        {
            if (ModelState.IsValid)
            {
                var newCar = new Car();
                newCar.ProductionYear = car.ProductionYear;
                newCar.CostPerHour = car.CostPerHour;
                newCar.Brand = car.Brand;
                newCar.Mileage = car.Mileage;
                newCar.IsRented = car.IsRented;
                newCar.DoorCount = car.DoorCount;
                newCar.GearBox = car.GearBox;
                newCar.ImageAddress = car.ImageAddress;
                newCar.IsActive = car.IsActive;
                newCar.RegistrationNumber = car.RegistrationNumber;
                newCar.Name = car.Name;
                newCar.SeatsCount = car.SeatsCount;

                _context.Add(newCar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        [Authorize(Policy = "moderatorsOnly")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "moderatorsOnly")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CostPerHour,ProductionYear,Mileage,DoorCount,GearBox,SeatsCount,IsRented,Brand,RegistrationNumber,ImageAddress,IsActive")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            return View(car);
        }

        // GET: Cars/Delete/5
        [Authorize(Policy = "moderatorsOnly")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "moderatorsOnly")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cars == null)
            {
                return Problem("Entity set 'RentalDataContext.Cars'  is null.");
            }
            var car = await _context.Cars.FindAsync(id);
            car.IsActive = false;
            if (car != null)
            {
                _context.Cars.Update(car);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
          return (_context.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
