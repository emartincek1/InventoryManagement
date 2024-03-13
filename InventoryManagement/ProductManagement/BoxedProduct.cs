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

        public override string DisplayDetailsFull(string extraDetails)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Boxed Product\n");

            sb.Append($"{Id} {Name} \n{Description}\n{Price}\n{AmountInStock} box(s) in stock. {AmountPerBox} item(s) per box.");

            sb.Append(extraDetails);

            if (IsBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");
            }

            return sb.ToString();
        }

        public override void UseProduct(int items) 
        {
            int smallestMultiple = 0;
            int batchSize;

            while(true)
            {
                smallestMultiple++;
                if (smallestMultiple * AmountPerBox > items)
                {
                    batchSize = smallestMultiple;
                    break;
                }
            }

            base.UseProduct(batchSize);
        }
    }
}
