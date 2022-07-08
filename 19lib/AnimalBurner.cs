namespace _19lib
{
    public class AnimalBurner
    {
        public IAnimalSaver Mode { get; set; }

        public AnimalBurner(IAnimalSaver mode)
        {
            Mode = mode;
        }

        public void Burn(List<IAnimal> animals)
        {
            Mode.SaveAnimals(animals);
        }
    }
}
