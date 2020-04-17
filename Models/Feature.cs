using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
namespace LaptopProject
{
    public partial class Feature
    {
        public Feature()
        {
            LaptopFeature = new HashSet<LaptopFeature>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Потрібно заповнити поле")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Довжина значення від 2 до 50 символів")]
        [Display(Name = "ОСОБЛИВОСТІ НОУТБУКУ")]
        public string Feature1 { get; set; }

        public virtual ICollection<LaptopFeature> LaptopFeature { get; set; }
    }
}
