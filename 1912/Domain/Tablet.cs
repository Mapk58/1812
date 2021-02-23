using System;
using System.Collections.Generic;
using System.Text;

namespace _1912.Domain
{
    struct Attack
    {
        public int commanderAttack;
        public int campAttack;
    }
    class Tablet
    {
        const int campHPMax = 26;
        const int commanderHPMax = 10;
        public int commanderHP { get { return commanderHP; } private set { if (value > commanderHPMax) commanderHP = commanderHPMax; else if (value < 0) commanderHP = 0; else commanderHP = value; } }
        public int campHP { get { return campHP; } private set { if (value > campHPMax) campHP = campHPMax; else if (value < 0) campHP = 0; else campHP = value; } }
        public Card battalion { get; private set; }
        public Card cannon { get; private set; }
        public Card operation { get; private set; }
        public Commander commander { get; private set; }
        public Effects effects { get; private set; }
        public Tablet(int commanderHP, int campHP, Card battalion, Card cannon, Card operation, Commander commander, Effects effects)
        {
            this.commanderHP = commanderHP;
            this.campHP = campHP;
            this.battalion = battalion;
            this.cannon = cannon;
            this.commander = commander;
            this.effects = effects;
        }
        public bool SetBattalion(Card toSet)
        {
            if (campHP - battalion.ReplacementStat > 0)
            {
                campHP -= battalion.ReplacementStat;

            }
        }
        private void CheckEffects(int Dice = -100)
        {
            effects = new Effects(cannon, battalion, null, Dice, commanderHP);
        }
        public Attack ActCampAttack(int Dice)
        {
            CheckEffects(Dice);
            Attack attack = new Attack();
            if (effects.AttackBoth)
            {
                attack.commanderAttack += Dice;
                attack.commanderAttack += cannon.MainStat;
                attack.commanderAttack += effects.Attack;
                attack.commanderAttack += effects.AttackCommander;
            }
            attack.campAttack += Dice;
            attack.campAttack += cannon.MainStat;
            attack.campAttack += effects.Attack;

            return attack;
        }
        public Attack ActCommanderAttack(int Dice)
        {
            CheckEffects(Dice);
            Attack attack = new Attack();
            if (effects.AttackBoth)
            {
                attack.campAttack += Dice;
                attack.campAttack += cannon.MainStat;
                attack.campAttack += effects.Attack;
            }
            attack.commanderAttack += Dice;
            attack.commanderAttack += cannon.MainStat;
            attack.commanderAttack += effects.Attack;
            attack.commanderAttack += effects.AttackCommander;
            return attack;
        }
        public int ActKnockout()
        {
            CheckEffects();
            return effects.AttackKnockout;
        }
        public List<Card> DefKnockout() // решить, где и в каком формате будут передаваться урон и выбиваемые карты
        {
            List<Card> toReturn = new List<Card>();



            return toReturn;
        }
        public bool Defence(Attack attack)
        {
            CheckEffects();
            int Commander = attack.commanderAttack;
            int Camp = attack.campAttack;

            Commander -= battalion.MainStat;
            Camp -= battalion.MainStat;
            Commander -= effects.DefenceBattalion;
            Camp -= effects.DefenceBattalion;

            Commander -= effects.DefenceCommander;

            if (Commander > 0)
            {
                commanderHP -= Commander;
            }
            if (Camp > 0)
            {
                campHP -= Commander;
            }

            if (campHP > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
