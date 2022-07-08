using System.Text.Json;

namespace _19lib
{
    public class AnimalExportTxt : IAnimalSaver
    {
        private string fileName;
        public AnimalExportTxt(string fileName)
        {
            this.fileName = fileName;
        }

        public void SaveAnimals(List<IAnimal> animals)
        {
            using (StreamWriter sw = new StreamWriter($"{fileName}.txt"))
            {
                foreach (var animal in animals)
                {
                    sw.WriteLine(animal);
                }
            }
        }
    }

    public class AnimalExportJson : IAnimalSaver
    {
        private string fileName;
        public AnimalExportJson(string fileName)
        {
            this.fileName = fileName;
        }

        public void SaveAnimals(List<IAnimal> animals)
        {
            using (StreamWriter sw = new StreamWriter($"{fileName}.json"))
            {
                string jsonstring = JsonSerializer.Serialize(animals);
                sw.WriteLine(jsonstring);
            }
        }
    }
}
