﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Park_WebApi_Udemy.Models.Trail;

namespace Park_WebApi_Udemy.Models.Dtos
{
    public class TrailCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Distance { get; set; }

        public DifficultyType Difficulty { get; set; }

        [Required]
        public int NationalParkId { get; set; }

        [Required]
        public double Elevation { get; set; }
    }
}
