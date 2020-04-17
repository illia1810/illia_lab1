using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace LaptopProject
{
    public partial class LaptopFeature
    {
        public int Id { get; set; }
        [Display(Name = "ХАРАКТЕРИСТИКА")]
        public int FeatureId { get; set; }
        [Display(Name = "НАЗВА МОДЕЛІ")]
        public int LaptopId { get; set; }
        [Display(Name = "ХАРАКТЕРИСТИКА")]
        public virtual Feature Feature { get; set; }
        [Display(Name = "НАЗВА МОДЕЛІ")]
        public virtual Laptop Laptop { get; set; }
    }
}
