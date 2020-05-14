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
using ChaosEngine.Classes;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameSession _gameSession= new GameSession();
        private readonly Dictionary<Key, Action> _userInputActions =
           new Dictionary<Key, Action>();
        public MainWindow()
        {
            InitializeComponent();
            InitializeUserInputActions();
            _gameSession.OnMessageRaised += OnGameMessageRaised;

            DataContext = _gameSession;

        }

        private void InitializeUserInputActions()
        {
            /*_userInputActions.Add(Key.W, () => _gameSession.MoveNorth());
            _userInputActions.Add(Key.A, () => _gameSession.MoveWest());
            _userInputActions.Add(Key.S, () => _gameSession.MoveSouth());
            _userInputActions.Add(Key.D, () => _gameSession.MoveEast());
            _userInputActions.Add(Key.Z, () => _gameSession.AttackCurrentMonster());
            _userInputActions.Add(Key.C, () => _gameSession.UseCurrentConsumable());*/
            //Movement
            _userInputActions.Add(Key.Up, () => _gameSession.MoveNorth());
            _userInputActions.Add(Key.Left, () => _gameSession.MoveWest());
            _userInputActions.Add(Key.Down, () => _gameSession.MoveSouth());
            _userInputActions.Add(Key.Right, () => _gameSession.MoveEast());
            //Actions
            _userInputActions.Add(Key.A, () => _gameSession.AttackCurrentMonster());
            _userInputActions.Add(Key.C, () => _gameSession.UseCurrentConsumable());
            //Shops
            _userInputActions.Add(Key.I, () => OnClick_DisplayItemTradeScreen(this, new RoutedEventArgs()));
            _userInputActions.Add(Key.K, () => OnClick_DisplayWeaponTradeScreen(this, new RoutedEventArgs()));
            //Navigating UI
            _userInputActions.Add(Key.E, () => SetTabFocusTo("InventoryTabItem"));
            _userInputActions.Add(Key.Q, () => SetTabFocusTo("QuestsTabItem"));
            _userInputActions.Add(Key.W, () => SetTabFocusTo("WeaponsTabItem"));
            _userInputActions.Add(Key.R, () => SetTabFocusTo("RecipesTabItem"));
        }

        private void SetTabFocusTo(string tabName)
        {
            foreach (object item in PlayerDataTabControl.Items)
            {
                if (item is TabItem tabItem)
                {
                    if (tabItem.Name == tabName)
                    {
                        tabItem.IsSelected = true;
                        return;
                    }
                }
            }
        }
        private void MainWindow_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (_userInputActions.ContainsKey(e.Key))
            {
                _userInputActions[e.Key].Invoke();
            }
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
            if (_gameSession.HasTrader)
            {
                ItemTradeScreen tradeScreen = new ItemTradeScreen();
                tradeScreen.Owner = this;
                tradeScreen.DataContext = _gameSession;
                tradeScreen.ShowDialog();
            }
            
        }

        private void OnClick_DisplayWeaponTradeScreen(object sender, RoutedEventArgs e)
        {
            if(_gameSession.TradeWeapons)
            {
                WeaponTradeScreen tradeScreen = new WeaponTradeScreen();
                tradeScreen.Owner = this;
                tradeScreen.DataContext = _gameSession;
                tradeScreen.ShowDialog();
            }
        
        }
       

        private void OnClick_UseCurrentConsumable(object sender, RoutedEventArgs e)
        {
            _gameSession.UseCurrentConsumable();
        }

        private void OnClick_Craft(object sender, RoutedEventArgs e)
        {
            Recipe recipe = ((FrameworkElement)sender).DataContext as Recipe;
            _gameSession.CraftItemUsing(recipe);
        }

    }
}
