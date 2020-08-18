using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Card
    {
        //Member Variables (Has A)
        protected double funds;

        public double Funds { get { return funds; } set { funds = value; } }
        //Constructor (Spawner)
        public Card()
        {
            Funds = 20.00;
        }

        //Member Methods (Can Do)
    }
}
