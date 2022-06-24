using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChaosEngine.Classes
{
   public class Quest
    {
        public int ID { get; }
        [JsonIgnore]
        public string Name { get; }
        [JsonIgnore]
        public string Description { get; }
        [JsonIgnore]
        public List<ItemQuantity> ItemsToComplete { get; }
        [JsonIgnore]
        public int RewardExperiencePoints { get;  }
        [JsonIgnore]
        public int RewardGold { get; }
        [JsonIgnore]
        public List<ItemQuantity> RewardItems { get; }
        [JsonIgnore]
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
