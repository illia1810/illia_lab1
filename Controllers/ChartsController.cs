using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LaptopProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly LaptopBaseContext _context;
        public ChartsController( LaptopBaseContext context)
        {
            _context = context;
        }
        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var colors = _context.Color.Include(c =>c.Laptop).ToList();
            List<object> colorLap = new List<object>();
            colorLap.Add(new[] { "Колір", "Кількість моделей" });
            foreach (var c in colors)
            {
                colorLap.Add(new object[] { c.Color1, c.Laptop.Count() });
            }
            return new JsonResult(colorLap);
        }

        [HttpGet("JsonData1")]
        public JsonResult JsonData1()
        {
            var producers = _context.Producer.Include(c => c.Laptop).ToList();
            List<object> prodLap = new List<object>();
            prodLap.Add(new[] { "Виробник", "Кількість моделей" });
            foreach (var c in producers)
            {
                prodLap.Add(new object[] { c.Model, c.Laptop.Count() });
            }
            return new JsonResult(prodLap);
        }
    }
}