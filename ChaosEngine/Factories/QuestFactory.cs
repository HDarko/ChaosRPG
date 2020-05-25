using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;
using System.Threading.Tasks;
using ChaosEngine.Classes;
using ChaosEngine.Shared;

namespace ChaosEngine.Factories
{
   internal static class QuestFactory
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
            // Declare the items need to complete the quest, and its reward items
            List<ItemQuantity> itemsToComplete = new List<ItemQuantity>();
            List<ItemQuantity> rewardItems = new List<ItemQuantity>();

            itemsToComplete.Add(new ItemQuantity(3000, 3));
            rewardItems.Add(new ItemQuantity(3001, 1));

            // Create the quest
            _quests.Add(new Quest(0,
                                  "Have an adventure!",
                                  "Beat all the quests and get their tokens",
                                  itemsToComplete,
                                  25, 10,
                                  rewardItems));
            _quests.Add(new Quest(1,
                                  "Mushroom Hunt!",
                                  "Defeat Turkeysaurs for Korra and bring her 3 turkeysaur legs",
                                  new List<ItemQuantity>{ new ItemQuantity(9001,3) },
                                  30, 12,
                                  new List<ItemQuantity> { new ItemQuantity(3000, 1)}));
            _quests.Add(new Quest(2,
                                  "Stop the Frogmen Raiders!",
                                  "Some Frogmen are harrasing the Kobold folk when they try to leave the village. Take them down!" +
                                  "and take their tounges as warning",
                                  new List<ItemQuantity> { new ItemQuantity(9005, 3) },
                                  40, 22,
                                  new List<ItemQuantity> { new ItemQuantity(3000, 1) }));
            _quests.Add(new Quest(3,
                                 "Attack of a Jungle God!",
                                 "A mighty Jung-Beast has taken the jungle as its new territory!" +
                                 " Can you be the hero the kobolds need?",
                                 new List<ItemQuantity> { new ItemQuantity(9007, 3) },
                                 50, 40,
                                 new List<ItemQuantity> { new ItemQuantity(3000, 1) }));

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
                    if(childNode.GetXmlAttributeAsBool("IsWeapon",true))
                    itemsToComplete.Add(new ItemQuantity(childNode.GetXmlAttributeAsInt("ID"),
                                                         childNode.GetXmlAttributeAsInt("Quantity"),
                                                         childNode.GetXmlAttributeAsBool("IsWeapon", true,false)));
                }

                foreach (XmlNode childNode in node.SelectNodes("./RewardItems/Item"))
                {
                    rewardItems.Add(new ItemQuantity(childNode.GetXmlAttributeAsInt("ID"),
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
