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
    }
}
