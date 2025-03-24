using System.Collections.ObjectModel;
using System.Collections.ObjectModel;
using Assignment2.Models;
using Assignment2.Views;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

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

    [ObservableProperty]
    private string dropTextBox;

    [ObservableProperty]
    private string subName;

    [ObservableProperty]
    private string subDet;

    [ObservableProperty]
    private string delSubName;
    [ObservableProperty]
    private ObservableCollection<Subject> linkedSubjects = new ObservableCollection<Subject>();

    [ObservableProperty]
    private ObservableCollection<Subject> allSubjects = new ObservableCollection<Subject>();
    
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

        if (model.current_user is Student student)
        {
            linkedSubjects = new ObservableCollection<Subject>(student.enrolledSubjects);
        }
        else if (model.current_user is Teacher teacher) {
            linkedSubjects = new ObservableCollection<Subject>(teacher.subjects);
        }

        allSubjects = new ObservableCollection<Subject>(model.subjects);
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
    
    // Wrapper for Enroll button
    public void EnrollButton()
    {
        model.enroll_subject(enrollTextBox);

//      if (model.current_user != null)
        // {
        //     var updatedUser = model.users.Find(u => u.username == model.current_user.username);
        //     if (updatedUser != null)
        //     {
        //         model.current_user = updatedUser;
        //     }
        // }
        // UpdateUI();
        if (model.current_user is Student student) {
            LinkedSubjects.Clear();
            foreach (var subj in student.enrolledSubjects)
            {
                LinkedSubjects.Add(subj);
            }
        }
    }

    public void DropSubject () {
        model.drop_subject(dropTextBox);
        // if (model.current_user != null)
        // {
        //     var updatedUser = model.users.Find(u => u.username == model.current_user.username);
        //     if (updatedUser != null)
        //     {
        //         model.current_user = updatedUser;
        //     }
        // }
        // UpdateUI();
        if (model.current_user is Student student) {
            LinkedSubjects.Clear();
            foreach (var subj in student.enrolledSubjects)
            {
                LinkedSubjects.Add(subj);
            }
        }
    }

    public void CreateSub () {
        model.create_subject(subName, subDet);

        // if (model.current_user != null)
        // {
        //     var updatedUser = model.users.Find(u => u.username == model.current_user.username);
        //     if (updatedUser != null)
        //     {
        //         model.current_user = updatedUser;
        //     }
        // }
        // UpdateUI();
        if (model.current_user is Teacher teacher)
        {
            // Refresh the LinkedSubjects observable collection
            LinkedSubjects.Clear();
            foreach (var subj in teacher.subjects)
            {
                LinkedSubjects.Add(subj);
            }
        }
        AllSubjects = new ObservableCollection<Subject>(model.subjects);
    }

    public void DeleteSub()
    {
        model.delete_subject(delSubName);

        // First update AllSubjects since that's working correctly
        AllSubjects = new ObservableCollection<Subject>(model.subjects);
        
        if (model.current_user is Teacher teacher)
        {
            // Explicitly update the teacher's subjects to match what's in model.subjects
            teacher.subjects = model.subjects
                .Where(s => s.teacher?.id == teacher.id)
                .ToList();
                
            // Now refresh the LinkedSubjects
            LinkedSubjects.Clear();
            foreach (var subj in teacher.subjects)
            {
                LinkedSubjects.Add(subj);
            }
        }
    }

    public void Logout () {
        model.logout();
        // _loginView = new LoginView() { DataContext = new LoginViewModel(this, model) };
        // currentView = _loginView;
        CurrentView = new LoginView() { DataContext = new LoginViewModel(this) };
    }

}
