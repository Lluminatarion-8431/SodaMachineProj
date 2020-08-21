using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public void CheckEnoughMoneyCard()
        {

        }
        
        public void CheckChangeAvailable()
        {

        }
    }
}
