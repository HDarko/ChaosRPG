using ChaosEngine.Classes.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Classes
{
    public class Weapon : GameItem
    {
        private AttackWithWeapon _action;
       public AttackWithWeapon Action
        { get { return _action; }
            set 
            {
                _action = value;
                SetDamageRange();
            } }
        public string DamageRange { get; set; }
        public Weapon(int itemTypeID, string name, int price, AttackWithWeapon command=null)
           : base(itemTypeID, ItemCategory.Weapon, name, price,true)
        {
            Action = command;
        }

        public void SetDamageRange()
        {
            if (Action != null)
            {
                DamageRange = $"{Action.GetMinDamage()}-{Action.GetMaxDamage()}";
            }
            else
            {
                DamageRange = "";
            }
        }

        public void PerformAction(LivingEntity actor, LivingEntity target)
        {
            Action?.Execute(actor, target);
        }
        public new Weapon Clone()
        {
            //fix either with virtual/orveride or through composition- 5.1
            return new Weapon(ItemTypeID, Name, Price, Action);
        }
    }
}
