using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ChaosEngine.Classes
{
    class Monster: BaseNotificationClass
    {
        private int _hitPoints;

        public string name { get; private set; }
        public string imageName { get; set; }
        public int maximumHitPoints { get; private set; }
        public int hitPoints
        {
            get { return _hitPoints; }
            private set
            {
                _hitPoints = value;
                OnPropertyChanged(nameof(hitPoints));
            }
        }

        public int rewardExperiencePoints { get; private set; }
        public int rewardGold { get; private set; }

        public ObservableCollection<ItemQuantity> inventory { get; set; }

        public Monster(string monName, string imageFileName,
            int maxHitPoints, int startHitPoints,
            int rewardExpPoints, int rewardGoldAmount)
        {
            name = monName;
            imageName = string.Format("/Engine;component/Images/Monsters/{0}", imageFileName);
            //If first fails then try this
            //string.Format("pack://application:,,,/Engine;component/Images/Monsters/{0}", imageName);
            maximumHitPoints = maxHitPoints;
            hitPoints = startHitPoints;
            rewardExperiencePoints = rewardExpPoints;
            rewardGold = rewardGoldAmount;

            inventory = new ObservableCollection<ItemQuantity>();
        }
    }
}
