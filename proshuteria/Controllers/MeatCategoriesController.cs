using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proshuteria.Data;

namespace proshuteria.Controllers
{
    public class MeatCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MeatCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MeatCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.MeatCategories.ToListAsync());
        }

        // GET: MeatCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meatCategory = await _context.MeatCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meatCategory == null)
            {
                return NotFound();
            }

            return View(meatCategory);
        }

        // GET: MeatCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MeatCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] MeatCategory meatCategory)
        {
            meatCategory.RegisterOn = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return View(meatCategory);
            }
            _context.Add(meatCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: MeatCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meatCategory = await _context.MeatCategories.FindAsync(id);
            if (meatCategory == null)
            {
                return NotFound();
            }
            return View(meatCategory);
        }

        // POST: MeatCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] MeatCategory meatCategory)
        {
            if (id != meatCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    meatCategory.RegisterOn = DateTime.Now;
                    _context.MeatCategories.Update(meatCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeatCategoryExists(meatCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        meatCategory.RegisterOn = DateTime.Now;
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(meatCategory);
        }

        // GET: MeatCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meatCategory = await _context.MeatCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meatCategory == null)
            {
                return NotFound();
            }

            return View(meatCategory);
        }

        // POST: MeatCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meatCategory = await _context.MeatCategories.FindAsync(id);
            if (meatCategory != null)
            {
                _context.MeatCategories.Remove(meatCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeatCategoryExists(int id)
        {
            return _context.MeatCategories.Any(e => e.Id == id);
        }
    }
}
