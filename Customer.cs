using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachineProj
{
    class Customer
    {
        //Member Variables (Has A)
        public Wallet wallet;
        public Backpack backpack;

        //Constructor (Spawner)
        public Customer()
        {
            wallet = new Wallet();
            backpack = new Backpack();
        }
        //Member Methods (Can Do)
    }
}
