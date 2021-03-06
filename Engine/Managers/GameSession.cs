﻿using System;
using System.Collections.Generic;
using System.Text;
using ChaosEngine.Classes;

namespace ChaosEngine.Managers
{
   public class GameSession
    {
        public Player currentPlayer { get; set; }
        public Location currentLocation { get; set; }

        public GameSession()
        {
            currentPlayer = new Player
            {
                Name = "Player",
                Gold = 100000,
                CharacterClass = "Paladin",
                Level = 1,
                HitPoints = 10,
                ExperiencePoints = 0
            };

            currentLocation = new Location
            {
                Name = "Hero",
                XCoordinate = 0,
                YCoordinate = -1,
                Description = "",
                /* Description = $"This is you, {currentPlayer.Name}.\n A kobold who dreams of bigger things." +
                 $"\n Of being a mighty hero or great magician!\n  Now where will your journey begin? ",*/
                ImageName = "/Engine;component/Images/Avatars/Hero.jpg"

            };

        }
    }
}
