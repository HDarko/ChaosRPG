using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Classes
{
    public class GameItem
    {
        public int ItemTypeID { get;  }
        public string Name { get;  }
        public int Price { get;}

        public bool IsUnique { get; }

        public GameItem(int itemID, string itemName, int itemPrice, bool isUnique=false)
        {
            ItemTypeID = itemID;
            Name = itemName;
            Price = itemPrice;
            IsUnique = isUnique;
        }

        public GameItem Clone()
        {
            return new GameItem(ItemTypeID, Name, Price, IsUnique);
        }
    }
}
