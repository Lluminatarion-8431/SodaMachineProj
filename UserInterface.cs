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
            Console.WriteLine("Awww, too bad, out of muscle juice. Make another selection!.");

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
    }
}
