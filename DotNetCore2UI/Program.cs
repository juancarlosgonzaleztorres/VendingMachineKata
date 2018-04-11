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
        
        static void Main(string[] args)
        {
            vendingMachine = new VendingMachine();
            vendingMachine.LoadCoins(USCoinTypes.Nickel, 100);
            vendingMachine.LoadCoins(USCoinTypes.Quarter, 100);
            vendingMachine.LoadCoins(USCoinTypes.Dime, 100);
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
                    coinsReturned = vendingMachine.CoinReturn;
                    break;
                case "5":
                    productBought = vendingMachine.SelectProduct(ProductTypes.Chips);
                    coinsReturned = vendingMachine.CoinReturn;
                    break;
                case "6":
                    productBought = vendingMachine.SelectProduct(ProductTypes.Candy);
                    coinsReturned = vendingMachine.CoinReturn;
                    break;
                case "7":
                    vendingMachine.GetMoneyReturn();
                    coinsReturned = vendingMachine.CoinReturn;
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
            var displayPosition = "              {0}";
            Console.WriteLine(    "       VENDING MACHINE BRAINS\n");            
            Console.WriteLine(displayPosition, vendingMachine.Display);
            Console.WriteLine();
            Console.WriteLine(    "[1] Insert Quarter [2] Insert Dime  [3] Insert Nickel");
            Console.WriteLine(    "[4] Select Cola    [5] Select Chips [6] Select Candy");
            Console.WriteLine(    "[7] Return Coins");            
            Console.WriteLine();
            showOutput();
        }

        private static void showOutput()
        {
            Console.WriteLine("Product dispensed: {0}", productBought?.Name);
            Console.WriteLine();            
            Console.WriteLine("Coin return: Quarters:{0}, Dimes:{1}, Nickels:{2} = ${3}",
                coinsReturned?[USCoinTypes.Quarter] ??0,
                coinsReturned?[USCoinTypes.Dime] ?? 0,
                coinsReturned?[USCoinTypes.Nickel] ?? 0,
                vendingMachine.GetValueOfMoney(coinsReturned));
            Console.WriteLine();
            Console.Write("Enter number option and press enter: ");
        }
    }
}