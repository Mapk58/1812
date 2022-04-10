using System;
using System.Collections.Generic;
using System.Text;

namespace _1812.Domain
{
    public class Game
    {
        public Player PlayerOne { get; private set; }
        public Player PlayerTwo { get; private set; }
        public Deck Deck { get; private set; }
        public HeadQuarter HQ { get; private set; }

        private int turn = 0;

        public Game(Player player1, Player player2, Deck deck, HeadQuarter hq, int turn)
        {
            PlayerOne = player1;
            PlayerTwo = player2;
            Deck = deck;
            HQ = hq;
            this.turn = turn;
        }

        private void Preparings()
        {
            List<Card> toPlace1 = Api.ChooseCardsToPlaceOnTablet(PlayerOne.hand);
            List<Card> toPlace2 = Api.ChooseCardsToPlaceOnTablet(PlayerTwo.hand);

            foreach (Card card in toPlace1)
            {
                if (card.Type == Card.CardType.Battalion)
                {
                    PlayerOne.tablet.SetBattalion(card);
                }
                if (card.Type == Card.CardType.Cannon)
                {
                    PlayerOne.tablet.SetCannon(card);
                }
                if (card.Type == Card.CardType.Operation)
                {
                    PlayerOne.tablet.SetOperation(card);
                }
            }
            foreach (Card card in toPlace2)
            {
                if (card.Type == Card.CardType.Battalion)
                {
                    PlayerTwo.tablet.SetBattalion(card);
                }
                if (card.Type == Card.CardType.Cannon)
                {
                    PlayerTwo.tablet.SetCannon(card);
                }
                if (card.Type == Card.CardType.Operation)
                {
                    PlayerTwo.tablet.SetOperation(card);
                }
            }

            turn += 1;
        }
    }
}
