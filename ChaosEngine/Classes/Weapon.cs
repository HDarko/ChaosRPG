using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Classes
{
    public class Weapon : GameItem
    {
        public int MinimumDamage { get; }
        public int MaximumDamage { get;}
        public string DamageRange { get;}
        public Weapon(int itemTypeID, string name, int price, int minDamage, int maxDamage)
           : base(itemTypeID, name, price,true)
        {
            MinimumDamage = minDamage;
            MaximumDamage = maxDamage;
            DamageRange = $"{MinimumDamage}-{MaximumDamage}";
        }

        public new Weapon Clone()
        {
            //fix either with virtual/orveride or through composition- 5.1
            return new Weapon(ItemTypeID, Name, Price, MinimumDamage, MaximumDamage);
        }
    }
}
