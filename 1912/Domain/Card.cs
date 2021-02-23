namespace _1912.Domain
{

    public class Card
    {
        public string ID { get; }
        public string Name { get; }
        public string Description { get; }
        public string ImageFolder { get; }

        public enum CardType
        {
            Battalion,
            Cannon,
            Operation
        };

        public CardType Type { get; }

        public int KnockStat { get; }
        public int ReplacementStat { get; }
        public int MainStat { get; }

        public Card(CardType type, string id, string name, string description, int knockStat = 0, int replacementStat = 0, int mainStat = 0)
        {
            Type = type;
            ID = id;
            Name = name;
            Description = description;
            KnockStat = knockStat;
            ReplacementStat = replacementStat;
            MainStat = mainStat;
            ImageFolder = "../../../Data/Images/CardImages/" + ID + ".png"; //проверить, написать норм путь
        }

        public Card() { }

        public override string ToString()
        {
            return Name;
        }
    }
}