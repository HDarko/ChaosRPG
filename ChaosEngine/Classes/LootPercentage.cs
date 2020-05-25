using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Classes
{
    class LootPercentage
    {
        public int ID { get; }
        public int Percentage { get; }
        public int Quantity { get; }
        public LootPercentage(int id, int percentage, int quanitity)
        {
            ID = id;
            Percentage = percentage;
            Quantity = quanitity;
        }
    }
}
