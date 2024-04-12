namespace QuanLyHSBA.Models
{
    public class CartItem
    {
        public int MedicineId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string origin { get; set; }
        public int Quantity { get; set; }
    }
}
