namespace _19lib
{
    internal class Bird : IAnimal
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public string Description { get; set; }

        public Bird(string name, string species, string description)
        {
            Name = name;
            Species = species;
            Description = description;
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {Species}, {Name}. {Description}";
        }
    }
}
