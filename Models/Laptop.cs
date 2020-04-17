using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
namespace LaptopProject
{
    public partial class Laptop
    {
        public Laptop()
        {
            LaptopFeature = new HashSet<LaptopFeature>();
        }

        public int Id { get; set; }
        [Display(Name = "НАЗВА БРЕНДУ")]
        public int ModelId { get; set; }
        [Display(Name = "ПРОЦЕССОР")]
        public int CpuId { get; set; }
        [Display(Name = "КОЛІР НОУТБУКУ")]
        public int ColorId { get; set; }
        [Required(ErrorMessage = "Потрібно заповнити поле")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Довжина значення від 2 до 50 символів")]
        [Display(Name = "НАЗВА МОДЕЛІ")]
        public string Name { get; set; }

        [Display(Name = "КРАЇНА-ВИРОБНИК")]
        public int CountryId { get; set; }

        [Display(Name = "КОЛІР НОУТБУКУ")]

        public virtual Color Color { get; set; }
        [Display(Name = "КРАЇНА-ВИРОБНИК")]
        public virtual Country Country { get; set; }
        [Display(Name = "ПРОЦЕССОР")]
        public virtual Processor Cpu { get; set; }
        [Display(Name = "НАЗВА БРЕНДУ")]
        public virtual Producer Model { get; set; }
        public virtual ICollection<LaptopFeature> LaptopFeature { get; set; }
    }
}
