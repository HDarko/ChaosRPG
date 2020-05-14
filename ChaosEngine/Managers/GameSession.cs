using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ChaosEngine.Classes;
using ChaosEngine.Factories;
using ChaosEngine.GameEvents;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ChaosEngine.Managers
{
   public class GameSession: BaseNotificationClass
    {
        #region Fields
        private Location _currentLocation;
        private Monster _currentMonster;
        public event EventHandler<GameMessageEvent> OnMessageRaised;
        private Player _player;
        private Trader _currentTrader;
        #endregion

        #region Properties

        public bool HasMonster => CurrentMonster != null;
        public bool HasTrader => CurrentTrader != null;
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
                    _player.OnActionPerformed -= OnCurrentPlayerPerformedAction;
                }

                _player = value;

                if (_player != null)
                {
                    _player.OnKilled += OnCurrentPlayerKilled;
                    _player.OnLeveledUp += OnCurrentPlayerLeveledUp;
                    _player.OnActionPerformed += OnCurrentPlayerPerformedAction;
                }
            }
        }

        public World CurrentWorld { get; set; }
        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;

                OnPropertyChanged(nameof(CurrentLocation));
                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToEast));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToSouth));
                CurrentTrader = CurrentLocation.TraderHere;
                CompleteQuestsAtLocation();
                GivePlayerQuestsAtLocation();
                GetMonsterAtLocation();
            }
        }

        public Trader CurrentTrader
        {
            get { return _currentTrader; }
            set
            {
                _currentTrader = value;

                OnPropertyChanged(nameof(CurrentTrader));
                OnPropertyChanged(nameof(HasTrader));
                OnPropertyChanged(nameof(TradeWeapons));
            }
        }


        #region  Button Properties
        public bool HasLocationToNorth=> 
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;
            

        public bool HasLocationToEast=>
          CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;
    

        public bool HasLocationToSouth=>
                 CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;

        public bool HasLocationToWest=>
            CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;
     
        #endregion
        //========================================================

        public Monster CurrentMonster
        {
            get { return _currentMonster; }
            set
            {
                if (_currentMonster != null)
                {
                    _currentMonster.OnActionPerformed -= OnCurrentMonsterPerformedAction;
                    _currentMonster.OnKilled -= OnCurrentMonsterKilled;
                }

                _currentMonster = value;

                if (_currentMonster != null)
                {
                    _currentMonster.OnActionPerformed += OnCurrentMonsterPerformedAction;
                    _currentMonster.OnKilled += OnCurrentMonsterKilled;

                    RaiseMessage("");
                    RaiseMessage($"You see a {CurrentMonster.Name} here!");
                }

                OnPropertyChanged(nameof(CurrentMonster));
                OnPropertyChanged(nameof(HasMonster));
                
            }
        }

        #endregion
        //-------------------------------------------------------------------------------------------
        public GameSession()
        {


            CurrentPlayer = new Player
            (
               "Player",
               "Paladin",
                15,
              10,
              10,
              20
            );
            
            if (!CurrentPlayer.Weapons.Any())
            {
                CurrentPlayer.AddWeaponToWeapons(WeaponFactory.CreateWeapon(1001));
            }
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(6001));
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(4000),4);
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(4001),4);
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(4002),4);
            CurrentPlayer.AddItemToInventory(ItemFactory.CreateGameItem(4003), 4);
            CurrentPlayer.LearnRecipe(RecipeFactory.RecipeByID(1));
            CurrentPlayer.LearnRecipe(RecipeFactory.RecipeByID(2));
            CurrentWorld = WorldFactory.CreateWorld(CurrentPlayer.Name);
            CurrentLocation = CurrentWorld.LocationAt(0, 0);


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
                    RaiseMessage("");
                    RaiseMessage($"You receive the '{quest.Name}' quest");
                    RaiseMessage(quest.Description);
                    RaiseMessage("");
                    RaiseMessage("Objective: Return with:");
                    foreach (ItemQuantity itemQuantity in quest.ItemsToComplete)
                    {
                        RaiseMessage($"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
                    }

                    RaiseMessage("And you will receive:");
                    RaiseMessage($"   {quest.RewardExperiencePoints} experience points");
                    RaiseMessage($"   {quest.RewardGold} gold");
                    foreach (ItemQuantity itemQuantity in quest.RewardItems)
                    {
                        RaiseMessage($"   {itemQuantity.Quantity} {ItemFactory.CreateGameItem(itemQuantity.ItemID).Name}");
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

                        RaiseMessage("");
                        RaiseMessage($"You completed the '{quest.Name}' quest");

                        // Give the player the quest rewards
                        RaiseMessage($"You receive {quest.RewardExperiencePoints} experience points");
                        CurrentPlayer.AddExperience( quest.RewardExperiencePoints);

                        RaiseMessage($"You receive {quest.RewardGold} gold");
                        CurrentPlayer.ReceiveGold(quest.RewardGold);

                        foreach (ItemQuantity itemQuantity in quest.RewardItems)
                        {
                            GameItem rewardItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                            RaiseMessage($"You receive a {rewardItem.Name}");
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
                        RaiseMessage($"You craft a {outputWeapon.Name}");
                    }
                    else
                    {
                        GameItem outputItem = ItemFactory.CreateGameItem(itemQuantity.ItemID);
                        CurrentPlayer.AddItemToInventory(outputItem, itemQuantity.Quantity);
                        RaiseMessage($"You craft {itemQuantity.Quantity} {outputItem.Name}");
                    }
                }
            }
            else
            {
                RaiseMessage("You do not have the required ingredients:");
                foreach (ItemQuantity itemQuantity in recipe.Ingredients)
                    if(itemQuantity.isWeapon)
                    {
                        RaiseMessage($"  {itemQuantity.Quantity} {WeaponFactory.WeaponName(itemQuantity.ItemID)}");
                    }
                else
                    {
                    RaiseMessage($"  {itemQuantity.Quantity} {ItemFactory.ItemName(itemQuantity.ItemID)}");
                    }
            }
        }
        private void OnCurrentPlayerPerformedAction(object sender, string result)
        {
            RaiseMessage(result);
        }
        private void OnCurrentMonsterPerformedAction(object sender, string result)
        {
            RaiseMessage(result);
        }

        
        private void OnCurrentPlayerKilled(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage("");
            RaiseMessage($"You have been slain");

            CurrentLocation = CurrentWorld.LocationAt(0, 0);
            CurrentPlayer.CompletelyHeal();
        }

        private void OnCurrentMonsterKilled(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage("");
            RaiseMessage($"You defeated the {CurrentMonster.Name}!");

            RaiseMessage($"You receive {CurrentMonster.RewardExperiencePoints} experience points.");
            CurrentPlayer.AddExperience(CurrentMonster.RewardExperiencePoints);

            RaiseMessage($"You receive {CurrentMonster.Gold} gold.");
            CurrentPlayer.ReceiveGold(CurrentMonster.Gold);

            foreach (GameItem gameItem in CurrentMonster.Inventory)
            {
                RaiseMessage($"You receive one {gameItem.Name}.");
                CurrentPlayer.AddItemToInventory(gameItem);
            }
        }
        private void GetMonsterAtLocation()
        {
            CurrentMonster = CurrentLocation.GetMonster();
        }

        public void UseCurrentConsumable()
        {
            if (CurrentPlayer.CurrentConsumable != null)
            {
                CurrentPlayer.UseCurrentConsumableOnSelf();
            }
                
        }

        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEvent(message));
        }

        private void OnCurrentPlayerLeveledUp(object sender, System.EventArgs eventArgs)
        {
            RaiseMessage($"You are now level {CurrentPlayer.Level}!");
        }
        public void AttackCurrentMonster()
        {
            if (CurrentMonster == null)
            {
                return;
            }
            if (CurrentPlayer.CurrentWeapon == null)
            {
                RaiseMessage("You must select a weapon, to attack.");
                return;
            }

            // Determine damage to monster
            CurrentPlayer.UseCurrentWeaponOn(CurrentMonster);

            // If monster if killed, collect rewards and loot
            if (CurrentMonster.IsDead)
            {
                //Maybe a way to wait for new monster 
                // Get another monster to fight
                GetMonsterAtLocation();
            }
            else
            {
                // If monster is still alive, let the monster attack
                CurrentMonster.UseWeaponOn(CurrentPlayer);
               
                
                
            }

               
                
            
        }
    }
}
