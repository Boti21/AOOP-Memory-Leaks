

namespace Assignment2.Models;

public abstract class Person {
    public int id { get; set; }
    public string name { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public abstract bool isteacher { get; }

    // public void register (string iuser, string ipass, Person visitor) {
    //     string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Assets", "user_data.json");
    //     string content = iuser + "; \nPass: " + ipass;
    //     content = content + this.accept(visitor);

    //     using (StreamWriter outputFile = new StreamWriter(filePath)) {
    //         utputFile.WriteLine(content);
    //     }
    // } 

    // public int login (string iuser, string ipass) {

    // }
}