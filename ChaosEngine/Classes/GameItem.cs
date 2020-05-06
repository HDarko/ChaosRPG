
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosEngine.Classes.Actions;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ChaosEngine.Classes
{
    public class GameItem
    {
        public enum ItemCategory
        {
            Miscellaneous,
            Weapon
        }
        public int ItemTypeID { get;  }
        public string Name { get;  }
        public int Price { get;}
        public IAction ItemAction { get; set; }
        public bool IsUnique { get; }
        public ItemCategory Category { get; }
        public GameItem(int itemID, ItemCategory category, string itemName, int itemPrice, bool isUnique=false, IAction itemAction=null)
        {
            ItemTypeID = itemID;
            Category = category;
            Name = itemName;
            Price = itemPrice;
            IsUnique = isUnique;
            ItemAction = itemAction;
        }

        public void PerformAction(LivingEntity actor, LivingEntity target)
        {
            ItemAction?.Execute(actor, target);
        }
        public GameItem Clone()
        {
            return new GameItem(ItemTypeID, Category, Name, Price, IsUnique,ItemAction);
        }
    }
}
