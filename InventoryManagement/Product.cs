using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement
{
    public class Product
    {
        private int id;
        private string name = string.Empty;
        private string? description;
        private int maxItemsInStock = 0;
        private UnitType unitType;
        private int amountInStock = 0;
        private bool isBelowStockThreshold = false;

        public void Use()
        {
            amountInStock--;
        }

        public void DisplayDetails()
        {
            Console.WriteLine(this.description);
        }

        public void IncreaseStock()
        {
            amountInStock++;
        }
    }
}
