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
        set => SetProperty(ref _username, value);
    }
    public string password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    public IRelayCommand LoginCommand { get; }
    public IRelayCommand RegisterCommand { get; }

    public LoginViewModel()
    {
        _model = new MainWindowModel();

        LoginCommand = new RelayCommand(() => _model.login(username, password));
        RegisterCommand = new RelayCommand(() => _model.register(username, password, false));
    }
}