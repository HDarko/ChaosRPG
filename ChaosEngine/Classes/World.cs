using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Classes
{
    public class World
    {

        private List<Location> _locations = new List<Location>();
     

        internal void AddLocation(int xCoordinate, int yCoordinate, string name, string description,
       string imageFileName)
        {
            Location loc = new Location();
            loc.xCoordinate = xCoordinate;
            loc.yCoordinate = yCoordinate;
            loc.name = name;
            loc.description = description;
            loc.imageName = imageFileName;

            _locations.Add(loc);

        }

        internal void AddIntroLocation(int xCoordinate, int yCoordinate, string name, string playerName,
      string imageFileName)
        {
            AddLocation(xCoordinate, yCoordinate,name, $"This is you, {playerName}.\n A kobold who dreams of bigger things." +
                $"\n Of being a mighty hero of legend!\n  Now where will your journey begin? ",
                imageFileName);
        }

        public Location LocationAt(int xCoordinate, int yCoordinate)
        {
            foreach (Location loc in _locations)
            {
                if (loc.xCoordinate == xCoordinate && loc.yCoordinate == yCoordinate)
                {
                    return loc;
                }
            }

            return null;
        }
    }

   
}
