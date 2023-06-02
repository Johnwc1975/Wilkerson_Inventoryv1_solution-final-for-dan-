
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Threading;
using System;
using System.Collections.Generic;


// Jonathan Wilkerson

// IT112 

// NOTES: Notes the instructor should readInterfaces and Classes:

//At the start, the program defines an interface IShippable which has two properties: ShipCost and Product. Then, four classes (Bicycle, LawnMower, BaseballGlove, and Cracker) are defined, each implementing the IShippable interface. This means they provide values for ShipCost and Product. Each class represents a different type of product with its own shipping cost.

//Shipper Class:

//The Shipper class represents a shipping service. It has a private member _products, which is a list of IShippable objects (i.e., the products to be shipped). This class has three methods: Add, ListShipmentItems, and ComputeShippingCharges.

//The Add method adds a product to the _products list and prints a message stating that the product has been added.

//The ListShipmentItems method displays a list of all products added to the shipment so far, including their quantities.

//The ComputeShippingCharges method calculates the total shipping cost for all the products in the _products list.

//Program Class:

//This is the entry point of the application. Inside the Main method, an instance of the Shipper class is created.Then, a while loop begins, which continues indefinitely until the program is manually stopped. This loop represents the interactive menu where the user chooses what action to perform.

//The menu options include adding each type of product, listing the products added so far, and computing the total shipping cost. Depending on the user's input, different actions are taken.

//Menu Options:

//If the user chooses to add a product (options 1-4), a new product of the chosen type is created and added to the Shipper object.

//If the user chooses to list shipment items (option 5), the ListShipmentItems method of the Shipper object is called, which displays a list of all products in the shipment.

//If the user chooses to compute the shipping charges (option 6), the ComputeShippingCharges method of the Shipper object is called. The total shipping cost is then printed, and the program ends.

//Remember, this is a simplified console application with minimal error checking and handling. Depending on the real-world needs of your program, you might want to add more robust error handling, input validation, and possibly additional features.


// BEHAVIORS NOT IMPLEMENTED AND WHY: Are there any parts of the assignment you did not complete?

//Error Checking and Input Validation: The program currently assumes correct input from the user. It does not handle cases where the user enters something other than the numbers 1-6. Adding input validation and error handling would make the program more robust and user-friendly.

//Limit on the Number of Products: The requirement stated that the shippable products would never exceed 10. However, this limit is not enforced in the code. To fully implement this, the program could keep track of the total number of products and prevent more than 10 products from being added.

//Program Termination: The program only ends when the shipping cost is computed. Additional options to exit the program, perhaps after confirming with the user, could make it more flexible.

//Multiple Instances of the Same Product: The current implementation creates a new instance of a product each time it is added. An enhancement could be to create just one instance of each product and keep track of the quantity, rather than creating multiple instances.

//Unit Testing: No unit tests have been implemented for this application. Including unit tests would ensure the logic of the application is working as expected and make it more maintainable in the future.

//These are not gaps as such, but opportunities to enhance the application if you choose to do so.


namespace WIlkerson_Inventoryv1
{
    interface IShippable
    {
        decimal ShipCost { get; }
        string Product { get; }
    }

    class Bicycle : IShippable
    {
        public decimal ShipCost { get; } = 9.50M;
        public string Product { get; } = "Bicycle";
    }

    class LawnMower : IShippable
    {
        public decimal ShipCost { get; } = 24.00M;
        public string Product { get; } = "Lawn Mower";
    }

    class BaseballGlove : IShippable
    {
        public decimal ShipCost { get; } = 3.23M;
        public string Product { get; } = "Baseball Glove";
    }

    class Cracker : IShippable
    {
        public decimal ShipCost { get; } = 0.57M;
        public string Product { get; } = "Cracker";
    }

    class Shipper
    {
        private List<IShippable> _products = new List<IShippable>();
        public void Add(IShippable product)
        {
            _products.Add(product);
            Console.WriteLine($"1 {product.Product} has been added");
        }

        public void ListShipmentItems()
        {
            var itemCounts = new Dictionary<string, int>();
            foreach (var item in _products)
            {
                if (!itemCounts.ContainsKey(item.Product))
                {
                    itemCounts[item.Product] = 0;
                }
                itemCounts[item.Product]++;
            }

            Console.WriteLine("Shipment manifest:");
            foreach (var item in itemCounts)
            {
                Console.WriteLine($"{item.Value} {item.Key}" + (item.Value > 1 && item.Key != "Cracker" ? "s" : ""));
            }
        }

        public decimal ComputeShippingCharges()
        {
            decimal totalCost = 0;
            foreach (var item in _products)
            {
                totalCost += item.ShipCost;
            }
            return totalCost;
        }
    }
    public partial class Program
    {
        static void Main(string[] args)
        {
            Shipper shipper = new Shipper();
            while (true)
            {
                Console.WriteLine("Choose from the following options:");
                Console.WriteLine("1. Add a Bicycle to the shipment");
                Console.WriteLine("2. Add a Lawn Mower to the Shipment");
                Console.WriteLine("3. Add a Baseball Glove to the shipment");
                Console.WriteLine("4. Add Crackers to the shipment");
                Console.WriteLine("5. List Shipment Items");
                Console.WriteLine("6. Compute Shipping Charges");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        shipper.Add(new Bicycle());
                        break;
                    case "2":
                        shipper.Add(new LawnMower());
                        break;
                    case "3":
                        shipper.Add(new BaseballGlove());
                        break;
                    case "4":
                        shipper.Add(new Cracker());
                        break;
                    case "5":
                        shipper.ListShipmentItems();
                        break;
                    case "6":
                        Console.WriteLine($"Total shipping cost for this order is ${shipper.ComputeShippingCharges():F2}");
                        return;
                }
            }
        }
    }
}
