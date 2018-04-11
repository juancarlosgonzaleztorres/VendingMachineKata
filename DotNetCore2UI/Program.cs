using System;
using System.Collections.Generic;
using VendingMachineBusinessLogic;

namespace DotNetCore2UI
{
    class Program
    {
        static VendingMachine vendingMachine;
        static Product productBought;
        static Dictionary<USCoinTypes, int> coinsReturned = new Dictionary<USCoinTypes, int>
        {
            {USCoinTypes.Quarter, 0 },
            {USCoinTypes.Dime, 0 },
            {USCoinTypes.Nickel, 0 },
        };
        static decimal moneyValueReturned = 0;
        static void Main(string[] args)
        {
            vendingMachine = new VendingMachine();
            vendingMachine.LoadCoins(USCoinTypes.Nickel, 3);
            while (true)
            {
                showInterface();
                selectOption(Console.ReadLine());
                Console.Clear();                
            }
        }

        private static void selectOption(string option)
        {
            var quarter = new USCoin(new QuarterFeatures());
            var dime = new USCoin(new DimeFeatures());
            var nickel = new USCoin(new NickelFeatures());
            switch (option.ToLower())
            {
                case "1":
                    vendingMachine.InsertCoin(quarter);
                    break;
                case "2":
                    vendingMachine.InsertCoin(dime);
                    break;
                case "3":
                    vendingMachine.InsertCoin(nickel);
                    break;
                case "4":
                    productBought = vendingMachine.SelectProduct(ProductTypes.Cola);
                    break;
                case "5":
                    productBought = vendingMachine.SelectProduct(ProductTypes.Chips);
                    break;
                case "6":
                    productBought = vendingMachine.SelectProduct(ProductTypes.Candy);
                    break;
                case "7":
                    coinsReturned = vendingMachine.GetMoneyReturn();
                    moneyValueReturned = vendingMachine.GetValueOfMoney(coinsReturned);
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        private static void showInterface()
        {
            var displayPosition = "             {0}";
            Console.WriteLine("       AWESOME VENDING MACHINE\n");
            Console.WriteLine("Amount: {0}", vendingMachine.Amount);
            Console.WriteLine(displayPosition, vendingMachine.Display);
            Console.WriteLine();
            Console.WriteLine("[1] Insert Quarter [2] Insert Dime  [3] Insert Nickel");
            Console.WriteLine("[4] Select Cola    [5] Select Chips [6] Select Candy");
            Console.WriteLine("[7] Return Coins");            
            Console.WriteLine();
            showOutput();
        }

        private static void showOutput()
        {
            Console.WriteLine("Product Returned: {0}", productBought?.Name);
            Console.WriteLine();
            Console.WriteLine("Money Returned:");
            Console.WriteLine("Quarters:{0}, Dimes:{1}, Nickels:{2}",
                coinsReturned[USCoinTypes.Quarter],
                coinsReturned[USCoinTypes.Dime],
                coinsReturned[USCoinTypes.Nickel]);
            Console.WriteLine();
            Console.WriteLine("Money value Returned:");
            Console.WriteLine("{0}", moneyValueReturned);
            Console.WriteLine();
            Console.Write("Enter number option and press enter: ", moneyValueReturned);
        }
    }
}