using System.ComponentModel.DataAnnotations;

namespace QuanLyHSBA.Models
{
    public class CartItem
    {
        public int MedicineId { get; set; }
        public string Name { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
        public decimal Price { get; set; }
        public string origin { get; set; }
        public int Quantity { get; set; }
    }
}
