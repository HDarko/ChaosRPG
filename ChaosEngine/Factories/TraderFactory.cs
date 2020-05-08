using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosEngine.Classes;

namespace ChaosEngine.Factories
{
    class TraderFactory
    {
        private static readonly List<Trader> _traders = new List<Trader>();

        static TraderFactory()
        {
            Trader korra = new Trader("Korra the Mage", false);
            korra.AddItemToInventory(ItemFactory.CreateGameItem(9010),6);
            korra.AddItemToInventory(ItemFactory.CreateGameItem(6001), 7);

            Trader ikka = new Trader("Ikka the Trader",true);
            ikka.AddItemToInventory(ItemFactory.CreateGameItem(9009),4);
            ikka.AddWeaponToWeapons(WeaponFactory.CreateWeapon(1002));
            ikka.AddWeaponToWeapons(WeaponFactory.CreateWeapon(1003));

            

            AddTraderToList(korra);
            AddTraderToList(ikka);
          
        }

        public static Trader GetTraderByName(string name)
        {
            return _traders.FirstOrDefault(t => t.Name == name);
        }

        private static void AddTraderToList(Trader trader)
        {
            if (_traders.Any(t => t.Name == trader.Name))
            {
                throw new ArgumentException($"There is already a trader named '{trader.Name}'");
            }

            _traders.Add(trader);
        }
    }
}
