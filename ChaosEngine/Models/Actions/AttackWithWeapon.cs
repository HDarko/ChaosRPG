using ChaosEngine.Core;
using ChaosEngine.Shared;
using System;

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
            if (AttackSucceeded(actor, target))
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
        private bool AttackSucceeded(LivingEntity attacker, LivingEntity target)
        {
            // Currently using the same formula as FirstAttacker initiative.
            // This will change as we include attack/defense skills,
            // armor, weapon bonuses, enchantments/curses, etc.
            int attackerDexterity = attacker.GetAttribute("DEX").ModifiedValue *
                                 attacker.GetAttribute("DEX").ModifiedValue;
            int targetDexterity = target.GetAttribute("DEX").ModifiedValue *
                                    target.GetAttribute("DEX").ModifiedValue;
            decimal dexterityOffset = (attackerDexterity - targetDexterity) / 10m;
            int randomOffset = DiceService.Instance.Roll(20).Value - 10;
            decimal totalOffset = dexterityOffset + randomOffset;

            return DiceService.Instance.Roll(100).Value <= (50 + totalOffset);
        }
    }
}
