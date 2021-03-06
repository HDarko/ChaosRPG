﻿using System;
using ChaosEngine.Classes;
using ChaosEngine.Sevices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEngine.Classes
{
    [TestClass]
    public class TestCombatService
    {
        [TestMethod]
        public void Test_FirstAttacker()
        {
          //Built for debugging purposes
            Player player = new Player("", "", 0, 0, 0, 18, 4);
            Monster monster = new Monster(0, "", "", 0, 12, 0, 0);

            CombatService.Combatant result = CombatService.FirstAttacker(player, monster);
        }
    }
}
