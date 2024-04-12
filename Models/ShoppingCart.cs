namespace QuanLyHSBA.Models
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddItem(CartItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.MedicineId == item.MedicineId); if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }
        public void RemoveItem(int medicineId)
        {
            Items.RemoveAll(i => i.MedicineId == medicineId);
        }
        // Các phương thức khác...
    }
}
