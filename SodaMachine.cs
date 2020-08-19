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

        private void CansInventory()//verbs for words
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

        public bool SodaInventory(string name)// verb based 
        {
            bool sodaInventory = true;
            
            for(int i = 0; i < cans.Count; i++)
            {
                if (cans[i].name == name)
                {
                    sodaInventory = true;
                    can = cans[i];
                    break;
                }
                else
                {
                    sodaInventory = false;
                }
            }
            return sodaInventory;
        }

        public double DisplayCost()//total money in register
        {
            moneyTotal = 0.0;
            for (int i = 0; i < inRegister.Count; i++)
            {
                moneyTotal += inRegister[i].Value;
            }
            return moneyTotal;
        }
    }
}
