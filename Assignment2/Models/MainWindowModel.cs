using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Assignment2.Models;


public class ModelData {
    public List<User> users { get; set; }
    public List<Subject> subjects { get; set; }
}
public class MainWindowModel
{
    private string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Assets", "user_data.json");
    private string data { get; set; }
    private User current_user { get; set; }
    public List<User> users { get; set; }
    public List<Subject> subjects { get; set; }
    private ModelData modelData { get; set; }


    private static readonly JsonSerializerOptions jsonOptions = new() {
        WriteIndented = true, // Pretty-print JSON
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        IncludeFields = true
    };

    public MainWindowModel() {
        modelData = new ModelData();
        subjects = new List<Subject>();
        users = new List<User>();
        if (File.Exists(filePath)) {
            data = File.ReadAllText(filePath);
            if (!string.IsNullOrWhiteSpace(data)) {
                modelData = JsonSerializer.Deserialize<ModelData>(data, jsonOptions) ?? new ModelData();
                users = modelData.users;
                subjects = modelData.subjects;
            }
        }
    }
    public int register (string iuser, string ipass, bool isteacher) {

        if (File.Exists(filePath)) {
            if(File.ReadAllText(filePath).Contains(iuser)) {
                Console.WriteLine("User already exists");
                return 1;
            }
            // Read data in again in someone changed it on disk
            data = File.ReadAllText(filePath);
            modelData = JsonSerializer.Deserialize<ModelData>(data, jsonOptions) ?? new ModelData();
            users = modelData.users;
            subjects = modelData.subjects;
        }
        else {
            users = new List<User>(); // If file doesn't exist, create a new list
        }

        current_user = isteacher ? new Teacher() : new Student();

        current_user.username = iuser;
        current_user.password = ipass;
        users.Add(current_user);

        modelData.users = users;
        modelData.subjects = subjects;
        data = JsonSerializer.Serialize(modelData, jsonOptions);

        File.WriteAllText(filePath, data);

        return 0;
    }

    public int login (string iuser, string ipass) {

        if (File.Exists(filePath)) {
            if(!File.ReadAllText(filePath).Contains(iuser)) {
                Console.WriteLine("User does not exist");
                return -1;
            }
            // Read data in again in someone changed it on disk
            data = File.ReadAllText(filePath);
            modelData = JsonSerializer.Deserialize<ModelData>(data, jsonOptions) ?? new ModelData();
            users = modelData.users;
            subjects = modelData.subjects;
        }

        // current_user = users.Find(user => current_user.username == iuser);
        current_user = users.Find(user => user.username == iuser);


        if(current_user != null && current_user.password == ipass) {
            Console.WriteLine("Match");
            return 1;
        }

        Console.WriteLine("No match.. :/");
        return 0;
    }

    public int create_subject (string iname, string idetails, string iteacher) {
        Teacher current_teacher = (Teacher)users.Find(user => user.username == iteacher);
        if (current_teacher == null || current_teacher is not Teacher) return -1;

        if (current_teacher.subjects == null) {
            current_teacher.subjects = new List<Subject>();
        }

        Subject subject = new Subject(iname, idetails, current_teacher.username);
        current_teacher.subjects.Add(subject);
        subjects.Add(subject);

        modelData.users = users;
        modelData.subjects = subjects;
        data = JsonSerializer.Serialize(modelData, jsonOptions);

        File.WriteAllText(filePath, data);

        return 0;        
    }
}