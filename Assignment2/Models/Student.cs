using System;
using System.Collections.Generic;

namespace Assignment2.Models;

public class Student : Person {
    public List<int> enrolledSubjects { get; set; }
}