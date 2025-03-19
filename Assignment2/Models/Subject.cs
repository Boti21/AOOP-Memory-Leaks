namespace Assignment2.Models;

public class Subject {
    public string name { get; set; }
    public string details { get; set; }
    public string teacher { get; set; }

    public Subject(string iname, string idetails, string iteacher) {
        this.name = iname;
        this.details = idetails;
        this.teacher = iteacher;
    }
}