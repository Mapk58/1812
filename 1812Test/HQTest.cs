using Microsoft.VisualStudio.TestTools.UnitTesting;
using _1812.Domain;
using System.Collections.Generic;
using System.IO;

namespace _1812Test
{
    [TestClass]
    public class HQTest
    {
        [TestMethod]
        public void CommanderNationTest()
        {
            //Arrange
            HeadQuarter HQtest = Manager.CreateHQ();
            Commander testFR = HQtest.PickFR();
            Commander testRU = HQtest.PickRU();

            //Act
            bool FR = testFR.Side == Commander.CommanderSide.FrenchEmpire;
            bool RU = testRU.Side == Commander.CommanderSide.RussianEmpire;

            //Assert
            Assert.AreEqual(testFR.Side, Commander.CommanderSide.FrenchEmpire, "Wrong French side");
            Assert.AreEqual(testRU.Side, Commander.CommanderSide.RussianEmpire, "Wrong Russian side");
        }
    }
}
