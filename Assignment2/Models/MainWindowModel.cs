using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Assignment2.Models;


public class MainWindowModel
{
    private string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Assets", "user_data.json");
    private string data { get; set; }
    public class ModelData {
        public List<User> users { get; set; }
        public List<Subject> subjects { get; set; }
    }
    private User current_user { get; set; }
    public List<User> users { get; set; }
    public List<Subject> subjects { get; set; }
    public List<Subject> studentEnrolledSubjects { get; set; }
    public List<Subject> studentNotEnrolledSubjects { get; set; }
    private ModelData modelData { get; set; }

    public uint no_users;
    public uint no_subjects;

    public bool IsStudent => current_user is Student;
    public bool IsTeacher => current_user is Teacher;


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

    public void logout () {
        current_user = null;
    }

    public int login (string iuser, string ipass, bool isteacher) {

        if (File.Exists(filePath)) {
            if(!File.ReadAllText(filePath).Contains(iuser)) {
                Console.WriteLine("User does not exist");
                return -1;
            }
            // Read data in again in someone changed it on disk
            fetch_data();
        }

        var tmp = current_user;
        current_user = users.Find(user => user.username == iuser);

        if(current_user != null && 
           current_user.password == ipass && 
           current_user.GetType() == (isteacher ? typeof(Teacher) : typeof(Student))) {
            
            studentEnrolledSubjects = new List<Subject>();
            if (current_user.GetType() == typeof(Student))
            {
                Student current_student = (Student)current_user;
                studentEnrolledSubjects = current_student.displayedSubjects;
                foreach (Subject subject in subjects)
                {
                    if (subject == null)
                    {
                        continue;
                    }
                    bool studentIsEnrolled = false;
                    foreach (uint id in subject.studentsEnrolled)
                    {
                        if (id == current_student.id)
                        {
                            studentIsEnrolled = true;
                        }
                    }
                    if (!studentIsEnrolled)
                    {
                        if (subject != null)
                        {
                            Console.WriteLine("hello");
                            studentNotEnrolledSubjects.Add(subject);
                        }
                            Console.WriteLine("world");
                    }
                }
            } else if (current_user.GetType() == typeof(Teacher))
            {
                Teacher current_teacher = (Teacher)current_user;
            }
            
            Console.WriteLine("Match");
            return 1;
        }

        current_user = tmp;
        
        Console.WriteLine("Incorrect Password");
        return 0;
    }

    public int create_subject (string iname, string idetails, string iteacher) {
        fetch_data();
        // Teacher current_teacher = (Teacher)users.Find(user => user.username == iteacher);
        Teacher current_teacher = (Teacher)current_user;
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
        uint? enrolled = current_student.enrolledSubjects.Find(id => id == isubject.id);
        if (enrolled != null) {
            Console.WriteLine ("Already enrolled in course");
            return -1;
        }
        isubject.studentsEnrolled.Add(current_student.id);
        if (current_student.enrolledSubjects == null) current_student.enrolledSubjects = new List<uint>();
        if (current_student.displayedSubjects == null) current_student.displayedSubjects = new List<Subject>();

        current_student.enrolledSubjects.Add(isubject.id);
        current_student.displayedSubjects.Add(isubject);

        push_data();

        return 0;
    }

    public int delete_subject (string iname) {
        fetch_data();
        if (current_user is not Teacher) {
            Console.WriteLine("You do not have the permissions to delete a subject");
            return -1;
        }
        Subject rsubject = subjects.Find(subject => subject.name == iname);
        if (rsubject == null) {
            Console.WriteLine("No such subject");
            return -1;
        }

        subjects.Remove(rsubject);

        foreach (var user in users) {
            if (user is Teacher teacher && teacher.subjects != null) {
                teacher.subjects.RemoveAll(s => s == rsubject.id); 
            }
        }
            
        foreach (var user in users) {
            if (user is Student student && student.enrolledSubjects != null) {
                student.enrolledSubjects.RemoveAll(s => s == rsubject.id);
            }
        }  

        push_data();

        return 0;
    }
 
    public int drop_subject (string iname) {
        fetch_data();
        if (current_user is not Student) {
            Console.WriteLine("You cannot drop courses as a teacher");
            return -1;
        }

        Student current_student = (Student)current_user;

        Subject rsubject = subjects.Find(subject => subject.name == iname);
        if (rsubject == null) {
            Console.WriteLine("No such course exists");
            return -1;
        }

        uint? enrollment = current_student.enrolledSubjects.Find(id => id == rsubject.id);

        if (enrollment == null) {
            Console.WriteLine("You are not enrolled in such a course");
            return -1;
        }

        current_student.enrolledSubjects.Remove(rsubject.id);
        current_student.displayedSubjects.Remove(rsubject);
        rsubject.studentsEnrolled.Remove(current_student.id);

        push_data();

        return 0;
    }
 
    private void fetch_data () {
        User tmp; // Save current user otherwise fetch results in dangling pointer
        if (current_user is Teacher) tmp = new Teacher();
        else tmp = new Student();

        tmp = current_user;

        data = File.ReadAllText(filePath); 
        if (!string.IsNullOrWhiteSpace(data)) {
            modelData = JsonSerializer.Deserialize<ModelData>(data, jsonOptions) ?? new ModelData();
            users = modelData.users;
            subjects = modelData.subjects;
            no_users = (uint)modelData.users.Count;
            no_subjects = (uint)modelData.subjects.Count;
        } 
        current_user = tmp;
    }
 
    private void push_data () {

        modelData.users = users;
        modelData.subjects = subjects;
        data = JsonSerializer.Serialize(modelData, jsonOptions);

        File.WriteAllText(filePath, data);

    }
}