using System;
using System.IO;

namespace Assignment2.Models;

public class MainWindowModel
{
    public Subject subject { get; }
    public Teacher user { get; set; }
    public MainWindowModel() {
        subject = new Subject("MATH3", "It will not be taught at all");
        user = new Teacher();
    }

    // public login (string iuser, string ipass) {
        
    // }

    // public void register (string iuser, string ipass) {
    //     teacher.register(iuser, ipass);
    // }
    public void register (string iuser, string ipass) {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Assets", "user_data.json");
        string content = iuser + "; \nPass: " + ipass;
        if (user.isteacher) content += "\nSubjects created: ";
        else content += "\nSubjects enrolled: ";

        using (StreamWriter outputFile = new StreamWriter(filePath)) {
            outputFile.WriteLine(content);
        }
    } 
}