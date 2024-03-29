﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using Microsoft.Win32;
using WPFUI.Windows;
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
using ChaosEngine.Models;
using ChaosEngine.Services;
using ChaosEngine.Core;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string SAVE_GAME_FILE_EXTENSION = "soscsrpg";
        private GameSession _gameSession;
        private Point? _dragStart;
        private readonly MessageBroker _messageBroker = MessageBroker.GetInstance();
        private readonly Dictionary<Key, Action> _userInputActions =
           new Dictionary<Key, Action>();
        public MainWindow(Player player, int xLocation = 0, int yLocation = 0)
        {
            InitializeComponent();
            InitializeUserInputActions();
            SetActiveGameSessionTo(new GameSession(player,xLocation, yLocation));

            // Enable drag for popup details canvases
            foreach (UIElement element in GameCanvas.Children)
            {
                if (element is Canvas)
                {
                    element.MouseDown += GameCanvas_OnMouseDown;
                    element.MouseMove += GameCanvas_OnMouseMove;
                    element.MouseUp += GameCanvas_OnMouseUp;
                }
            }

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
            _userInputActions.Add(Key.T, () => OnClick_DisplayItemTradeScreen(this, new RoutedEventArgs()));
            _userInputActions.Add(Key.Y, () => OnClick_DisplayWeaponTradeScreen(this, new RoutedEventArgs()));
            //Navigating UI
            _userInputActions.Add(Key.I, () => _gameSession.InventoryDetails.IsVisible = !_gameSession.InventoryDetails.IsVisible);
            _userInputActions.Add(Key.W, () => _gameSession.WeaponryDetails.IsVisible = !_gameSession.WeaponryDetails.IsVisible);
            _userInputActions.Add(Key.P, () => _gameSession.PlayerDetails.IsVisible = !_gameSession.PlayerDetails.IsVisible);
            _userInputActions.Add(Key.Q, () => SetTabFocusTo("QuestsTabItem"));
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
                e.Handled = true;
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


        private void ClosePlayerDetailsWindow_OnClick(object sender, RoutedEventArgs e)
        {
            _gameSession.PlayerDetails.IsVisible = false;
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

        private void StartNewGame_OnClick(object sender, RoutedEventArgs e)
        {
            Startup startup = new Startup();
            startup.Show();
            Close();
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

        private void SetActiveGameSessionTo(GameSession gameSession)
        {
            // Unsubscribe from OnMessageRaised, or we will get double messages
            _messageBroker.OnMessageRaised -= OnGameMessageRaised;
            _gameSession = gameSession;
            DataContext = _gameSession;
            // Clear out previous game's messages
            gameMessages.Document.Blocks.Clear();
            _messageBroker.OnMessageRaised += OnGameMessageRaised;
        }

        private void SaveGame_OnClick(object sender, RoutedEventArgs e)
        {
            SaveGame();
        }

        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            AskToSaveGame();
        }

        private void AskToSaveGame()
        {
            YesNoWindow message =
                new YesNoWindow("Save Game", "Do you want to save your game?");
            message.Owner = GetWindow(this);
            message.ShowDialog();
            if (message.ClickedYes)
            {
                SaveGame();
            }
        }

        private void SaveGame()
        {
            SaveFileDialog saveFileDialog =
                new SaveFileDialog
                {
                    InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,
                    Filter = $"Saved games (*.{SAVE_GAME_FILE_EXTENSION})|*.{SAVE_GAME_FILE_EXTENSION}"
                };
            if (saveFileDialog.ShowDialog() == true)
            {
              SaveGameService.Save(new GameState(_gameSession.CurrentPlayer,
                    _gameSession.CurrentLocation.XCoordinate,
                    _gameSession.CurrentLocation.YCoordinate), saveFileDialog.FileName);
            }
        }

        private void CloseInventoryWindow_OnClick(object sender, RoutedEventArgs e)
        {
            _gameSession.InventoryDetails.IsVisible = false;
        }

        private void CloseWeaponryWindow_OnClick(object sender, RoutedEventArgs e)
        {
            _gameSession.WeaponryDetails.IsVisible = false;
        }

        private void GameCanvas_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
            {
                return;
            }

            UIElement movingElement = (UIElement)sender;
            _dragStart = e.GetPosition(movingElement);
            movingElement.CaptureMouse();

            e.Handled = true;
        }

        private void GameCanvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragStart == null || e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            Point mousePosition = e.GetPosition(GameCanvas);
            UIElement movingElement = (UIElement)sender;

            // Don't let player move popup details off the board
            if (mousePosition.X < _dragStart.Value.X ||
                mousePosition.Y < _dragStart.Value.Y ||
                mousePosition.X > GameCanvas.ActualWidth - ((Canvas)movingElement).ActualWidth + _dragStart.Value.X ||
                mousePosition.Y > GameCanvas.ActualHeight - ((Canvas)movingElement).ActualHeight + _dragStart.Value.Y)
            {
                return;
            }

            Canvas.SetLeft(movingElement, mousePosition.X - _dragStart.Value.X);
            Canvas.SetTop(movingElement, mousePosition.Y - _dragStart.Value.Y);

            e.Handled = true;
        }

        private void GameCanvas_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var movingElement = (UIElement)sender;
            movingElement.ReleaseMouseCapture();
            _dragStart = null;

            e.Handled = true;
        }

    }
}