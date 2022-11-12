using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Threading.Tasks;
using ChaosEngine.Models;
using ChaosEngine.Shared;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace ChaosEngine.Factories
{
    public static class RecipeFactory
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
                var ingredients = new List<ItemQuantity>();

                foreach (XmlNode childNode in node.SelectNodes("./Ingredients/Item"))
                {
                    GameItem item = ItemFactory.CreateGameItem(childNode.GetXmlAttributeAsInt("ID"));

                    ingredients.Add(new ItemQuantity(item, childNode.GetXmlAttributeAsInt("Quantity")));
                }

                var outputItems = new List<ItemQuantity>();

                foreach (XmlNode childNode in node.SelectNodes("./Ingredients/Weapon"))
                {
                    Weapon weapon = WeaponFactory.CreateWeapon(childNode.GetXmlAttributeAsInt("ID"));

                    ingredients.Add(new ItemQuantity(weapon, 1, true));
                }
                foreach (XmlNode childNode in node.SelectNodes("./OutputItems/Item"))
                {
                    GameItem item = ItemFactory.CreateGameItem(childNode.GetXmlAttributeAsInt("ID"));

                    outputItems.Add(new ItemQuantity(item, childNode.GetXmlAttributeAsInt("Quantity")));
                }

                foreach (XmlNode childNode in node.SelectNodes("./OutputItems/Weapon"))
                {
                    Weapon weapon = WeaponFactory.CreateWeapon(childNode.GetXmlAttributeAsInt("ID"));
                    outputItems.Add(new ItemQuantity(weapon,1, true));
                }

                Recipe recipe =
                    new Recipe(node.GetXmlAttributeAsInt("ID"),
                        node.SelectSingleNode("./Name")?.InnerText ?? "",
                        ingredients, outputItems);

                _recipes.Add(recipe);
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
