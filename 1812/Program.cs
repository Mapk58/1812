using System;
using System.Collections.Generic;
using System.IO;
using _1812.Domain;

namespace _1812
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = Manager.CreateGame();
            game.PlayerOne.tablet.SetCannon(Api.ChooseCardsToPlaceOnTablet(game.PlayerOne.hand)[0]);
        }
    }
}
