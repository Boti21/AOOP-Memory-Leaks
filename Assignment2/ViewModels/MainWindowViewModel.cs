using Assignment2.Models;

namespace Assignment2.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    
    private MainWindowModel Model;

    public string Greeting { get; }
    public MainWindowViewModel()
    {
        Model = new MainWindowModel(); // Instantiate the model
        Model.register("Bjarne", "apparatus1234");
        Greeting = Model.subject.name;
    }

}
