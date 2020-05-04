﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ChaosEngine.Classes
{
    public abstract class LivingEntity : BaseNotificationClass
    {
        private string _name;
        private int _currentHitPoints;
        private int _maximumHitPoints;
        private int _gold;
        private int _level;
        public bool IsDead => CurrentHitPoints <= 0;

        #region Properties
        public string Name
        {
            get { return _name; }
            private set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int CurrentHitPoints
        {
            get { return _currentHitPoints; }
            private set
            {
                _currentHitPoints = value;
                OnPropertyChanged(nameof(CurrentHitPoints));
            }
        }

        public int MaximumHitPoints
        {
            get { return _maximumHitPoints; }
            protected set
            {
                _maximumHitPoints = value;
                OnPropertyChanged(nameof(MaximumHitPoints));
            }
        }

        public int Gold
        {
            get { return _gold; }
            private set
            {
                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }

        public int Level
        {
            get { return _level; }
            protected set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }

        public List<GameItem> Inventory { get;  }
        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get;  }
        public ObservableCollection<Weapon> Weapons { get; set; }

        #endregion

        public event EventHandler OnKilled;
        protected LivingEntity(string name, int maximumHitPoints, int currentHitPoints, int gold, int level=1)
        {
            Name = name;
            MaximumHitPoints = maximumHitPoints;
            CurrentHitPoints = currentHitPoints;
            Gold = gold;
            Level = level;
            Inventory = new List<GameItem>();
            Weapons = new ObservableCollection<Weapon>();
            GroupedInventory = new ObservableCollection<GroupedInventoryItem>();
        }
        //Will try to keep one instance of everything in inventory but correct num in GroupedInventory
        public void AddItemToInventory(GameItem item, int quantity=1)
        {
           

            if (item.IsUnique)
            {
                GroupedInventory.Add(new GroupedInventoryItem(item,1));
                Inventory.Add(item);
            }
            else
            {
                if (!GroupedInventory.Any(gi => gi.Item.ItemTypeID == item.ItemTypeID))
                {
                    GroupedInventory.Add(new GroupedInventoryItem(item,1));
                    Inventory.Add(item);
                    quantity -= 1;
                }
                
                GroupedInventory.First(gi => gi.Item.ItemTypeID == item.ItemTypeID).Quantity+=quantity;
            }

           
        }

        //The binding in Views will prevent us from going over or under
        public void RemoveItemFromInventory(GameItem item, int quantity=1)
        {
            
            GroupedInventoryItem groupedInventoryItemToRemove = item.IsUnique ?
               GroupedInventory.FirstOrDefault(gi => gi.Item == item) :
               GroupedInventory.FirstOrDefault(gi => gi.Item.ItemTypeID == item.ItemTypeID);

            if (groupedInventoryItemToRemove != null)
            {
                groupedInventoryItemToRemove.Quantity -= quantity;
                if(groupedInventoryItemToRemove.Quantity==0)
                {
                    GroupedInventory.Remove(groupedInventoryItemToRemove);
                    Inventory.Remove(item);
                }
            }

          
        }
        public void TakeDamage(int hitPointsOfDamage)
        {
            CurrentHitPoints -= hitPointsOfDamage;

            if (IsDead)
            {
                CurrentHitPoints = 0;
                RaiseOnKilledEvent();
            }
        }

        public void Heal(int hitPointsToHeal)
        {
            CurrentHitPoints += hitPointsToHeal;

            if (CurrentHitPoints > MaximumHitPoints)
            {
                CurrentHitPoints = MaximumHitPoints;
            }
        }

        public void CompletelyHeal()
        {
            CurrentHitPoints = MaximumHitPoints;
        }

        public void ReceiveGold(int amountOfGold)
        {
            Gold += amountOfGold;
        }

        public void SpendGold(int amountOfGold)
        {
            if (amountOfGold > Gold)
            {
                throw new ArgumentOutOfRangeException($"{Name} only has {Gold} gold, and cannot spend {amountOfGold} gold");
            }

            Gold -= amountOfGold;
        }
        public void AddWeaponToWeapons(Weapon weapon)
        {
            Weapons.Add(weapon);

            OnPropertyChanged(nameof(Weapons));
        }
        public void RemoveWeaponToWeapons(Weapon weapon)
        {
            Weapons.Remove(weapon);

            OnPropertyChanged(nameof(Weapons));
        }

        private void RaiseOnKilledEvent()
        {
            OnKilled?.Invoke(this, new System.EventArgs());
        }
    }
}

