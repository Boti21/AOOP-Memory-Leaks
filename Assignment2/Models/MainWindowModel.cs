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

    public uint no_users;
    public uint no_subjects;

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
            fetch_data();
        }
        
    }
    public int register (string iuser, string ipass, bool isteacher) {

        if (File.Exists(filePath)) {
            if(File.ReadAllText(filePath).Contains(iuser)) {
                Console.WriteLine("User already exists");
                return 1;
            }
            // Read data in again in someone changed it on disk
            fetch_data();
        }
        else {
            users = new List<User>(); // If file doesn't exist, create a new list
        }

        current_user = isteacher ? new Teacher() : new Student();

        current_user.id = no_users;
        no_users++;
        current_user.username = iuser;
        current_user.password = ipass;

        users.Add(current_user);

        push_data();

        return 0;
    }

    public int login (string iuser, string ipass) {

        if (File.Exists(filePath)) {
            if(!File.ReadAllText(filePath).Contains(iuser)) {
                Console.WriteLine("User does not exist");
                return -1;
            }
            // Read data in again in someone changed it on disk
            fetch_data();
        }

        current_user = users.Find(user => user.username == iuser);


        if(current_user != null && current_user.password == ipass) {
            Console.WriteLine("Match");
            return 1;
        }

        Console.WriteLine("Incorrect password");
        return 0;
    }

    public void logout () {
        current_user = null;
    }

    public int create_subject (string iname, string idetails, string iteacher) {
        fetch_data();
        Teacher current_teacher = (Teacher)users.Find(user => user.username == iteacher);
        if (current_teacher == null || current_teacher is not Teacher) return -1;

        if (subjects.Find(subject => subject.name.Trim() == iname.Trim()) != null) {
            Console.WriteLine("Subject already exists");
            return -1;
        }

        if (current_teacher.subjects == null) {
            current_teacher.subjects = new List<uint>();
        }

        Subject subject = new Subject(iname, idetails, current_teacher.id);
        subject.id = no_subjects;
        no_subjects++;
        current_teacher.subjects.Add(subject.id);
        subjects.Add(subject);

       push_data();

        return 0;        
    }

    public int enroll_subject (string iname) {
        fetch_data();
        current_user = users.Find(user => user.username == current_user.username);
        if (current_user == null || current_user is not Student) return -1;
        Subject isubject = subjects.Find(subject => subject.name == iname);

        if (isubject == null) {
            Console.WriteLine("No such subject");
            return -1;
        }

        Student current_student = (Student)current_user;
        isubject.studentsEnrolled.Add(current_student.id);
        if (current_student.enrolledSubjects == null) current_student.enrolledSubjects = new List<uint>();
        current_student.enrolledSubjects.Add(isubject.id);

        push_data();

        return 0;
    }

    private void fetch_data () {
        data = File.ReadAllText(filePath); 
        if (!string.IsNullOrWhiteSpace(data)) {
            modelData = JsonSerializer.Deserialize<ModelData>(data, jsonOptions) ?? new ModelData();
            users = modelData.users;
            subjects = modelData.subjects;
            no_users = (uint)modelData.users.Count;
            no_subjects = (uint)modelData.subjects.Count;
        } 
    }

    private void push_data () {
        modelData.users = users;
        modelData.subjects = subjects;
        data = JsonSerializer.Serialize(modelData, jsonOptions);
        File.WriteAllText(filePath, data);
    }
}