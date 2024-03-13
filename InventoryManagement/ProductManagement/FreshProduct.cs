using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.ProductManagement
{
    public class FreshProduct : Product
    {
        public DateTime ExpirationDate { get; set; }
        public string? StorageInstructions { get; set; }

        public FreshProduct(int id, string name, string? description, Price price, UnitType unitType, int maxAmountInStock, DateTime expirationDate, string storageInstructions) : base(id, name, description, price, unitType, maxAmountInStock)
        {
            ExpirationDate = expirationDate;
            StorageInstructions = storageInstructions;
        }

        public override string DisplayDetailsFull(string extraDetails)
        {
            StringBuilder sb = new();

            sb.Append($"{Id} {Name} \n{Description}\n{Price}\n{AmountInStock} item(s) in stock");

            sb.Append(extraDetails);

            if (IsBelowStockThreshold)
            {
                sb.Append("\n!!STOCK LOW!!");
            }

            sb.AppendLine($"Storage instructions: {StorageInstructions}");
            sb.AppendLine($"Exipration date: {ExpirationDate.ToShortDateString()}");

            return sb.ToString();
        }
    }
}
