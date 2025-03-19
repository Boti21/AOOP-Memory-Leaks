using Assignment2.Models;
using Assignment2.Views;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Assignment2.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    
    private MainWindowModel Model;

    [ObservableProperty] public UserControl currentView;
    private StudentView _studentView = new StudentView(){DataContext=new StudentViewModel()};
    
    public MainWindowViewModel()
    {
        /*
        Model = new MainWindowModel(); // Instantiate the model
        Model.register("Bjarne", "apparatus1234", true); // isteacher = true
        Model.register("Fateme", "WHAAAT??", true);
        Model.register("Balage", "tricking2000", false);
        Model.register("Arturo", "pogacs4", false);
        Model.login("Bjarne", "apparatus1234");
        Model.login("Paride", "fakenews");
        */
        currentView = _studentView;
        
    }

    
    [RelayCommand]
    public void ToStudentView()
    {
        currentView = _studentView;
    }

}
