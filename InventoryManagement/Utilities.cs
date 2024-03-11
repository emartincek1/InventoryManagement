using InventoryManagement.OrderManagement;
using InventoryManagement.ProductManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine("********************");
            Console.WriteLine("* Select an action *");
            Console.WriteLine("********************");

            Console.WriteLine("1: Inventory management");
            Console.WriteLine("2: Order management");
            Console.WriteLine("3: Settings");
            Console.WriteLine("4: Save all data");
            Console.WriteLine("0: Close application");

            Console.Write("Your Selection: ");

            string? userSelection = Console.ReadLine();
            switch (userSelection)
            {
                case "1":
                    ShowInventoryManagemntMenu();
                    break;
                case "2":
                    ShowOrderManagementMenu();
                    break;
                case "3":
                    ShowSettingsMenu();
                    break;
                case "4":
                    // SaveAllData();
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
            string? userSelection;

            do
            {
                Console.WriteLine("************************");
                Console.WriteLine("* Inventory management *");
                Console.WriteLine("************************");

                ShowAllProductsOverview();

                Console.WriteLine("What do you want to do");

                Console.WriteLine("1: View details of product");
                Console.WriteLine("2: Add new product");
                Console.WriteLine("3: Clone product");
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
                        // ShowCreateNewProduct();
                        break;
                    case "3":
                        // ShowCloneExistingProduct();
                        break;
                    case "4":
                        ShowProductsLowOnStock();
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again");
                        break;

                }
                while (userSelection != "0") ;
                ShowMainMenu();
            }
        }

        private static void ShowAllProductsOverview()
        {
            foreach(Product product in inventory)
            {
                Console.WriteLine(product.DisplayDetailsShort());
                Console.WriteLine();
            }
        }
    }
}
