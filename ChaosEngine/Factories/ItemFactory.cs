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
        private static List<GameItem> _standardGameItems;

        static ItemFactory()
        {
            _standardGameItems = new List<GameItem>();

            _standardGameItems.Add(new Weapon(1001, "Pointy Stick", 1, 1, 2));
            _standardGameItems.Add(new Weapon(1002, "Rusty Sword", 5, 1, 3));
            _standardGameItems.Add(new GameItem(9001, "Juicy Mushroom", 1));
            _standardGameItems.Add(new GameItem(9002, "Enchanted FingerNails", 2));
            _standardGameItems.Add(new GameItem(9003, "Frog Legs", 1));
            _standardGameItems.Add(new GameItem(9004, "Frog Eyes", 2));
            _standardGameItems.Add(new GameItem(9005, "Frog Tongue", 1));
            _standardGameItems.Add(new GameItem(9006, "Engraved Rings", 2));
            _standardGameItems.Add(new GameItem(9007, "Nasty Fangs", 1));
            _standardGameItems.Add(new GameItem(9008, "Great Moss", 2));
            _standardGameItems.Add(new GameItem(3000, "Quest Token", 99));

        }

        public static GameItem CreateGameItem(int itemTypeID)
        {
            GameItem standardItem = _standardGameItems.FirstOrDefault(item => item.itemTypeID == itemTypeID);

            if (standardItem != null)
            {
                return standardItem.Clone();
            }
            return null;
        }


    }
}
