namespace _19lib
{
    public class Presenter
    {
        IView view;
        IModel model;

        public static List<IModel> models;
        public Presenter(IView view)
        {
            Model.LoadModels();
            models = Model.models;

            this.view = view;
            model = new ModelAdd();
        }

        public void Result()
        {
            model = view.SelectedModel == null ? new ModelAdd() : view.SelectedModel;
            model.GetData(view.Animals, view.CurrentAnimal, view.Strings);
            List<IAnimal> result = model.Result();
            view.Result = result;
        }
    }
}
