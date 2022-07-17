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
using ChaosEngine.Classes;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for WeaponsTradeScreen.xaml
    /// </summary>
    public partial class WeaponTradeScreen : Window
    {
        public GameSession Session => DataContext as GameSession;

        public WeaponTradeScreen()
        {
            InitializeComponent();
        }

        private void OnClick_Sell(object sender, RoutedEventArgs e)
        {
            Weapon weapon = ((FrameworkElement)sender).DataContext as Weapon;

            if (weapon != null)
            {
                Session.CurrentPlayer.ReceiveGold (weapon.Price);
                Session.CurrentTrader.AddWeaponToWeapons(weapon);
                Session.CurrentPlayer.RemoveWeaponFromWeapons(weapon);
            }
        }

        private void OnClick_Buy(object sender, RoutedEventArgs e)
        {
            Weapon weapon = ((FrameworkElement)sender).DataContext as Weapon;

            if (weapon != null)
            {
                if (Session.CurrentPlayer.Gold >= weapon.Price)
                {
                    Session.CurrentPlayer.SpendGold( weapon.Price);
                    Session.CurrentTrader.RemoveWeaponFromWeapons(weapon);
                    Session.CurrentPlayer.AddWeaponToWeapons(weapon);
                }
                else
                {
                    MessageBox.Show("You do not have enough gold");
                }
            }
        }

        private void OnClick_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
       
    }
}
