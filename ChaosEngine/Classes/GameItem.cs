using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Classes
{
    public class GameItem
    {
        public int itemTypeID { get; set; }
        public string name { get; set; }
        public int price { get; set; }

        public GameItem(int itemID, string itemName, int itemPrice)
        {
            itemTypeID = itemID;
            name = itemName;
            price = itemPrice;
        }

        public GameItem Clone()
        {
            return new GameItem(itemTypeID, name, price);
        }
    }
}
