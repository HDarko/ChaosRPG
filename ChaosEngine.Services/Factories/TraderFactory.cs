using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ChaosEngine.Models;
using ChaosEngine.Shared;

namespace ChaosEngine.Services.Factories
{
    public class TraderFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Traders.xml";
        private static readonly List<Trader> _traders = new List<Trader>();

        static TraderFactory()
        {

            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                LoadTradersFromNodes(data.SelectNodes("/Traders/Trader"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }
        

    private static void LoadTradersFromNodes(XmlNodeList nodes)
    {
        foreach (XmlNode node in nodes)
        {
            Trader trader =
                new Trader(node.GetXmlAttributeAsInt("ID"),
                           node.SelectSingleNode("./Name")?.InnerText ?? "",
                           node.GetXmlAttributeAsBool("HasWeapons"));

            foreach (XmlNode childNode in node.SelectNodes("./InventoryItems/Item"))
            {
                
                    trader.AddItemToInventory(ItemFactory.CreateGameItem(childNode.GetXmlAttributeAsInt("ID")),
                                                         childNode.GetXmlAttributeAsInt("Quantity"));
            }

            foreach (XmlNode childNode in node.SelectNodes("./Weapons/Weapon"))
                {
                    trader.AddWeaponToWeapons(WeaponFactory.CreateWeapon(childNode.GetXmlAttributeAsInt("ID")));
                }

            AddTraderToList(trader);
        }
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

    public static Trader GetTraderByID(int id)
    {
        return _traders.FirstOrDefault(t => t.ID == id);
    }
}
}
