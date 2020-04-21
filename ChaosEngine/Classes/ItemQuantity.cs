using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Classes
{
    public class ItemQuantity
    {
        public int itemID { get; set; }
        public int quantity { get; set; }

        public ItemQuantity(int itID, int itemQuantity)
        {
            itemID = itID;
            quantity = itemQuantity;
        }
    }
}
