
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ChaosEngine.Classes.Actions;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ChaosEngine.Classes
{
    public class GameItem
    {
        public enum ItemCategory
        {
            Miscellaneous,
            Weapon,
            Consumable
        }
        public int ItemTypeID { get;  }
        [JsonIgnore]
        public string Name { get;  }
        [JsonIgnore]
        public int Price { get;}
        [JsonIgnore]
        public IAction Action { get; set; }
        [JsonIgnore]
        public bool IsUnique { get; }
        [JsonIgnore]
        public ItemCategory Category { get; }
        public GameItem( ItemCategory category, int itemID, string itemName, int itemPrice, bool isUnique=false, IAction itemAction=null)
        {
            ItemTypeID = itemID;
            Category = category;
            Name = itemName;
            Price = itemPrice;
            IsUnique = isUnique;
            Action = itemAction;
        }

        public void PerformAction(LivingEntity actor, LivingEntity target)
        {
            Action?.Execute(actor, target);
        }
        public GameItem Clone()
        {
            return new GameItem( Category, ItemTypeID, Name, Price, IsUnique,Action);
        }
    }
}
