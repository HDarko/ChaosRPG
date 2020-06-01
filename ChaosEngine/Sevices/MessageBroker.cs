using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosEngine.GameEvents;

namespace ChaosEngine.Sevices
{
   public class MessageBroker
    {
        private static readonly MessageBroker s_messageBroker= new MessageBroker();

        private MessageBroker()
        {

        }

        public event EventHandler<GameMessageEvent> OnMessageRaised;

        public static MessageBroker GetInstance()
        {
            return s_messageBroker;
        }

        internal void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEvent(message));
        }

    }
}
