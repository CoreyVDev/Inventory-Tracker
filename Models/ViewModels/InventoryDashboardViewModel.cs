using Microsoft.AspNetCore.Mvc;

namespace Inventory_Tracker.Models
{
	public class InventoryDashboardViewModel
	{
		public int TotalItems { get; set; }
		public decimal TotalValue { get; set; }
		public int LowStock { get; set; }
	}
}
