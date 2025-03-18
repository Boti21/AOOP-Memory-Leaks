using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Assignment2.Models;


public class MainWindowModel
{
    private string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Assets", "user_data.json");
    private string data { get; set; }
    public List<User> users { get; set; }


    private static readonly JsonSerializerOptions jsonOptions = new() {
        WriteIndented = true, // Pretty-print JSON
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        IncludeFields = true
    };

    public MainWindowModel() {
        if (File.Exists(filePath)) {
            data = File.ReadAllText(filePath);
            if (!string.IsNullOrWhiteSpace(data)) {
                users = JsonSerializer.Deserialize<List<User>>(data, jsonOptions) ?? new List<User>();
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
            users = JsonSerializer.Deserialize<List<User>>(data, jsonOptions) ?? new List<User>();
        }
        else {
            users = new List<User>(); // If file doesn't exist, create a new list
        }

        User user = isteacher ? new Teacher() : new Student();

        user.username = iuser;
        user.password = ipass;
        users.Add(user);

        data = JsonSerializer.Serialize(users, jsonOptions);

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
            users = JsonSerializer.Deserialize<List<User>>(data, jsonOptions) ?? new List<User>();
        }

        User user = users.Find(user => user.username == iuser);

        if(user.password == ipass) {
            Console.WriteLine("Match");
            return 1;
        }

        Console.WriteLine("No match.. :/");
        return 0;
    }
}