using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using QuanLyHSBA.Extensions;
using QuanLyHSBA.Models;
using QuanLyHSBA.Repositories;

namespace QuanLyHSBA.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IMedicineRepository _medicineRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ShoppingCartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IMedicineRepository medicineRepository)
        {
            _medicineRepository = medicineRepository;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart == null || !cart.Items.Any())
            {
                // Xử lý giỏ hàng trống...
                return RedirectToAction("Index"); 
            }

            var user = await _userManager.GetUserAsync(User);
            order.UserId = user.Id;
            order.OrderDate = DateTime.UtcNow;
            order.TotalPrice = cart.Items.Sum(i => i.Price * i.Quantity); order.OrderDetails = cart.Items.Select(i => new OrderDetail
            {
                MedicineId = i.MedicineId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList();

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove("Cart");

            return View("OrderCompleted", order.Id);
        }
        public async Task<IActionResult> AddToCart(int medicineId, int quantity)
        {
            // Giả sử bạn có phương thức lấy thông tin sản phẩm từ productId
            var medicine = await GetProductFromDatabase(medicineId);

            var cartItem = new CartItem
            {
                MedicineId = medicineId,
                Name = medicine.Name,
                origin = medicine.origin,
                Price = medicine.Price,
                Quantity = quantity
            };
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();

            cart.AddItem(cartItem);

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            return View(cart);
        }
        // Các actions khác...
        private async Task<Medicine> GetProductFromDatabase(int medicineId)
        {
            // Truy vấn cơ sở dữ liệu để lấy thông tin sản phẩm 
            var medicine = await _medicineRepository.GetByIdAsync(medicineId);
            return medicine;
        }

        public IActionResult RemoveFromCart(int medicineId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");

            if (cart is not null)
            {
                cart.RemoveItem(medicineId);
                // Lưu lại giỏ hàng vào Session sau khi đã xóa mục
                HttpContext.Session.SetObjectAsJson("Cart", cart); 
            }
            return RedirectToAction("Index");
        }


        //plus function
        public IActionResult Plus(int medicineId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            var cartItem = cart.Items.FirstOrDefault(item => item.MedicineId == medicineId);

            if (cartItem != null)
            {
                cartItem.Quantity++; // Increment quantity
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }
        //minus function
        public IActionResult Minus(int medicineId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            var cartItem = cart.Items.FirstOrDefault(item => item.MedicineId == medicineId);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--; // Decrement quantity if greater than 1
                }
                else
                {
                    // Optionally remove the item from cart if quantity reaches 0
                    cart.RemoveItem(medicineId);
                }
            }

            HttpContext.Session.SetObjectAsJson("Cart", cart);

            return RedirectToAction("Index");
        }
    }
}
