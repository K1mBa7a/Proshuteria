using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using proshuteria.Data;

namespace proshuteria.Controllers
{
    public class MeatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Meats
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Meats.Include(m => m.MeatCategories);
            return View(await applicationDbContext.ToListAsync());
        }

        /*public async Task<IActionResult> Products()
        {
            var applicationDbContext = _context.Meats.Include(m => m.MeatCategories);
            return View(await applicationDbContext.ToListAsync());
        }*/

        public async Task<IActionResult> Products(string searchString)
        {
            if (searchString.IsNullOrEmpty())
            {
                return View(await _context.Meats.ToListAsync());
            }

            if (_context.Meats == null)
            {
                return Problem("Няма вина в асортимента, моля проверете по-късно!");
            }


            var meats = from m in _context.Meats select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                meats = meats.Where(s => s.Name.Contains(searchString) || s.MeatCategories.Name.Contains(searchString));
            }
            return View(meats.ToList());
        }

        // GET: Meats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meat = await _context.Meats
                .Include(m => m.MeatCategories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meat == null)
            {
                return NotFound();
            }

            return View(meat);
        }

        // GET: Meats/Create
        public IActionResult Create()
        {
            ViewData["MeatCategoryId"] = new SelectList(_context.MeatCategories, "Id", "Name");
            return View();
        }

        // POST: Meats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CatalogeNumber,Name,MeatCategoryId,Price,Description,ImageUrl")] Meat meat)
        {
            meat.DateCreated = DateTime.Now;
            if (!ModelState.IsValid)
            {
                ViewData["MeatCategoryId"] = new SelectList(_context.MeatCategories, "Id", "Name", meat.MeatCategoryId);
                return View(meat);
            }
            _context.Meats.Add(meat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Meats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meat = await _context.Meats.FindAsync(id);
            if (meat == null)
            {
                return NotFound();
            }
            ViewData["MeatCategoryId"] = new SelectList(_context.MeatCategories, "Id", "Name", meat.MeatCategoryId);
            return View(meat);
        }

        // POST: Meats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CatalogeNumber,Name,MeatCategoryId,Price,Description,ImageUrl")] Meat meat)
        {
            if (id != meat.Id)
            {
                return NotFound();
            }
            meat.DateCreated = DateTime.Now;
            if (!ModelState.IsValid)
            {
                ViewData["MeatCategoryId"] = new SelectList(_context.MeatCategories, "Id", "Name", meat.MeatCategoryId);
                return View(meat);
            }

            try
            {
                _context.Meats.Update(meat);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeatExists(meat.Id))
                {
                    return NotFound();
                }
                else
                {
                    meat.DateCreated = DateTime.Now;
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Meats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meat = await _context.Meats
                .Include(m => m.MeatCategories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meat == null)
            {
                return NotFound();
            }

            return View(meat);
        }

        // POST: Meats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meat = await _context.Meats.FindAsync(id);
            if (meat != null)
            {
                _context.Meats.Remove(meat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeatExists(int id)
        {
            return _context.Meats.Any(e => e.Id == id);
        }
    }
}
