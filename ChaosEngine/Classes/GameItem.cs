using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool IsUnique { get; }
        public ItemCategory Category { get; }
        public GameItem(int itemID, ItemCategory category, string itemName, int itemPrice, bool isUnique=false)
        {
            ItemTypeID = itemID;
            Category = category;
            Name = itemName;
            Price = itemPrice;
            IsUnique = isUnique;
        }

        public GameItem Clone()
        {
            return new GameItem(ItemTypeID, Category, Name, Price, IsUnique);
        }
    }
}
