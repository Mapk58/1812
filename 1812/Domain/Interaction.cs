using System;
using System.Collections.Generic;
using System.Text;

namespace _1812.Domain
{
    public class Interaction
    {
        public Attack Attack { get; set; }
        public Tuple<int, List<Card.CardType>> KnockOut { get; set; }
        public bool Sabotage { get; set; }
        public bool Substitution { get; set; }
        public Card SubToPlace { get; set; }
        public Card.CardType ToGet { get; set; }

        public Interaction(Attack attack, Tuple<int, List<Card.CardType>> knockOut, bool sabotage, bool substitution, Card subToPlace, Card.CardType toGet)
        {
            Attack = attack;
            KnockOut = knockOut;
            Sabotage = sabotage;
            Substitution = substitution;
            SubToPlace = subToPlace;
            ToGet = toGet;
        }

        public Interaction()
        {
            Attack = new Attack();
            KnockOut = null;
            Sabotage = false;
            Substitution = false;
            SubToPlace = null;
            ToGet = 0;
        }
    }
}
