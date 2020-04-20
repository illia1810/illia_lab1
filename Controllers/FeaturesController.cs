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
    public class FeaturesController : Controller
    {
        private readonly LaptopBaseContext _context;

        public FeaturesController(LaptopBaseContext context)
        {
            _context = context;
        }

        // GET: Features
        public async Task<IActionResult> Index()
        {
            return View(await _context.Feature.ToListAsync());
        }

        // GET: Features/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feature = await _context.Feature
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feature == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "LaptopFeatures", new { f_id = feature.Id, name = feature.Feature1 });
        }

        // GET: Features/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Features/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Feature1")] Feature feature)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feature);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feature);
        }

        // GET: Features/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feature = await _context.Feature.FindAsync(id);
            if (feature == null)
            {
                return NotFound();
            }
            return View(feature);
        }

        // POST: Features/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Feature1")] Feature feature)
        {
            if (id != feature.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feature);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeatureExists(feature.Id))
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
            return View(feature);
        }

        // GET: Features/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feature = await _context.Feature
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feature == null)
            {
                return NotFound();
            }

            return View(feature);
        }

        // POST: Features/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lapF = _context.LaptopFeature.Where(f => f.FeatureId == id).Include(f => f.Laptop).ToList();
              _context.LaptopFeature.RemoveRange(lapF);
            var feature = await _context.Feature.FindAsync(id);
            _context.Feature.Remove(feature);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeatureExists(int id)
        {
            return _context.Feature.Any(e => e.Id == id);
        }
    }
}
