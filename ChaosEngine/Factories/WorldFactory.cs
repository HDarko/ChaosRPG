using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosEngine.Classes;

namespace ChaosEngine.Factories
{
    internal static class WorldFactory
    {
        internal static World CreateWorld(string playerName)
        {
            World newWorld = new World();

            newWorld.AddIntroLocation(0, 0, "Intro",playerName,
                "/ChaosEngine;component/Images/Avatars/Hero.jpg");
             //Quests
            newWorld.LocationAt(0, 2).QuestsAvailableHere.Add(QuestFactory.GetQuestByID(0));

            newWorld.AddLocation(-1, 1, "Bridge",
                "The path to the outside world.",
                "/ChaosEngine;component/Images/Locations/Bridge.jpg");

            newWorld.AddLocation(-2, 1, "Farm",
                "A recently abandoned farm.",
                "/ChaosEngine;component/Images/Locations/Home.png");

            newWorld.AddLocation(0, 1, "Home",
                "This is your cave village",
                "/ChaosEngine;component/Images/Locations/CaveTown.png");

            newWorld.AddLocation(0, 2, "Korra the Mage",
                "She seems excited as always.",
                "/ChaosEngine;component/Images/Avatars/herbalist.jpg");
            //Quests
           

            newWorld.AddLocation(0, 0, "Hero",
               "",
               "/ChaosEngine;component/Images/Avatars/Hero.jpg");

            newWorld.AddLocation(0, 2, "Ikka the Trader",
                "Business as usual with Ikka",
                "/ChaosEngine;component/Images/Avatars/Kobold.png");

            //newWorld.AddLocation(-1, 3, "Hand of Shrooms",
          //      "Korra is very proud of her pet project",
             //   "/ChaosEngine;component/Images/Locations/mushroomhand.png");

            newWorld.AddLocation(-1, 3, "Forest",
                "A creepy place, filled with good resources",
                "/ChaosEngine;component/Images/Locations/Forest.png");

            return newWorld;
        }
    }
}
