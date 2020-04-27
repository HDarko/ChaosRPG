using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace ChaosEngine.Classes
{
    public class Trader
    {
        public string Name { get; set; }

        public ObservableCollection<GameItem> Inventory { get; set; }
        public ObservableCollection<Weapon> Weapons { get; set; }
       public bool weaponsAvailable = false;

        public Trader(string name,bool hasWeapons)
        {
            Name = name;
            Inventory = new ObservableCollection<GameItem>();
            if(hasWeapons)
            {
                weaponsAvailable = true;
                Weapons = new ObservableCollection<Weapon>();
            }
            else
            {
                weaponsAvailable = false;
                Weapons = null;
            }
        }

        public void AddItemToInventory(GameItem item, int quantity=1)
        {
            if(quantity<=0)
            {

                 throw new ArgumentException($"The value of '{quantity}' is less than 1");
               
            }
            for(int i=0; i<quantity; i++)
            {
                Inventory.Add(item);
            }
          
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory.Remove(item);
        }

        public void AddWeaponToWeapons(Weapon weapon)
        {
            if(weaponsAvailable)
            Weapons.Add(weapon);

            
        }

        public void RemoveWeaponToWeapons(Weapon weapon)
        {
            Weapons.Remove(weapon);

           
        }

    }
}
