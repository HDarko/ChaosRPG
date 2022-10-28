using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosEngine.Factories;
namespace ChaosEngine.Models
{
    public class ItemQuantity
    {
        readonly GameItem _item = null;
        public int ItemID => _item.ItemTypeID;
        public int Quantity { get; }
        public bool isWeapon { get; }

        public string ItemDescription =>
            $"{Quantity} {(_item.Name)}";

        public ItemQuantity(GameItem item, int itemQuantity, bool itemIsWeapon = false)
        {
            _item = item;
            Quantity = itemQuantity;
            isWeapon = itemIsWeapon;
        }
    }
}
