﻿
using System.Linq;
using ChaosEngine.Models;
using ChaosEngine.Services.Factories;
using ChaosEngine.Core;
using ChaosEngine.Services;
using System.ComponentModel;
using Newtonsoft.Json;
using ChaosEngine.Models.UI;

namespace ChaosEngine.Managers
{
   public class GameSession: INotifyPropertyChanged
    {
        #region Fields
        private Location _currentLocation;
        private Monster _currentMonster;
        private Player _player;
        private Battle _currentBattle;
        #endregion

        private readonly MessageBroker _messageBroker = MessageBroker.GetInstance();

        public event PropertyChangedEventHandler PropertyChanged;

        #region Properties

        public string Version { get; } = "0.1.001";
        [JsonIgnore]
        public bool HasMonster => CurrentMonster != null;
        [JsonIgnore]
        public bool HasTrader => CurrentTrader != null;

        public string TraderShopIcon { get; } = $".{"/Images/Icons/"}{"pngwave2.png"}";

        public string WeaponShopIcon { get; } = $".{"/Images/Icons/"}{"pngwave.png"}";

        public bool TradeWeapons => (HasTrader && (CurrentTrader.weaponsAvailable));

        public Player CurrentPlayer
        {
            get { return _player; }
            set
            {
                if (_player != null)
                {
                    _player.OnKilled -= OnCurrentPlayerKilled;
                    _player.OnLeveledUp -= OnCurrentPlayerLeveledUp;
                   // _player.OnActionPerformed -= OnCurrentPlayerPerformedAction;
                }

                _player = value;

                if (_player != null)
                {
                    _player.OnKilled += OnCurrentPlayerKilled;
                    _player.OnLeveledUp += OnCurrentPlayerLeveledUp;
                  // _player.OnActionPerformed += OnCurrentPlayerPerformedAction;
                }
            }
        }
        [JsonIgnore]
        public World CurrentWorld { get; set; }

        public Location CurrentLocation
        {
            get => _currentLocation;
            set
            {
                _currentLocation = value;

                CompleteQuestsAtLocation();
                GivePlayerQuestsAtLocation();
                CurrentTrader = _currentLocation.TraderHere;
                CurrentMonster = MonsterFactory.GetMonsterFromLocation(_currentLocation);
                   
            }
        }
        [JsonIgnore]
        public Trader CurrentTrader { get; private set; }

        [JsonIgnore]
        public GameDetails GameDetails { get; private set; }

        public PopupDetails InventoryDetails { get; set; }

        public PopupDetails WeaponryDetails { get; set; }

        public PopupDetails PlayerDetails { get; set; }

        #region  Button Properties
        [JsonIgnore]
        public bool HasLocationToNorth=> 
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;

        [JsonIgnore]
        public bool HasLocationToEast=>
          CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;

        [JsonIgnore]
        public bool HasLocationToSouth=>
                 CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;
        [JsonIgnore]
        public bool HasLocationToWest=>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;

        #endregion
        //========================================================
        [JsonIgnore]
        public Monster CurrentMonster
        {
            get { return _currentMonster; }
            set
            {
                if (_currentBattle != null)
                {
                    _currentBattle.OnCombatVictory -= OnCurrentMonsterKilled;
                    _currentBattle.Dispose();
                    _currentBattle = null;
                }


                _currentMonster = value;

                if (_currentMonster != null)
                {
                    _currentBattle = new Battle(CurrentPlayer, CurrentMonster);
                    _currentBattle.OnCombatVictory += OnCurrentMonsterKilled;
                }
            }
        }

        #endregion
        //-------------------------------------------------------------------------------------------

        public GameSession(Player player, int xCoordinate, int yCoordinate)
        {
            PopulateGameDetails();
            CurrentPlayer = player;
            CurrentWorld = WorldFactory.CreateWorld(CurrentPlayer.Name);    
            CurrentLocation = CurrentWorld.LocationAt(xCoordinate, yCoordinate);

            // Setup popup window properties
            InventoryDetails = new PopupDetails
            {
                IsVisible = false,
                Top = 225,
                Left = 275,
                MinHeight = 75,
                MaxHeight = 175,
                MinWidth = 250,
                MaxWidth = 400
            };

            WeaponryDetails = new PopupDetails
            {
                IsVisible = false,
                Top = 225,
                Left = 275,
                MinHeight = 75,
                MaxHeight = 175,
                MinWidth = 250,
                MaxWidth = 400
            };

            PlayerDetails = new PopupDetails
            {
                IsVisible = false,
                Top = 10,
                Left = 10,
                MinHeight = 75,
                MaxHeight = 400,
                MinWidth = 265,
                MaxWidth = 400
            };
        }
        #region Move Functions

        public void MoveNorth()
        {
            if (HasLocationToNorth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
            }
        }
    

        public void MoveEast()
        {
            if (HasLocationToEast)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
            }
        }

