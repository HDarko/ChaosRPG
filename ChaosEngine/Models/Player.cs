using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChaosEngine.Models
{
    public class Player: LivingEntity
    {
        //--------------------------------------Private properties-----------------------
        
        private int _experiencePoints;


        public event EventHandler OnLeveledUp;



        public ObservableCollection<QuestStatus> Quests { get; } =
            new ObservableCollection<QuestStatus>();
        public ObservableCollection<Recipe> Recipes { get; } =
            new ObservableCollection<Recipe>();

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


        public Player(string name, int experiencePoints,
                      int maximumHitPoints, int currentHitPoints, int gold, IEnumerable<PlayerAttribute> attributes, int level = 1) :
            base(name, maximumHitPoints, currentHitPoints, gold, attributes, level)
        {
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
