using System;
using System.Collections.Generic;
using System.Text;

namespace ChaosEngine.Classes
{
    public class Location
    {
        public int xCoordinate { get; set; }
        public int yCoordinate { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string imageName { get; set; }
        public List<Quest> QuestsAvailableHere { get; set; } = new List<Quest>();
    }
}
