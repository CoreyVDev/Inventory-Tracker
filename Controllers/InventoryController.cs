using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory_Tracker.Data;
using Inventory_Tracker.Models;

namespace Inventory_Tracker.Controllers
{
    public class InventoryController : Controller
    {
        private readonly InventoryContext _context;

        public InventoryController(InventoryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Lists");
        }

        public async Task<IActionResult> Add(string listName)
        {
            // Try URL first
            if (string.IsNullOrWhiteSpace(listName))
                listName = Request.Query["listName"];

            // Try POST fallback
            if (string.IsNullOrWhiteSpace(listName))
                listName = Request.Form["listName"];

            // Load lists from DB
            var lists = await _context.ItemLists.ToListAsync();
            ViewBag.Lists = lists;

            // If no listName, show dropdown
            if (string.IsNullOrWhiteSpace(listName))
            {
                ViewBag.ListId = null;
                ViewBag.ListName = "";
                return View();
            }

            // Find list by name
            var list = lists.FirstOrDefault(l =>
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

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Item item, int? itemListId, string listName)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Lists = await _context.ItemLists.ToListAsync();
                ViewBag.ListName = listName;
                return View(item);
            }

            // Add by list name
            if (!string.IsNullOrEmpty(listName))
            {
                var list = await _context.ItemLists
                    .Include(l => l.Items)
                    .FirstOrDefaultAsync(l => l.Name == listName);

                if (list == null)
                    return NotFound();

                list.Items.Add(item);
                await _context.SaveChangesAsync();

                return Redirect($"/Lists/ViewList?name={list.Name}");
            }

            // Add by list ID
            if (itemListId.HasValue)
            {
                var list = await _context.ItemLists
                    .Include(l => l.Items)
                    .FirstOrDefaultAsync(l => l.Id == itemListId.Value);

                if (list == null)
                    return NotFound();

                list.Items.Add(item);
                await _context.SaveChangesAsync();

                return Redirect($"/Lists/ViewList?name={list.Name}");
            }

            return RedirectToAction("Add");
        }
    }
}
