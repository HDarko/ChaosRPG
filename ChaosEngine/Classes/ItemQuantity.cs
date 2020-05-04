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

        public ItemQuantity(int itID, int itemQuantity)
        {
            ItemID = itID;
            Quantity = itemQuantity;
        }
    }
}
