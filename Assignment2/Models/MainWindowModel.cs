using System;
using System.IO;
using System.Text;

namespace Assignment2.Models;

public class MainWindowModel
{
    private string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Assets", "user_data.json");
    public Subject subject { get; }
    public Teacher user { get; set; }


    public MainWindowModel() {
        subject = new Subject("MATH3", "It will not be taught at all");
        user = new Teacher();
    }
    public int register (string iuser, string ipass) {

        if(File.ReadAllText(filePath).Contains(iuser)) {
            Console.WriteLine("User already exists");
            return 1;
        }

        string content = "\n\n" + iuser + "; \nPass: " + ipass;
        if (user.isteacher) content += "\nSubjects created: ";
        else content += "\nSubjects enrolled: ";

        File.AppendAllText(filePath, content);

        return 0;
    }

    public int login (string iuser, string ipass) {
        if(File.ReadAllText(filePath).Contains(iuser)) {
            Console.WriteLine("Match");
            return 1;
        }

        Console.WriteLine("No match.. :/");
        return 0;
    }
}