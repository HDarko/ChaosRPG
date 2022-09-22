using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ChaosEngine.Managers;
using ChaosEngine.Models;
using System.Text.RegularExpressions;
using System.Data;
using System.Windows.Controls.Primitives;
using Xceed.Wpf.Toolkit;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for TradeScreen.xaml
    /// </summary>
    public partial class ItemTradeScreen : Window
    {
        public GameSession Session => DataContext as GameSession;


        public ItemTradeScreen()
        {
            InitializeComponent();
        }

        private void OnClick_Sell(object sender, RoutedEventArgs e)
        {
            GroupedInventoryItem inventoryItem = ((FrameworkElement)sender).DataContext as GroupedInventoryItem;
            int amounttoSell = inventoryItem.QuantityForTrade;

            GameItem item = inventoryItem.Item;
           
            int fullPrice = item.Price * amounttoSell;
            if (item != null)
            {
                if (Session.CurrentPlayer.Gold >= fullPrice)
                {
                    Session.CurrentPlayer.ReceiveGold(fullPrice);
                    Session.CurrentTrader.AddItemToInventory(item,amounttoSell);
                    Session.CurrentPlayer.RemoveItemFromInventory(item,amounttoSell);
                }
            }
        }

        private void OnClick_Buy(object sender, RoutedEventArgs e)
        {
           // GameItem item = ((FrameworkElement)sender).DataContext as GameItem;
            GroupedInventoryItem inventoryItem = ((FrameworkElement)sender).DataContext as GroupedInventoryItem;
            GameItem item = inventoryItem.Item;
            int amounttoBuy = inventoryItem.QuantityForTrade;
            int fullPrice = item.Price * amounttoBuy;
          

            if (item != null)
            {
                if (Session.CurrentPlayer.Gold >= fullPrice)
                {
                    Session.CurrentPlayer.SpendGold(fullPrice);
                    Session.CurrentTrader.RemoveItemFromInventory(item,amounttoBuy);
                    Session.CurrentPlayer.AddItemToInventory(item,amounttoBuy);
                }
                else
                {
                    System.Windows.MessageBox.Show("You do not have enough gold");
                }
            }
        }
   

        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
}
}
