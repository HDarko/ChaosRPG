
using System.Collections.Generic;

namespace ChaosEngine.Models
{
    /*This is a simple model to hold the deserialized data from GameDetails.json
     * */
   public class GameDetails
    {
        public string Title { get;}
        public string SubTitle { get;}
        public string Version { get;}
        public List<PlayerAttribute> PlayerAttributes { get; set; } =
            new List<PlayerAttribute>();
        public List<Race> Races { get; } =
           new List<Race>();
        public GameDetails(string title, string subTitle, string version)
        {
            Title = title;
            SubTitle = subTitle;
            Version = version;
        }
    }
}
