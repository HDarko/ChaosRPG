using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Classes
{
   public class Quest
    {
        public int iD { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public List<ItemQuantity> itemsToComplete { get; set; }

        public int rewardExperiencePoints { get; set; }
        public int rewardGold { get; set; }
        public List<ItemQuantity> rewardItems { get; set; }

        public Quest(int id, string questName, string questDescription, List<ItemQuantity> itemsToCompleteQuest,
                     int questExperiencePoints, int questRewardGold, List<ItemQuantity> questRewardItems)
        {
            iD = id;
            name = questName;
            description = questDescription;
            itemsToComplete = itemsToCompleteQuest;
            rewardExperiencePoints = questExperiencePoints;
            rewardGold = questRewardGold;
            rewardItems = questRewardItems;
        }
    }
}
