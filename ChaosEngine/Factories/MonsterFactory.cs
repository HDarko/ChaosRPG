using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosEngine.Classes;

namespace ChaosEngine.Factories
{
   internal static class MonsterFactory
    {
        public static Monster GetMonster(int monsterID)
        {
            switch (monsterID)
            {
                case 1:
                    Monster turkeySaur =
                        new Monster("Turkosaur", "Turkeydon.gif", 6, 4,1,3, 5, 2);

                    AddLootItem(turkeySaur, 9001, 75);
                    AddLootItem(turkeySaur, 9002, 25);

                    return turkeySaur;

                case 2:
                    Monster frogMan =
                        new Monster("Frogman", "Frog.gif", 11, 7,2,4, 8, 5);

                    AddLootItem(frogMan, 9003, 25);
                    AddLootItem(frogMan, 9004, 25);
                    AddLootItem(frogMan, 9005, 50);

                    return frogMan;

                case 3:
                    Monster forestBoss =
                        new Monster("Jung-Beast", "JungleBoss.gif", 16, 11,1,10, 15, 18);

                    AddLootItem(forestBoss, 9006, 15);
                    AddLootItem(forestBoss, 9007, 70);
                    AddLootItem(forestBoss, 9008, 15);

                    return forestBoss;

                default:
                    throw new ArgumentException(string.Format("MonsterType '{0}' does not exist", monsterID));
            }
        }

        private static void AddLootItem(Monster monster, int itemID, int percentage)
        {
            if (RandomNumberGenerator.NumberBetween(1, 100) <= percentage)
            {
                monster.inventory.Add(new ItemQuantity(itemID, 1));
            }
        }
    }
}
