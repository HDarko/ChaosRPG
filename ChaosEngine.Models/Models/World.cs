using System.Collections.Generic;

namespace ChaosEngine.Models
{
    public class World
    {

        private readonly List<Location> _locations = new List<Location>();
     

       public void AddLocation(int xCoordinate, int yCoordinate, string name, string description,
       string imageFileName)
        {
            Location loc = new Location(
            xCoordinate,
            yCoordinate,
             name,
            description,
           string.Format("/ChaosEngine;component/Images/Locations/{0}", imageFileName)); 

            _locations.Add(loc);

        }
        public void AddLocation(Location location)
        {
            _locations.Add(location);
        }

        public void AddIntroLocation(int xCoordinate, int yCoordinate, string name, string playerName,
            string imageFileName)
        {

            Location loc = new Location(
            xCoordinate,
            yCoordinate,
             name,
             $"This is you, {playerName}.\n A kobold who dreams of bigger things." +
                $"\n Of being a mighty hero of legend!\n  Now where will your journey begin? ",
             imageFileName);
            _locations.Add(loc);
        }
        public void AddIntroLocation2(int xCoordinate, int yCoordinate, string name, string description,
            string imageFileName)
        {
            Location loc = new Location(
             xCoordinate,
             yCoordinate,
            name,
             description,
            imageFileName);
            _locations.Add(loc);
        }

        public Location LocationAt(int xCoordinate, int yCoordinate)
        {
            foreach (Location loc in _locations)
            {
                if (loc.XCoordinate == xCoordinate && loc.YCoordinate == yCoordinate)
                {
                    return loc;
                }
            }

            return null;
        }
    }

   
}
