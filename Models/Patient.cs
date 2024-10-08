﻿using System.ComponentModel.DataAnnotations;

namespace QuanLyHSBA.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Age must be a non-negative number.")]
        public int Age { get; set; }
        public string PhoneContact { get; set; }
        public string? ImageUrl { get; set; }
        public List<PatientImage>? Images { get; set; }
    }
}
