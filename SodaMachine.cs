using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachineProj
{
    class SodaMachine
    {
        //Member Variables (Has A)
        public List<Coin> register;
        public List<Can> cans;
        public double eRegister;

        public List<Coin> inRegister;
        public double moneyTotal;
        public Can can;

        //Constructor (Spawner)
        public SodaMachine()
        {
            register = new List<Coin>();
            cans = new List<Can>();

            InRegister();
            CansInventory();
        }

        //Member Methods (Can Do)
        private void InRegister()
        {
            for (int i = 0; i < 20; i++)
            {
                register.Add(new Quarter());
            }
            for (int i = 0; i < 10; i++)
            {
                register.Add(new Dime());
            }
            for (int i = 0; i < 50; i++)
            {
                register.Add(new Penny());
            }
        }

        private void CansInventory()
        {
            for (int i = 0; i < 20; i++)
            {
                cans.Add(new RootBeer());
            }
            for (int i = 0; i < 20; i++)
            {
                cans.Add(new Cola());
            }
            for (int i = 0; i < 5; i++)
            {
                cans.Add(new OrangeSoda());
            }
        }

    }
}
