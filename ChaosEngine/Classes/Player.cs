using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace ChaosEngine.Classes
{
    public class Player: BaseNotificationClass
    {
        //--------------------------------------Private properties-----------------------
        private string _name;
        private string _characterClass;
        private int _hitPoints;
        private int _experiencePoints;
        private int _level;
        private int _gold;

        //----------------------------------------Getter and Setters----------------------------

        public ObservableCollection<GameItem> inventory { get; set; }
        public ObservableCollection<Weapon> weapons { get; set; }
        public ObservableCollection<QuestStatus> quests { get; set; }

        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(name));
            }
        }
        public string characterClass
        {
            get { return _characterClass; }
            set
            {
                _characterClass = value;
                OnPropertyChanged(nameof(characterClass));
            }
        }

        public int hitPoints
        {
            get { return _hitPoints; }
            set
            {
                _hitPoints = value;
                OnPropertyChanged(nameof(hitPoints));
            }
        }

        public int experiencePoints
        {
            get { return _experiencePoints; }
            set
            {
                _experiencePoints = value;
                OnPropertyChanged(nameof(experiencePoints));
            }
        }
        public int level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged(nameof(level));
            }
        }

        public int gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged(nameof(gold));
            }
        }

        public void AddItemToInventory(GameItem item)
        {
            inventory.Add(item);

            OnPropertyChanged(nameof(inventory));
        }

        public void AddItemToWeapon(Weapon weaapon)
        {
            weapons.Add(weaapon);

            OnPropertyChanged(nameof(weapons));
        }
        public Player()
        {
            inventory = new ObservableCollection<GameItem>();
            weapons = new ObservableCollection<Weapon>();
            quests = new ObservableCollection<QuestStatus>();
        }
        //---------------------------------------------------------------------------------------------------
    }
}
