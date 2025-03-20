using System.Collections.Generic;
using Assignment2.Models;
using CommunityToolkit.Mvvm.Input;

namespace Assignment2.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private readonly MainWindowModel _model;
    private readonly MainWindowViewModel _viewModel;

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

    public LoginViewModel(MainWindowViewModel viewModel)
    {
        _model = new MainWindowModel();
        _viewModel = viewModel;

        LoginCommand = new RelayCommand(async () => await LoginAndUpdateView());
        RegisterCommand = new RelayCommand(() => _model.register(username, password, false));
    }

    public async Task LoginAndUpdateView()
    {
        bool loginSuccessful = _model.login(username, password) == 1;

        if (loginSuccessful)
        {
            if (_model.IsStudent)
            {
                _viewModel.CurrentView = _viewModel.StudentView;
                Console.WriteLine("Student");
            }
            else if (_model.IsTeacher)
            {
                Console.WriteLine("Teacher");
            }
            else
            {
                Console.WriteLine("Cannot find type of User");
            }
        }
    }
}