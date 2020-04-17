using System;
using System.Collections.Generic;
using System.Text;
using ChaosEngine.Classes;
using ChaosEngine.Factories;

namespace ChaosEngine.Managers
{
   public class GameSession
    {
        public Player currentPlayer { get; set; }
        public Location currentLocation { get; set; }
        public World currentWorld { get; set; }


        public GameSession()
        {
            WorldFactory factory = new WorldFactory();
           
            currentPlayer = new Player
            {
                Name = "Player",
                Gold = 100000,
                CharacterClass = "Paladin",
                Level = 1,
                HitPoints = 10,
                ExperiencePoints = 0
            };
            currentWorld = factory.CreateWorld(currentPlayer.Name);


            currentLocation = currentWorld.LocationAt(0, 0);


        }
    }
}
