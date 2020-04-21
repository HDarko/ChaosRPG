using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Classes
{
    class Weapon : GameItem
    {
        public int minimumDamage { get; set; }
        public int maximumDamage { get; set; }
        public Weapon(int itemTypeID, string name, int price, int minDamage, int maxDamage)
           : base(itemTypeID, name, price)
        {
            minimumDamage = minDamage;
            maximumDamage = maxDamage;
        }

        public new Weapon Clone()
        {
            //fix either with virtual/orveride or through composition- 5.1
            return new Weapon(itemTypeID, name, price, minimumDamage, maximumDamage);
        }
    }
}
