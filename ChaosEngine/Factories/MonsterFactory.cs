using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosEngine.Classes;
using ChaosEngine.Shared;
using System.IO;
using System.Xml;

namespace ChaosEngine.Factories
{
   internal static class MonsterFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\GameMonsters.xml";

        private static readonly List<Monster> _baseMonsters = new List<Monster>();
        static MonsterFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                string rootImagePath =
                    data.SelectSingleNode("/Monsters")
                        .GetXmlAttributeAsString("RootImagePath");

                LoadMonstersFromNodes(data.SelectNodes("/Monsters/Monster"), rootImagePath);
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }

        private static void LoadMonstersFromNodes(XmlNodeList nodes, string rootImagePath)
        {
            if (nodes == null)
            {
                return;
            }
            foreach (XmlNode node in nodes)
            {
                Monster monster =
                    new Monster(node.GetXmlAttributeAsInt("ID"),
                                node.GetXmlAttributeAsString("Name"),
                                $".{rootImagePath}{node.GetXmlAttributeAsString("ImageName")}",
                                node.GetXmlAttributeAsInt("MaxHitPoints"),
                                node.GetXmlAttributeAsInt("RewardExp"),
                                node.GetXmlAttributeAsInt("Gold"));

                LoadLootTableFromMonsterNode(node, monster);

                _baseMonsters.Add(monster);
            }
        }

        private static void LoadLootTableFromMonsterNode(XmlNode monsterNode, Monster monsterType)
        {
            XmlNodeList lootItemNodes = monsterNode.SelectNodes("./LootTable/LootItem");
            if (lootItemNodes != null)
            {
                foreach (XmlNode lootItemNode in lootItemNodes)
                {
                    /*If there is no Quanity attribute then the value is meant to be 1
                     */
                    int? getQuantity = lootItemNode.GetXmlAttributeAsInt("Quantity",true);
                    int quantity = getQuantity ?? 1;
                    monsterType.AddItemToLootTable(lootItemNode.GetXmlAttributeAsInt("ID"),
                                               lootItemNode.GetXmlAttributeAsInt("Percentage"),
                                               quantity
                                               );
                }
            }
        }

        private static void LoadWeaponsFromMonsterNode(XmlNode monsterNode, Monster monsterType)
        {
            XmlNodeList weaponNodes = monsterNode.SelectNodes("./Weapons/WeaponID");
            if (weaponNodes != null)
            {
                foreach (XmlNode weaponNode in weaponNodes)
                {
                    monsterType.AddWeaponToWeapons(
                        WeaponFactory.CreateWeapon(weaponNode.GetXmlAttributeAsInt("ID")));
                }
            }
        }

        public static Monster GetMonster(int id)
        {
            return _baseMonsters.FirstOrDefault(m => m.ID == id)?.GetNewInstance();
        }
    }
        
}
