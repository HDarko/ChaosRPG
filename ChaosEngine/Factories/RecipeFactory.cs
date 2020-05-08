using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosEngine.Classes;

namespace ChaosEngine.Factories
{
    class RecipeFactory
    {
        private static readonly List<Recipe> _recipes = new List<Recipe>();

        static RecipeFactory()
        {
            Recipe healthPotionLesser = new Recipe(1, "Health Potion(Lesser)");
            healthPotionLesser.AddIngredient(4000, 1);
            healthPotionLesser.AddIngredient(4001, 1);
            healthPotionLesser.AddIngredient(4002, 1);
            healthPotionLesser.AddOutputItem(2001, 1);

            _recipes.Add(healthPotionLesser);
        }

        public static Recipe RecipeByID(int id)
        {
            return _recipes.FirstOrDefault(x => x.ID == id);
        }
    }
}
