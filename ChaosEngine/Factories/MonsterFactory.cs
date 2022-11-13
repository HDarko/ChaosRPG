using System;
using System.Collections.Generic;
using System.Linq;
using ChaosEngine.Models;
using ChaosEngine.Shared;
using ChaosEngine.Services;
using ChaosEngine.Core.Services;
using System.IO;
using System.Xml;

namespace ChaosEngine.Factories
{
   internal static class MonsterFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\GameMonsters.xml";

        private static readonly GameDetails s_gameDetails;
        private static readonly List<Monster> s_baseMonsters = new List<Monster>();
        static MonsterFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                s_gameDetails = GameDetailsService.ReadGameDetails();
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
                var attributes = s_gameDetails.PlayerAttributes;
                attributes.First(a => a.Key.Equals("DEX")).BaseValue =
                    Convert.ToInt32(node.SelectSingleNode("./Dexterity").InnerText);
                attributes.First(a => a.Key.Equals("DEX")).ModifiedValue =
                    Convert.ToInt32(node.SelectSingleNode("./Dexterity").InnerText);
                Monster monster =
                    new Monster(node.GetXmlAttributeAsInt("ID"),
                                node.GetXmlAttributeAsString("Name"),
                                $".{rootImagePath}{node.GetXmlAttributeAsString("ImageName")}",
                                node.GetXmlAttributeAsInt("MaxHitPoints"),
                                node.GetXmlAttributeAsInt("RewardExp"),
                                node.GetXmlAttributeAsInt("Gold"),
                                attributes);

                LoadLootTableFromMonsterNode(node, monster);
                LoadWeaponsFromMonsterNode(node, monster);

                s_baseMonsters.Add(monster);
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
                    Weapon monsterWeapon = WeaponFactory.CreateWeapon(weaponNode.GetXmlAttributeAsInt("ID"));

                    monsterType.AddWeaponToWeapons(monsterWeapon);
                }
            }
        }
        public static Monster GetMonster(int id)
        {
            Monster newMonster = s_baseMonsters.FirstOrDefault(m => m.ID == id)?.Clone();

            foreach (LootPercentage lootPercentage in newMonster.LootTable)
            {
                // Populate the new monster's inventory, using the loot table
                if (DiceService.Instance.Roll(100).Value <= lootPercentage.Percentage)
                {
                    newMonster.AddItemToInventory(ItemFactory.CreateGameItem(lootPercentage.ID));
                }
            }

            return newMonster;
        }

        public static Monster GetMonsterFromLocation(Location location)
        {
            if (!location.MonstersHere.Any())
            {
                return null;
            }

            // Total the percentages of all monsters at this location.
            int totalChances = location.MonstersHere.Sum(m => m.chanceOfEncountering);

            // Select a random number between 1 and the total (in case the total chances is not 100).
            int randomNumber = DiceService.Instance.Roll(totalChances, 1).Value;
            ;
            // Loop through the monster list, 
            // adding the monster's percentage chance of appearing to the runningTotal variable.
            // When the random number is lower than the runningTotal,
            // that is the monster to return.
            int runningTotal = 0;

            foreach (MonsterEncounter monsterEncounter in location.MonstersHere)
            {
                runningTotal += monsterEncounter.chanceOfEncountering;

                if (randomNumber <= runningTotal)
                {
                    return MonsterFactory.GetMonster(monsterEncounter.monsterID);
                }
            }

            // If there was a problem, return the last monster in the list.
            return GetMonster(location.MonstersHere.Last().monsterID);
        }
    }
        
}
