
namespace ChaosEngine.Core
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
