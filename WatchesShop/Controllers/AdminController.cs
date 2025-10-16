using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WatchesShop.Models;
using WatchesShop.Services;

namespace WatchesShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly AuthenticationService _authService;
        private readonly IWatchService _watchService;
        private readonly IOrderService _orderService;

        public AdminController(AuthenticationService authService, IWatchService watchService, IOrderService orderService)
        {
            _authService = authService;
            _watchService = watchService;
            _orderService = orderService;
        }


        // Метод для отображения страницы входа
        public IActionResult Login()
        {
            return View();
        }

        // Метод для обработки входа
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (_authService.Authenticate(username, password))
            {
                return RedirectToAction("AddWatch");
            }
            else
            {
                // Если аутентификация не удалась, вернуть страницу входа с сообщением об ошибке
                ViewBag.Error = "Invalid username or password";
                return View();
            }
        }

        // Метод для отображения страницы добавления часов
        public IActionResult AddWatch()
        {
            ViewBag.Genders = _watchService.GetGenders();
            ViewBag.Brands = _watchService.GetBrands();
            ViewBag.Styles = _watchService.GetStyles();
            ViewBag.Mechanisms = _watchService.GetMechanisms();
            ViewBag.Materials = _watchService.GetMaterials();
            ViewBag.Colors = _watchService.GetColors();
            return View();
        }
        [HttpPost]
        public IActionResult AddWatch(Watch watch, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    Directory.CreateDirectory(uploadsFolder); // если папки нет
                    var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fs);
                    }

                    watch.ImagePath = "/images/" + fileName; // путь для отображения в <img src="...">
                }
                else
                {
                    ModelState.AddModelError("ImageData", "Image is required.");
                    PopulateViewData();
                    return View(watch);
                }

                var newCase = new Case
                {
                    Diameter = watch.Case.Diameter,
                    MaterialId = watch.Case.MaterialId,
                    WaterResistance = watch.Case.WaterResistance,
                    LengthCase = watch.Case.LengthCase,
                    Thickness = watch.Case.Thickness,
                    Weight = watch.Case.Weight,
                    ColorId = watch.Case.ColorId
                };

                _watchService.AddCase(newCase);

                watch.CaseId = newCase.CaseId;
                watch.Case = newCase;

                _watchService.AddWatch(watch);
                return RedirectToAction("Index", "Watches");
            }

            PopulateViewData();
            return View(watch);
        }

        private void PopulateViewData()
        {
            ViewBag.Genders = _watchService.GetGenders();
            ViewBag.Brands = _watchService.GetBrands();
            ViewBag.Styles = _watchService.GetStyles();
            ViewBag.Mechanisms = _watchService.GetMechanisms();
            ViewBag.Materials = _watchService.GetMaterials();
            ViewBag.Colors = _watchService.GetColors();
        }


        public IActionResult Orders()
        {
            var orders = _orderService.GetAllOrders();
            return View(orders);
        }

        public IActionResult EditWatch()
        {
            ViewBag.Watches = _watchService.GetWatches();
            return View();
        }

        [HttpPost]
        public IActionResult EditWatch(int watchId)
        {
            var watch = _watchService.GetWatchById(watchId);
            if (watch == null)
            {
                return NotFound();
            }
            PopulateViewData();
            return View("EditWatchDetails", watch);
        }

        [HttpPost]
        public IActionResult EditWatchDetails(Watch watch, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    Directory.CreateDirectory(uploadsFolder); // если папки нет
                    var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var fs = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fs);
                    }

                    watch.ImagePath = "/images/" + fileName; // путь для отображения в <img src="...">
                }
                _watchService.UpdateWatch(watch);
                return RedirectToAction("Index", "Watches");
            }
            PopulateViewData();
            return View(watch);
        }

        [HttpPost]
        public IActionResult DeleteWatch(int id)
        {
            _watchService.DeleteWatch(id);
            return RedirectToAction("Index", "Watches");
        }
    }
}
