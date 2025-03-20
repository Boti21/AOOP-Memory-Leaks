﻿using Assignment2.Models;
using Assignment2.Views;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Assignment2.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    
    private MainWindowModel Model;

    [ObservableProperty] public UserControl currentView;
    private Login _loginView;

    public StudentView StudentView { get; private set; }
    
    public MainWindowViewModel()
    {
        Model = new MainWindowModel(); // Instantiate the model

        var loginViewModel = new LoginViewModel(this);
        _loginView = new Login() { DataContext = loginViewModel };

        StudentView = new StudentView() { DataContext = new StudentViewModel() };
        /*
        Model.register("Bjarne", "apparatus1234", true); // isteacher = true
        Model.register("Fateme", "WHAAAT??", true);
        Model.register("Balage", "tricking2000", false);
        Model.register("Arturo", "pogacs4", false);
        Model.login("Bjarne", "apparatus1234");
        Model.login("Paride", "fakenews");
        Model.create_subject("MATH1", "The best course you'll ever have", "Bjarne");
        Model.login("Balage", "triking1000");
        Model.login("Balage", "tricking2000");
        Model.enroll_subject("MATH3");
        Model.enroll_subject("MATH1");
        Model.login("Bjarne", "apparatus1234");
        Model.create_subject("Dynamics", "The other best course", "Bjarne");
        Model.delete_subject("MATH1");
        Model.login("Arturo", "pogacs4");
        Model.enroll_subject("Dynamics");
        Model.drop_subject("Dynamics");
        */
        currentView = _loginView;
        
        
    }

    
    [RelayCommand]
    public void ToStudentView()
    {
        currentView = StudentView;
    }

    // [RelayCommand]
    // public void ToTeacherView()
    // {
    //     private TeacherView _teacherView = new TeacherView(){DataContext= new TeacherViewModel()};

    //     currentView = _teacherView;
    // }

}
