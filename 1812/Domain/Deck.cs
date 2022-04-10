using System;
using System.Collections.Generic;

namespace _1812.Domain
{
    public class Deck
    {
        private Stack<Card> deckCards;
        private List<Card> throwCards;
        private List<Card> allCards;
        public int DeckCount { get { return deckCards.Count; } }
        public int ThrowCount { get { return throwCards.Count; } }
        public int AllCount { get { return allCards.Count; } }
        private static List<Card> Shuffle(List<Card> toShuffle)
        {
            List<Card> data = toShuffle;
            for (int i = data.Count - 1; i >= 1; i--)
            {
                var rand = new Random();
                int j = rand.Next(i + 1);
                // обменять значения data[j] и data[i]
                Card temp = data[j];
                data[j] = data[i];
                data[i] = temp;
            }

            return data;
        }
        private List<Card> Take(int count)
        {
            List<Card> toReturn = new List<Card>();

            for (int i = 0; i < count; i++)
            {
                toReturn.Add(deckCards.Pop());
            }
            return toReturn;
        }
        public List<Card> GetCards(int count)
        {
            if (count <= deckCards.Count)
            {
                return Take(count);
            }
            else
            {
                List<Card> toReturn = Take(deckCards.Count);

                foreach(Card i in Shuffle(throwCards))
                {
                    deckCards.Push(i);
                }
                throwCards.Clear();

                toReturn.AddRange(Take(count - toReturn.Count));
                return toReturn;
            }
        }

        public void PutCards(List<Card> toPut)
        {
            throwCards.AddRange(toPut);
        }

        public Deck(List<Card> cards, List<Card> drop)
        {
            deckCards = new Stack<Card>();
            allCards = cards;
            foreach (Card i in Shuffle(cards))
            {
                deckCards.Push(i);
            }
            throwCards = drop;
        }

        public override string ToString()
        {
            return "Колода из "+AllCount.ToString()+" карт";
        }
    }
}


