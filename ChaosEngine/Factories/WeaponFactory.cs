using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosEngine.Classes;

namespace ChaosEngine.Factories
{
    class WeaponFactory
    {
        private static List<Weapon> _allweaponsinGame;

        static WeaponFactory()
        {
            _allweaponsinGame = new List<Weapon>();

            _allweaponsinGame.Add(new Weapon(1001, "Frail Stick", 1, 1, 2));
            _allweaponsinGame.Add(new Weapon(1002, "Thick Stick", 3, 1, 4));
            _allweaponsinGame.Add(new Weapon(1003, "Stonka Stick", 5, 3, 4));
            _allweaponsinGame.Add(new Weapon(1004, "Old Rusty Sword", 5, 1, 8));
            _allweaponsinGame.Add(new Weapon(1005, "Sharpened Spade", 6, 5, 6));


        }

        public static Weapon CreateWeapon(int weaponTypeID)
        {
            Weapon newWeapon = _allweaponsinGame.FirstOrDefault(weapon => weapon.ItemTypeID == weaponTypeID);

            if (newWeapon != null)
            {   
                    return newWeapon.Clone();
            }
            return null;
        }

    }
}
