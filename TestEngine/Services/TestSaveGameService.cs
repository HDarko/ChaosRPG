
using System.Linq;
using ChaosEngine.Services;
using ChaosEngine.Managers;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace TestEngine.Services
{
    [TestClass]
    public class TestSaveGameService
    {
        [TestMethod]
        public void Test_Restore_0_1_001()
        {
          /*  GameSession gameSession =
                SaveGameService
                    .LoadLastSaveOrCreateNew(@".\TestFiles\SavedGames\TestGame.soscsrpg");

            // Game session data
            Assert.AreEqual("0.1.001", gameSession.GameDetails.Version);
            Assert.AreEqual(0, gameSession.CurrentLocation.XCoordinate);
            Assert.AreEqual(0, gameSession.CurrentLocation.YCoordinate);

            // Player data
            Assert.AreEqual("Player", gameSession.CurrentPlayer.Name);
            Assert.AreEqual(22, gameSession.CurrentPlayer.Dexterity);
            Assert.AreEqual(10, gameSession.CurrentPlayer.CurrentHitPoints);
            Assert.AreEqual(10, gameSession.CurrentPlayer.MaximumHitPoints);
            Assert.AreEqual(15, gameSession.CurrentPlayer.ExperiencePoints);
            Assert.AreEqual(1, gameSession.CurrentPlayer.Level);
            Assert.AreEqual(20, gameSession.CurrentPlayer.Gold);

            // Player quest data
            Assert.AreEqual(1, gameSession.CurrentPlayer.Quests.Count);
            Assert.AreEqual(0, gameSession.CurrentPlayer.Quests[0].PlayerQuest.ID);
            Assert.IsFalse(gameSession.CurrentPlayer.Quests[0].IsCompleted);

            // Player recipe data
            Assert.AreEqual(2, gameSession.CurrentPlayer.Recipes.Count);
            Assert.AreEqual(1, gameSession.CurrentPlayer.Recipes[0].ID);

            // Player inventory data
            Assert.AreEqual(5, gameSession.CurrentPlayer.Inventory.Count);
            Assert.AreEqual(1, gameSession.CurrentPlayer.GroupedInventory[0].Quantity);
            Assert.AreEqual(6001, gameSession.CurrentPlayer.GroupedInventory[0].Item.ItemTypeID);
            /*Assert.AreEqual(1, gameSession.CurrentPlayer.Inventory.Items.Count(i => i.ItemTypeID.Equals(2001)));
            Assert.AreEqual(1, gameSession.CurrentPlayer.Inventory.Items.Count(i => i.ItemTypeID.Equals(3001)));
            Assert.AreEqual(1, gameSession.CurrentPlayer.Inventory.Items.Count(i => i.ItemTypeID.Equals(3002)));
            Assert.AreEqual(1, gameSession.CurrentPlayer.Inventory.Items.Count(i => i.ItemTypeID.Equals(3003)));*/
          //*/
        }
    }
}
