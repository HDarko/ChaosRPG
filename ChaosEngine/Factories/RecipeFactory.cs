using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using ChaosEngine.Models;
using ChaosEngine.Shared;

namespace ChaosEngine.Factories
{
    class RecipeFactory
    {
        private static readonly List<Recipe> _recipes = new List<Recipe>();
        private const string GAME_DATA_FILENAME = ".\\GameData\\Recipes.xml";

        static RecipeFactory()
        {
            if (File.Exists(GAME_DATA_FILENAME))
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(File.ReadAllText(GAME_DATA_FILENAME));

                LoadRecipesFromNodes(data.SelectNodes("/Recipes/Recipe"));
            }
            else
            {
                throw new FileNotFoundException($"Missing data file: {GAME_DATA_FILENAME}");
            }
           
        }

        private static void LoadRecipesFromNodes(XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {
                Recipe recipe =
                    new Recipe(node.GetXmlAttributeAsInt("ID"),
                               node.SelectSingleNode("./Name")?.InnerText ?? "");

                foreach (XmlNode childNode in node.SelectNodes("./Ingredients/Item"))
                {
                    recipe.AddIngredient(childNode.GetXmlAttributeAsInt("ID"),
                                         childNode.GetXmlAttributeAsInt("Quantity"));
                }

                foreach (XmlNode childNode in node.SelectNodes("./Ingredients/Weapon"))
                {
                    recipe.AddWeaponIngredient(childNode.GetXmlAttributeAsInt("ID"));
                }
                foreach (XmlNode childNode in node.SelectNodes("./OutputItems/Item"))
                {
                    recipe.AddOutputItem(childNode.GetXmlAttributeAsInt("ID"),
                                         childNode.GetXmlAttributeAsInt("Quantity"));
                }

                foreach (XmlNode childNode in node.SelectNodes("./OutputItems/Weapon"))
                {
                    recipe.AddOutputWeapon(childNode.GetXmlAttributeAsInt("ID"));
                }

                AddRecipeToList(recipe);
            }
        }

        public static Recipe RecipeByID(int id)
        {
            return _recipes.FirstOrDefault(x => x.ID == id);
        }

        private static void AddRecipeToList(Recipe recipe)
        {
            if (_recipes.Any(r => r.ID == recipe.ID))
            {
                throw new ArgumentException($"There is already a recipe with ID'{recipe.ID}'");
            }

            _recipes.Add(recipe);
        }
    }
}
