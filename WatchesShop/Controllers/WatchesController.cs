using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using WatchesShop.Data;
using WatchesShop.Models;
using WatchesShop.Services;
using WatchesShop.Utilities;
using System.Linq;


namespace WatchesShop.Controllers
{
    public class WatchesController : Controller
    {
        private readonly WatchContext _context;
        private readonly IWatchService _watchService;

        public WatchesController(WatchContext context, IWatchService watchService)
        {
            _context = context;
            _watchService = watchService;
        }


        public IActionResult WatchCatalog(int page = 1, int pageSize = 12)
        {
            var watches = _watchService.GetWatches(); // Получаем список часов
            var count = watches.Count(); // Получаем количество часов
            var paginatedWatches = watches.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var model = new PaginatedList<Watch>(paginatedWatches, count, page, pageSize);
            return View(model); // Возвращаем весь объект PaginatedList
        }

        public IActionResult Details(int id)
        {
            var watch = _watchService.GetWatchById(id);
            if (watch == null)
            {
                return NotFound();
            }
            return View(watch);
        }

        public IActionResult Index(int page = 1, int pageSize = 12, int? brandId = null, int? genderId = null, int? styleId = null, int? colorId = null, int? materialId = null, int? mechanismId = null, int? minPrice = null, int? maxPrice = null)
        {
            var watches = _watchService.GetWatches();

            // Фильтрация по бренду
            if (brandId != null)
            {
                watches = watches.Where(w => w.BrandId == brandId);
            }

            // Фильтрация по полу
            if (genderId != null)
            {
                watches = watches.Where(w => w.GenderId == genderId);
            }

            // Фильтрация по стилю
            if (styleId != null)
            {
                watches = watches.Where(w => w.StyleId == styleId);
            }

            // Фильтрация по цвету
            if (colorId != null)
            {
                watches = watches.Where(w => w.Case.ColorId == colorId);
            }

            // Фильтрация по материалу
            if (materialId != null)
            {
                watches = watches.Where(w => w.Case.MaterialId == materialId);
            }

            // Фильтрация по механизму
            if (mechanismId != null)
            {
                watches = watches.Where(w => w.MechanismTypeId == mechanismId);
            }

            if (Request.Query.ContainsKey("minPrice") && int.TryParse(Request.Query["minPrice"], out int minPriceValue))
            {
                minPrice = minPriceValue;
            }

            if (Request.Query.ContainsKey("maxPrice") && int.TryParse(Request.Query["maxPrice"], out int maxPriceValue))
            {
                maxPrice = maxPriceValue;
            }

            // Фильтрация по цене
            if (minPrice != null)
            {
                watches = watches.Where(w => w.Price >= minPrice);
            }

            if (maxPrice != null)
            {
                watches = watches.Where(w => w.Price <= maxPrice);
            }

            var count = watches.Count();
            var paginatedWatches = watches.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var model = new PaginatedList<Watch>(paginatedWatches, count, page, pageSize);

            // Передача дополнительной информации о доступных фильтрах во ViewBag
            ViewBag.Brands = _watchService.GetBrands();
            ViewBag.Genders = _watchService.GetGenders();
            ViewBag.Styles = _watchService.GetStyles();
            ViewBag.Colors = _watchService.GetColors();
            ViewBag.Materials = _watchService.GetMaterials();
            ViewBag.Mechanisms = _watchService.GetMechanisms();

            return View(model);
        }
    }
}
