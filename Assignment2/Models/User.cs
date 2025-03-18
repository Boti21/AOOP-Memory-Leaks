namespace Assignment2.Models;

public abstract class User {
    public int id { get; set; }
    public string name { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public abstract bool isteacher { get; }

}