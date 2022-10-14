using System;
using System.Collections.ObjectModel;
using System.Linq;
using ChaosEngine.Models;
using ChaosEngine.Services;
using ChaosEngine.Factories;
using System.ComponentModel;

namespace ChaosEngine.Managers
{
    public class CharacterCreationManager : INotifyPropertyChanged
    {
        private Race _selectedRace;

        public event PropertyChangedEventHandler PropertyChanged;

        public GameDetails GameDetails { get; }
        public Race SelectedRace
        {
            get => _selectedRace;
            set
            {
                _selectedRace = value;
            }
        }
        public string Name { get; set; }
        public ObservableCollection<PlayerAttribute> PlayerAttributes { get; set; } =
            new ObservableCollection<PlayerAttribute>();
        public bool HasRaces =>
            GameDetails.Races.Any();
        public bool HasRaceAttributeModifiers =>
            HasRaces && GameDetails.Races.Any(r => r.PlayerAttributeModifiers.Any());
        public CharacterCreationManager()
        {
            GameDetails = GameDetailsService.ReadGameDetails();
            if (HasRaces)
            {
                SelectedRace = GameDetails.Races.First();
            }

            RollNewCharacter();
        }
        public void RollNewCharacter()
        {
            PlayerAttributes.Clear();
            foreach (PlayerAttribute playerAttribute in GameDetails.PlayerAttributes)
            {
                playerAttribute.ReRoll();
                PlayerAttributes.Add(playerAttribute);
            }

            ApplyAttributeModifiers();
        }
        public void ApplyAttributeModifiers()
        {
            foreach (PlayerAttribute playerAttribute in PlayerAttributes)
            {
                var attributeRaceModifier =
                    SelectedRace.PlayerAttributeModifiers
                                .FirstOrDefault(pam => pam.AttributeKey.Equals(playerAttribute.Key));
                playerAttribute.ModifiedValue =
                    playerAttribute.BaseValue + (attributeRaceModifier?.Modifier ?? 0);
            }
        }
        public Player GetPlayer()
        {
            Player player = new Player(Name, 0, 10, 10,5, PlayerAttributes);


            if (!player.Weapons.Any())
            {
                player.AddWeaponToWeapons(WeaponFactory.CreateWeapon(1001));
            }
            player.AddItemToInventory(ItemFactory.CreateGameItem(6001));
            player.AddItemToInventory(ItemFactory.CreateGameItem(4000), 4);
            player.AddItemToInventory(ItemFactory.CreateGameItem(4001), 4);
            player.AddItemToInventory(ItemFactory.CreateGameItem(4002), 4);
            player.AddItemToInventory(ItemFactory.CreateGameItem(4003), 4);
            player.LearnRecipe(RecipeFactory.RecipeByID(1));
            player.LearnRecipe(RecipeFactory.RecipeByID(2));
            // Give player default inventory items, weapons, recipes, etc.

            return player;
        }
    }
}
