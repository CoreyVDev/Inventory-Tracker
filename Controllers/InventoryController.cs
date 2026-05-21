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
    if (string.IsNullOrWhiteSpace(listName))
    {
        ViewBag.ListId = null;
        ViewBag.ListName = "";
        ViewBag.Lists = ListsController.ItemLists;
        return View();
    }

    // Bulletproof lookup
    var list = ListsController.ItemLists
        .FirstOrDefault(l => l.Name != null &&
                             listName != null &&
                             l.Name.Trim().Equals(listName.Trim(), StringComparison.OrdinalIgnoreCase));

    if (list != null)
        ViewBag.ListId = list.Id;
    else
        Console.WriteLine("LIST NOT FOUND: " + listName);

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

       
