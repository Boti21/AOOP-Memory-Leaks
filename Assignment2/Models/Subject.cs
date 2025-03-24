using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;

namespace Assignment2.Models;

public class Subject {
    public string name { get; set; }
    public uint id { get; set; }
    public string details { get; set; }
    public uint teacher { get; set; }
    public List<Student> studentsEnrolled { get; set; }


    public Subject() {}

    public Subject(string iname, string idetails, uint iteacher) {
        name = iname;
        details = idetails;
        teacher = iteacher;
        studentsEnrolled = new List<Student>();
    }
}