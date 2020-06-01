using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ChaosEngine.Classes.Actions;
using ChaosEngine.Factories;

namespace ChaosEngine.Classes
{

    public class Monster: LivingEntity
    {
        #region Properties    

        public int ID { get; }
        public string ImageName { get;  }
       
        public int RewardExperiencePoints { get; private set; }

        // public List<IAction> monsterActions { get; set; }
        private readonly List<LootPercentage> _lootTable =
           new List<LootPercentage>();

        #endregion

        public Monster(int id,string name, string imageFileName,
                       int maximumHitPoints,
                       int rewardExpPoints, int gold) :
            base(name, maximumHitPoints, maximumHitPoints, gold)
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

        public void UseWeaponOn(LivingEntity target)
        {
            //If there is no multiple weapons then Current Weapon must have been assigned
            //Else pick a weapon randomly and use it
            if (Weapons.Count > 1)
            {
                int index = RandomNumberGenerator.NumberBetween(0, Weapons.Count - 1);
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
            _lootTable.RemoveAll(ip => ip.ID == id);

            _lootTable.Add(new LootPercentage(id, percentage, quantity));
        }

        public Monster GetNewInstance()
        {
            // "Clone" this monster to a new Monster object
            Monster newMonster =
                new Monster(ID, Name, ImageName, MaximumHitPoints,
                            RewardExperiencePoints, Gold);

            foreach (LootPercentage lootPercentage in _lootTable)
            {
                // Clone the loot table - even though we probably won't need it
                newMonster.AddItemToLootTable(lootPercentage.ID, lootPercentage.Percentage, lootPercentage.Quantity);

                // Populate the new monster's inventory, using the loot table
                if (RandomNumberGenerator.NumberBetween(1, 100) <= lootPercentage.Percentage)
                {
                    newMonster.AddItemToInventory(ItemFactory.CreateGameItem(lootPercentage.ID));
                }
                //Add the weaponry
                foreach(Weapon weapon in Weapons)
                {
                    newMonster.AddWeaponToWeapons(WeaponFactory.CreateWeapon(weapon.ItemTypeID));
                }
                
            }

            return newMonster;
        }
    }
}
