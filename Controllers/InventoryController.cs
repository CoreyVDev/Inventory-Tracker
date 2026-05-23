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
		listName = Request.Form["listName"];
	}
    if (string.IsNullOrWhiteSpace(listName))
    {
        ViewBag.ListId = null;
        ViewBag.ListName = "";
        ViewBag.Lists = ListsController.ItemLists;
        return View();
    }

    // Bulletproof lookup
   var list = ListsController.ItemLists
    .FirstOrDefault(l =>
        string.Equals(
            l.Name?.Trim(),
            listName?.Trim(),
            StringComparison.OrdinalIgnoreCase
        )
    );


    if (list != null)
	{
        ViewBag.ListId = list.Id;
		ViewBag.ListName = list.Name;
		}
    else
	{
        Console.WriteLine("LIST NOT FOUND: " + listName);
		ViewBag.ListId = null;
    	ViewBag.ListName = listName;
		}
		
    ViewBag.Lists = ListsController.ItemLists;

    return View();
}


        [HttpPost]
public IActionResult Add(Item item, int? itemListId, string listName)
{
    // If validation fails, we must repopulate ViewBag so the page doesn't break
    if (!ModelState.IsValid)
    {
        ViewBag.ListName = listName;
        ViewBag.Lists = ListsController.ItemLists;
        return View(item);
    }

    item.Id = Guid.NewGuid().GetHashCode();

    if (itemListId.HasValue)
    {
        var list = ListsController.ItemLists.FirstOrDefault(l => l.Id == itemListId.Value);
        if (list != null)
        {
            list.Items.Add(item);
            return Redirect($"/Lists/ViewList?name={list.Name}");
        }
    }

    // If itemListId is missing, show error and reload page correctly
    ViewBag.ListName = listName;
    ViewBag.Lists = ListsController.ItemLists;
    ModelState.AddModelError("", "You must select a list.");
    return View(item);
}


		
	}
}

       
