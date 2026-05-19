using Microsoft.AspNetCore.Mvc;
using Inventory_Tracker.Models;
using System.Linq;

namespace Inventory_Tracker.Controllers
{
    public class InventoryController : Controller
    {

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Lists");
        }
        

        public IActionResult Add(string listName)
        {
            ViewBag.Lists = ListsController.ItemLists;
            ViewBag.ListName = listName;
            return View();
        }

        [HttpPost]
        public IActionResult Add(Item item, string listName, int? itemListId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Lists = ListsController.ItemLists;
                ViewBag.ListName = listName;
                return View(item);
            }
			item.Id = Guid.NewGuid().GetHashCode();
            if (!string.IsNullOrEmpty(listName))
            {
                var list = ListsController.ItemLists.FirstOrDefault(l => l.Name == listName);
                list.Items.Add(item);

                return Redirect($"/Lists/ViewList?name={list.Name}");
            }
            if (itemListId.HasValue)
            {
                var list = ListsController.ItemLists.FirstOrDefault(l => l.Id == itemListId.Value);
                list.Items.Add(item);
               
                return Redirect($"/Lists/ViewList?name={list.Name}");
            }
            return RedirectToAction("Add");
        }
        public IActionResult Dashboard()
        {
            var items = ListsController.ItemLists
                .SelectMany(l => l.Items)
                .ToList();

            var model = new InventoryDashboardViewModel
            {
                TotalItems = items.Count
        };
        return View(model);
        }

		
	}
}

       