using System.ComponentModel.DataAnnotations;

namespace QuanLyHSBA.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        public string origin { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public List<MedicineImage>? Images { get; set; }
    }
}
