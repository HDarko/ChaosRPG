using System;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChaosEngine.Models
{
    public class Player: LivingEntity
    {
        //--------------------------------------Private properties-----------------------
      
        private string _characterClass;
       
        private int _experiencePoints;


        public event EventHandler OnLeveledUp;



        public ObservableCollection<QuestStatus> Quests { get; } =
            new ObservableCollection<QuestStatus>();
        public ObservableCollection<Recipe> Recipes { get; } =
            new ObservableCollection<Recipe>();

        public string CharacterClass
        {
            get { return _characterClass; }
            set
            {
                _characterClass = value;
                OnPropertyChanged(nameof(CharacterClass));
            }
        }

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            private set
            {
                _experiencePoints = value;
                SetLevelAndMaximumHitPoints();
                OnPropertyChanged(nameof(ExperiencePoints));
                //Can also be parantheseless on property change
            }
        }

        public void AddExperience(int experiencePoints)
        {
            ExperiencePoints += experiencePoints;
        }

        private void SetLevelAndMaximumHitPoints()
        {
            int originalLevel = Level;

            Level = (ExperiencePoints / 100) + 1;

            if (Level != originalLevel)
            {
                MaximumHitPoints = Level * 10;

                OnLeveledUp?.Invoke(this, System.EventArgs.Empty);
            }
        }
       
        public void UseCurrentWeaponOn(LivingEntity target)
        {
            CurrentWeapon.PerformAction(this, target);
        }


        public Player(string name, string characterClass, int experiencePoints,
                      int maximumHitPoints, int currentHitPoints, int gold,int dexterity, int level = 1) :
            base(name, maximumHitPoints, currentHitPoints, gold,dexterity, level)
        {
            CharacterClass = characterClass;
            ExperiencePoints = experiencePoints;
        }

        public void LearnRecipe(Recipe recipe)
        {
            if (!Recipes.Any(r => r.ID == recipe.ID))
            {
                Recipes.Add(recipe);
            }
        }
        //---------------------------------------------------------------------------------------------------
    }
}
