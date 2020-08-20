using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public bool MakeAnotherPurchase()
        {
            string anotherPurchase = UserInterface.AnotherPruchase().ToLower();
            switch (anotherPurchase)
            {
                case "yes":
                    sodaMachineChoice = true;
                    break;
                case "no":
                    sodaMachineChoice = false;
                default:
                    sodaMachineChoice = false;
                    break;
            }
            return sodaMachineChoice;
        }
        public void CheckEnoughMoney()
        {
            
        }
        public void SodaSelection()
        {
            
        }
        public void CheckEnoughMoneyCard()
        {

        }
        public void AnotherPruchase()
        {

        }
        public void CheckChangeAvailable()
        {

        }
    }
}
