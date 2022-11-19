using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace ChaosEngine.Models
{
    public abstract class LivingEntity : INotifyPropertyChanged
    {
        public string Name { set; get; }
        public int CurrentHitPoints { get; private set; }
        public int MaximumHitPoints { get; protected set; }
        public int Gold { get; private set; }

        public int Level { get; protected set; }
        private Weapon _currentWeapon;
        private GameItem _currentConsumable;
        public ObservableCollection<PlayerAttribute> Attributes { get; } =
           new ObservableCollection<PlayerAttribute>();

        public event PropertyChangedEventHandler PropertyChanged;

        [JsonIgnore]
        public string HitPoints => $"{CurrentHitPoints}/{MaximumHitPoints}";

        [JsonIgnore]
        public bool IsAlive => CurrentHitPoints > 0;
        [JsonIgnore]
        public bool IsDead => !IsAlive;
        public bool HasConsumable => Consumables.Any();

        #region Properties

        public Weapon CurrentWeapon
        {
            get => _currentWeapon; 
            set
            {
                if (_currentWeapon != null)
                {
                    _currentWeapon.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }

                _currentWeapon = value;

                if (_currentWeapon != null)
                {
                    _currentWeapon.Action.OnActionPerformed += RaiseActionPerformedEvent;
                }
            }
        }

        public GameItem CurrentConsumable
        {
            get => _currentConsumable;
            set
            {
                if (_currentConsumable != null)
                {
                    _currentConsumable.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }

                _currentConsumable = value;

                if (_currentConsumable != null)
                {
                    _currentConsumable.Action.OnActionPerformed += RaiseActionPerformedEvent;
                }
            }
        }

        public List<GameItem> Inventory { get;  }
        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get;  }
        public ObservableCollection<Weapon> Weapons { get; set; }
        public List<GameItem> Consumables =>
            Inventory.Where(i => i.Category == GameItem.ItemCategory.Consumable).ToList();

        #endregion

        public event EventHandler OnKilled;
        public event EventHandler<string> OnActionPerformed;
        protected LivingEntity(string name, int maximumHitPoints, int currentHitPoints, int gold,
            IEnumerable<PlayerAttribute> attributes, int level=1 )
        {
            Name = name;
            MaximumHitPoints = maximumHitPoints;
            CurrentHitPoints = currentHitPoints;
            Gold = gold;
            Level = level;
            Inventory = new List<GameItem>();
            Weapons = new ObservableCollection<Weapon>();
            GroupedInventory = new ObservableCollection<GroupedInventoryItem>();
            foreach (PlayerAttribute attribute in attributes)
            {
                Attributes.Add(attribute);
            }
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

        public void RemoveItemsFromInventory(List<ItemQuantity> itemQuantities)
        {
            foreach (ItemQuantity itemQuantity in itemQuantities)
            {
                
                    RemoveItemFromInventory(Inventory.First(item => item.ItemTypeID == itemQuantity.ItemID),
                        itemQuantity.Quantity);
             
            }
        }
        //Recipe Functions
        public bool HasAllTheseItems(List<ItemQuantity> items)
        {
            foreach (ItemQuantity item in items)
            {     //Check if the item is a non weapon item or not
                if (item.isWeapon)
                {
                    Weapon weapon = Weapons.FirstOrDefault(i => i.ItemTypeID == item.ItemID);
                    if (weapon == null) return false;
                }
                else
                {
                    GroupedInventoryItem groupedInventoryItem =
                   GroupedInventory.FirstOrDefault(i => i.Item.ItemTypeID == item.ItemID);
                    if (groupedInventoryItem == null) return false;
                    if (groupedInventoryItem.Quantity < item.Quantity)
                    {
                        return false;
                    }

                }
            }
            return true;
        }
         public void RemoveRecipeIngredientsFromInventory(List<ItemQuantity> itemQuantities)
        {
            foreach (ItemQuantity item in itemQuantities)
            {
                if(item.isWeapon)
                {
                    Weapon weapon = Weapons.FirstOrDefault(i => i.ItemTypeID == item.ItemID);
                   if(weapon!=null)
                    {
                        RemoveWeaponFromWeapons(weapon);
                    }
                    
                }
                else
                {
                    RemoveItemFromInventory(Inventory.First(i => i.ItemTypeID == item.ItemID),
                        item.Quantity);
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

        public void UseCurrentConsumableOnSelf()
        {
            CurrentConsumable.PerformAction(this, this);
            RemoveItemFromInventory(CurrentConsumable);
        }
        public void AddWeaponToWeapons(Weapon weapon)
        {
            Weapons.Add(weapon);
        }
        public void RemoveWeaponFromWeapons(Weapon weapon)
        {
            Weapons.Remove(weapon);
        }

        private void RaiseOnKilledEvent()
        {
            OnKilled?.Invoke(this, new System.EventArgs());
        }

        private void RaiseActionPerformedEvent(object sender, string result)
        {
            OnActionPerformed?.Invoke(this, result);
        }
    }
}

