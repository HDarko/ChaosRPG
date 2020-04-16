using System;
using System.Collections.Generic;
using System.Text;
using Engine.Classes;

namespace Engine.Managers
{
   public class GameSession
    {
        public Player currentPlayer { get; set; }

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

        }
    }
}
