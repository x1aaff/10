namespace _19lib
{
    public static class AnimalFactory
    {
        private static Random random = new();
        private static List<Type> animals = new List<Type>()
        {
            typeof(Mammal),
            typeof(Amphibia),
            typeof(Bird)
        };

        public static IAnimal CreateAnimal(string animalType, string species, string name, string desc)
        {
            switch (animals.FindIndex(x => x.Name == animalType))
            {
                case 0: return new Mammal(name, species, desc);
                case 1: return new Amphibia(name, species, desc);
                case 2: return new Bird(name, species, desc);
                default: return new NullAnimal();
            }
        }
    }
}
