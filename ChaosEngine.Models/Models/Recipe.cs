using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ChaosEngine.Models
{
    public class Recipe
    {
        public int ID { get; }
        [JsonIgnore]
        public string Name { get; }
        [JsonIgnore]
        public List<ItemQuantity> Ingredients { get; }
        [JsonIgnore]
        public List<ItemQuantity> OutputItems { get; }

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

        public Recipe(int id, string name, List<ItemQuantity> ingredients, List<ItemQuantity> outputItems)
        {
            ID = id;
            Name = name;
            Ingredients = ingredients;
            OutputItems = outputItems;
        }
    }
}
