using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Classes
{
    public class ItemQuantity
    {
        public int ItemID { get;  }
        public int Quantity { get; }
        public bool isWeapon { get; }

        public ItemQuantity(int itID, int itemQuantity, bool itemIsWeapon=false)
        {
            ItemID = itID;
            Quantity = itemQuantity;
            isWeapon = itemIsWeapon;
        }
    }
}
