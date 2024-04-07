using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using QuanLyHSBA.Models;
using QuanLyHSBA.Repositories;
using System.Numerics;

namespace QuanLyHSBA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)] 
    public class MedicalRecordController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        public MedicalRecordController(IDoctorRepository doctorRepository, IPatientRepository patientRepository, IMedicalRecordRepository medicalRecordRepository)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _medicalRecordRepository = medicalRecordRepository;
        }

        // Hiển thị danh sách sản phẩm
        public async Task<IActionResult> Index()
        {
            var medicalrecords = await _medicalRecordRepository.GetAllAsync();
            return View(medicalrecords);
        }
        // Hiển thị form thêm sản phẩm mới
        public async Task<IActionResult> Add()
        {
            var patients = await _patientRepository.GetAllAsync();
            var doctors = await _doctorRepository.GetAllAsync();
            ViewBag.Patients = new SelectList(patients, "Id", "Name");
            ViewBag.Doctors = new SelectList(doctors, "Id", "Name");
            return View();
        }

        // Xử lý thêm sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> Add(MedicalRecord medicalRecord)
        {
            if (ModelState.IsValid)
            {            
                await _medicalRecordRepository.AddAsync(medicalRecord);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var patients = await _patientRepository.GetAllAsync();
            var doctors = await _patientRepository.GetAllAsync();
            ViewBag.Patients = new SelectList(patients, "Id", "Name");
            ViewBag.Doctors = new SelectList(doctors, "Id", "Name");
            return View(medicalRecord);
        }

        // Hiển thị form cập nhật sản phẩm
        public async Task<IActionResult> Update(int id)
        {
            var medicalrecord = await _medicalRecordRepository.GetByIdAsync(id);
            if (medicalrecord == null)
            {
                return NotFound();
            }
            var patients = await _patientRepository.GetAllAsync();
            var doctors = await _doctorRepository.GetAllAsync();
            ViewBag.Patients = new SelectList(patients, "Id", "Name", medicalrecord.PatientId);
            ViewBag.Doctors = new SelectList(doctors, "Id", "Name", medicalrecord.DoctorId);
            return View(medicalrecord);
        }

        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, MedicalRecord medicalRecord)
        {
            if (id != medicalRecord.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingMedicalRecord = await _medicalRecordRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync               
                // Cập nhật các thông tin khác của sản phẩm
                existingMedicalRecord.Symptom = medicalRecord.Symptom;
                existingMedicalRecord.AdmissionDate = medicalRecord.AdmissionDate;
                existingMedicalRecord.DischargeDate = medicalRecord.DischargeDate;
                existingMedicalRecord.DoctorNote = medicalRecord.DoctorNote;
                existingMedicalRecord.PatientId = medicalRecord.PatientId;
                existingMedicalRecord.DoctorId = medicalRecord.DoctorId;
                await _medicalRecordRepository.UpdateAsync(existingMedicalRecord);
                return RedirectToAction(nameof(Index));
            }
            var patients = await _patientRepository.GetAllAsync();
            var doctors = await _doctorRepository.GetAllAsync();
            ViewBag.Doctors = new SelectList(doctors, "Id", "Name");
            ViewBag.Patients = new SelectList(patients, "Id", "Name");
            return View(medicalRecord);
        }



        // Hiển thị thông tin chi tiết sản phẩm
        public async Task<IActionResult> Display(int id)
        {
            var medicalrecord = await _medicalRecordRepository.GetByIdAsync(id);
            if (medicalrecord == null)
            {
                return NotFound();
            }
            return View(medicalrecord);
        }
        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var medicalrecord = await _medicalRecordRepository.GetByIdAsync(id);
            if (medicalrecord == null)
            {
                return NotFound();
            }
            return View(medicalrecord);
        }

        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _medicalRecordRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
