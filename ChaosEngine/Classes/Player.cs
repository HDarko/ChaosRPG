using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChaosEngine.Classes
{
    public class Player: LivingEntity
    {
        //--------------------------------------Private properties-----------------------
      
        private string _characterClass;
       
        private int _experiencePoints;
        private int _level;
     

        //----------------------------------------Getter and Setters----------------------------

       
        public ObservableCollection<QuestStatus> Quests { get; set; }

        
        public string CharacterClass
        {
            get { return _characterClass; }
            set
            {
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
            }
        }

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set
            {
                _experiencePoints = value;
                OnPropertyChanged(nameof(ExperiencePoints));
            }
        }
        public int Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }


        public bool HasAllTheseItems(List<ItemQuantity> items)
        {
            foreach (ItemQuantity item in items)
            {
                if (Inventory.Count(i => i.ItemTypeID == item.ItemID) < item.Quantity)
                {
                    return false;
                }
            }

            return true;
        }
    public Player()
        {
            Quests = new ObservableCollection<QuestStatus>();
        }
        //---------------------------------------------------------------------------------------------------
    }
}
