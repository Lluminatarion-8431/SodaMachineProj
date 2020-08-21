using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SodaMachineProj
{
    class Simulation
    {
        //Member Variables (Has A)
        public SodaMachine sodaMachine;
        public Customer customer;
        bool sodaMachineChoice = true;

        //Constructor (Spawner)
        public Simulation()
        {
            sodaMachine = new SodaMachine();
            customer = new Customer();
            UserInterface.Welcome();
            string paymentChoice = UserInterface.ChoosePayment();
            PaymentChoice(paymentChoice);
        }


        //Member Methods (Can Do)
        public void PaymentChoice(string paymentChoice)
        {
            while (sodaMachineChoice)
            {
                switch (paymentChoice) 
                {
                    case "1":
                        CoinSodaMachineLoop();
                        MakeAnotherPurchase();
                        paymentChoice = UserInterface.ChoosePayment();
                        break;
                    case "2":
                        CardSodaMachineLoop();
                        MakeAnotherPurchase();
                        paymentChoice = UserInterface.ChoosePayment();
                        break;
                    default:
                        paymentChoice = UserInterface.ChoosePayment();
                        break;
                }
            }
        }
        public void CardSodaMachineLoop()
        {
            bool inSodaMachineInventory = false;

            UserInterface.CardBalance(customer.wallet.card);

            while (!inSodaMachineInventory)
            {
                string sodaChoice = SodaSelection();
                inSodaMachineInventory = sodaMachine.SodaInsideInventory(sodaChoice);

            }
            CheckEnoughMoneyCard();
        }
        public void CoinSodaMachineLoop()
        {
            bool inSodaMachineInventory = false;
            double moneyPrintOut;

            InitTempRegister();
            moneyPrintOut = sodaMachine.DisplayCostInRegister();
            UserInterface.MoneyPrintOut(Math.Round(moneyPrintOut, 2));

            while (!inSodaMachineInventory)
            {
                string sodaChoice = SodaSelection();
                inSodaMachineInventory = sodaMachine.SodaInsideInventory(sodaChoice);
            }
            CheckEnoughMoney();

        }

        public bool MakeAnotherPurchase()
        {
            string anotherPurchase = UserInterface.AnotherPurchase().ToLower();
            switch (anotherPurchase)
            {
                case "yes":
                    sodaMachineChoice = true;
                    break;
                case "no":
                    sodaMachineChoice = false;
                    break;
                default:
                    sodaMachineChoice = false;
                    break;
            }
            return sodaMachineChoice;
        }
        public void CheckEnoughMoney()
        {
            bool changeCheck = CheckChangeAvailable();

            if((sodaMachine.moneyTotal >= sodaMachine.can.Cost) || changeCheck)
            {
                customer.backpack.cans.Add(sodaMachine.can);
                sodaMachine.cans.Remove(sodaMachine.can);
                AddMoneyToSodaMachine();
                GiveChange();
            }
            else if ((sodaMachine.moneyTotal < sodaMachine.can.Cost) || changeCheck)
            {
                UserInterface.InsufficientFunds();
                ReturnMoney();
            }
        }
        public void CheckEnoughMoneyCard()
        {
            if (customer.wallet.card.Funds >= sodaMachine.can.Cost)
            {
                customer.backpack.cans.Add(sodaMachine.can);
                sodaMachine.cans.Remove(sodaMachine.can);
                customer.wallet.card.Funds -= sodaMachine.can.Cost;
            }
            else if (customer.wallet.card.Funds < sodaMachine.can.Cost)
            {
                UserInterface.InsufficientFunds();
            }
        }
        public void ReturnMoney()
        {
            int tempRegisterCount = sodaMachine.inRegister.Count;
            for (int i = 0; i < tempRegisterCount; i++)
            {
                customer.wallet.coins.Add(sodaMachine.inRegister[0]);
                sodaMachine.inRegister.RemoveAt(0);
            }
        }
        public void AddMoneyToSodaMachine()
        {
            int tempRegisterCount = sodaMachine.inRegister.Count;
            for (int i = 0; i < tempRegisterCount; i++)
            {
                sodaMachine.register.Add(sodaMachine.inRegister[0]);
                sodaMachine.inRegister.RemoveAt(0);
            }
        }
        public void GiveChange()
        {
            double change = sodaMachine.moneyTotal - sodaMachine.can.Cost;
            UserInterface.ChangeAmount(change);

            while (change >= 0.25)
            {
                if (customer.wallet.coins.Where(c => c.name == "Quarter").ToList().Count > 0)
                {
                    change -= 0.25;
                    AddQuarterChangeToWallet();
                }
            }
            while (change >= 0.10)
            {
                if (customer.wallet.coins.Where(c => c.name == "Dime").ToList().Count > 0)
                {
                    change -= 0.10;
                    AddDimeChangeToWallet();
                }
            }
            while (change >=0.05)
            {
                if (customer.wallet.coins.Where(c => c.name == "Nickle").ToList().Count > 0)
                {
                    change -= 0.05;
                    AddNickleChangeToWallet();
                }
            }
            while (change >= 0.01)
            {
                if (customer.wallet.coins.Where(c => c.name == "Penny").ToList().Count > 0)
                {
                    change -= 0.01;
                    AddPennyChangeToWallet();
                }
            }
        }
       public bool CheckChangeAvailable()
        {
            double change = sodaMachine.moneyTotal - sodaMachine.can.Cost;
            while (change >= 0.25)
            {
                if (customer.wallet.coins.Where(c => c.name == "Quarter").ToList().Count > 0)
                {
                    change -= 0.25;
                    AddQuarterChangeToWallet();
                }
            }
            while (change >= 0.10)
            {
                if (customer.wallet.coins.Where(c => c.name == "Dime").ToList().Count > 0)
                {
                    change -= 0.10;
                    AddDimeChangeToWallet();
                }
            }
            while (change >= 0.05)
            {
                if (customer.wallet.coins.Where(c => c.name == "Nickle").ToList().Count > 0)
                {
                    change -= 0.05;
                    AddNickleChangeToWallet();
                }
            }
            while (change >= 0.01)
            {
                if (customer.wallet.coins.Where(c => c.name == "Penny").ToList().Count > 0)
                {
                    change -= 0.01;
                    AddPennyChangeToWallet();
                }
            }
            if (change == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public string SodaSelection()
        {
            string sodaName = "";
            string sodaChoice = UserInterface.ChooseSodaMenu();

            switch (sodaChoice)
            {
                case "1":
                    sodaName = "Hardcore Root Beer";
                    break;
                case "2":
                    sodaName = "Psychotic Cola";
                    break;
                case "3":
                    sodaName = "Insane Orange Soda";
                    break;
                default:
                    UserInterface.ValidSelection();
                    SodaSelection();
                    break;
            }
            return sodaName;
        }
        public void InitTempRegister()
        {
            sodaMachine.inRegister = new List<Coin>();
            UserInterface.WhatCoinsInMachine();
            AddQuartersToTempRegister(UserInterface.InsertQuarters());
            AddDimesToTempRegister(UserInterface.InsertDimes());
            AddNicklesToTempRegister(UserInterface.InsertNickles());
            AddPenniesToTempRegister(UserInterface.InsertPennies());
            Console.Clear();
        }
        public void AddQuartersToTempRegister(int quarters)
        {
            int amountOfQuarters = customer.wallet.coins.Where(c => c.name == "Quarter").ToList().Count;

            if (amountOfQuarters >= quarters) 
            { 
                for (int i = 0; i < quarters; i++)
                {
                    for (int j = 0; j < customer.wallet.coins.Count; j++)
                    {
                        if (customer.wallet.coins[j].name == "Quarter")
                        {
                            sodaMachine.inRegister.Add(customer.wallet.coins[j]);
                            customer.wallet.coins.RemoveAt(j);
                            break;
                        }
                    }
                }
            }
            else
            {
                UserInterface.ValidSelection();
                AddQuartersToTempRegister(UserInterface.InsertQuarters());
            }
        }

        public void AddDimesToTempRegister(int dimes)
        {
            int amountOfDimes = customer.wallet.coins.Where(c => c.name == "Dime").ToList().Count;

            if (amountOfDimes >= dimes)
            {
                for (int i = 0; i < dimes; i++)
                {
                    for (int j = 0; j < customer.wallet.coins.Count; j++)
                    {
                        if (customer.wallet.coins[j].name == "Dime")
                        {
                            sodaMachine.inRegister.Add(customer.wallet.coins[j]);
                            customer.wallet.coins.RemoveAt(j);
                            break;
                        }
                    }
                }

            }
            else
            {
                UserInterface.ValidSelection();
                AddDimesToTempRegister(UserInterface.InsertDimes());

            }
        }
        public void AddNicklesToTempRegister(int nickles)
        {
            int amountOfNickles = customer.wallet.coins.Where(c => c.name == "Nickle").ToList().Count;

            if (amountOfNickles >= nickles)
            {
                for (int i = 0; i < nickles; i++)
                {
                    for (int j = 0; j < customer.wallet.coins.Count; j++)
                    {
                        if (customer.wallet.coins[j].name == "Nickle")
                        {
                            sodaMachine.inRegister.Add(customer.wallet.coins[j]);
                            customer.wallet.coins.RemoveAt(j);
                            break;
                        }
                    }
                }
            }
            else
            {
                UserInterface.ValidSelection();
                AddNicklesToTempRegister(UserInterface.InsertNickles());
            }
        }
        public void AddPenniesToTempRegister(int pennies)
        {
            int amountOfPennies = customer.wallet.coins.Where(c => c.name == "Penny").ToList().Count;

            if (amountOfPennies >= pennies)
            {
                for (int i = 0; i < pennies; i++)
                {
                    for (int j = 0; j < customer.wallet.coins.Count; j++)
                    {
                        if (customer.wallet.coins[j].name == "Penny")
                        {
                            sodaMachine.inRegister.Add(customer.wallet.coins[j]);
                            customer.wallet.coins.RemoveAt(j);
                            break;
                        }
                    }
                }
            }
            else
            {
                UserInterface.ValidSelection();
                AddPenniesToTempRegister(UserInterface.InsertPennies());
            }
        }
        public void AddQuarterChangeToWallet()
        {
            for (int i = 0; i < sodaMachine.register.Count; i++)
            {
                if (sodaMachine.register[i].name == "Quarter")
                {
                    customer.wallet.coins.Add(sodaMachine.register[i]);
                    sodaMachine.register.RemoveAt(i);
                    break;
                }
            }
        }
        public void AddDimeChangeToWallet()
        {
            for (int i = 0; i < sodaMachine.register.Count; i++)
            {
                if (sodaMachine.register[i].name == "Dime")
                {
                    customer.wallet.coins.Add(sodaMachine.register[i]);
                    sodaMachine.register.RemoveAt(i);
                    break;
                }
            }
        }
        public void AddNickleChangeToWallet()
        {
           for (int i = 0; i < sodaMachine.register.Count; i++)
            {
                if (sodaMachine.register[i].name == "Nickle")
                {
                    customer.wallet.coins.Add(sodaMachine.register[i]);
                    sodaMachine.register.RemoveAt(i);
                    break;
                }
            }
        }
        public void AddPennyChangeToWallet()
        {
           for(int i = 0; i < sodaMachine.register.Count; i++)
            {
                if (sodaMachine.register[i].name == "Penny")
                {
                    customer.wallet.coins.Add(sodaMachine.register[i]);
                    sodaMachine.register.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
