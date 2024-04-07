using System.ComponentModel.DataAnnotations;

namespace QuanLyHSBA.Models
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        //trieu chung
        [Required]
        public string Symptom { get; set; }
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        //ngay nhap vien
        public DateTime AdmissionDate { get; set; }
        //ngay xuat vien
        public DateTime DischargeDate { get; set; }
        //ghi chu cua bac si
        public string DoctorNote { get; set; }
    }
}
