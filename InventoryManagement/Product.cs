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


        public int Id
        {
            get { return id; }
            set
            {
                id = value;
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value.Length > 50 ? value[..50] : value;
            }
        }

        public string? Description
        {
            get { return description; }
            set
            {
                if (value == null)
                {
                    description = string.Empty;
                }
                else
                {
                    description = value.Length > 250 ? value[..250] : value;
                }
            }
        }

        public UnitType UnitType { get; set; }

        public int AmountInStock { get; private set; }

        public bool IsBelowStockThreshold { get; private set; }

        public Product()
        {

        }

        public void UseProduct(int items)
        {
            if (items <= AmountInStock)
            {
                AmountInStock -= items;

                UpdateLowStock();

                Console.WriteLine($"Amount in stock updated. Now {AmountInStock} items in stock.");
            }
            else
            {
                Console.WriteLine($"Not enough items in stock for {CreateSimpleProductRepresentation()}. {AmountInStock} available but {items} requested.");
            }
        }

        public void IncreaseStock()
        {
            AmountInStock++;
        }

        private void DecreaseStock(int items, string reason)
        {
            if (items <= AmountInStock)
            {
                AmountInStock -= items;
            }
            else
            {
                AmountInStock = 0;
            }

            UpdateLowStock();

            Console.WriteLine(reason);
        }

        public string DisplayDetailsShort()
        {
            return $"{Id} {Name} \n{AmountInStock} item(s) in stock";
        }

        public string DisplayDetailsFull()
        {
            StringBuilder sb = new();

            sb.Append($"{Id} {Name} \n{Description}\n{AmountInStock} item(s) in stock");

            if (IsBelowStockThreshold)
            {
                sb.Append("\n!!StOCK LOW!!");
            }

            return sb.ToString();
        }

        private void UpdateLowStock()
        {
            if (AmountInStock < 10)
            {
                IsBelowStockThreshold = true;
            }
        }

        private string CreateSimpleProductRepresentation()
        {
            return $"Product {Id} ({Name})";
        }
    }
}
