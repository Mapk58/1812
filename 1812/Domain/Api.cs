using System;
using System.Collections.Generic;
using System.Text;
using _1812.Domain;

namespace _1812.Domain
{
    public class Api
    {
        public enum ActionType
        {
            AttackCommander,
            DamageCamp,
            UseOperation,
            PerformKnockout
        }

        public static ActionType ChooseAction(List<ActionType> actions)
        {
            Console.WriteLine("Выберите тип действия, которое будете совершать: ");
            for (int i = 0; i < actions.Count; i++)
            {
                Console.WriteLine(i + " " + actions[i]);
            }

            ActionType decision = ActionType.DamageCamp; // api 

            return decision;
        }

        public static List<Card> ChooseChanges(List<Card> hand)
        {
            Console.WriteLine("Выберите карты на замену: ");

            List<Card> toReturn = new List<Card>();
            toReturn.Add(hand[0]);// api 

            return toReturn;
        }

        public static List<Card> ChooseCardsToKeep(List<Card> hand)
        {
            Console.WriteLine("Выберите 6 карт, которые хотите оставить: ");

            List<Card> toReturn = new List<Card>();
            for (int i = 0; i < 6 && i < hand.Count; i++)// api 
            {
                toReturn.Add(hand[i]);
            }
            return toReturn;
        }

        public static Card ChooseYour(List<Card> hand)
        {
            Console.WriteLine("Выберите карту, которую хотите заменить: ");
            for (int i = 0; i < hand.Count; i++)
            {
                Console.WriteLine(i + " " + hand[i]);
            }

            Card decision = hand[0];// api 

            return decision;
        }

        public static Card ChooseTheir(List<Card> hand)
        {
            Console.WriteLine("Выберите карту, которую хотите забрать: ");
            for (int i = 0; i < hand.Count; i++)
            {
                Console.WriteLine(i + " " + hand[i]);
            }

            Card decision = hand[0];// api 

            return decision;
        }

        public static List<Card.CardType> ChooseWhatToKnockout()
        {
            Console.WriteLine("Выберите, что хотите выбивать: ");

            List<Card.CardType> decision = new List<Card.CardType>();

            decision.Add(Card.CardType.Cannon);// api 

            return decision;
        }

        public static List<Card> ChooseCardsToKnockoutWith(List<Card> hand)
        {
            Console.WriteLine("Выберите, чем хотите выбивать: ");

            List<Card> cannons = new List<Card>();
            List<Card> decision = new List<Card>();
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i].Type == Card.CardType.Cannon)
                    cannons.Add(hand[i]);
            }
            for (int i = 0; i < cannons.Count; i++)
            {
                Console.WriteLine(i + " " + hand[i]);
            }

            decision.Add(cannons[0]);// api 

            return decision;
        }

        public static List<Card> ChooseCardsToPlaceOnTablet(List<Card> hand)
        {
            Console.WriteLine("Выберите карты, которые хотите разместить на поле: ");

            List<Card> toReturn = new List<Card>();
            toReturn.Add(hand[0]);// api 

            return toReturn;
        }
    }
}