        public void MoveSouth()
        {
            if (HasLocationToSouth)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
            }
        }

        public void MoveWest()
        {
            if (HasLocationToWest)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
            }
        }
        #endregion
        //====================================================================================================================
        private void GivePlayerQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID))
                {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));
                    _messageBroker.RaiseMessage("");
                    _messageBroker.RaiseMessage($"You receive the '{quest.Name}' quest");
                    _messageBroker.RaiseMessage(quest.Description);
                    _messageBroker.RaiseMessage("");
                    _messageBroker.RaiseMessage("Objective: Return with:");
                    foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                    {
                       _messageBroker.RaiseMessage($"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }

                    _messageBroker.RaiseMessage("And you will receive:");
                    _messageBroker.RaiseMessage($"{quest.RewardExperiencePoints} experience points");
                    _messageBroker.RaiseMessage($"{quest.RewardGold} gold");
                    foreach (ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        _messageBroker.RaiseMessage($"{itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }
                }
            }
        }

        private void CompleteQuestsAtLocation()
        {
            foreach (Quest quest in CurrentLocation.QuestsAvailableHere)
            {
                QuestStatus questToComplete =
                    CurrentPlayer.Quests.FirstOrDefault(q => q.PlayerQuest.ID == quest.ID &&
                                                             !q.IsCompleted);

                if (questToComplete != null)
                {
                    if (CurrentPlayer.HasAllTheseItems(quest.ItemsToComplete))
                    {
                        // Remove the quest completion items from the player's inventory
                        CurrentPlayer.RemoveItemsFromInventory(quest.ItemsToComplete);

                        _messageBroker.RaiseMessage("");
                        _messageBroker.RaiseMessage($"You completed the '{quest.Name}' quest");

                        // Give the player the quest rewards
                        _messageBroker.RaiseMessage($"You receive {quest.RewardExperiencePoints} experience points");
                        CurrentPlayer.AddExperience( quest.RewardExperiencePoints);

                        _messageBroker.RaiseMessage($"You receive {quest.RewardGold} gold");
                        CurrentPlayer.ReceiveGold(quest.RewardGold);

                        foreach (ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                            _messageBroker.RaiseMessage($"You receive a {rewardItem.Name}");
                            CurrentPlayer.AddItemToInventory(rewardItem);
                           
                        }

                        // Mark the Quest as completed
                        questToComplete.IsCompleted = true;
                    }
                }
            }
        }

        public void CraftItemUsing(Recipe recipe)
        {
            if (CurrentPlayer.HasAllTheseItems(recipe.Ingredients))
            {
                CurrentPlayer.RemoveRecipeIngredientsFromInventory(recipe.Ingredients);

                foreach (ItemQuantity itemQuantity in recipe.OutputItems)
                {
                    if(itemQuantity.isWeapon)
                    {
                        Weapon outputWeapon = WeaponFactory.CreateWeapon(itemQuantity.ItemID);
                        CurrentPlayer.AddWeaponToWeapons(outputWeapon);
                        _messageBroker.RaiseMessage($"You craft a {outputWeapon.Name}");
                    }
                    else
                    {
                        GameItem outputItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                        CurrentPlayer.AddItemToInventory(outputItem, itemQuantity.Quantity);
                        _messageBroker.RaiseMessage($"You craft {itemQuantity.Quantity} {outputItem.Name}");
                    }
                }
            }
            else
            {
                _messageBroker.RaiseMessage("You do not have the required ingredients:");
                foreach (ItemQuantity itemQuantity in recipe.Ingredients)
                    if(itemQuantity.isWeapon)
                    {
                        _messageBroker.RaiseMessage($"{itemQuantity.Quantity} {itemQuantity.ItemDescription}");
                    }
                else
                    {
                        _messageBroker.RaiseMessage($"{itemQuantity.Quantity} {itemQuantity.ItemDescription}");
                    }
            }
        }
        private void OnCurrentPlayerPerformedAction(object sender, string result)
        {
            _messageBroker.RaiseMessage(result);
        }
        private void OnCurrentMonsterPerformedAction(object sender, string result)
        {
            _messageBroker.RaiseMessage(result);
        }

        
        private void OnCurrentPlayerKilled(object sender, System.EventArgs eventArgs)
        {
            _messageBroker.RaiseMessage("");
            _messageBroker.RaiseMessage($"You have been slain");

            CurrentLocation = CurrentWorld.LocationAt(0, 0);
            CurrentPlayer.CompletelyHeal();
        }

        private void OnCurrentMonsterKilled(object sender, System.EventArgs eventArgs)
        {
            // Get another monster to fight
            CurrentMonster = MonsterFactory.GetMonsterFromLocation(_currentLocation);
        }
   

        public void UseCurrentConsumable()
        {
            if (CurrentPlayer.CurrentConsumable != null)
            {
                //To ensure messages for using consumable items works if the player isn’t in a battle.
                if (_currentBattle == null)
                {
                    CurrentPlayer.OnActionPerformed += OnConsumableActionPerformed;
                }
                CurrentPlayer.UseCurrentConsumableOnSelf();
                if (_currentBattle == null)
                {
                    CurrentPlayer.OnActionPerformed -= OnConsumableActionPerformed;
                }
            }
                
        }

        private void OnCurrentPlayerLeveledUp(object sender, System.EventArgs eventArgs)
        {
            _messageBroker.RaiseMessage($"You are now level {CurrentPlayer.Level}!");
        }

        private void OnConsumableActionPerformed(object sender, string result)
        {
            _messageBroker.RaiseMessage(result);
        }
        public void AttackCurrentMonster()
        {
            _currentBattle?.AttackOpponent();
        }

        private void PopulateGameDetails()
        {
           GameDetails = GameDetailsService.ReadGameDetails();
        }
    }
}