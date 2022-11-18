using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using ChaosEngine.Models;
using ChaosEngine.Shared;

namespace ChaosEngine.Services.Factories
{
    public static class WorldFactory
    {
        private const string GAME_DATA_FILENAME = ".\\GameData\\Locations.xml";
        public static World CreateWorld(string playerName)
        {
            World newWorld = new World();
            //Intro Specific
            newWorld.AddIntroLocation(0, 0, "Intro",playerName,
                "/Images/Avatars/Hero.jpg");
             
            newWorld.LocationAt(0, 0).QuestsAvailableHere.Add(QuestFactory.GetQuestByID(0));
            //Rest of the world

            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));
                string locationImagePath =
                    data.SelectSingleNode("/Locations/BasicLocations")
                        .GetXmlAttributeAsString("RootImagePath");

                string traderLocationImagePath =
                    data.SelectSingleNode("/Locations/TraderLocations").
                    GetXmlAttributeAsString("RootImagePath");

                LoadLocationsFromNodes(newWorld,
                                       locationImagePath,
                                       data.SelectNodes("/Locations/BasicLocations/Location"));
                LoadLocationsFromNodes(newWorld,
                                      traderLocationImagePath,
                                      data.SelectNodes("/Locations/TraderLocations/Location")); 
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }

            return newWorld;
        }

        private static void LoadLocationsFromNodes(World world, string rootImagePath, XmlNodeList nodes)
        {
            if (nodes == null)
            {
                return;
            }

            foreach (XmlNode node in nodes)
            {
                Location location =
                    new Location(node.GetXmlAttributeAsInt("X"),
                                 node.GetXmlAttributeAsInt("Y"),
                                 node.GetXmlAttributeAsString("Name"),
                                 node.SelectSingleNode("./Description")?.InnerText ?? "",
                                 $".{rootImagePath}{node.GetXmlAttributeAsString("ImageName")}");

                AddMonsters(location, node.SelectNodes("./Monsters/Monster"));
                AddQuests(location, node.SelectNodes("./Quests/Quest"));
                AddTrader(location, node.SelectSingleNode("./Trader"));

                world.AddLocation(location);
            }
        }

       
        private static void AddMonsters(Location location, XmlNodeList monsters)
        {
            if (monsters == null)
            {
                return;
            }

            foreach (XmlNode monsterNode in monsters)
            {
                location.AddMonster(monsterNode.GetXmlAttributeAsInt("ID"),
                                    monsterNode.GetXmlAttributeAsInt("Percent"));
            }
        }

        private static void AddQuests(Location location, XmlNodeList quests)
        {
            if (quests == null)
            {
                return;
            }

            foreach (XmlNode questNode in quests)
            {
                location.QuestsAvailableHere
                        .Add(QuestFactory.GetQuestByID(questNode.GetXmlAttributeAsInt("ID")));
            }
        }

        private static void AddTrader(Location location, XmlNode traderHere)
        {
            if (traderHere == null)
            {
                return;
            }

            location.TraderHere =
                TraderFactory.GetTraderByName(traderHere.GetXmlAttributeAsString("Name"));
        }
    }
}


