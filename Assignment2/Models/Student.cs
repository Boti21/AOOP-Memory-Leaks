using System;
using System.Collections.Generic;

namespace Assignment2.Models;

public class Student : User {
    public List<uint> enrolledSubjects { get; set; }
    
    public Student() {
        enrolledSubjects = new List<uint>();
    }
}