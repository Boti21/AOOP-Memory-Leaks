using System;
using System.IO;

namespace Assignment2.Models;

public class Person {
    public int id { get; set; }
    public string name { get; set; }
    public string username { get; set; }
    public string password { get; set; }

    public void register (string iuser, string ipass) {
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Assets", "user_data.json");
        string content = iuser + "; \nPass: " + ipass;

        using (StreamWriter outputFile = new StreamWriter(filePath)) {
            outputFile.WriteLine(content);
        }
    } 

    // public int login (string iuser, string ipass) {

    // }
}