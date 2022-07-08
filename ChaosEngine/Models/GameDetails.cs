using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Models
{
    /*This is a simple model to hold the deserialized data from GameDetails.json
     * */
   public class GameDetails
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public List<PlayerAttribute> PlayerAttributes { get; set; } =
            new List<PlayerAttribute>();
        public GameDetails(string name, string version)
        {
            Name = name;
            Version = version;
        }
    }
}
