﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
namespace LaptopProject
{
    public partial class Producer
    {
        public Producer()
        {
            Laptop = new HashSet<Laptop>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Потрібно заповнити поле")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Довжина значення від 2 до 50 символів")]
        [Display(Name = "БРЕНД")]
        public string Model { get; set; }

        public virtual ICollection<Laptop> Laptop { get; set; }
    }
}
