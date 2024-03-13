using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.ProductManagement
{
    public class BoxedProduct : Product
    {
        public int AmountPerBox { get; set; }

        public BoxedProduct(int id, string name, string? description, Price price, int maxAmountInStock, int amountPerBox) : base(id, name, description, price, UnitType.PerBox, maxAmountInStock)
        {
            AmountPerBox = amountPerBox;
        }

        public string DisplayBoxedProductDetails()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Boxed Product\n");

            sb.Append($"{Id} {Name} \n{Description}\n{Price}\n{AmountInStock} item(s) in stock");

            if (IsBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");
            }

            return sb.ToString();
        }

        public void UseBoxedProduct(int items)
        {
            int smallestMultiple = 0;
            int batchSize;

            while(true)
            {
                smallestMultiple++;
                if (smallestMultiple * AmountPerBox > items)
                {
                    batchSize = smallestMultiple * AmountPerBox;
                    break;
                }
            }

            UseProduct(batchSize);
        }
    }
}
