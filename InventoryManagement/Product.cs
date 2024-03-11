using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        public void UseProduct(int items)
        {
            if (items <= amountInStock)
            {
                amountInStock -= items;

                UpdateLowStock();

                Console.WriteLine($"Amount in stock updated. Now {amountInStock} items in stock.");
            }
            else
            {
                Console.WriteLine($"Not enough items in stock for {CreateSimpleProductRepresentation()}. {amountInStock} available but {items} requested.");
            }
        }
            
        public void IncreaseStock()
        {
            amountInStock++;
        }

        private void DecreaseStock(int items, string reason)
        {
            if (items <= amountInStock)
            {
                amountInStock -= items;
            }
            else
            {
                amountInStock = 0;
            }

            UpdateLowStock();

            Console.WriteLine(reason);
        }

        public string DisplayDetailsShort()
        {
            return $"{id} {name} \n{amountInStock} item(s) in stock";
        }

        public string DisplayDetailsFull()
        {
            StringBuilder sb = new();

            sb.Append($"{id} {name} \n{description}\n{amountInStock} item(s) in stock");

            if ( isBelowStockThreshold )
            {
                sb.Append("\n!!StOCK LOW!!");
            }

            return sb.ToString();  
        }

        private void UpdateLowStock()
        {
            if (amountInStock < 10)
            {
                isBelowStockThreshold = true;
            }
        }

        private string CreateSimpleProductRepresentation()
        {
            return $"Product {id} ({name})";
    }
}
