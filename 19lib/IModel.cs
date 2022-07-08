namespace _19lib
{
    public interface IModel
    {
        List<IAnimal> Result();
        void GetData(List<IAnimal> animals, IAnimal currentAnimal, List<string> strings);
    }

    public abstract class Model : IModel
    {
        internal List<IAnimal> animals;
        internal IAnimal currentAnimal;
        internal List<string> strings;

        public static List<IModel> models;

        public Model() { }

        public void GetData(List<IAnimal> animals, IAnimal currentAnimal, List<string> strings)
        {
            this.animals = animals;
            this.currentAnimal = currentAnimal;
            this.strings = strings;
        }
        public static void LoadModels()
        {
            models = new List<IModel>();
            models.Add(new ModelAdd());
            models.Add(new ModelDelete());
            models.Add(new ModelEdit());
            models.Add(new ModelSave());
            models.Add(new ModelGen());
            models.Add(new ModelLoad());
        }

        public virtual List<IAnimal> Result()
        {
            return animals;
        }
    }

    public class ModelAdd : Model
    {
        public override List<IAnimal> Result()
        {
            animals.Add(AnimalFactory.CreateAnimal(strings[0], strings[1], strings[2], strings[3]));
            return animals;
        }
    }

    public class ModelDelete : Model
    {
        public override List<IAnimal> Result()
        {
            animals.Remove(currentAnimal);
            return animals;
        }
    }

    public class ModelEdit : Model
    {
        public override List<IAnimal> Result()
        {
            var animal = animals.Find(x => x == currentAnimal);
            animal.Species = strings[1];
            animal.Name = strings[2];
            animal.Description = strings[3];
            return animals;
        }
    }

    public class ModelSave : Model
    {
        public override List<IAnimal> Result()
        {
            var animalExportTxt = new AnimalExportTxt("animals_texted");
            var animalExportJson = new AnimalExportJson("animals_jsoned");

            AnimalBurner ab = new AnimalBurner(animalExportTxt);

            ab.Burn(animals);
            ab.Mode = animalExportJson;
            ab.Burn(animals);

            return animals;
        }
    }

    public class ModelGen : Model
    {
        public override List<IAnimal> Result()
        {
            if (animals == null)
            {
                animals = new List<IAnimal>();
            }
            animals.Add(AnimalFactory.CreateAnimal("Mammal", "Akerman Mammal", "Jokcx", "Small"));
            animals.Add(AnimalFactory.CreateAnimal("Amphibia", "Flogstone Amphibia", "Xcv-123", "Cloned"));
            animals.Add(AnimalFactory.CreateAnimal("Mammal", "Sdax Mammal", "Nzzxre", "Egyptian"));
            animals.Add(AnimalFactory.CreateAnimal("Mushroom", "Some Mushroom", "777", "Not a number"));
            animals.Add(AnimalFactory.CreateAnimal("Bird", "Fffuu Bird", "Ololosh", "Pizza"));
            animals.Add(AnimalFactory.CreateAnimal("Bird", "Hot Bird", "Gunna", "Dope"));

            return animals;
        }
    }

    public class ModelLoad : Model
    {
        public override List<IAnimal> Result()
        {
            animals = AnimalImport.LoadAnimals("animals_texted.txt");

            return animals;
        }
    }
}
