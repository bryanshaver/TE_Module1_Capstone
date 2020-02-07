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
            #region totalSales
            foreach (KeyValuePair<string, Products> product in vnMain.productList) // rename vars
            {
                totalSales += (product.Value.ItemSales * Convert.ToDecimal(product.Value.Price));
            }
            //foreach (KeyValuePair<string, Products> candy in vnMain.productList)
            //{
            //    totalSales += (candy.ItemSales * Convert.ToDecimal(candy.Price));
            //}
            //foreach (KeyValuePair<string, Products> drink in vnMain.productList)
            //{
            //    totalSales += (drink.ItemSales * Convert.ToDecimal(drink.Price));
            //}
            //foreach (KeyValuePair<string, Products> gum in vnMain.productList)
            //{
            //    totalSales += (gum.ItemSales * Convert.ToDecimal(gum.Price));
            //}
            #endregion

            void SalesReport()
            {
                using(StreamWriter sw = new StreamWriter("SalesReport.txt"))
                {
                    foreach(KeyValuePair<string, Products> product in vnMain.productList)
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
 __ __    ___  ____   ___     ___          ___ ___   ____  ______  ____   __     
|  |  |  /  _]|    \ |   \   /   \        |   |   | /    ||      ||    | /  ]    
|  |  | /  [_ |  _  ||    \ |     | _____ | _   _ ||  o  ||      | |  | /  /     
|  |  ||    _]|  |  ||  D  ||  O  ||     ||  \_/  ||     ||_|  |_| |  |/  /      
|  :  ||   [_ |  |  ||     ||     ||_____||   |   ||  _  |  |  |   |  /   \_     
 \   / |     ||  |  ||     ||     |       |   |   ||  |  |  |  |   |  \     |    
  \_/  |_____||__|__||_____| \___/        |___|___||__|__|  |__|  |____\____|    
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
