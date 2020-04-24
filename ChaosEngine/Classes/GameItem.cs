using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Classes
{
    public class GameItem
    {
        public int ItemTypeID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public GameItem(int itemID, string itemName, int itemPrice)
        {
            ItemTypeID = itemID;
            Name = itemName;
            Price = itemPrice;
        }

        public GameItem Clone()
        {
            return new GameItem(ItemTypeID, Name, Price);
        }
    }
}
