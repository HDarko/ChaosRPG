using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Classes
{
   public class Quest
    {
        public int ID { get; }
        public string Name { get; }
        public string Description { get; }

        public List<ItemQuantity> ItemsToComplete { get; }

        public int RewardExperiencePoints { get;  }
        public int RewardGold { get; }
        public List<ItemQuantity> RewardItems { get; }

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
