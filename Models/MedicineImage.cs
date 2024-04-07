namespace QuanLyHSBA.Models
{
    public class MedicineImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int MedicineId { get; set; }
        public Medicine? Medicine { get; set; }
    }
}
