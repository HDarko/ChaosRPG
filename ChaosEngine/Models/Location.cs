using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Newtonsoft.Json;

namespace ChaosEngine.Models
{
    public class Location
    {
        #region Properties
        public int XCoordinate { get;  }
        public int YCoordinate { get;  }
        [JsonIgnore]
        public string Name { get;  }
        [JsonIgnore]
        public string Description { get; }
        [JsonIgnore]
        public string ImageName { get;  }
        [JsonIgnore]
        public List<Quest> QuestsAvailableHere { get; } = new List<Quest>();
        [JsonIgnore]
        public Trader TraderHere { get; set; }

        public List<MonsterEncounter> MonstersHere { get; set; } =
            new List<MonsterEncounter>();
        #endregion

        public Location(int xCoordinate, int yCoordinate, string name, string description, string imageName)
        {
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
            Name = name;
            Description = description;
            ImageName = imageName;
        }
        public void AddMonster(int monsterID, int chanceOfEncountering)
        {
            if (MonstersHere.Exists(m => m.monsterID == monsterID))
            {
                // This monster has already been added to this location.
                // So, overwrite the ChanceOfEncountering with the new number.
                MonstersHere.First(m => m.monsterID == monsterID)
                            .chanceOfEncountering = chanceOfEncountering;
            }
            else
            {
                // This monster is not already at this location, so add it.
                MonstersHere.Add(new MonsterEncounter(monsterID, chanceOfEncountering));
            }
        }     
    }
}

