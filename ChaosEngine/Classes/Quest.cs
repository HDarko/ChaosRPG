using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Classes
{
    class Quest
    {
        public int iD { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public List<ItemQuantity> itemsToComplete { get; set; }

        public int rewardExperiencePoints { get; set; }
        public int rewardGold { get; set; }
        public List<ItemQuantity> rewardItems { get; set; }

        public Quest(int id, string itemName, string itemDescription, List<ItemQuantity> itemsToCompleteQuest,
                     int questExperiencePoints, int questRewardGold, List<ItemQuantity> questRewardItems)
        {
            iD = id;
            name = name;
            description = description;
            itemsToComplete = itemsToCompleteQuest;
            rewardExperiencePoints = questExperiencePoints;
            rewardGold = questRewardGold;
            rewardItems = questRewardItems;
        }
    }
}
