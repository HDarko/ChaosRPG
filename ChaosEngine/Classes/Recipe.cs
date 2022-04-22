using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChaosEngine.Classes
{
    public class Recipe
    {
        public int ID { get; }
        [JsonIgnore]
        public string Name { get; }
        [JsonIgnore]
        public List<ItemQuantity> Ingredients { get; } = new List<ItemQuantity>();
        [JsonIgnore]
        public List<ItemQuantity> OutputItems { get; } = new List<ItemQuantity>();

        //Might replace item desciption in ToolTipContents to just quanity plus name in future
        [JsonIgnore]
        public string ToolTipContents =>
            "Ingredients" + Environment.NewLine +
            "===========" + Environment.NewLine +
            string.Join(Environment.NewLine, Ingredients.Select(i => i.ItemDescription)) +
            Environment.NewLine + Environment.NewLine +
            "Creates" + Environment.NewLine +
            "===========" + Environment.NewLine +
            string.Join(Environment.NewLine, OutputItems.Select(i => i.ItemDescription));

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
