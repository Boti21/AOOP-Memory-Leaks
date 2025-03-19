using System;
using System.Collections.Generic;

namespace Assignment2.Models;

public class Teacher : User {

    public List<uint> subjects { get; set; }
    public Teacher() {
        subjects = new List<uint>();
    }
}