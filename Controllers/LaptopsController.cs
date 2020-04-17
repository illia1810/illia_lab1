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
    public class LaptopsController : Controller
    {
        private readonly LaptopBaseContext _context;

        public LaptopsController(LaptopBaseContext context)
        {
            _context = context;
        }

        // GET: Laptops
        public async Task<IActionResult> Index(int? color_id, int? country_id, int? cpu_id, int? pr_id, int? id)
        {
            if (color_id!= null) {
                var laptop1 = _context.Laptop.Where(l=>l.ColorId==color_id).Include(l => l.Color).Include(l => l.Country).Include(l => l.Cpu).Include(l => l.Model);
                return View(await laptop1.ToListAsync());
            };
            if (country_id!= null) {
                var laptop1 = _context.Laptop.Where(l => l.CountryId == country_id).Include(l => l.Color).Include(l => l.Country).Include(l => l.Cpu).Include(l => l.Model);
                return View(await laptop1.ToListAsync());
            };
            if (cpu_id!= null) {
                var laptop1 = _context.Laptop.Where(l => l.CpuId == cpu_id).Include(l => l.Color).Include(l => l.Country).Include(l => l.Cpu).Include(l => l.Model);
                return View(await laptop1.ToListAsync());
            };
            if (pr_id!=null) {
                var laptop1 = _context.Laptop.Where(l => l.ModelId == pr_id).Include(l => l.Color).Include(l => l.Country).Include(l => l.Cpu).Include(l => l.Model);
                return View(await laptop1.ToListAsync());
            };
            if (id!= null) {
                var laptop1 = _context.Laptop.Where(l => l.Id == id).Include(l => l.Color).Include(l => l.Country).Include(l => l.Cpu).Include(l => l.Model);
                return View(await laptop1.ToListAsync());
            };
            var laptopBaseContext = _context.Laptop.Include(l => l.Color).Include(l => l.Country).Include(l => l.Cpu).Include(l => l.Model);
            return View(await laptopBaseContext.ToListAsync());
        }

        // GET: Laptops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptop = await _context.Laptop
                .Include(l => l.Color)
                .Include(l => l.Country)
                .Include(l => l.Cpu)
                .Include(l => l.Model)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (laptop == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index_feature", "Laptopfeature", new { id = laptop.Id });
        }

        // GET: Laptops/Create
        public IActionResult Create()
        {
            ViewData["ColorId"] = new SelectList(_context.Color, "Id", "Color1");
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name");
            ViewData["CpuId"] = new SelectList(_context.Processor, "Id", "Name");
            ViewData["ModelId"] = new SelectList(_context.Producer, "Id", "Model");
            return View();
        }
        public IActionResult Feature(int? l_id)
        {
            return RedirectToAction("Index","Laptopfeatures",new { l_id});
        }

        // POST: Laptops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ModelId,CpuId,ColorId,Name,CountryId")] Laptop laptop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(laptop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColorId"] = new SelectList(_context.Color, "Id", "Color1", laptop.ColorId);
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name", laptop.CountryId);
            ViewData["CpuId"] = new SelectList(_context.Processor, "Id", "Name", laptop.CpuId);
            ViewData["ModelId"] = new SelectList(_context.Producer, "Id", "Id", laptop.ModelId);
            return View(laptop);
        }

        // GET: Laptops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptop = await _context.Laptop.FindAsync(id);
            if (laptop == null)
            {
                return NotFound();
            }
            ViewData["ColorId"] = new SelectList(_context.Color, "Id", "Color1", laptop.ColorId);
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name", laptop.CountryId);
            ViewData["CpuId"] = new SelectList(_context.Processor, "Id", "Name", laptop.CpuId);
            ViewData["ModelId"] = new SelectList(_context.Producer, "Id", "Id", laptop.ModelId);
            return View(laptop);
        }

        // POST: Laptops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ModelId,CpuId,ColorId,Name,CountryId")] Laptop laptop)
        {
            if (id != laptop.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laptop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaptopExists(laptop.Id))
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
            ViewData["ColorId"] = new SelectList(_context.Color, "Id", "Color1", laptop.ColorId);
            ViewData["CountryId"] = new SelectList(_context.Country, "Id", "Name", laptop.CountryId);
            ViewData["CpuId"] = new SelectList(_context.Processor, "Id", "Name", laptop.CpuId);
            ViewData["ModelId"] = new SelectList(_context.Producer, "Id", "Id", laptop.ModelId);
            return View(laptop);
        }

        // GET: Laptops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptop = await _context.Laptop
                .Include(l => l.Color)
                .Include(l => l.Country)
                .Include(l => l.Cpu)
                .Include(l => l.Model)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (laptop == null)
            {
                return NotFound();
            }

            return View(laptop);
        }

        // POST: Laptops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var LF = _context.LaptopFeature.Where(lf => lf.LaptopId == id).Include(lf => lf.Feature).ToList();
            _context.LaptopFeature.RemoveRange(LF);
            await _context.SaveChangesAsync();
            var laptop = await _context.Laptop.FindAsync(id);
            _context.Laptop.Remove(laptop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LaptopExists(int id)
        {
            return _context.Laptop.Any(e => e.Id == id);
        }
    }
}
