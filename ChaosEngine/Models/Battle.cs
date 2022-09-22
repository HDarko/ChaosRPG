﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosEngine.Services;
using ChaosEngine.GameEvents;

namespace ChaosEngine.Models
{
    public class Battle : IDisposable
    {

        
        private readonly MessageBroker _messageBroker = MessageBroker.GetInstance();
        private readonly Player _player;
        private readonly Monster _opponent;
       

       

        public Battle(Player player, Monster opponent)
        {
            _player = player;
            _opponent = opponent;

            _player.OnActionPerformed += OnCombatantActionPerformed;
            _opponent.OnActionPerformed += OnCombatantActionPerformed;
            _opponent.OnKilled += OnOpponentKilled;

            _messageBroker.RaiseMessage("");
            _messageBroker.RaiseMessage($"You see a {_opponent.Name} here!");

            if (CombatService.FirstAttacker(_player,_opponent) == CombatService.Combatant.Opponent)
            {
                AttackPlayer();
            }
        }

        public void AttackOpponent()
        {
            if (_player.CurrentWeapon == null)
            {
                _messageBroker.RaiseMessage("You must select a weapon, to attack.");
                return;
            }

            _player.UseCurrentWeaponOn(_opponent);

            if (_opponent.IsAlive)
            {
                AttackPlayer();
            }
        }


        private void OnOpponentKilled(object sender, System.EventArgs eventArgs)
        {
            _messageBroker.RaiseMessage("");
            _messageBroker.RaiseMessage($"You defeated the {_opponent.Name}!");

            _messageBroker.RaiseMessage($"You receive {_opponent.RewardExperiencePoints} experience points.");
            _player.AddExperience(_opponent.RewardExperiencePoints);

            _messageBroker.RaiseMessage($"You receive {_opponent.Gold} gold.");
            _player.ReceiveGold(_opponent.Gold);

            foreach (GameItem gameItem in _opponent.Inventory)
            {
                _messageBroker.RaiseMessage($"You receive one {gameItem.Name}.");
                _player.AddItemToInventory(gameItem);
            }
            OnCombatVictory?.Invoke(this, new CombatVictoryEvent());
        }
        private void AttackPlayer()
        {
            _opponent.UseWeaponOn(_player);
        }
        public event EventHandler<CombatVictoryEvent> OnCombatVictory;

        private void OnCombatantActionPerformed(object sender, string result)
        {
            _messageBroker.RaiseMessage(result);
        }

        public void Dispose()
        {
            _player.OnActionPerformed -= OnCombatantActionPerformed;
            _opponent.OnActionPerformed -= OnCombatantActionPerformed;
            _opponent.OnKilled -= OnOpponentKilled;
        }
    }
}
