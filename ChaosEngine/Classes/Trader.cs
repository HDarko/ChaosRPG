using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace ChaosEngine.Classes
{
    public class Trader: LivingEntity
    {
       public bool weaponsAvailable = false;
        public int ID { get; }
        public Trader(int iD, string name,bool hasWeapons) :base(name,500,500,9999)
        {
            ID = ID;
            if(hasWeapons)
            {
                weaponsAvailable = true;
                Weapons = new ObservableCollection<Weapon>();
            }
            else
            {
                weaponsAvailable = false;
                Weapons = null;
            }
        }

       

    

       

    }
}
