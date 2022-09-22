using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChaosEngine.Models
{
    public class GroupedInventoryItem: BaseNotificationClass
    {
        private GameItem _item;
        private int _quantity;
        private int _quantityForTrade;

        public GameItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged(nameof(Item));
            }
        }
        public int QuantityForTrade
        {
            get { return _quantityForTrade; }
            set
            {
                _quantityForTrade = value;
                OnPropertyChanged(nameof(QuantityForTrade));
            }
        }
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
                QuantityForTrade = value;
            }
        }

       
        public GroupedInventoryItem(GameItem item, int quantity=1)
        {
            Item = item;
            Quantity = quantity;
        }
    }
}
