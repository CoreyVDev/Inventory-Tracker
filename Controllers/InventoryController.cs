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
    // Try URL first
    if (string.IsNullOrWhiteSpace(listName))
        listName = Request.Query["listName"];

    // Try POST fallback
    if (string.IsNullOrWhiteSpace(listName))
        listName = Request.Form["listName"];

    // If still empty, show dropdown
    if (string.IsNullOrWhiteSpace(listName))
    {
        ViewBag.ListId = null;
        ViewBag.ListName = "";
        ViewBag.Lists = ListsController.ItemLists;
        return View();
    }

    // Find list
    var list = ListsController.ItemLists
        .FirstOrDefault(l =>
            string.Equals(l.Name?.Trim(), listName.Trim(), StringComparison.OrdinalIgnoreCase));

    if (list != null)
    {
        ViewBag.ListId = list.Id;
        ViewBag.ListName = list.Name;
    }
    else
    {
        ViewBag.ListId = null;
        ViewBag.ListName = listName;
    }

    ViewBag.Lists = ListsController.ItemLists;
    return View();
}



		
	}
}

       
