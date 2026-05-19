using System.Collections.Generic;

namespace Inventory_Tracker.Models { 
public class ItemList
{
	public int Id { get; set; }
	public string Name { get; set; }

	public decimal Price { get; set; }
	public List<Item> Items { get; set; } = new List<Item>();
	}
}
