using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Models
{
    public class GameState
    {
        public Player Player { get; set; }
        public int XCoordinate { get; set; }
        public int YCoordinate { get; set; }
        public GameState(Player player, int xCoordinate, int yCoordinate)
        {
            Player = player;
            XCoordinate = xCoordinate;
            YCoordinate = yCoordinate;
        }
    }
}
