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
        public Monster(string name, string imageFileName,
                       int maximumHitPoints, int currentHitPoints,
                       int minimumDamage, int maxmumDamage,
                       int rewardExpPoints, int gold) :
            base(name, maximumHitPoints, currentHitPoints, gold)
        {        
            ImageName = string.Format("/ChaosEngine;component/Images/Monsters/{0}", imageFileName);
            //or
            // ImageName = $"/ChaosEngine;component/Images/Monsters/{imageName}";
            //If first fails then try this
            //string.Format("pack://application:,,,/Engine;component/Images/Monsters/{0}", imageName);
            RewardExperiencePoints = rewardExpPoints;
            MinimumDamage = minimumDamage;
            MaximumDamage = maxmumDamage;

            
        }
    }
}
