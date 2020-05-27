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

        public string ToolTipContents =>
            Description + Environment.NewLine + Environment.NewLine +
            "Items to complete the quest" + Environment.NewLine +
            "===========================" + Environment.NewLine +
            string.Join(Environment.NewLine, ItemsToComplete.Select(i => i.ItemDescription)) +
            Environment.NewLine + Environment.NewLine +
            "Rewards\r\n" +
            "===========================" + Environment.NewLine +
            $"{RewardExperiencePoints} experience points" + Environment.NewLine +
            $"{RewardGold} gold pieces" + Environment.NewLine +
            string.Join(Environment.NewLine, RewardItems.Select(i => i.ItemDescription));

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
