using ReactiveUI;
using System.Reactive;
using System.Collections.Generic;
using Assignment2.Models;
using CommunityToolkit.Mvvm.Input;

namespace Assignment2.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private readonly MainWindowModel _model;

    private string _username;
    private string _password;

    public string username
    {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }
    public string password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }

    public ReactiveCommand<Unit, Unit> LoginCommand { get; }
    public ReactiveCommand<Unit, Unit> RegisterCommand { get; }

    public LoginViewModel()
    {
        _model = new MainWindowModel();

        LoginCommand = ReactiveCommand.Create(() =>
        {
            int result = _model.login(username, password);

            if (result == 1)
            {
                Console.WriteLine("Match");
            }
            else if (result == 0)
            {
                Console.WriteLine("No match... :/");
            }
            else if (result == -1)
            {
                Console.WriteLine("User does not exist");
            }
        });
        RegisterCommand = ReactiveCommand.Create(() =>
        {
            bool isteacher = false; //Add from checkbox
            
            int result = _model.register(username, password, isteacher);

            if (result == 1)
            {
                Console.WriteLine("User already exists");
            }
            else if (result == 0)
            {
                Console.WriteLine("Successfully registered!");
            }
        });
    }
}