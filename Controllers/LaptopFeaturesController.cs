using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaptopProject;

namespace LaptopProject.Controllers
{
    public class LaptopFeaturesController : Controller
    {
        private readonly LaptopBaseContext _context;

        public LaptopFeaturesController(LaptopBaseContext context)
        {
            _context = context;
        }

        // GET: LaptopFeatures
        public async Task<IActionResult> Index(int? f_id, int? l_id)
        {
            if (l_id == null)
            {
                var laptopBaseContext = _context.LaptopFeature.Where(l => l.FeatureId == f_id).Include(l => l.Feature).Include(l => l.Laptop);
                return View(await laptopBaseContext.ToListAsync());
            }
            else
            {
                var laptopBaseContext = _context.LaptopFeature.Where(l => l.LaptopId == l_id).Include(l => l.Feature).Include(l => l.Laptop);
                return View(await laptopBaseContext.ToListAsync());
            }
        }
        
        // GET: LaptopFeatures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptopFeature = await _context.LaptopFeature
                .Include(l => l.Feature)
                .Include(l => l.Laptop)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (laptopFeature == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Laptops", new { id = laptopFeature.LaptopId });
        }

        // GET: LaptopFeatures/Create
        public IActionResult Create()
        {
            ViewData["FeatureId"] = new SelectList(_context.Feature, "Id", "Feature1");
            ViewData["LaptopId"] = new SelectList(_context.Laptop, "Id", "Name");
            return View();
        }

        // POST: LaptopFeatures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FeatureId,LaptopId")] LaptopFeature laptopFeature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(laptopFeature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FeatureId"] = new SelectList(_context.Feature, "Id", "Feature1", laptopFeature.FeatureId);
            ViewData["LaptopId"] = new SelectList(_context.Laptop, "Id", "Name", laptopFeature.LaptopId);
            return View(laptopFeature);
        }

        // GET: LaptopFeatures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptopFeature = await _context.LaptopFeature.FindAsync(id);
            if (laptopFeature == null)
            {
                return NotFound();
            }
            ViewData["FeatureId"] = new SelectList(_context.Feature, "Id", "Feature1", laptopFeature.FeatureId);
            ViewData["LaptopId"] = new SelectList(_context.Laptop, "Id", "Name", laptopFeature.LaptopId);
            return View(laptopFeature);
        }

        // POST: LaptopFeatures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FeatureId,LaptopId")] LaptopFeature laptopFeature)
        {
            if (id != laptopFeature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laptopFeature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaptopFeatureExists(laptopFeature.Id))
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
            ViewData["FeatureId"] = new SelectList(_context.Feature, "Id", "Feature1", laptopFeature.FeatureId);
            ViewData["LaptopId"] = new SelectList(_context.Laptop, "Id", "Name", laptopFeature.LaptopId);
            return View(laptopFeature);
        }

        // GET: LaptopFeatures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptopFeature = await _context.LaptopFeature
                .Include(l => l.Feature)
                .Include(l => l.Laptop)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (laptopFeature == null)
            {
                return NotFound();
            }

            return View(laptopFeature);
        }

        // POST: LaptopFeatures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var laptopFeature = await _context.LaptopFeature.FindAsync(id);
            _context.LaptopFeature.Remove(laptopFeature);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LaptopFeatureExists(int id)
        {
            return _context.LaptopFeature.Any(e => e.Id == id);
        }
    }
}
