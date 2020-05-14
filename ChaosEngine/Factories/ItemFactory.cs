using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using ChaosEngine.Classes;
using ChaosEngine.Classes.Actions;

namespace ChaosEngine.Factories
{
    public static class ItemFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\GameItems.xml";
       
        private static readonly List<GameItem> _standardGameItems= new List<GameItem>();

        static ItemFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                //LoadItemsFromNodes(data.SelectNodes("/GameItems/Weapons/Weapon"));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/HealingItems/HealingItem"));
                LoadItemsFromNodes(data.SelectNodes("/GameItems/MiscellaneousItems/MiscellaneousItem"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }


        }

        private static void LoadItemsFromNodes(XmlNodeList nodes)
        {
            if (nodes == null)
            {
                return;
            }

            foreach (XmlNode node in nodes)
            {
                GameItem.ItemCategory itemCategory = DetermineItemCategory(node.Name);

                GameItem gameItem =
                    new GameItem(itemCategory,
                                 GetXmlAttributeAsInt(node, "ID"),
                                 GetXmlAttributeAsString(node, "Name"),
                                 GetXmlAttributeAsInt(node, "Price"),
                                 false );         
                if (itemCategory == GameItem.ItemCategory.Consumable)
                {
                    gameItem.Action =
                        new Heal(gameItem,
                                 GetXmlAttributeAsInt(node, "HitPointsToHeal"));
                }

                _standardGameItems.Add(gameItem);
            }
        }

        private static GameItem.ItemCategory DetermineItemCategory(string itemType)
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
        }

        private static int GetXmlAttributeAsInt(XmlNode node, string attributeName)
        {
            return Convert.ToInt32(GetXmlAttribute(node, attributeName));
        }

        private static string GetXmlAttributeAsString(XmlNode node, string attributeName)
        {
            return GetXmlAttribute(node, attributeName);
        }

        private static string GetXmlAttribute(XmlNode node, string attributeName)
        {
            XmlAttribute attribute = node.Attributes?[attributeName];

            if (attribute == null)
            {
                throw new ArgumentException($"The attribute '{attributeName}' does not exist");
            }

            return attribute.Value;
        }

        public static GameItem CreateGameItem(int itemTypeID)
        {
            GameItem standardItem = _standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID);

            if (standardItem != null)
            {
                return standardItem;
            }
            return null;
        }

        private static void BuildMiscellaneousItem(int id, string name, int price)
        {
            _standardGameItems.Add(new GameItem( GameItem.ItemCategory.Miscellaneous,id, name, price));
        }

        private static void BuildHealingItem(int id, string name, int price, int hitPointsToHeal)
        {
            GameItem item = new GameItem(GameItem.ItemCategory.Consumable, id, name, price);
            item.Action = new Heal(item, hitPointsToHeal);
            _standardGameItems.Add(item);
        }

        public static string ItemName(int itemTypeID)
        {
            return _standardGameItems.FirstOrDefault(i => i.ItemTypeID == itemTypeID)?.Name ?? "";
        }

    }
}
