namespace _19lib
{
    static class AnimalImport
    {
        public static List<IAnimal> LoadAnimals(string fileName)
        {
            //Mammal: Akerman Mammal, Jokcx. Small
            List<IAnimal> animals = new List<IAnimal>();
            using (StreamReader sr = new StreamReader("animals_texted.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string type = line.Split(':')[0].Trim();
                    string species = line.Split(':')[1].Split(',')[0].Trim();
                    string name = line.Split(',')[1].Split('.')[0].Trim();
                    string dsc = line.Split('.')[1].Trim();
                    animals.Add(AnimalFactory.CreateAnimal(type, species, name, dsc));
                }
            }

            return animals;
        }
    }
}
