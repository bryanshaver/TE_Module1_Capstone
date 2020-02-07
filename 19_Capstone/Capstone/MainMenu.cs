using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone
{
    public class MainMenu
    {
        VendingMachine vnMain = new VendingMachine();
        public void MainMenuMethod()
        {
            decimal totalSales = 0.00M;

            void SalesReport()
            {
                using(StreamWriter sw = new StreamWriter("SalesReport.txt"))
                {
                    foreach (KeyValuePair<string, Products> product in vnMain.productList) 
                    {
                        totalSales += (product.Value.ItemSales * Convert.ToDecimal(product.Value.Price));
                    }
                    foreach (KeyValuePair<string, Products> product in vnMain.productList)
                    {
                        sw.WriteLine($"{product.Value.ProductName} | {product.Value.ItemSales}");
                    }

                    sw.WriteLine();
                    sw.WriteLine($"Total Sales: {totalSales:C}");
                }
            }



            bool keepGoing = true;
            while (keepGoing)
            {
                Console.Clear();

                Console.WriteLine(@"
██╗   ██╗███████╗███╗   ██╗██████╗  ██████╗       ███╗   ███╗ █████╗ ████████╗██╗ ██████╗
██║   ██║██╔════╝████╗  ██║██╔══██╗██╔═══██╗      ████╗ ████║██╔══██╗╚══██╔══╝██║██╔════╝
██║   ██║█████╗  ██╔██╗ ██║██║  ██║██║   ██║█████╗██╔████╔██║███████║   ██║   ██║██║     
╚██╗ ██╔╝██╔══╝  ██║╚██╗██║██║  ██║██║   ██║╚════╝██║╚██╔╝██║██╔══██║   ██║   ██║██║     
 ╚████╔╝ ███████╗██║ ╚████║██████╔╝╚██████╔╝      ██║ ╚═╝ ██║██║  ██║   ██║   ██║╚██████╗
  ╚═══╝  ╚══════╝╚═╝  ╚═══╝╚═════╝  ╚═════╝       ╚═╝     ╚═╝╚═╝  ╚═╝   ╚═╝   ╚═╝ ╚═════╝
                                                                                         ");

                Console.WriteLine("Welcome to Vendo-Matic 800. Please select an option.");
                Console.WriteLine();

                Console.WriteLine("(1) Display Vending Machine Items");
                Console.WriteLine("(2) Purchase");
                Console.WriteLine("(3) Exit");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "3":
                        keepGoing = false;
                        continue;
                    case "1":
                        vnMain.DisplayItems();
                        break;
                    case "2":
                        vnMain.Purchase();
                        break;
                    case "4":
                        SalesReport();
                        break;

                }


            }

        }
    }
}
