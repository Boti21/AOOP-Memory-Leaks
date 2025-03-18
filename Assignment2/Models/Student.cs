using System;
using System.Collections.Generic;

namespace Assignment2.Models;

public class Student : User {
    public List<int> enrolledSubjects { get; set; }
    // public override bool isteacher { get; } = false;
}