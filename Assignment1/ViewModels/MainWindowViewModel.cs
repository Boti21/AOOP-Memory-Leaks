using Assignment1.Models;
using Avalonia.Rendering;

namespace Assignment1.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    public string Greeting { get; set; } = "Welcome to Avalonia!";

    public MainWindowModel model;

    public void Test()
    {
        Greeting = this.model.otherGreeting;
    }
}