using SodaMachineProj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachineProj
{
    class Wallet
    {
        //Member Variables (Has A)
        public List<Coin> coins;
        public Card card;

        //Constructor (Spawner)
        public Wallet()
        {
            coins = new List<Coin>();
            card = new Card();
        }

        //Member Methods (Can Do)
        public void FizzBuzz()
        {
            for (int i = 0; i < 101; i++)
            {

            }
        }
    }
}
