using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace ChaosEngine.Models
{
    public class GroupedInventoryItem: INotifyPropertyChanged
    {
        private GameItem _item;
        private int _quantity;
        private int _quantityForTrade;

        public event PropertyChangedEventHandler PropertyChanged;
        public GameItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
            }
        }
        public int QuantityForTrade
        {
            get { return _quantityForTrade; }
            set
            {
                _quantityForTrade = value;
            }
        }
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
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
