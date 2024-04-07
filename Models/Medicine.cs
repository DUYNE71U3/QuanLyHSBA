using System.ComponentModel.DataAnnotations;

namespace QuanLyHSBA.Models
{
    public class Medicine
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        public string origin { get; set; }
        public string? ImageUrl { get; set; }
        public List<MedicineImage>? Images { get; set; }
    }
}
