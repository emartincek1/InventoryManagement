using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.ProductManagement
{
    public class Price
    {
        // Properties

        public double ItemPrice { get; set; }
        public Currency Currency { get; set; }

        // Override ToString Method

        public override string ToString()
        {
            return $"{ItemPrice} {Currency}";
        }

        // Constructor

        public Price(double price, Currency currency)
        {
            ItemPrice = price;
            Currency = currency;
        }
    }
}