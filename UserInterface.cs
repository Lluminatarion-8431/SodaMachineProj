using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachineProj
{
    class UserInterface
    {
        
        public static void Welcome()
        {
            Console.WriteLine("Welcome to the Swole Machine");
            Console.WriteLine("I am here to fuel your thristy muscles!");
        }
       public static void NotInInventory()
        {
            Console.WriteLine("Out of muscle juice, please, make another selection.");

        }
        public static string ChoosePayment()
        {
            Console.WriteLine("Please select a payment method to get swole:");
            Console.WriteLine("1: Coins\t2: Card");
            string paySelection = Console.ReadLine();
            return paySelection;
        }
        public static string ChooseSodaMenu()
        {
            Console.WriteLine("Please select which 'soda' flavor you would like:");
            Console.WriteLine("1: Hardcore Root Beer 60¢\n2: Psychotic Cola 35¢\n3: Insane Orange Soda 6¢");
            string sodaSelection = Console.ReadLine();
            return sodaSelection;
        }
        public static void WhatCoinsInMachine()
        {
            Console.WriteLine("Please Insert Coins to fuel your spaghetti arms");
        }
        public static int InsertQuarters()
        {
            Console.Write("How many Quarters would you like to insert? ");
            int tempQuarters = Int32.Parse(Console.ReadLine());
            return tempQuarters;
        }
        public static int InsertDimes()
        {
            Console.Write("How many Dimes would you like to insert? ");
            int tempDimes = Int32.Parse(Console.ReadLine());
            return tempDimes;
        }
        public static int InsertNickles()
        {
            Console.Write("How many Nickles would you like to insert? ");
            int tempNickles = Int32.Parse(Console.ReadLine());
            return tempNickles;
        }
        public static int InsertPennies()
        {
            Console.WriteLine("How many Pennies would you like to insert: ");
            int tempPennies = Int32.Parse(Console.ReadLine());
            return tempPennies;
        }
        public static void MoneyPrintOut(double money)
        {
            Console.WriteLine("You have inserted ${0} into the machine", money);
        }
        public static void InsufficientFunds()
        {
            Console.WriteLine("You have not inserted enough money for this purchase.");
            Console.WriteLine("Refunding money...");
        }
        public static void ChangeAmount(double change)
        {
            Console.WriteLine("Your change amount is: ${0}", Math.Round(change, 2));
        }
        public static string AnotherPurchase()
        {
            Console.WriteLine("Make another purchase and cure your spaghetti arms? Yes or no");
            string anotherPurchase = Console.ReadLine();
            return anotherPurchase;
        }
        public static void CardBalance(Card card)
        {
            Console.WriteLine("Your card balance is : ${0}", Math.Round(card.Funds, 2));
        }
        public static void ValidSelection()
        {
            Console.WriteLine("Invalid selection, please make another selection");
        }

    }
}
