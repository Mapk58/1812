namespace _1812.Domain
{

    public class Commander
    {
        public string ID { get; }
        public string Name { get; }
        public string Description { get; }
        public string ImageFolder { get; }

        public enum CommanderSide
        {
            RussianEmpire,
            FrenchEmpire
        };

        public CommanderSide Side { get; }
        public int Health { get; }

        public Commander(CommanderSide side, string id, string name, string description, int health = 0)
        {
            Side = side;
            ID = id;
            Name = name;
            Description = description;
            Health = health;
            ImageFolder = "../../../Data/Images/CommanderImages/" + ID + ".png"; //проверить, написать норм путь
        }

        public override string ToString()
        {
            return Name;
        }
    }
}