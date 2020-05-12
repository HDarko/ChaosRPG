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
            healthPotionLesser.AddIngredient(4001, 2);
            healthPotionLesser.AddIngredient(4002, 1);
            healthPotionLesser.AddOutputItem(6001, 1);

            _recipes.Add(healthPotionLesser);

            Recipe stonkaStick = new Recipe(2, "Stonka Stick");
            stonkaStick.AddIngredient(4003, 1);
            stonkaStick.AddWeaponIngredient(1001);
            stonkaStick.AddOutputWeapon(1003);
            _recipes.Add(stonkaStick);
        }

        public static Recipe RecipeByID(int id)
        {
            return _recipes.FirstOrDefault(x => x.ID == id);
        }
    }
}
