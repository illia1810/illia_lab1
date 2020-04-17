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
    public class ProcessorsController : Controller
    {
        private readonly LaptopBaseContext _context;

        public ProcessorsController(LaptopBaseContext context)
        {
            _context = context;
        }

        // GET: Processors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Processor.ToListAsync());
        }

        // GET: Processors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processor = await _context.Processor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (processor == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Laptops", new { cpu_id = processor.Id});
        }

        // GET: Processors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Processors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Processor processor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(processor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(processor);
        }

        // GET: Processors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processor = await _context.Processor.FindAsync(id);
            if (processor == null)
            {
                return NotFound();
            }
            return View(processor);
        }

        // POST: Processors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Processor processor)
        {
            if (id != processor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(processor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcessorExists(processor.Id))
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
            return View(processor);
        }

        // GET: Processors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var processor = await _context.Processor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (processor == null)
            {
                return NotFound();
            }

            return View(processor);
        }

        // POST: Processors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var processor = await _context.Processor.FindAsync(id);
            _context.Processor.Remove(processor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcessorExists(int id)
        {
            return _context.Processor.Any(e => e.Id == id);
        }
    }
}
