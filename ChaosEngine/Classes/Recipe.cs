using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Classes
{
    public class Recipe
    {
        public int ID { get; }
        public string Name { get; }
        public List<ItemQuantity> Ingredients { get; } = new List<ItemQuantity>();
        public List<ItemQuantity> OutputItems { get; } = new List<ItemQuantity>();
        
        public Recipe(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public void AddIngredient(int itemID, int quantity)
        {
            if (!Ingredients.Any(x => x.ItemID == itemID))
            {
                Ingredients.Add(new ItemQuantity(itemID, quantity));
            }
            
        }

        public void AddWeaponIngredient(int weaponID)
        {
            if (!Ingredients.Any(x => x.ItemID == weaponID))
            {
                Ingredients.Add(new ItemQuantity(weaponID, 1,true));
            }

        }

        public void AddOutputItem(int itemID, int quantity)
        {
            if (!OutputItems.Any(x => x.ItemID == itemID))
            {
                OutputItems.Add(new ItemQuantity(itemID, quantity));
            }
        }

        public void AddOutputWeapon(int weaponID)
        {
            if (!OutputItems.Any(x => x.ItemID == weaponID))
            {
                OutputItems.Add(new ItemQuantity(weaponID, 1,true));
            }

        }
    }
}
