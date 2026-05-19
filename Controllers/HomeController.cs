using Inventory_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Inventory_Tracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var items = ListsController.ItemLists
                .SelectMany(l => l.Items)
                .ToList();
            var model = new InventoryDashboardViewModel
            {
                TotalItems = items.Count,
                TotalValue = items.Sum(i => i.Price * i.Quantity),
                LowStock = items.Count(i => i.Quantity < 2)
            };
            return View(model);
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