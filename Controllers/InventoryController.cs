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
			var list = ListsController.ItemLists.FirstOrDefault(l => l.Name.Equals(listName, StringComparison.OrdinalIgnoreCase));

			if (list != null)
				ViewBag.ListId = list.Id;
            
            ViewBag.ListName = listName;
			ViewBag.Lists = ListsController.ItemLists;
            return View();
        }

        [HttpPost]
        public IActionResult Add(Item item, int? itemListId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Lists = ListsController.ItemLists;
                return View(item);
            }
			item.Id = Guid.NewGuid().GetHashCode();
			
            if (itemListId.HasValue)
            {
                var list = ListsController.ItemLists.FirstOrDefault(l => l.Id == itemListId.Value);
                list.Items.Add(item);

                return Redirect($"/Lists/ViewList?name={list.Name}");
            }

			ViewBag.Lists = ListsController.ItemLists;
			ModelState.AddModelError("", "You must select a list.");
			return View(item);
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

       
