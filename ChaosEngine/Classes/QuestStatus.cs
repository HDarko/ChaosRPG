using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Classes
{
    public class QuestStatus
    {
        public Quest playerQuest { get; set; }
        public bool isCompleted { get; set; }

        public QuestStatus(Quest quest)
        {
            playerQuest = quest;
            isCompleted = false;
        }
    }
}
