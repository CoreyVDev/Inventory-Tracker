using Microsoft.EntityFrameworkCore;
using Inventory_Tracker.Models;

namespace Inventory_Tracker.Data   // or Inventory_Tracker.Data
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        {
        }

        public DbSet<ItemList> ItemLists { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}


