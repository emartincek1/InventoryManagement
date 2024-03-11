using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.OrderManagement
{
    public class Order
    {
        // Properties

        public int Id { get; private set; }
        public DateTime OrderFulfilmentDate { get; private set; }
        public List<OrderItem> OrderItems { get; }

        public bool Fulfilled { get; set; } = false;

        // Constructor

        public Order()
        {
            //Random Id

            Id = new Random().Next(9999999);

            // Have order fill after random number of seconds

            int numberofSeconds = new Random().Next(100);
            OrderFulfilmentDate = DateTime.Now.AddSeconds(numberofSeconds);

            // Initalize OrderItems list

            OrderItems = new List<OrderItem>();
        }

        // Methods

        public string ShowOrderDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Order ID: {Id}");
            sb.AppendLine($"Order fulfilment date: {OrderFulfilmentDate.ToShortTimeString()}");
            
            // For each OrderItem in OrderItem list add its details to Order details

            if (OrderItems != null)
            {
                foreach ( OrderItem item in OrderItems )
                {
                    sb.AppendLine($"{item.ProductId}. {item.ProductName}: {item.AmountOrdered}");  
                }
            }

            return sb.ToString();
        }
    }
}
