using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ChaosEngine.Classes.Actions;

namespace ChaosEngine.Classes
{
    public class Monster: LivingEntity
    {
        #region Properties    
        public string ImageName { get;  }
       
        public int RewardExperiencePoints { get; private set; }

       // public List<IAction> monsterActions { get; set; }
    
        
        #endregion
        public Monster(string name, string imageFileName,
                       int maximumHitPoints, int currentHitPoints,
                       int rewardExpPoints, int gold) :
            base(name, maximumHitPoints, currentHitPoints, gold)
        {        
            ImageName = string.Format("/ChaosEngine;component/Images/Monsters/{0}", imageFileName);
            //or
            // ImageName = $"/ChaosEngine;component/Images/Monsters/{imageName}";
            //If first fails then try this
            //string.Format("pack://application:,,,/Engine;component/Images/Monsters/{0}", imageName);
            RewardExperiencePoints = rewardExpPoints;
     
        }

        public void UseWeaponOn(LivingEntity target)
        {
            //If there is no multiple weapons then Current Weapon must have been assigned
            //Else pick a weapon randomly and use it
            if(Weapons.Count>0)
            {           
                int index=RandomNumberGenerator.NumberBetween(0, Weapons.Count - 1);
                CurrentWeapon = Weapons[index];
            }
            CurrentWeapon.PerformAction(this, target);
        }
        //If Monster weapons is null then use current weapon else pick a weapon at random from weapons to attack
    }
}
