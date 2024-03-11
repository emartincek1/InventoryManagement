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
    }
}
