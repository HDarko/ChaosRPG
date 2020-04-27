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
            newWorld.LocationAt(0, 0).QuestsAvailableHere.Add(QuestFactory.GetQuestByID(0));

            newWorld.AddLocation(-1, 1, "Bridge",
                "The path to the outside world.",
                "Bridge.jpg");
            newWorld.LocationAt(-1,1).AddMonster(2,100);

            newWorld.AddLocation(-2, 1, "Farm",
                "A recently abandoned farm.",
                "Home.png");

            newWorld.AddLocation(0, 1, "Home",
                "This is your cave village",
                "CaveTown.png");

            newWorld.AddIntroLocation2(0, 2, "Korra the Mage",
                "She seems excited as always.",
                "/ChaosEngine;component/Images/Avatars/herbalist.jpg");
            newWorld.LocationAt(0, 2).TraderHere =
               TraderFactory.GetTraderByName("Korra the Mage");
            //Quests
            newWorld.LocationAt(0, 2).QuestsAvailableHere.Add(QuestFactory.GetQuestByID(1));
                newWorld.LocationAt(0, 2).QuestsAvailableHere.Add(QuestFactory.GetQuestByID(3));

            newWorld.AddIntroLocation2(1, 1, "Ikka the Trader",
                "Business as usual with Ikka",
                "/ChaosEngine;component/Images/Avatars/Kobold.png");
            newWorld.LocationAt(1, 1).QuestsAvailableHere.Add(QuestFactory.GetQuestByID(2));
            newWorld.LocationAt(1, 1).TraderHere =
               TraderFactory.GetTraderByName("Ikka the Trader");


            newWorld.AddLocation(-1, 2, "Forest",
                "A creepy place, filled with good resources",
                "Forest.png");
            newWorld.LocationAt(-1, 2).AddMonster(1, 95);
            newWorld.LocationAt(-1, 2).AddMonster(1, 15);

            return newWorld;
        }
    }
}
