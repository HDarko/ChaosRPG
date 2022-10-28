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
        public GameItem Item { get; set; }
        public int Quantity { get; set; }
        public int QuantityForTrade { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
 
       
        public GroupedInventoryItem(GameItem item, int quantity=1)
        {
            Item = item;
            Quantity = quantity;
        }
    }
}
