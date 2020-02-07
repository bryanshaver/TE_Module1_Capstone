using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone
{
    public class VendingMachine
    {
        /***********************************
         * VENDING MACHINE WILL REQUIRE:
         * 
         * 1. Sales Report Method
         * 2. Automated per-sale report (log.txt)
         * 3. A Balance property DONE
         * 4. A Total Sales Property DONE
         * 5. A purchase method, this will also print out the appropriate message based on what type is purchased. Updates inventory
         * 6. Finish transaction, which will contain a method to return current balance in change and then set balance to zero
         * 7. Feed Money method for when a customer puts in money
         * 
         ***********************************/


        //public List<Products> productList = new List<Products>();
        public Dictionary<string, Products> productList = new Dictionary<string, Products>();

        public VendingMachine()
        {
            using (StreamReader src = new StreamReader("vendingmachine.csv"))
            {
                while (!src.EndOfStream)
                {
                    string[] sArray = src.ReadLine().Split("|");
                    if (sArray[3] == "Chip")
                    {
                        productList.Add(sArray[0], new Chip() { SlotLocation = sArray[0], ProductName = sArray[1], Price = sArray[2], Type = sArray[3] });
                    }
                    else if (sArray[3] == "Candy")
                    {
                        productList.Add(sArray[0], new Candy() { SlotLocation = sArray[0], ProductName = sArray[1], Price = sArray[2], Type = sArray[3] });
                    }
                    else if (sArray[3] == "Drink")
                    {
                        productList.Add(sArray[0], new Drink() { SlotLocation = sArray[0], ProductName = sArray[1], Price = sArray[2], Type = sArray[3] });
                    }
                    else if (sArray[3] == "Gum")
                    {
                        productList.Add(sArray[0], new Gum() { SlotLocation = sArray[0], ProductName = sArray[1], Price = sArray[2], Type = sArray[3] });
                    }
                }
            }
        }


        public decimal Balance { get; set; } = 0.00M;
        public decimal totalCashEarned { get; set; } = 0.00M;

        public void DisplayItems()
        {
            Console.Clear();
            foreach (KeyValuePair<string, Products> kvp in productList)
            {
                if (kvp.Value.Amount == 0)
                {
                    Console.WriteLine($"{kvp.Value.SlotLocation} | {kvp.Value.ProductName} | {kvp.Value.Price} | {kvp.Value.Type} | SOLD OUT");
                }
                else
                {
                    Console.WriteLine($"{kvp.Value.SlotLocation} | {kvp.Value.ProductName} | {kvp.Value.Price} | {kvp.Value.Type} | {kvp.Value.Amount}");
                }
            }
            Console.WriteLine();
            Console.WriteLine("Hit enter to continue.");
            Console.ReadLine();
        }

        public void Purchase()
        {

            void FeedMoney()
            {
                Console.Clear();
                int fedMoney;
                Console.WriteLine("Please insert cash ($1, $2, $5, and $10 bills only)");
                fedMoney = Convert.ToInt32(Console.ReadLine());
                if (fedMoney == 1 || fedMoney == 2 || fedMoney == 5 || fedMoney == 10)
                {
                    Balance += fedMoney;
                    Console.WriteLine($"Current Money Provided: {Balance:C}");
                    using (StreamWriter sw = new StreamWriter("log.txt", true))
                    {
                        sw.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt")} FEED MONEY: {fedMoney:C} {Balance:C}");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Hit enter to return to purchase page");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("The Vendo-Matic 800 accepts $1, $2, $5, and $10 bills only.");
                    System.Threading.Thread.Sleep(3000);
                    Console.WriteLine("Returning to Purchase Page..");
                    System.Threading.Thread.Sleep(2000);
                }
            }

            void SelectProduct()
            {
                Console.WriteLine("The current available items are: ");
                DisplayItems();
                Console.WriteLine();
                Console.WriteLine("Enter the product code for the item you wish to purchase: ");

                string selectedProduct = Console.ReadLine();

                if (!productList.ContainsKey(selectedProduct))
                {
                    Console.WriteLine("That product is not available");
                    Console.ReadLine();
                }
                // TODO : Change amount == 0 to amount == "SOLD OUT"
                else
                {
                    Products product = productList[selectedProduct];
                    if (product.Amount == 0)
                    {
                        Console.WriteLine("That product is SOLD OUT");
                        Console.ReadLine();
                    }
                    else
                    {
                        decimal afterPrice = Balance - Convert.ToDecimal(product.Price);
                        using (StreamWriter sw = new StreamWriter("log.txt", true))
                        {
                            sw.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt")} {product.ProductName}: {Balance:C} {afterPrice:C}");
                        }

                        Balance -= Convert.ToDecimal(product.Price);
                        Console.WriteLine($"{product.ProductName} | {product.Price} | Your remaining balance is: {Balance:C}");
                        Console.WriteLine($"{product.Display}");
                        totalCashEarned += Convert.ToDecimal(product.Price);
                        product.Amount--;
                        product.ItemSales++;
                        Console.ReadLine();
                    }
                }


            }

            void FinishTransaction()
            {
                int quarters;
                int nickels;
                int dimes;

                decimal bal = Balance * 100;

                quarters = (int)(bal / 25);
                bal = bal % 25;

                dimes = (int)(bal / 10);
                bal = bal % 10;

                nickels = (int)(bal / 5);
                bal = bal % 5;

                Console.WriteLine($"Your change is: {quarters} Quarters, {dimes} Dimes, and {nickels} Nickels.");

                Balance = 0;
                Console.WriteLine("Hit enter to continue.");
                Console.ReadLine();
            }

            bool backToMain = true;

            while (backToMain)
            {
                Console.Clear();
                Console.WriteLine("(1) Feed Money");
                Console.WriteLine("(2) Select Product");
                Console.WriteLine("(3) Finish Transaction");
                Console.WriteLine();
                Console.WriteLine($"Current Money Provided: {Balance:C}");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "3":
                        FinishTransaction();
                        backToMain = false;
                        continue;
                    case "1":
                        FeedMoney();
                        break;
                    case "2":
                        SelectProduct();
                        break;

                }
            }
        }
    }
}
