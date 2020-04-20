using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LaptopProject;
using ClosedXML.Excel;
using System.IO;

namespace LaptopProject.Controllers
{
    public class ProducersController : Controller
    {
        private readonly LaptopBaseContext _context;

        public ProducersController(LaptopBaseContext context)
        {
            _context = context;
        }

        // GET: Producers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Producer.ToListAsync());
        }

        // GET: Producers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = await _context.Producer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producer == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Laptops", new { pr_id = producer.Id });
        }

        // GET: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }
        public ActionResult Export(int? id)
        {
            using XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled);
            string producer = _context.Producer.FirstOrDefault(m => m.Id == id).Model;
            var laptops = _context.Laptop.Where(f => f.ModelId == id).Include(f => f.Color).Include(f => f.Cpu).Include(f => f.Country).ToList();
            var worksheet = workbook.Worksheets.Add(producer);
            foreach (var g in laptops)
            {
                worksheet.Cell("A1").Value = "Модель ноутбуку";
                worksheet.Cell("B1").Value = "Колір";
                worksheet.Cell("C1").Value = "Процессор";
                worksheet.Cell("D1").Value = "Країна";
                worksheet.Row(1).Style.Font.Bold = true;
                for (int i = 0; i < laptops.Count; i++)
                {
                    worksheet.Cell(i + 2, 1).Value = laptops[i].Name;
                    worksheet.Cell(i + 2, 2).Value = laptops[i].Color.Color1;
                    worksheet.Cell(i + 2, 3).Value = laptops[i].CpuId;
                    worksheet.Cell(i + 2, 4).Value = laptops[i].Country.Name;
                }
            }
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Flush();

            return new FileContentResult(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = $"Lab1_{DateTime.UtcNow.ToShortDateString()}.xlsx"
            };
        }
        // GET: Producers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = await _context.Producer.FindAsync(id);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model")] Producer producer)
        {
            if (id != producer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducerExists(producer.Id))
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
            return View(producer);
        }

        // GET: Producers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = await _context.Producer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lap = _context.Laptop.Where(l => l.ModelId == id).Include(l => l.Country).Include(l => l.Color).Include(l => l.Model).ToList();
            foreach (var l in lap)
            {
                var lapF = _context.LaptopFeature.Where(f => f.LaptopId == l.Id).Include(f => f.Feature).ToList();
                _context.LaptopFeature.RemoveRange(lapF);
            }
            _context.Laptop.RemoveRange(lap);
            var producer = await _context.Producer.FindAsync(id);
            _context.Producer.Remove(producer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProducerExists(int id)
        {
            return _context.Producer.Any(e => e.Id == id);
        }
    }
}
