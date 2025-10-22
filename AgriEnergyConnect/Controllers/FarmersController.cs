using AgriEnergyConnect.Data;
using AgriEnergyConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnect.Controllers
{
    [Authorize(Roles = "Employee")] // Only employees can manage farmers
    public class FarmersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FarmersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET Farmers
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Farmers.ToListAsync());
        }

        // GET Farmers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST Farmers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Farmer farmer)
        {
            if (ModelState.IsValid)
            {
                _context.Farmers.Add(farmer);
                await _context.SaveChangesAsync();
                TempData["ToastMessage"] = "Farmer added successfully!";
                TempData["ToastType"] = "success";
                return RedirectToAction(nameof(Index));
            }
            return View(farmer);
        }


        // GET Farmers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var farmer = await _context.Farmers.FindAsync(id);
            if (farmer == null) return NotFound();

            return View(farmer);
        }

        // POST Farmers/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Farmer farmer)
        {
            if (id != farmer.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(farmer);
                await _context.SaveChangesAsync();

                TempData["ToastMessage"] = "Farmer updated successfully!";
                TempData["ToastType"] = "info";

                return RedirectToAction(nameof(Index));
            }
            return View(farmer);
        }

        // GET Farmers/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var farmer = await _context.Farmers.FirstOrDefaultAsync(f => f.Id == id);
            if (farmer == null) return NotFound();

            return View(farmer);
        }

        // POST Farmers/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var farmer = await _context.Farmers.FindAsync(id);
            if (farmer != null)
            {
                _context.Farmers.Remove(farmer);
                await _context.SaveChangesAsync();

                TempData["ToastMessage"] = "Farmer deleted successfully!";
                TempData["ToastType"] = "danger";
            }
            return RedirectToAction(nameof(Index));
        }

        // GET Farmers/Details
        [Authorize(Roles = "Farmer,Employee")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var farmer = await _context.Farmers.FirstOrDefaultAsync(m => m.Id == id);
            if (farmer == null)
                return NotFound();

            return View(farmer);
        }

        // GET Farmers/Products
        [Authorize(Roles = "Employee,Farmer")]
        public async Task<IActionResult> Products(int? id)
        {
            if (id == null)
                return NotFound();

            var farmer = await _context.Farmers
                .Include(f => f.Products)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (farmer == null)
                return NotFound();

            return View(farmer);
        }


    }
}
