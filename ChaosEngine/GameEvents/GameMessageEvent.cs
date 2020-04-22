using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.GameEvents
{
   public class GameMessageEvent: System.EventArgs
    {
        public string message { get; private set; }

        public GameMessageEvent(string eventMessage)
        {
            message = eventMessage;
        }
    }
}
