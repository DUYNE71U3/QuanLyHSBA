using System.ComponentModel.DataAnnotations;

namespace QuanLyHSBA.Models
{
    public class Specialty
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        public List<Doctor>? Doctors { get; set; }
    }
}
