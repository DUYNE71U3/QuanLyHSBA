using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyHSBA.Models;
using QuanLyHSBA.Repositories;

namespace QuanLyHSBA.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly ISpecialtyRepository _specialtyRepository;

        public DoctorController(IDoctorRepository doctorRepository, ISpecialtyRepository specialtyRepository)
        {
            _doctorRepository = doctorRepository;
            _specialtyRepository = specialtyRepository;
        }

        // Hiển thị danh sách sản phẩm
        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorRepository.GetAllAsync();
            return View(doctors);
        }
        // Hiển thị form thêm sản phẩm mới
        public async Task<IActionResult> Add()
        {
            var specialtys = await _specialtyRepository.GetAllAsync();
            ViewBag.Specialtys = new SelectList(specialtys, "Id", "Name");
            return View();
        }

        // Xử lý thêm sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> Add(Doctor doctor, IFormFile imageUrl)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                {
                    // Lưu hình ảnh đại diện tham khảo bài 02 hàm SaveImage
                    doctor.ImageUrl = await SaveImage(imageUrl);
                }

                await _doctorRepository.AddAsync(doctor);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var specialtys = await _specialtyRepository.GetAllAsync();
            ViewBag.Specialtys = new SelectList(specialtys, "Id", "Name");
            return View(doctor);
        }

        private async Task<string> SaveImage(IFormFile imageUrl)
        {
            var savePath = Path.Combine("wwwroot/images", imageUrl.FileName); // Thay đổi đường dẫn theo cấu hình của bạn
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await imageUrl.CopyToAsync(fileStream);
            }
            return "/images/" + imageUrl.FileName; // Trả về đường dẫn tương đối

        }

        // Hiển thị form cập nhật sản phẩm
        public async Task<IActionResult> Update(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            var specialtys = await _specialtyRepository.GetAllAsync();
            ViewBag.Specialtys = new SelectList(specialtys, "Id", "Name", doctor.SpecialtyId);
            return View(doctor);
        }

        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, Doctor doctor, IFormFile imageUrl)
        {
            ModelState.Remove("ImageUrl"); // Loại bỏ xác thực ModelState cho ImageUrl
            if (id != doctor.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingDoctor = await _doctorRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync
                if (imageUrl == null)
                {
                    doctor.ImageUrl = existingDoctor.ImageUrl;
                }
                else
                {
                    // Lưu hình ảnh mới
                    doctor.ImageUrl = await SaveImage(imageUrl);
                }
                // Cập nhật các thông tin khác của sản phẩm
                existingDoctor.Name = doctor.Name;
                existingDoctor.PhoneContact = doctor.PhoneContact;
                existingDoctor.Info = doctor.Info;
                existingDoctor.SpecialtyId = doctor.SpecialtyId;
                existingDoctor.ImageUrl = doctor.ImageUrl;
                await _doctorRepository.UpdateAsync(existingDoctor);
                return RedirectToAction(nameof(Index));
            }
            var specialtys = await _specialtyRepository.GetAllAsync();
            ViewBag.Specialtys = new SelectList(specialtys, "Id", "Name");
            return View(doctor);
        }



        // Hiển thị thông tin chi tiết sản phẩm
        public async Task<IActionResult> Display(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }
        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _doctorRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
