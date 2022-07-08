namespace _19lib
{
    public interface IView
    {
        List<IAnimal> Animals { get; }
        IAnimal CurrentAnimal { get; }
        List<string> Strings { get; }
        IModel SelectedModel { get; }

        List<IAnimal> Result { set; }
    }
}
