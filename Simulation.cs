﻿using System;
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
                inSodaMachineInventory = sodaMachine.SodaInventory(sodaChoice);

            }
            CheckEnoughMoneyCard();
        }
        public void CoinSodaMachineLoop()
        {
            bool inSodaMachineInventory = false;
            double moneyPrintOut;

            InitTempRegister();
            moneyPrintOut = sodaMachine.DisplayCost();
            UserInterface.MoneyPrintOut(Math.Round(moneyPrintOut, 2));

            while (!inSodaMachineInventory)
            {
                string sodaChoice = SodaSelection();
                inSodaMachineInventory = sodaMachine.SodaInventory(sodaChoice);
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
            
        }

        public void AddDimesToTempRegister(int dimes)
        {
            
           
        }
        public void AddNicklesToTempRegister(int nickles)
        {
            
        }
        public void AddPenniesToTempRegister(int pennies)
        {
            
        }
        public void AddQuarterChangeToWallet()
        {
            
        }
        public void AddDimeChangeToWallet()
        {
            
        }
        public void AddNickleChangeToWallet()
        {
           
        }
        public void AddPennyChangeToWallet()
        {
           
        }
    }
}
