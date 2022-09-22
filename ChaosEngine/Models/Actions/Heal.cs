using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosEngine.Models.Actions
{
    class Heal:BaseAction, IAction
    {
       
        private readonly int _hitPointsToHeal;


        public Heal(GameItem item, int hitPointsToHeal)
            :base(item)
        {
            if (item.Category != GameItem.ItemCategory.Consumable)
            {
                throw new ArgumentException($"{item.Name} is not consumable");
            }

       
            _hitPointsToHeal = hitPointsToHeal;
        }

        public void Execute(LivingEntity actor, LivingEntity target)
        {
            string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
            string targetName = (target is Player) ? "yourself" : $"the {target.Name.ToLower()}";

            ReportResult($"{actorName} heal {targetName} for {_hitPointsToHeal} point{(_hitPointsToHeal > 1 ? "s" : "")}.");
            target.Heal(_hitPointsToHeal);
        }

        
    }
}
