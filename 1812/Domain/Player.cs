using System;
using System.Collections.Generic;
using System.Text;

namespace _1812.Domain
{
    public class Player
    {
        public Tablet tablet { get; private set; }
        public List<Card> hand { get; private set; }
        private static List<Card> toDrop { get; set; }
        public Player(Tablet tablet, List<Card> hand)
        {
            this.tablet = tablet;
            this.hand = hand;
        }
        public static List<Card> CleanBuffer()
        {
            List<Card> toReturn = toDrop;
            toDrop = new List<Card>();
            return toReturn;
        }
        public Interaction FirstPhase(int Dice)
        {
            Interaction interaction = new Interaction();

            List<Api.ActionType> allowedActions = new List<Api.ActionType>();

            int cannonCount = 0;
            foreach (Card i in hand)
            {
                if (i.Type == Card.CardType.Cannon)
                {
                    cannonCount++;
                }
            }

            if (cannonCount != 0)
            {
                allowedActions.Add(Api.ActionType.PerformKnockout);
            }
            if (tablet.operation != null)
            {
                allowedActions.Add(Api.ActionType.UseOperation);
            }
            allowedActions.Add(Api.ActionType.AttackCommander);
            allowedActions.Add(Api.ActionType.DamageCamp);

            Api.ActionType action = Api.ChooseAction(allowedActions);

            if (action == Api.ActionType.AttackCommander)
            {
                interaction.Attack = tablet.ActCommanderAttack(Dice);
            }
            if (action == Api.ActionType.DamageCamp)
            {
                interaction.Attack = tablet.ActCampAttack(Dice);
            }
            if (action == Api.ActionType.UseOperation)
            {
                Console.WriteLine("PLAYER FIRST PHASE OPERATION");
            }
            if (action == Api.ActionType.PerformKnockout)
            {
                List<Card.CardType> target = Api.ChooseWhatToKnockout();
                List<Card> weapon = Api.ChooseCardsToKnockoutWith(hand);
                Tuple<int, List<Card.CardType>> knockoutAction = tablet.ActKnockout(weapon, target);

                foreach (Card i in weapon) // убрать карты, которыми атакуем
                {
                    hand.Remove(i);
                }
                interaction.KnockOut = knockoutAction;
            }
            return interaction;
        }
    }
}
