using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace _1812.Domain
{
    public static class Manager
    {
        public static HeadQuarter CreateHQ(string deckType = "classicHQ")
        {
            string path = "../../../Data/Cards/HQData/" + deckType + ".json";
            string namesPath = "../../../Data/Cards/HQData/commanderNames.json";
            string readText = File.ReadAllText(namesPath);
            List<string> commanderNames = JObject.Parse(readText).Value<Newtonsoft.Json.Linq.JArray>("commanderNames").ToObject<List<string>>();
            readText = File.ReadAllText(path);

            List<Commander> hq = new List<Commander>();

            List<Commander> ruCommanders = new List<Commander>();
            List<Commander> frCommanders = new List<Commander>();

            foreach (string i in commanderNames)
            {
                for (int j = 0; j < JObject.Parse(readText).Value<int>(i); j++)
                {
                    Commander toAdd = CreateCommander(i);
                    if (toAdd.Side == Commander.CommanderSide.RussianEmpire)
                    {
                        ruCommanders.Add(toAdd);
                    }
                    if (toAdd.Side == Commander.CommanderSide.FrenchEmpire)
                    {
                        frCommanders.Add(toAdd);
                    }
                }
            }

            HeadQuarter toReturn = new HeadQuarter(ruCommanders, frCommanders);

            LogManager.Debug("HQ " + toReturn + " was assembled");

            return toReturn;
        }

        private static Commander CreateCommander(string CommanderName)
        {
            string path = "../../../Data/Cards/CommandersData/" + CommanderName + ".json";
            string readText = File.ReadAllText(path);

            var o = JObject.Parse(readText).Value<string>("ID");

            Commander toReturn = new Commander((Commander.CommanderSide)JObject.Parse(readText).Value<int>("Side"),
                JObject.Parse(readText).Value<string>("ID"),
                JObject.Parse(readText).Value<string>("Name"),
                JObject.Parse(readText).Value<string>("Description"),
                JObject.Parse(readText).Value<int>("Health")
                );

            LogManager.Debug("Commander " + toReturn + " has arrived");

            return toReturn;
        }

        public static Deck CreateDeck(string deckType = "classicDeck")
        {
            string path = "../../../Data/Cards/DeckData/" + deckType + ".json";
            string namesPath = "../../../Data/Cards/DeckData/cardNames.json";
            string readText = File.ReadAllText(namesPath);
            List<string> cardNames = JObject.Parse(readText).Value<Newtonsoft.Json.Linq.JArray>("cardNames").ToObject<List<string>>();
            readText = File.ReadAllText(path);

            List<Card> deck = new List<Card>();

            foreach (string i in cardNames)
            {
                for (int j = 0; j < JObject.Parse(readText).Value<int>(i); j++)
                {
                    deck.Add(CreateCard(i));
                }
            }

            Deck toReturn = new Deck(deck, new List<Card>());

            LogManager.Debug("Deck " + toReturn + " was created");

            return toReturn;
        }

        private static Card CreateCard(string cardName)
        {
            string path = "../../../Data/Cards/CardsData/" + cardName + ".json";
            string readText = File.ReadAllText(path);

            //Card toReturn = JsonConvert.DeserializeObject<Card>(readText);

            var o = JObject.Parse(readText).Value<string>("ID");

            Card toReturn = new Card((Card.CardType)JObject.Parse(readText).Value<int>("Type"),
                JObject.Parse(readText).Value<string>("ID"),
                JObject.Parse(readText).Value<string>("Name"),
                JObject.Parse(readText).Value<string>("Description"),
                JObject.Parse(readText).Value<int>("KnockStat"),
                JObject.Parse(readText).Value<int>("ReplacementStat"),
                JObject.Parse(readText).Value<int>("MainStat")
                );

            LogManager.Debug("Card " + toReturn + " was created");

            return toReturn;
        }

        public static Tablet CreateTablet(Commander commander)
        {
            return new Tablet(10, 26, null, null, null, commander, new Effects());
        }
        public static Game CreateGame()
        {
            Deck Deck = Manager.CreateDeck();
            HeadQuarter HQ = Manager.CreateHQ();
            List<Card> handOne = Deck.GetCards(6);
            List<Card> handTwo = Deck.GetCards(6);
            Player PlayerOne = new Player(Manager.CreateTablet(HQ.PickFR()), handOne);
            Player PlayerTwo = new Player(Manager.CreateTablet(HQ.PickRU()), handTwo);

            return new Game(PlayerOne, PlayerTwo, Deck, HQ, 0);
        }
    }
}
