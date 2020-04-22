using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ChaosEngine.Factories;

namespace ChaosEngine.Classes
{
    public class Location
    {
        public int xCoordinate { get; set; }
        public int yCoordinate { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string imageName { get; set; }
        public List<Quest> QuestsAvailableHere { get; set; } = new List<Quest>();


         public List<MonsterEncounter> monstersHere { get; set; } =
            new List<MonsterEncounter>();
        public void AddMonster(int monsterID, int chanceOfEncountering)
        {
            if (monstersHere.Exists(m => m.monsterID == monsterID))
            {
                // This monster has already been added to this location.
                // So, overwrite the ChanceOfEncountering with the new number.
                monstersHere.First(m => m.monsterID == monsterID)
                            .chanceOfEncountering = chanceOfEncountering;
            }
            else
            {
                // This monster is not already at this location, so add it.
                monstersHere.Add(new MonsterEncounter(monsterID, chanceOfEncountering));
            }
        }

        public Monster GetMonster()
        {
            if (!monstersHere.Any())
            {
                return null;
            }

            // Total the percentages of all monsters at this location.
            int totalChances = monstersHere.Sum(m => m.chanceOfEncountering);

            // Select a random number between 1 and the total (in case the total chances is not 100).
            int randomNumber = RandomNumberGenerator.NumberBetween(1, totalChances);

            // Loop through the monster list, 
            // adding the monster's percentage chance of appearing to the runningTotal variable.
            // When the random number is lower than the runningTotal,
            // that is the monster to return.
            int runningTotal = 0;

            foreach (MonsterEncounter monsterEncounter in monstersHere)
            {
                runningTotal += monsterEncounter.chanceOfEncountering;

                if (randomNumber <= runningTotal)
                {
                    return MonsterFactory.GetMonster(monsterEncounter.monsterID);
                }
            }

            // If there was a problem, return the last monster in the list.
            return MonsterFactory.GetMonster(monstersHere.Last().monsterID);
        }
    }
}

