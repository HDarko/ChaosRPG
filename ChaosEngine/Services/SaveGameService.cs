using System.IO;
using System.Collections.Generic;
using ChaosEngine.Models;
using ChaosEngine.Factories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChaosEngine.Services
{
    public static class SaveGameService
    {

        private const string _currentGameVersion = "0.1.001";

        public static void Save(GameState gameState, string fileName)
        {
            File.WriteAllText(fileName,
                              JsonConvert.SerializeObject(gameState, Formatting.Indented));
        }

        public static GameState LoadLastSaveOrCreateNew(string fileName)
        {
            if (!File.Exists(fileName))
            {
               throw new FileNotFoundException($"Filename: {fileName}");
            }

            // Save game file exists, so create the GameSession object from it.
            try
            {
                JObject data = JObject.Parse(File.ReadAllText(fileName));

                // Populate Player object
                Player player = CreatePlayer(data);

                int x = (int)data[nameof(GameState.XCoordinate)];
                int y = (int)data[nameof(GameState.YCoordinate)];

                // Create GameSession object with saved game data
                return new GameState(player, x, y);
            }
            catch
            {
                // If there was an error loading/deserializing the saved game, 
                // create a brand new GameSession object.
                throw new System.FormatException($"Error reading: {fileName}");
            }
        }

        private static Player CreatePlayer(JObject data)
        {
              Player player =  
                new Player((string)data[nameof(GameState.Player)][nameof(Player.Name)],
                (int)data[nameof(GameState.Player)][nameof(Player.ExperiencePoints)],
                (int)data[nameof(GameState.Player)][nameof(Player.MaximumHitPoints)],
                (int)data[nameof(GameState.Player)][nameof(Player.CurrentHitPoints)],
                (int)data[nameof(GameState.Player)][nameof(Player.Gold)],
                GetPlayerAttributes(data),
                (int)data[nameof(GameState.Player)][nameof(Player.Level)]);
              

              PopulatePlayerInventory(data, player);

              PopulatePlayerQuests(data, player);

              PopulatePlayerRecipes(data, player);

              return player;
        }

        private static IEnumerable<PlayerAttribute> GetPlayerAttributes(JObject data)
        {
            List<PlayerAttribute> attributes =
                new List<PlayerAttribute>();

            foreach (JToken itemToken in (JArray)data[nameof(GameState.Player)]
                [nameof(Player.Attributes)])
            {
                attributes.Add(new PlayerAttribute(
                                   (string)itemToken[nameof(PlayerAttribute.Key)],
                                   (string)itemToken[nameof(PlayerAttribute.DisplayName)],
                                   (string)itemToken[nameof(PlayerAttribute.DiceNotation)],
                                   (int)itemToken[nameof(PlayerAttribute.BaseValue)],
                                   (int)itemToken[nameof(PlayerAttribute.ModifiedValue)]));
            }
            return attributes;
        }

        private static void PopulatePlayerInventory(JObject data, Player player)
        {
            foreach (JToken itemToken in (JArray)data[nameof(GameState.Player)]
                [nameof(Player.Inventory)])
            {
                int itemId = (int)itemToken[nameof(GameItem.ItemTypeID)];

                player.AddItemToInventory(ItemFactory.CreateGameItem(itemId));
            }
        }

        private static void PopulatePlayerQuests(JObject data, Player player)
        {
            foreach (JToken questToken in (JArray)data[nameof(GameState.Player)]
                [nameof(Player.Quests)])
            {
                int questId =
                    (int)questToken[nameof(QuestStatus.PlayerQuest)][nameof(QuestStatus.PlayerQuest.ID)];

                Quest quest = QuestFactory.GetQuestByID(questId);
                QuestStatus questStatus = new QuestStatus(quest);
                questStatus.IsCompleted = (bool)questToken[nameof(QuestStatus.IsCompleted)];

                player.Quests.Add(questStatus);
            }
        }

        private static void PopulatePlayerRecipes(JObject data, Player player)
        {

            foreach (JToken recipeToken in
                (JArray)data[nameof(GameState.Player)][nameof(Player.Recipes)])
            {
                int recipeId = (int)recipeToken[nameof(Recipe.ID)];

                Recipe recipe = RecipeFactory.RecipeByID(recipeId);

                player.Recipes.Add(recipe);
            }
        }
    }
}
