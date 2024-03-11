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
    }
}
