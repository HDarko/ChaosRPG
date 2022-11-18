using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using ChaosEngine.Models;
using ChaosEngine.Shared;

namespace ChaosEngine.Services.Factories
{
   public static class QuestFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Quests.xml";

        private static readonly List<Quest> _quests = new List<Quest>();

        static QuestFactory()
        {

            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                LoadQuestsFromNodes(data.SelectNodes("/Quests/Quest"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
        }

        private static void LoadQuestsFromNodes(XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {
                // Declare the items need to complete the quest, and its reward items
                List<ItemQuantity> itemsToComplete = new List<ItemQuantity>();
                List<ItemQuantity> rewardItems = new List<ItemQuantity>();

                foreach (XmlNode childNode in node.SelectNodes("./ItemsToComplete/Item"))
                {
                    GameItem item = ItemFactory.CreateGameItem(childNode.GetXmlAttributeAsInt("ID"));

                    itemsToComplete.Add(new ItemQuantity(item,
                                                         childNode.GetXmlAttributeAsInt("Quantity"),
                                                         childNode.GetXmlAttributeAsBool("IsWeapon", true,false)));
                }

                foreach (XmlNode childNode in node.SelectNodes("./RewardItems/Item"))
                {
                    GameItem item = ItemFactory.CreateGameItem(childNode.GetXmlAttributeAsInt("ID"));

                    rewardItems.Add(new ItemQuantity(item,
                                                     childNode.GetXmlAttributeAsInt("Quantity"),
                                                     childNode.GetXmlAttributeAsBool("IsWeapon", true, false)));
                }

                _quests.Add(new Quest(node.GetXmlAttributeAsInt("ID"),
                                      node.SelectSingleNode("./Name")?.InnerText ?? "",
                                      node.SelectSingleNode("./Description")?.InnerText ?? "",
                                      itemsToComplete,
                                      node.GetXmlAttributeAsInt("RewardExperiencePoints"),
                                      node.GetXmlAttributeAsInt("RewardGold"),
                                      rewardItems));
            }
        }
        internal static Quest GetQuestByID(int id)
        {
            return _quests.FirstOrDefault(quest => quest.ID == id);
        }
    }
}
