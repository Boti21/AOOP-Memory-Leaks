using System;
using System.Collections.Generic;

namespace Assignment2.Models;

public class Teacher : User {
    public List<int> subjects { get; set; }
    // public override bool isteacher { get; } = true;
}