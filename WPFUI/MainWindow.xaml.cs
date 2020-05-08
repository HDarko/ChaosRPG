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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChaosEngine.Managers;
using ChaosEngine.GameEvents;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameSession _gameSession= new GameSession();
        public MainWindow()
        {
            InitializeComponent();
            _gameSession.OnMessageRaised += OnGameMessageRaised;

            DataContext = _gameSession;

        }

        private void OnClick_Move(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string direction = button.Tag.ToString();
            switch(direction)
            {
                case "N":
                    _gameSession.MoveNorth();
                    break;
                case "W":
                    _gameSession.MoveWest();
                    break;
                case "E":
                    _gameSession.MoveEast();
                    break;
                case "S":
                    _gameSession.MoveSouth();
                    break;
                default:
                    break;
            }
        }
        private void OnClick_MoveNorth(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveNorth();
        }

        private void OnClick_MoveWest(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveWest();
        }

        private void OnClick_MoveEast(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveEast();
        }

        private void OnClick_MoveSouth(object sender, RoutedEventArgs e)
        {
            _gameSession.MoveSouth();
        }

        private void OnGameMessageRaised(object sender, GameMessageEvent e)
        {
            gameMessages.Document.Blocks.Add(new Paragraph(new Run(e.message)));
            gameMessages.ScrollToEnd();
        }

        private void OnClick_AttackMonster(object sender, RoutedEventArgs e)
        {
            _gameSession.AttackCurrentMonster();
        }

        private void OnClick_DisplayItemTradeScreen(object sender, RoutedEventArgs e)
        {
            ItemTradeScreen tradeScreen = new ItemTradeScreen();
            tradeScreen.Owner = this;
            tradeScreen.DataContext = _gameSession;
            tradeScreen.ShowDialog();
        }

        private void OnClick_DisplayWeaponTradeScreen(object sender, RoutedEventArgs e)
        {
            WeaponTradeScreen tradeScreen = new WeaponTradeScreen();
            tradeScreen.Owner = this;
            tradeScreen.DataContext = _gameSession;
            tradeScreen.ShowDialog();
        }

        private void OnClick_UseCurrentConsumable(object sender, RoutedEventArgs e)
        {
            _gameSession.UseCurrentConsumable();
        }

    }
}
