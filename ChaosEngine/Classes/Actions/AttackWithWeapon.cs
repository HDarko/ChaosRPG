using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Classes.Actions
{
    public class AttackWithWeapon: BaseAction,IAction
    {
    
        private readonly int _maximumDamage;
        private readonly int _minimumDamage;

   

        public AttackWithWeapon(Weapon weapon, int minimumDamage, int maximumDamage)
            :base(weapon)
        {
            if (weapon.Category != GameItem.ItemCategory.Weapon)
            {
                throw new ArgumentException($"{weapon.Name} is not a weapon");
            }

            if (minimumDamage < 0)
            {
                throw new ArgumentException("minimumDamage must be 0 or larger");
            }

            if (maximumDamage < minimumDamage)
            {
                throw new ArgumentException("maximumDamage must be >= minimumDamage");
            }
            _minimumDamage = minimumDamage;
            _maximumDamage = maximumDamage;
        }

        public int GetMaxDamage()
        {
            return _maximumDamage;
        }

        public int GetMinDamage()
        {
            return _minimumDamage;
        }


        public void Execute(LivingEntity actor, LivingEntity target)
        {
            int damage = RandomNumberGenerator.NumberBetween(_minimumDamage, _maximumDamage);

            string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
            string targetName = (target is Player) ? "you" : $"the {target.Name.ToLower()}";
            if (damage == 0)
            {
                ReportResult($"{actorName} missed {targetName}.");
            }
            else
            {
                ReportResult($"{actorName} hit {targetName} for {damage} point{(damage > 1 ? "s" : "")}.");
                target.TakeDamage(damage);
            }
        }

     
    }
}
