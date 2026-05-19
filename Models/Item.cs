using System.ComponentModel.DataAnnotations;

namespace Inventory_Tracker.Models

{
	public class Item
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; }
		[Range(0, double.MaxValue, ErrorMessage = "Price cannot be negative.")]
		public decimal Price { get; set; }
		[Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
		public int Quantity { get; set; }

		public int ItemListId { get; set; }
		public ItemList ItemList { get; set; }

		public string Tags { get; set; }
	}
}
