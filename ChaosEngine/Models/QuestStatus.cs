using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Classes
{
    public class QuestStatus: BaseNotificationClass
    {
        private bool _isCompleted = false;
        public Quest PlayerQuest { get; }
        public bool IsCompleted
        { 
            get { return _isCompleted;}
            set
            {
                _isCompleted = value;
                OnPropertyChanged();
            }
        }
public QuestStatus(Quest quest)
        {
            PlayerQuest = quest;
            IsCompleted = false;
        }
    }
}
