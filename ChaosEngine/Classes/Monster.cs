using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ChaosEngine.Classes
{
    public class Monster: LivingEntity
    {
        #region Properties    
        public string ImageName { get; set; }
       
        public int RewardExperiencePoints { get; private set; }
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        
        #endregion
        public Monster(string monName, string imageFileName, int startHitPoints,
            int maxHitPoints, int minDmg, int maxDmg,
            int rewardExpPoints, int rewardGoldAmount)
        {
            Name = monName;
            ImageName = string.Format("/ChaosEngine;component/Images/Monsters/{0}", imageFileName);
            //or
            // ImageName = $"/ChaosEngine;component/Images/Monsters/{imageName}";
            //If first fails then try this
            //string.Format("pack://application:,,,/Engine;component/Images/Monsters/{0}", imageName);
            MaximumHitPoints = maxHitPoints;
            CurrentHitPoints = startHitPoints;
            RewardExperiencePoints = rewardExpPoints;
            Gold = rewardGoldAmount;
            MinimumDamage = minDmg;
            MaximumDamage = maxDmg;

            
        }
    }
}
