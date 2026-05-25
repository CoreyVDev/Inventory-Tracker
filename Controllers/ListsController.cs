using Microsoft.AspNetCore.Mvc;
using Inventory_Tracker.Models;
using System.Linq;

namespace Inventory_Tracker.Controllers
{
	public class ListsController : Controller
	{
		public static List<ItemList> ItemLists = new List<ItemList>();
		public IActionResult Index()
		{
			return View(ItemLists);
		}
		
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(ItemList list)
		{
			list.Id = Guid.NewGuid().GetHashCode();
			list.Items = new List<Item>();
			
			ItemLists.Add(list);
			return RedirectToAction("Index");
		}
		public IActionResult ViewList(string name)
		{
			var list = ItemLists.FirstOrDefault(l => l.Name == name);
			return View(list);
		}
		public IActionResult EditItem(int index, string name)
		{
			var list = ItemLists.FirstOrDefault(l => l.Name == name);
			if (list == null) return NotFound();

			if (index < 0 || index>= list.Items.Count)
				return NotFound();
			var item = list.Items[index];

			ViewBag.Index = index;
			ViewBag.ListName = name;
			return View(item);
			
		}
		[HttpPost]
		public IActionResult EditItem(int index, string name, Item updatedItem)
		{
			var list = ItemLists.FirstOrDefault(l => l.Name == name);
			if (list == null) return NotFound();
			list.Items[index] = updatedItem;
			return Redirect($"/Lists/ViewList?name={name}");
		}
		public IActionResult DeleteItem(int index, string name)
		{
			var list = ItemLists.FirstOrDefault(l => l.Name == name);
			if (list == null) return NotFound();

			if (index < 0 || index >= list.Items.Count)
				return NotFound();
			var item = list.Items[index];
			ViewBag.Index = index;
			ViewBag.ListName = name;

			return View(item);
		}
		[HttpPost]
		public IActionResult DeleteItemConfirmed(int index, string name)
		{
			var list = ItemLists.FirstOrDefault(l =>l.Name == name);
			if (list == null) return NotFound();

			list.Items.RemoveAt(index);
			return Redirect($"/Lists/ViewList?name={name}");
		}
		}
	}
