using Assignment2.Models;

namespace Assignment2.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    
    private MainWindowModel Model;

    public string Greeting { get; }
    public MainWindowViewModel()
    {
        Model = new MainWindowModel(); // Instantiate the model
        Model.register("Bjarne", "apparatus1234", true); // isteacher = true
        Model.register("Fateme", "WHAAAT??", true);
        Model.register("Balage", "tricking2000", false);
        Model.register("Arturo", "pogacs4", false);
        Model.login("Bjarne", "apparatus1234");
        Model.login("Paride", "fakenews");
    }

}
