using Microsoft.AspNetCore.Mvc;
using QuanLyHSBA.Models;
using QuanLyHSBA.Repositories;
using System.Diagnostics;

namespace QuanLyHSBA.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        public HomeController(IMedicineRepository medicineRepository, IMedicalRecordRepository medicalRecordRepository)
        {
            _medicineRepository = medicineRepository;
            _medicalRecordRepository = medicalRecordRepository;
        }

        public async Task<IActionResult> Index()
        {
            var medicines = await _medicineRepository.GetAllAsync();
            return View(medicines);
        }

        public async Task<IActionResult> HSBA()
        {
            var medicalrecords = await _medicalRecordRepository.GetAllAsync();
            return View(medicalrecords);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
