using System;
using System.Collections.Generic;
using System.Text;


namespace _1912.Domain
{
    class Effects
    {
        public int DiceBonus { get; private set; }
        public int Attack { get; private set; }
        public int AttackKnockout { get; private set; }
        public int AttackCommander { get; private set; }
        public bool AttackBoth { get; private set; }
        public int DefenceCommander { get; private set; }
        public int DefenceKnockout { get; private set; }
        public int DefenceBattalion { get; private set; }
        public int HealCommander { get; private set; }
        public int HealCamp { get; private set; }
        public bool KeepOperation { get; private set; }
        public bool UniqueBattalionAbility { get; private set; }
        public Effects(Card cannon, Card battalion, Card operation, int Dice = -100, int CommanderHealth = 0)
        {
            CheckBattalion(battalion);
            CheckCannon(cannon);
            CheckBoth(cannon, battalion);
            CheckDiceEffects(battalion, operation, Dice, CommanderHealth);
        }

        public Effects()
        {

        }
        private void CheckBoth(Card cannon, Card battalion)
        {
            if (battalion.ID == "battalionWhite" && cannon.ID == "cannonWhite")
            {
                DiceBonus += 1;
            }
            else
            if (battalion.ID == "battalionGreen" && cannon.ID == "cannonGreen")
            {
                Attack += 3;
            }
            else
            if (battalion.ID == "battalionRed" && cannon.ID == "cannonRed")
            {
                Attack += 2;
            }
            else
            if (battalion.ID == "battalionBlue" && cannon.ID == "cannonBlue")
            {
                AttackKnockout += 4;
            }
            else
            if (battalion.ID == "battalionPurple" && cannon.ID == "cannonPurple")
            {
                DefenceBattalion += 2;
            }
        }
        private void CheckCannon(Card cannon)
        {
            if (cannon.ID == "cannonBlue")
            {
                DefenceKnockout += 2;
            }
            if (cannon.ID == "cannonGreen")
            {
                DefenceCommander += 2;
            }
            if (cannon.ID == "cannonSilver")
            {
                AttackBoth = true;
            }
            if (cannon.ID == "cannonWhite")
            {
                DiceBonus += 1;
            }
        }
        private void CheckBattalion(Card battalion)
        {
            if (battalion.ID == "battalionBlue")
            {
                AttackKnockout += 2;
            }
            if (battalion.ID == "battalionPurple")
            {
                AttackCommander += 2;
            }
            if (battalion.ID == "battalionTactical")
            {
                KeepOperation = true;
            }
            if (battalion.ID == "battalionUnique")
            {
                Attack += 2;
            }
            if (battalion.ID == "battalionWhite")
            {
                DiceBonus += 1;
            }
        }
        private void CheckDiceEffects(Card battalion, Card operation, int Dice, int CommanderHealth)
        {
            if (battalion.ID == "battalionGold" && DiceCheck(Dice, new List<int> { 5, 6 }))
            {
                HealCamp += 3;
            }
            if (battalion.ID == "battalionRed" && DiceCheck(Dice, new List<int> { 1, 2 }))
            {
                Attack += 4;
            }
            if (battalion.ID == "cannonLegendary")
            {
                Attack += CommanderHealth;
            }
            if (battalion.ID == "battalionUnique" && DiceCheck(Dice, new List<int> { 4, 5, 6 }))
            {
                UniqueBattalionAbility = true;
            }
            if (battalion.ID == "cannonFamily")
            {
                HealCommander += Dice;
            }
        }
        private bool DiceCheck(int Dice, List<int> values)
        {
            foreach (int i in values)
            {
                if (i <= Dice + DiceBonus && i >= Dice - DiceBonus)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
