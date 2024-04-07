namespace QuanLyHSBA.Models
{
    public class PatientImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int PatientId { get; set; }
        public Patient? patient { get; set; }
    }
}
