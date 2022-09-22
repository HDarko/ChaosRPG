using ChaosEngine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Models.Actions
{
    public class AttackWithWeapon: BaseAction,IAction
    {
        private string _damageDice;

        public string DamageDice
        {
            get => _damageDice;
            set => _damageDice = value;
        }

        public AttackWithWeapon(Weapon weapon, string damageDice)
            :base(weapon)
        {
            if (weapon.Category != GameItem.ItemCategory.Weapon)
            {
                throw new ArgumentException($"{weapon.Name} is not a weapon");
            }

            if (string.IsNullOrEmpty(damageDice))
            {
                throw new ArgumentException("damageDice must be valid dice notation");
            }
            DamageDice = damageDice;
        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
           

            string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
            string targetName = (target is Player) ? "you" : $"the {target.Name.ToLower()}";
            if (CombatService.AttackSucceeded(actor, target))
            {
                int damage = DiceService.Instance.Roll(_damageDice).Value;
                ReportResult($"{actorName} hit {targetName} for {damage} point{(damage > 1 ? "s" : "")}.");
                target.TakeDamage(damage);
            }
            else
            {
                
                ReportResult($"{actorName} missed {targetName}.");
            }
        }

     
    }
}
