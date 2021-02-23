using System;
using System.Collections.Generic;

namespace _1912.Domain
{
    class HeadQuarter
    {

        private List<Commander> russianCommanders;
        private List<Commander> frenchCommanders;

        private List<Commander> Shuffle(List<Commander> toShuffle)
        {
            List<Commander> data = toShuffle;
            for (int i = data.Count - 1; i >= 1; i--)
            {
                var rand = new Random();
                int j = rand.Next(i + 1);
                // обменять значения data[j] и data[i]
                Commander temp = data[j];
                data[j] = data[i];
                data[i] = temp;
            }

            return data;
        }

        public HeadQuarter(List<Commander>ruCommanders, List<Commander> frCommanders)
        {
            russianCommanders = Shuffle(ruCommanders);
            frenchCommanders = Shuffle(frCommanders);
        }

        public Commander PickRU()
        {
            return pick(Commander.CommanderSide.RussianEmpire);
        }

        public Commander PickFR()
        {

            return pick(Commander.CommanderSide.FrenchEmpire);
        }

        public Commander Swap(Commander toSwap)
        {
            return pick(toSwap.Side, toSwap);
        }

        private Commander pick(Commander.CommanderSide side, Commander toSwap = null)
        {
            Commander toReturn = null;
            if (side == Commander.CommanderSide.RussianEmpire)
            {
                toReturn = russianCommanders[0];

                russianCommanders.Remove(toReturn);
                if (toSwap != null)
                {
                    russianCommanders.Add(toSwap);
                }
                russianCommanders = Shuffle(russianCommanders);
            }
            if (side == Commander.CommanderSide.FrenchEmpire)
            {
                toReturn = frenchCommanders[0];

                frenchCommanders.Remove(toReturn);
                if (toSwap != null)
                {
                    frenchCommanders.Add(toSwap);
                }
                frenchCommanders = Shuffle(frenchCommanders);
            }

            return toReturn;
        }

        public override string ToString()
        {
            return russianCommanders.Count.ToString() + " ru, " + frenchCommanders.Count.ToString() + " fr";
        }
    }
}
