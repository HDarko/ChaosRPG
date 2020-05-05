using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosEngine.Classes;

namespace ChaosEngine.Factories
{
    public static class ItemFactory
    {
        private static readonly List<GameItem> _standardGameItems= new List<GameItem>();

        static ItemFactory()
        {       //Quest Items in 9000s,
                //Trader Items in 3000s
            BuildMiscellaneousItem(3001, "Odd Pebble", 1);
            BuildMiscellaneousItem(3002, "Rusty Coin", 1);
            BuildMiscellaneousItem(3003, "Blue-Green Potion", 1);
            BuildMiscellaneousItem(9001, "Turkeysaur Leg", 1);
            BuildMiscellaneousItem(9002, "Turkeysaur Crest", 4);
            BuildMiscellaneousItem(9003, "Frog Legs", 3);
            BuildMiscellaneousItem(9004, "Frog Eyes", 6);
            BuildMiscellaneousItem(9005, "Frog Tongue", 18);
            BuildMiscellaneousItem(9006, "Engraved Rings", 18);
            BuildMiscellaneousItem(9007, "Nasty Fangs", 20);
            BuildMiscellaneousItem(9008, "Great Moss", 29);
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
            _standardGameItems.Add(new GameItem(id, GameItem.ItemCategory.Miscellaneous, name, price));
        }


    }
}
