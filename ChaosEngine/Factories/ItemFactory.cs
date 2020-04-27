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
            _standardGameItems.Add(new GameItem(3001, "Odd Pebble", 1));
            _standardGameItems.Add(new GameItem(3002, "Rusty Coin", 1));
            _standardGameItems.Add(new GameItem(3003, "Blue-Green Potion", 1));
            _standardGameItems.Add(new GameItem(9001, "Turkeysaur Leg", 1));
            _standardGameItems.Add(new GameItem(9002, "Turkeysaur Crest", 4));
            _standardGameItems.Add(new GameItem(9003, "Frog Legs", 3));
            _standardGameItems.Add(new GameItem(9004, "Frog Eyes", 6));
            _standardGameItems.Add(new GameItem(9005, "Frog Tongue", 18));
            _standardGameItems.Add(new GameItem(9006, "Engraved Rings", 18));
            _standardGameItems.Add(new GameItem(9007, "Nasty Fangs", 20));
            _standardGameItems.Add(new GameItem(9008, "Great Moss", 29));
            _standardGameItems.Add(new GameItem(3000, "Quest Token", 99));
            _standardGameItems.Add(new GameItem(3001, "Game Won Token", 1000));

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


    }
}
