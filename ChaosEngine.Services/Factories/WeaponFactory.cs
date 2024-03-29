﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using ChaosEngine.Models;
using ChaosEngine.Models.Actions;
using ChaosEngine.Shared;

namespace ChaosEngine.Services.Factories
{
    public static class WeaponFactory
    {
        private static List<Weapon> _allweaponsinGame;
        private const string GAME_DATA_FILENAME = ".\\GameData\\GameWeapons.xml";

        static WeaponFactory()
        {
            _allweaponsinGame = new List<Weapon>();

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
                             node.GetXmlAttributeAsString("DamageDice")
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
                                        string damageDice)
        {
            Weapon newWeapon = new Weapon(id, name, price);
            newWeapon.Action = new AttackWithWeapon(newWeapon, damageDice);
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
