using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using ChaosEngine.Classes;
using ChaosEngine.Classes.Actions;
using ChaosEngine.Shared;
namespace ChaosEngine.Factories
{
    class WeaponFactory
    {
        private static List<Weapon> _allweaponsinGame;
        private const string GAME_DATA_FILENAME = ".\\GameData\\GameWeapons.xml";

        static WeaponFactory()
        {
            _allweaponsinGame = new List<Weapon>();

           /* BuildWeapon(1001, "Frail Stick", 1, 0, 2);
            BuildWeapon(1002, "Thick Stick", 3, 0, 4);
            BuildWeapon(1003, "Stonka Stick", 5, 3, 4);
            BuildWeapon(1004, "Old Rusty Sword", 5, 1, 8);
            BuildWeapon(1005, "Sharpened Spade", 6, 5, 6);
            //Turkeysaur
            BuildWeapon(1006, "Vicious Maul", 4, 2, 4);
            //FrogMan
            BuildWeapon(1007, "Lashing Tounge", 6, 0, 5);
            BuildWeapon(1008, "Frogman Claws", 3, 0, 3);
            //Jungle-Beast
            BuildWeapon(1009, "Raging Fist", 3, 0, 8);
            BuildWeapon(1010, "Fanged Roar", 3, 2, 7);*/
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                LoadWeaponsFromNodes(data.SelectNodes("/Weapons/Weapon"));
               
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }


        }

        private static void LoadWeaponsFromNodes(XmlNodeList nodes)
        {
            if (nodes == null)
            {
                return;
            }

            foreach (XmlNode node in nodes)
            {
                BuildWeapon( node.GetXmlAttributeAsInt("ID"),
                             node.GetXmlAttributeAsString("Name"),
                             node.GetXmlAttributeAsInt("Price"),
                             node.GetXmlAttributeAsInt("minDamage"),
                             node.GetXmlAttributeAsInt("maxDamage")
                            );
            }
        }


        //Might adapt this for different weapon efffects like poison or stun
        /*  private static GameItem.ItemCategory DetermineItemCategory(string itemType)
          {
              switch (itemType)
              {
                  case "HealingItem":
                      return GameItem.ItemCategory.Consumable;
                  case "MiscellaneousItem":
                      return GameItem.ItemCategory.Miscellaneous;
                  default:
                      return GameItem.ItemCategory.Miscellaneous;
              }
          }*/
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
        public static string WeaponName(int weaponID)
        {
            return _allweaponsinGame.FirstOrDefault(weap => weap.ItemTypeID == weaponID)?.Name ?? "";
        }

    }
}
