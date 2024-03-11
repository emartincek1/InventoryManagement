using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.OrderManagement
{
    public class OrderItem
    {
        //Properties

        public int Id { get; set; }

        // Reference Product

        public int ProductId { get; set; } 
        public string ProductName { get; set; } = string.Empty;

        // Amount of Product

        public int AmountOrdered { get; set; }

        // Override ToString Method

        public override string ToString()
        {
            return $"Product ID: {ProductId} - Name: {ProductName} - Amount ordered: {AmountOrdered}";
        }

        // Constructor

        public OrderItem(int productId, string productName, int amountOrdered)
        {
            Id = new Random().Next(9999999);
            ProductId = productId ;
            ProductName = productName ;
            AmountOrdered = amountOrdered ;
        }
    }
}
