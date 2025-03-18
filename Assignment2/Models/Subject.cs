namespace Assignment2.Models;

public class Subject {
    public string name { get; set; }
    public string details { get; set; }

    public Subject(string name, string details) {
        this.name = name;
        this.details = details;
    }
}