using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ChaosEngine.Classes;
using ChaosEngine.Factories;


namespace ChaosEngine.Managers
{
   public class GameSession: BaseNotificationClass
    {
        public Player currentPlayer { get; set; }
        public Location _currentLocation;
        public World currentWorld { get; set; }

        //------------------------------------Getters-----------------------------------------------

        public Location currentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;

                OnPropertyChanged(nameof(currentLocation));
                OnPropertyChanged(nameof(hasLocationToNorth));
                OnPropertyChanged(nameof(hasLocationToEast));
                OnPropertyChanged(nameof(hasLocationToWest));
                OnPropertyChanged(nameof(hasLocationToSouth));
                GivePlayerQuestsAtLocation();
            }
        }

        public bool hasLocationToNorth
        {
            get
            {
                return currentWorld.LocationAt(currentLocation.xCoordinate, currentLocation.yCoordinate + 1) != null;
            }
        }

        public bool hasLocationToEast
        {
            get
            {
                return currentWorld.LocationAt(currentLocation.xCoordinate + 1, currentLocation.yCoordinate) != null;
            }
        }

        public bool hasLocationToSouth
        {
            get
            {
                return currentWorld.LocationAt(currentLocation.xCoordinate, currentLocation.yCoordinate - 1) != null;
            }
        }

        public bool hasLocationToWest
        {
            get
            {
                return currentWorld.LocationAt(currentLocation.xCoordinate - 1, currentLocation.yCoordinate) != null;
            }
        }

       

        //-------------------------------------------------------------------------------------------
        public GameSession()
        {
            
           
            currentPlayer = new Player
            {
                name = "Player",
                gold = 100000,
                characterClass = "Paladin",
                level = 1,
                hitPoints = 10,
                experiencePoints = 0
            };

            currentWorld = WorldFactory.CreateWorld(currentPlayer.name);
            currentLocation = currentWorld.LocationAt(0, 0);


        }
        //===============================Move Functions=======================================

        public void MoveNorth()
        {
            if (hasLocationToNorth)
            {
                currentLocation = currentWorld.LocationAt(currentLocation.xCoordinate, currentLocation.yCoordinate + 1);
            }
        }
    

        public void MoveEast()
        {
            if (hasLocationToEast)
            {
                currentLocation = currentWorld.LocationAt(currentLocation.xCoordinate + 1, currentLocation.yCoordinate);
            }
        }

        public void MoveSouth()
        {
            if (hasLocationToSouth)
            {
                currentLocation = currentWorld.LocationAt(currentLocation.xCoordinate, currentLocation.yCoordinate - 1);
            }
        }

        public void MoveWest()
        {
            if (hasLocationToWest)
            {
                currentLocation = currentWorld.LocationAt(currentLocation.xCoordinate - 1, currentLocation.yCoordinate);
            }
        }

        //====================================================================================================================
        private void GivePlayerQuestsAtLocation()
        {
            foreach (Quest quest in currentLocation.QuestsAvailableHere)
            {
                if (!currentPlayer.quests.Any(q => q.playerQuest.iD == quest.iD))
                {
                    currentPlayer.quests.Add(new QuestStatus(quest));
                }
            }
        }

    }
}
