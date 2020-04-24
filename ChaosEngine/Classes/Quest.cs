using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Classes
{
   public class Quest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<ItemQuantity> ItemsToComplete { get; set; }

        public int RewardExperiencePoints { get; set; }
        public int RewardGold { get; set; }
        public List<ItemQuantity> RewardItems { get; set; }

        public Quest(int id, string questName, string questDescription, List<ItemQuantity> itemsToCompleteQuest,
                     int questExperiencePoints, int questRewardGold, List<ItemQuantity> questRewardItems)
        {
            ID = id;
            Name = questName;
            Description = questDescription;
            ItemsToComplete = itemsToCompleteQuest;
            RewardExperiencePoints = questExperiencePoints;
            RewardGold = questRewardGold;
            RewardItems = questRewardItems;
        }
    }
}
