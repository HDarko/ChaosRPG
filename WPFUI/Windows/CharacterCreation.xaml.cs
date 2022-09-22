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
using ChaosEngine.Services;
using ChaosEngine.Models;
using ChaosEngine.Managers;

namespace WPFUI.Windows
{
    /// <summary>
    /// Interaction logic for CharacterCreation.xaml
    /// </summary>
    public partial class CharacterCreation : Window
    {
        private CharacterCreationManager _manager { get; set; }

        public CharacterCreation()
        {
            InitializeComponent();
            _manager = new CharacterCreationManager();
            DataContext = _manager;
        }

        private void RandomPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            _manager.RollNewCharacter();
        }
        private void UseThisPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_manager.GetPlayer());
            mainWindow.Show();
            Close();
        }
        private void Race_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _manager.ApplyAttributeModifiers();
        }
    }
}
