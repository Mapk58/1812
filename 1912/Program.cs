using System;
using System.Collections.Generic;
using System.IO;
using _1912.Domain;

namespace _1912
{

    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = Manager.CreateDeck();
            HeadQuarter generals = Manager.CreateHQ();
            Console.WriteLine(generals.PickFR().Description);
            Effects ef = new Effects();
        }
    }
}
