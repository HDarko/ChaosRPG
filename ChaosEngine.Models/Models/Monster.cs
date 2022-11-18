using System.Collections.Generic;
using System.Xml;
using ChaosEngine.Core;

namespace ChaosEngine.Models
{
    public class Monster: LivingEntity
    {
        #region Properties    
        public int ID { get; }
        public string ImageName { get;  }
       
        public int RewardExperiencePoints { get; private set; }

        // public List<IAction> monsterActions { get; set; }
        public List<LootPercentage> LootTable =
           new List<LootPercentage>();

        #endregion

        public Monster(int id,string name, string imageFileName,
                       int maximumHitPoints,
                       int rewardExpPoints, int gold, IEnumerable<PlayerAttribute> attributes) :
            base(name, maximumHitPoints, maximumHitPoints, gold, attributes)
        {
            //ImageName = string.Format("/ChaosEngine;component/Images/Monsters/{0}", imageFileName);
            //or
            // ImageName = $"/ChaosEngine;component/Images/Monsters/{imageName}";
            //If first fails then try this
            //string.Format("pack://application:,,,/Engine;component/Images/Monsters/{0}", imageName);
            ID = id;
            ImageName = imageFileName;
            RewardExperiencePoints = rewardExpPoints;
     
        }

        public Monster Clone()
        {
            Monster newMonster = new Monster(ID, Name, ImageName, MaximumHitPoints, RewardExperiencePoints, Gold, Attributes);

            newMonster.LootTable.AddRange(LootTable);

            foreach (Weapon weapon in Weapons)
            {
                newMonster.AddWeaponToWeapons(weapon);
            }

            return newMonster;
        }

        public void UseWeaponOn(LivingEntity target)
        {
            //If there is no multiple weapons then Current Weapon must have been assigned
            //Else pick a weapon randomly and use it
            if (Weapons.Count > 1)
            {
                int index = DiceService.Instance.Roll(Weapons.Count - 1, 1).Value;
                CurrentWeapon = Weapons[index];
            }
            else
            {
                CurrentWeapon = Weapons[0];
            }
            CurrentWeapon.PerformAction(this, target);
        }

        public void AddItemToLootTable(int id, int percentage, int quantity)
        {
            // Remove the entry from the loot table,
            // if it already contains an entry with this ID
            LootTable.RemoveAll(ip => ip.ID == id);

            LootTable.Add(new LootPercentage(id, percentage, quantity));
        }
    }
}
