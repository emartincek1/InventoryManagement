using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.ProductManagement
{
    public class Product
    {
        // Feilds

        private string name = string.Empty;
        private string? description;

        // Properties

        private int MaxItemsInStock { get; set; }

        public int Id { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                // Slice name if longer than 50 characters

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
                    // Slice description if longer than 250 characters

                    description = value.Length > 250 ? value[..250] : value;
                }
            }
        }

        public UnitType UnitType { get; set; }

        public int AmountInStock { get; private set; }

        public bool IsBelowStockThreshold { get; private set; }

        public Price Price { get; set; }

        // Constructors

        public Product(int id) : this(id, string.Empty)
        {
        }

        public Product(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Product(int id, string name, string? description, Price price, UnitType unitType, int maxAmountInStock)
        {
            Id = id;
            Name = name;
            Description = description;
            UnitType = unitType;
            Price = price;
            MaxItemsInStock = maxAmountInStock;

            UpdateLowStock();
        }

        // Methods

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

        // IncreaseStock Overload

        public void IncreaseStock()
        {
            AmountInStock++;
        }

        public void IncreaseStock(int amount)
        {
            int newStock = AmountInStock + amount;

            if (newStock <= MaxItemsInStock)
            {
                AmountInStock += amount;
            }
            else
            {
                AmountInStock = MaxItemsInStock;
                Console.WriteLine($"{CreateSimpleProductRepresentation()} stock overflow. {newStock - AmountInStock} item(s) ordered that couldn't be stored.");
            }

            UpdateLowStock();
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

        // DisplayDetailsFull Overload

        public string DisplayDetailsFull()
        {
            return DisplayDetailsFull("");
        }

        public string DisplayDetailsFull(string extraDetails)
        {
            StringBuilder sb = new();

            sb.Append($"{Id} {Name} \n{Description}\n{Price}\n{AmountInStock} item(s) in stock");

            sb.Append(extraDetails);

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
            else if (AmountInStock >= 10)
            {
                IsBelowStockThreshold = false;
            }

        }

        private string CreateSimpleProductRepresentation()
        {
            return $"Product {Id} ({Name})";
        }
    }
}
