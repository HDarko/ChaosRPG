using System.Collections.Generic;
using ChaosEngine.Factories;
using ChaosEngine.Services;

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
                       int rewardExpPoints, int gold, int dexterity) :
            base(name, maximumHitPoints, maximumHitPoints, gold, dexterity)
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
            _lootTable.RemoveAll(ip => ip.ID == id);

            _lootTable.Add(new LootPercentage(id, percentage, quantity));
        }

        public Monster GetNewInstance()
        {
            // "Clone" this monster to a new Monster object
            Monster newMonster =
                new Monster(ID, Name, ImageName, MaximumHitPoints,
                            RewardExperiencePoints, Gold, Dexterity);

            foreach (LootPercentage lootPercentage in _lootTable)
            {
                // Clone the loot table - even though we probably won't need it
                newMonster.AddItemToLootTable(lootPercentage.ID, lootPercentage.Percentage, lootPercentage.Quantity);

                // Populate the new monster's inventory, using the loot table
                if (DiceService.Instance.Roll(100,1).Value <= lootPercentage.Percentage)
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
