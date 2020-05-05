using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosEngine.Classes;
using ChaosEngine.Classes.Commands;
namespace ChaosEngine.Factories
{
    class WeaponFactory
    {
        private static List<Weapon> _allweaponsinGame;

        static WeaponFactory()
        {
            _allweaponsinGame = new List<Weapon>();

            BuildWeapon(1001, "Frail Stick", 1, 1, 2);
            BuildWeapon(1002, "Thick Stick", 3, 1, 4);
            BuildWeapon(1003, "Stonka Stick", 5, 3, 4);
            BuildWeapon(1004, "Old Rusty Sword", 5, 1, 8);
            BuildWeapon(1005, "Sharpened Spade", 6, 5, 6);


        }

        public static void BuildWeapon(int id, string name, int price,
                                        int minimumDamage, int maximumDamage)
        {
            Weapon newWeapon = new Weapon(id, name, price);
            newWeapon.Action = new AttackWithWeapon(newWeapon,minimumDamage, maximumDamage);
            _allweaponsinGame.Add(newWeapon);
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
