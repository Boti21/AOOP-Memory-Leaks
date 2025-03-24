using System;
using System.Collections.Generic;

namespace Assignment2.Models;

public class Teacher : User {
    public List<Subject> subjects { get; set; }
    public Teacher() {
        subjects = new List<Subject>();
    }
}