using System;
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

        #region Properties
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int CurrentHitPoints
        {
            get { return _currentHitPoints; }
            set
            {
                _currentHitPoints = value;
                OnPropertyChanged(nameof(CurrentHitPoints));
            }
        }

        public int MaximumHitPoints
        {
            get { return _maximumHitPoints; }
            set
            {
                _maximumHitPoints = value;
                OnPropertyChanged(nameof(MaximumHitPoints));
            }
        }

        public int Gold
        {
            get { return _gold; }
            set
            {
                _gold = value;
                OnPropertyChanged(nameof(Gold));
            }
        }

        public List<GameItem> Inventory { get; set; }
        public ObservableCollection<GroupedInventoryItem> GroupedInventory { get; set; }
        public ObservableCollection<Weapon> Weapons { get; set; }

        #endregion
        protected LivingEntity()
        {
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
                }

                GroupedInventory.First(gi => gi.Item.ItemTypeID == item.ItemTypeID).Quantity+=(quantity-1);
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
    }
}

