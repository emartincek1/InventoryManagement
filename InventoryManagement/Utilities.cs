﻿using InventoryManagement.OrderManagement;
using InventoryManagement.ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace InventoryManagement
{
    internal class Utilities
    {
        private static List<Product> inventory = new();
        private static List<Order> orders = new();

        // Mock data implementation
        internal static void InitalizeStock()
        {
            Product p1 = new Product(1, "Sugar", "Lorem ipsum", new Price(10, Currency.Euro), UnitType.PerKg, 100);
            Product p2 = new Product(2, "Cake decorations", "Lorem ipsum", new Price(8, Currency.Euro), UnitType.PerItem, 20);
            Product p3 = new Product(3, "Strawberry", "Lorem ipsum", new Price(3, Currency.Euro), UnitType.PerBox, 10);
            inventory.Add(p1);
            inventory.Add(p2);
            inventory.Add(p3);
        } 

        internal static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("********************");
            Console.WriteLine("* Select an action *");
            Console.WriteLine("********************");

            Console.WriteLine("1: Inventory management");
            Console.WriteLine("2: Order management");
            Console.WriteLine("3: Settings");
            Console.WriteLine("0: Close application");

            Console.Write("Your Selection: ");

            string? userSelection = Console.ReadLine();
            switch (userSelection)
            {
                case "1":
                    ShowInventoryManagementMenu();
                    break;
                case "2":
                    ShowOrderManagementMenu();
                    break;
                case "3":
                    ShowSettingsMenu();
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again");
                    break;

            }
        }

        private static void ShowInventoryManagementMenu()
        {
            string? userSelection = string.Empty;

            do
            {
                Console.Clear();
                Console.WriteLine("************************");
                Console.WriteLine("* Inventory management *");
                Console.WriteLine("************************");

                ShowAllProductsOverview();

                Console.WriteLine("What do you want to do");

                Console.WriteLine("1: View details of product");
                Console.WriteLine("2: Add new product");
                Console.WriteLine("4: View products with low stock");
                Console.WriteLine("0: Back to main menu");

                Console.Write("Your Selection: ");

                userSelection = Console.ReadLine();

                switch (userSelection)
                {
                    case "1":
                        ShowDetailsAndUseProduct();
                        break;
                    case "2":
                        ShowCreateNewProduct();
                        break;
                    case "3":
                        ShowProductsLowOnStock();
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again");
                        break;

                }
            } 
            while (userSelection != "0");
            ShowMainMenu();
        }

        private static void ShowAllProductsOverview()
        {
            foreach(Product product in inventory)
            {
                Console.WriteLine(product.DisplayDetailsShort());
                Console.WriteLine();
            }
        }

        private static void ShowDetailsAndUseProduct()
        {
            string? userSelection = string.Empty;

            Console.Write("Enter the ID of product: ");
            string? selectedProductId = Console.ReadLine();

            if (selectedProductId != null)
            {
                Product? selectedProduct = inventory.Where(p => p.Id == int.Parse(selectedProductId)).FirstOrDefault();

                if (selectedProduct != null) 
                {
                    Console.WriteLine(selectedProduct.DisplayDetailsFull());

                    Console.WriteLine("\nWhat do you want to do?");
                    Console.WriteLine("1: Use product");
                    Console.WriteLine("0: Back to inventory overview");

                    Console.Write("Your selection: ");
                    userSelection = Console.ReadLine();

                    if (userSelection == "1")
                    {
                        Console.WriteLine("How many products do you want to use?");
                        int amount = int.Parse(Console.ReadLine() ?? "0");

                        selectedProduct.UseProduct(amount);

                        Console.ReadLine();
                    }
                }
            }
            else
            {
                Console.WriteLine("Non-existing product selected. Please try again.");
            }
        }

        private static void ShowProductsLowOnStock()
        {
            List<Product> lowOnStockProduts = inventory.Where(p => p.IsBelowStockThreshold).ToList();

            if (lowOnStockProduts.Count > 0)
            {
                Console.WriteLine("The following items are low on stock, order more soon!");

                foreach( var product in lowOnStockProduts )
                {
                    Console.WriteLine(product.DisplayDetailsShort());
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("No itmes are currently low on stock");
            }

            Console.ReadLine();
        }

        private static void ShowOrderManagementMenu()
        {
            string? userSelection = string.Empty;

            do
            {
                Console.Clear();
                Console.WriteLine("********************");
                Console.WriteLine("* Select an action *");
                Console.WriteLine("********************");

                Console.WriteLine("1: Open order overview");
                Console.WriteLine("2: Add new order");
                Console.WriteLine("0: Back to main menu");

                Console.Write("Your selection: ");

                userSelection = Console.ReadLine();

                switch (userSelection)
                {
                    case "1":
                        ShowOpenOrderOverview();
                        break;
                    case "2":
                        ShowAddNewOrder();
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again");
                        break;
                }
            }
            while (userSelection != "0");
            ShowMainMenu();
        }

        private static void ShowOpenOrderOverview()
        {
            ShowFulfilledOrders();

            if (orders.Count > 0)
            {
                Console.WriteLine("Open orders:");

                foreach ( var order in orders )
                {
                    Console.WriteLine(order.ShowOrderDetails());
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("There are no open orders");
            }

            Console.ReadLine(); 
        }

        private static void ShowFulfilledOrders()
        {
            Console.WriteLine("Checking fulfilled orders.");
            foreach( var order in orders )
            {
                if (!order.Fulfilled && order.OrderFulfilmentDate < DateTime.Now)
                {
                    foreach (var orderItem in order.OrderItems )
                    {
                        Product? selectedProduct = inventory.Where(p => p.Id == orderItem.ProductId).FirstOrDefault();
                        if (selectedProduct != null)
                        {
                            selectedProduct.IncreaseStock(orderItem.AmountOrdered);
                        }
                    }
                    order.Fulfilled = true;
                }
            }

            orders.RemoveAll(o => o.Fulfilled);

            Console.WriteLine("Fulfilled orders checked");
        }

        private static void ShowAddNewOrder()
        {
            Order newOrder = new Order();
            string? selectedProductId = string.Empty;

            Console.WriteLine("Creating new order");

            do
            {
                ShowAllProductsOverview();

                Console.WriteLine("Which product do you want to order? (enter 0 to stop adding new products to order)");

                Console.Write("Enter the ID of product: ");
                selectedProductId = Console.ReadLine();

                if (selectedProductId != "0" && selectedProductId != null)
                {
                    Product? selectedProduct = inventory.Where(p => p.Id == int.Parse(selectedProductId)).FirstOrDefault();
                    
                    if (selectedProduct != null)
                    {
                        Console.Write("How many do you want to order?: ");
                        int amountOrdered = int.Parse(Console.ReadLine() ?? "0");

                        OrderItem orderItem = new OrderItem(selectedProduct.Id, selectedProduct.Name, amountOrdered);
                        newOrder.OrderItems.Add(orderItem);
                    }
                }
            }
            while (selectedProductId != "0");

            Console.WriteLine("Creating order...");

            orders.Add(newOrder);

            Console.WriteLine("Order now created.");
            
            Console.ReadLine();
        }

        private static void ShowSettingsMenu()
        {
            string? userSelection = string.Empty;

            do
            {
                Console.Clear();
                Console.WriteLine("************");
                Console.WriteLine("* Settings *");
                Console.WriteLine("************");

                Console.WriteLine("What do you want to do?");

                Console.WriteLine("1: Change stock threshold");
                Console.WriteLine("0: Back to main menu");

                Console.Write("Your selection: ");

                userSelection = Console.ReadLine();

                switch (userSelection)
                {
                    case "1":
                        ShowChangeStockThreshold();
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again");
                        break;
                }
            }
            while (userSelection != "0");
            ShowMainMenu();
        }

        private static void ShowChangeStockThreshold()
        {
            Console.WriteLine($"Enter the new stock threshold (current value: {Product.StockThreshold}). This applies to all products!");
            Console.Write("New value: ");
            int newValue = int.Parse(Console.ReadLine() ?? "0");
            Product.StockThreshold = newValue;
            Console.WriteLine($"New stock threshold set to {Product.StockThreshold}");

            foreach (var product in inventory)
            {
                product.UpdateLowStock();
            }

            Console.ReadLine();
        }

        private static void ShowAllUnitTypes()
        {
            int i = 1;
            foreach(string name in Enum.GetNames(typeof(UnitType)))
            {
                Console.WriteLine($"{i}. {name}");
                i++;
            }
        }

        private static void ShowAllCurrencies()
        {
            int i = 1;
            foreach (string name in Enum.GetNames(typeof(Currency)))
            {
                Console.WriteLine($"{i}. {name}");
                i++;
            }
        }


        private static void ShowCreateNewProduct()
        {
            UnitType unitType = UnitType.PerItem;

            Console.WriteLine("What type of product do you want to create?");
            Console.WriteLine("1. Regular product\n2. Fresh product\n3. Boxed product");
            Console.Write("Your selection: ");

            var productType = Console.ReadLine();
            if (productType != "1" && productType != "2" && productType != "3")
            {
                Console.WriteLine("Invalid selection!");
                return;
            }

            Product? newProduct = null;

            Console.Write("Enter the name of the product: ");
            string name = Console.ReadLine() ?? string.Empty;

            Console.Write("Enter the price of the product: ");
            double price = double.Parse(Console.ReadLine() ?? "0.0");

            ShowAllCurrencies();
            Console.Write("Select the currency: ");
            Currency currency = (Currency)Enum.Parse(typeof(Currency), Console.ReadLine() ?? "1");

            Console.WriteLine("Enter the description of the product: ");
            string description = Console.ReadLine() ?? string.Empty;

            if (productType != "3")
            {
                ShowAllUnitTypes();
                Console.Write("Select the unit type: ");
                unitType = (UnitType)Enum.Parse(typeof(UnitType), Console.ReadLine() ?? "1");
            }

            Console.Write("Enter the maximun number of units in stock for this product: ");
            int maxInStock = int.Parse(Console.ReadLine() ?? "0");

            int newId = inventory.Max(p => p.Id) + 1;

            switch (productType)
            {
                case "1":
                    newProduct = new Product(newId, name, description, new Price(price, currency), unitType, maxInStock);
                    break;
                case "2":
                    Console.Write("Days until expiration: ");
                    int numOfDays = int.Parse(Console.ReadLine() ?? "0");
                    DateTime ExpDate = DateTime.Now.AddDays(numOfDays);
                    Console.Write("Storage Instructions: ");
                    string storageInstructions = Console.ReadLine() ?? string.Empty;

                    newProduct = new FreshProduct(newId, name, description, new Price(price, currency), unitType, maxInStock, ExpDate, storageInstructions);
                    break;
                case "3":
                    Console.Write("Enter the number of items per box: ");
                    int numberInBox = int.Parse(Console.ReadLine() ?? "0");

                    newProduct = new BoxedProduct(newId, name, description, new Price(price, currency), maxInStock, numberInBox);
                    break;
            }

            if (newProduct != null)
                inventory.Add(newProduct);
        }

    }
}
