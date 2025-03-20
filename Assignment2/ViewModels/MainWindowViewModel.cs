using System.Collections.ObjectModel;
using Assignment2.Models;
using Assignment2.Views;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Assignment2.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private MainWindowModel model;
    
    // If student then enrolled subjects
    private List<Subject> _studentsEnrolled;

    public List<Subject> studentsNotEnrolled { get; set; }
    
    public List<Subject> teachersTeaching { get; set; }

    [ObservableProperty] public UserControl currentView;
    private LoginView _loginView;

    public StudentView StudentView { get; private set; }
    public TeacherView TeacherView { get; private set; }
    
    // Enroll textbox
    [ObservableProperty]
    private string enrollTextBox;
    
    
    
    // Wrapper for Enroll button
    public void EnrollButton()
    {
        model.enroll_subject(enrollTextBox);
    }
    
    public MainWindowViewModel()
    {
        model = new MainWindowModel(); // Instantiate the model

        var loginViewModel = new LoginViewModel(this);
        _loginView = new LoginView() { DataContext = loginViewModel };

        StudentView = new StudentView() { DataContext =  this };
        TeacherView = new TeacherView() { DataContext = this };
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


        _studentsEnrolled = model.studentEnrolledSubjects;
        _studentsEnrolled.Add(new Subject("Example of how this should work", "but it doesn't, and we don't know why and we also do not have time to fix it", 0));
    }

    
    [RelayCommand]
    public void ToStudentView()
    {
        currentView = StudentView;
    }

    [RelayCommand]
    public void ToTeacherView()
    {
        //private TeacherView _teacherView = new TeacherView(){DataContext= new TeacherViewModel()};
        currentView = TeacherView;
    }
    /*
    */

}
