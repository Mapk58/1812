using Microsoft.VisualStudio.TestTools.UnitTesting;
using _1812.Domain;
using System.Collections.Generic;
using System.IO;

namespace _1812Test
{
    [TestClass]
    public class DeckTest
    {
        [TestMethod]
        public void DeckUsingTest()
        {
            //Arrange
            Deck testDeck = Manager.CreateDeck();
            List<Card> toPut1 = new List<Card>();
            List<Card> toPut2 = new List<Card>();

            //Act
            List<Card> testCards1 = testDeck.GetCards(testDeck.AllCount - 1);
            testDeck.PutCards(testCards1);
            testCards1.AddRange(testDeck.GetCards(1));
            toPut1.Add(testCards1[testCards1.Count - 1]);
            testDeck.PutCards(toPut1);

            List<Card> testCards2 = testDeck.GetCards(testDeck.AllCount - 1);
            testDeck.PutCards(testCards2);
            testCards2.AddRange(testDeck.GetCards(1));
            toPut2.Add(testCards2[testCards2.Count - 1]);
            testDeck.PutCards(toPut2);

            //Assert
            Assert.AreEqual(testCards1.Count, testCards2.Count, 0, "Decks have different amount of cards");

            testCards1.Sort();
            testCards2.Sort();
            bool flag = true;
            for (int i = 0; i < testCards1.Count; i++)
            {
                if (testCards1[i].ID != testCards2[i].ID)
                {
                    flag = false;
                }
            }
            Assert.IsTrue(flag, "Decks have different cards");
        }

        [TestMethod]
        public void ClassicDeckTest()
        {
            //Arrange
            Deck testDeck = Manager.CreateDeck("classicDeck");
            List<Card> testCards = testDeck.GetCards(testDeck.AllCount);
            int battalionsAmount = 0;
            int cannonsAmount = 0;
            int operationsAmount = 0;

            //Act
            foreach (Card i in testCards)
            {
                if (i.Type == Card.CardType.Battalion)
                {
                    battalionsAmount++;
                }
                if (i.Type == Card.CardType.Cannon)
                {
                    cannonsAmount++;
                }
                if (i.Type == Card.CardType.Operation)
                {
                    operationsAmount++;
                }
            }

            //Assert
            const int battalionsExpected = 24;
            const int cannonsExpected = 26;
            const int operationsExpected = 31;
            Assert.AreEqual(battalionsAmount, battalionsExpected, 0, "Wrong battalions amount");
            Assert.AreEqual(cannonsAmount, cannonsExpected, 0, "Wrong cannons amount");
            Assert.AreEqual(operationsAmount, operationsExpected, 0, "Wrong operations amount");
        }
    }
}
