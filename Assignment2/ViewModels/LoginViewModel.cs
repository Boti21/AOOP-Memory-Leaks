using System.Collections.Generic;
using Assignment2.Models;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System;
using System.IO;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;

namespace Assignment2.ViewModels;

public enum UserType
{
    Student,
    Teacher
}

public class LoginViewModel : ViewModelBase
{
    private readonly MainWindowModel _model;
    private readonly MainWindowViewModel _viewModel;

    // private string _username;
    // private string _password;

    // public string username
    // {
    //     get => _username;
    //     set => SetProperty(ref _username, value);
    // }
    // public string password
    // {
    //     get => _password;
    //     set => SetProperty(ref _password, value);
    // }

    public string username {get;set;}
    public string password {get;set;}


    public bool teacherChecked { get; set; }
    public bool studentChecked { get; set; }
    public bool isteacher {get;set;}

    private UserType _currentUserType;
    public UserType CurrentUserType
    {
        get => _currentUserType;
        set => SetProperty(ref _currentUserType, value);
    }

    public IRelayCommand LoginCommand { get; }
    public IRelayCommand RegisterCommand { get; }

    public LoginViewModel(MainWindowViewModel viewModel, MainWindowModel model)
    {
        _model = model;
        _viewModel = viewModel;

        // Setting default state of radio buttons
        teacherChecked = true;
        studentChecked = false;

        // LoginCommand = new RelayCommand(async () => await LoginAndUpdateView());
        // RegisterCommand = new RelayCommand(() => Register());
    }

    public void login () {
        bool loginSuccessful = _model.login(username, password) == 1;

        if (loginSuccessful) 
        {
            if (_model.current_user is Student student)
            {
                _viewModel.LinkedSubjects = new ObservableCollection<Subject>(student.enrolledSubjects);
                        
                // Now refresh the LinkedSubjects
                _viewModel.LinkedSubjects.Clear();
                foreach (var subj in student.enrolledSubjects)
                {
                    _viewModel.LinkedSubjects.Add(subj);
                }

                _viewModel.CurrentView = _viewModel.StudentView;
            }
            else if (_model.current_user is Teacher teacher)
            {
                teacher.subjects = _model.subjects
                .Where(s => s.teacher.id == teacher.id) // or s.teacherId == teacher.id if you store just an ID
                .ToList();

                _viewModel.LinkedSubjects = new ObservableCollection<Subject>(teacher.subjects);
                _viewModel.CurrentView = _viewModel.TeacherView;
            }
        }

        _viewModel.AllSubjects = new ObservableCollection<Subject>(_model.subjects);
    }

    public void register () {
        _model.register(username, password, teacherChecked);
    }
}