using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyHSBA.Models;
using QuanLyHSBA.Repositories;

namespace QuanLyHSBA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class SpecialtyController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly ISpecialtyRepository _specialtyRepository;

        public SpecialtyController(IDoctorRepository doctorRepository, ISpecialtyRepository specialtyRepository)
        {
            _doctorRepository = doctorRepository;
            _specialtyRepository = specialtyRepository;
        }

        // Hiển thị danh sách sản phẩm
        public async Task<IActionResult> Index()
        {
            var specialtys = await _specialtyRepository.GetAllAsync();
            return View(specialtys);
        }
        // Hiển thị form thêm sản phẩm mới
        public async Task<IActionResult> Add()
        {
            return View();
        }

        // Xử lý thêm sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> Add(Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                await _specialtyRepository.AddAsync(specialty);
                return RedirectToAction(nameof(Index));
            }
            return View(specialty);
        }

        // Hiển thị thông tin chi tiết sản phẩm
        public async Task<IActionResult> Display(int id)
        {
            var specialty = await _specialtyRepository.GetByIdAsync(id);
            if (specialty == null)
            {
                return NotFound();
            }
            return View(specialty);
        }

        // Hiển thị form cập nhật sản phẩm
        public async Task<IActionResult> Update(int id)
        {
            var doctor = await _specialtyRepository.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, Specialty specialty)
        {
            if (id != specialty.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _specialtyRepository.UpdateAsync(specialty);
                return RedirectToAction(nameof(Index));
            }
            return View(specialty);
        }
        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var specialty = await _specialtyRepository.GetByIdAsync(id);
            if (specialty == null)
            {
                return NotFound();
            }
            return View(specialty);
        }

        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialty = await _specialtyRepository.GetByIdAsync(id);
            if (specialty != null)
            {
                await _specialtyRepository.DeleteAsync(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
