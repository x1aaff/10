namespace _19lib
{
    internal class NullAnimal : IAnimal
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public string Description { get; set; }

        public NullAnimal()
        {
            Name = "Alien";
            Species = "Alien species";
            Description = "N/A";
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {Species}, {Name}. {Description}";
        }
    }
}
