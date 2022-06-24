using ChaosEngine.Classes.Actions;
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
        public new AttackWithWeapon Action
        { get => _action;
            set => _action = value;
        }
        public string DamageRange
        {
            get => Action.DamageDice;        
        }
        public Weapon(int itemTypeID, string name, int price, AttackWithWeapon command=null)
           : base( ItemCategory.Weapon, itemTypeID, name, price,true)
        {
            Action = command;
        }

       
        

        public new void PerformAction(LivingEntity actor, LivingEntity target)
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
