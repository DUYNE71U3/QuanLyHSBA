namespace QuanLyHSBA.Models
{
    public class DoctorImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
