using System.Collections.Generic;
using Assignment2.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Assignment2.ViewModels;

public partial class StudentViewModel : ViewModelBase
{
    [ObservableProperty]
    public List<Subject> enrolledSubjects = new List<Subject>();
    
    

    public StudentViewModel()
    {
        Subject temp_subject = new Subject("asd", "foo", 0);
        enrolledSubjects.Add(temp_subject);
    }

    [RelayCommand]
    public void setup()
    {
        Subject temp_subject = new Subject("asd", "foo", 0);
        enrolledSubjects.Add(temp_subject);
    }
}