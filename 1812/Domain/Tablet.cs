using System;
using System.Collections.Generic;
using System.Text;

namespace _1812.Domain
{
    public struct Attack
    {
        public int commanderAttack;
        public int campAttack;
    }
    public class Tablet
    {
        private int commanderHP = 10;
        private int campHP = 26;

        const int campHPMax = 26;
        const int commanderHPMax = 10;
        public int CommanderHP { get { return commanderHP; } private set { if (value > commanderHPMax) commanderHP = commanderHPMax; else if (value < 0) commanderHP = 0; else commanderHP = value; } }
        public int CampHP { get { return campHP; } private set { if (value > campHPMax) campHP = campHPMax; else if (value < 0) campHP = 0; else campHP = value; } }
        public Card battalion { get; private set; }
        public Card cannon { get; private set; }
        public Card operation { get; private set; }
        public Commander commander { get; private set; }
        public Effects effects { get; private set; }
        public List<Card> toThrow { get; private set; }

        public Tablet(int commanderHP, int campHP, Card battalion, Card cannon, Card operation, Commander commander, Effects effects)
        {
            this.CommanderHP = commanderHP;
            this.CampHP = campHP;
            this.battalion = battalion;
            this.cannon = cannon;
            this.commander = commander;
            this.effects = effects;
        }
        public bool SetBattalion(Card toSet)
        {
            if (battalion is null)
            {
                battalion = toSet;
                return true;
            }
            if (CampHP - battalion.ReplacementStat > 0)
            {
                CampHP -= battalion.ReplacementStat;
                toThrow.Add(battalion);
                battalion = toSet;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool SetCannon(Card toSet)
        {
            if (cannon is null)
            {
                cannon = toSet;
                return true;
            }
            if (CampHP - cannon.ReplacementStat > 0)
            {
                CampHP -= cannon.ReplacementStat;
                toThrow.Add(cannon);
                cannon = toSet;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetOperation(Card toSet)
        {
            if (operation is null)
            {
                operation = toSet;
                return true;
            }
            toThrow.Add(operation);
            operation = toSet;
            return true;
        }
        private void CheckEffects(int Dice = -100)
        {
            effects = new Effects(cannon, battalion, null, Dice, CommanderHP);
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
        public Tuple<int, List<Card.CardType>> ActKnockout(List<Card> fromHand, List<Card.CardType> target)
        {
            int force = 0;
            foreach (Card i in fromHand)
            {
                if (i.Type == Card.CardType.Cannon)
                {
                    force += i.MainStat;
                }
            }
            CheckEffects();
            toThrow.AddRange(fromHand);
            return new Tuple<int, List<Card.CardType>>(effects.AttackKnockout + force, target);
        }
        public bool DefKnockout(Tuple<int, List<Card.CardType>> attack) // ������, ��� � � ����� ������� ����� ������������ ���� � ���������� �����
        {
            CheckEffects();
            int force = attack.Item1;
            if (attack.Item2.Count == 1)
            {
                if (attack.Item2[0] == Card.CardType.Battalion)
                {
                    if (battalion.KnockStat + effects.DefenceKnockout <= force)
                    {
                        toThrow.Add(battalion);
                        battalion = null;
                        return true;
                    }
                }
                if (attack.Item2[0] == Card.CardType.Cannon)
                {
                    if (cannon.KnockStat + effects.DefenceKnockout <= force)
                    {
                        toThrow.Add(cannon);
                        cannon = null;
                        return true;
                    }
                }
            }
            else
            if (attack.Item2.Count == 2)
            {
                if (battalion.KnockStat + cannon.KnockStat + effects.DefenceKnockout <= force)
                {
                    toThrow.Add(battalion);
                    toThrow.Add(cannon);
                    battalion = null;
                    cannon = null;
                    return true;
                }
            }

            return false;
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
                CommanderHP -= Commander;
            }
            if (Camp > 0)
            {
                CampHP -= Commander;
            }

            if (CampHP > 0)
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
