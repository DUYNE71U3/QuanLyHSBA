﻿using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace QuanLyHSBA.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, RegularExpression(@"^\d+$", ErrorMessage = "Phone number must contain only digits.")]
        public string PhoneContact { get; set; }
        public string Info { get; set; }
        public string? ImageUrl { get; set; }
        public List<DoctorImage>? Images { get; set; }
        public int SpecialtyId { get; set; }
        public Specialty? Specialty { get; set; }
    }
}
