using System;
using System.Collections.Generic;

namespace Assignment2.Models;

public class Student : User {
    public List<Subject> enrolledSubjects { get; set; }
    // public override bool isteacher { get; } = false;
    public Student() {
        enrolledSubjects = new List<Subject>();
    }
}