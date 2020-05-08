using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosEngine.Classes;
using ChaosEngine.Classes.Actions;

namespace ChaosEngine.Factories
{
    public static class ItemFactory
    {
        private static readonly List<GameItem> _standardGameItems= new List<GameItem>();

        static ItemFactory()
        {       //Quest Items in 9000s,
                //Trader Items in 3000s

            //LootItems in 9000s
            BuildMiscellaneousItem(9001, "Turkeysaur Leg", 1);
            BuildMiscellaneousItem(9002, "Turkeysaur Crest", 4);
            BuildMiscellaneousItem(9003, "Frog Legs", 3);
            BuildMiscellaneousItem(9004, "Frog Eyes", 6);
            BuildMiscellaneousItem(9005, "Frog Tongue", 18);
            BuildMiscellaneousItem(9006, "Engraved Rings", 18);
            BuildMiscellaneousItem(9007, "Nasty Fangs", 20);
            BuildMiscellaneousItem(9008, "Great Moss", 29);
            BuildMiscellaneousItem(9009, "Odd Pebble", 1);
            BuildMiscellaneousItem(9010, "Rusty Coin", 1);
            //ConsumableItems in 6000s
            BuildHealingItem(6001, "Health Potion(Lesser) ", 4, 7);
            //Ingredients in 400s
            BuildMiscellaneousItem(4000, "Blue-Green Moss", 5);
            BuildMiscellaneousItem(4001, "Bone Marrow", 5);
            BuildMiscellaneousItem(4002, "Lovely Ivy Extract", 5);
            //Quest Items in 3000s,
            BuildMiscellaneousItem(3000, "Quest Token", 99);
            BuildMiscellaneousItem(3001, "Game Won Token", 1000);
           

        }

        public static GameItem CreateGameItem(int itemTypeID)
        {
            GameItem standardItem = _standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID);

            if (standardItem != null)
            {
                return standardItem;
            }
            return null;
        }

        private static void BuildMiscellaneousItem(int id, string name, int price)
        {
            _standardGameItems.Add(new GameItem( GameItem.ItemCategory.Miscellaneous,id, name, price));
        }

        private static void BuildHealingItem(int id, string name, int price, int hitPointsToHeal)
        {
            GameItem item = new GameItem(GameItem.ItemCategory.Consumable, id, name, price);
            item.Action = new Heal(item, hitPointsToHeal);
            _standardGameItems.Add(item);
        }

    }
}
