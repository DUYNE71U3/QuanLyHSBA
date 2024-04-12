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

        public HomeController(IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
        }

        public async Task<IActionResult> Index()
        {
            var medicines = await _medicineRepository.GetAllAsync();
            return View(medicines);
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
