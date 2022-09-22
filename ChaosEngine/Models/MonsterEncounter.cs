using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Models
{
   public class MonsterEncounter
    {
        public int monsterID { get;  }
        public int chanceOfEncountering { get; set; }

        public MonsterEncounter(int monsID, int encounterChancePercent)
        {
            monsterID = monsID;
            chanceOfEncountering = encounterChancePercent;
        }
    }
}
