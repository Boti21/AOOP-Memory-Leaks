using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Linq;


namespace Assignment2.Models;


public class ModelData {
    public List<User> users { get; set; }
    public List<Subject> subjects { get; set; }
}

public class MainWindowModel
{
    private string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Assets", "user_data.json");
    private string data { get; set; }
    public User current_user { get; set; }
    public List<User> users { get; set; }
    public List<Subject> subjects { get; set; }
    private ModelData modelData { get; set; }

    public uint no_users;
    public uint no_subjects;

    private static readonly JsonSerializerOptions jsonOptions = new() {
        WriteIndented = true, // Pretty-print JSON
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        IncludeFields = true,
        ReferenceHandler = ReferenceHandler.Preserve
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

    public int create_subject (string iname, string idetails) {
        fetch_data();
        if (current_user is not Teacher) {
            Console.WriteLine("You don't have the privilages");
            return -1;
        }
        
        // Teacher current_teacher = (Teacher)users.Find(user => user.username == iteacher);
        Teacher current_teacher = (Teacher)current_user;
        if (current_teacher == null || current_teacher is not Teacher) return -1;

        if (subjects.Find(subject => subject.name.Trim() == iname.Trim()) != null) {
            Console.WriteLine("Subject already exists");
            return -1;
        }

        if (current_teacher.subjects == null) {
            current_teacher.subjects = new List<Subject>();
        }

        Subject subject = new Subject(iname, idetails, current_teacher);
        subject.id = no_subjects;
        no_subjects++;
        current_teacher.subjects.Add(subject);
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

        if (current_student.enrolledSubjects == null) current_student.enrolledSubjects = new List<Subject>();
        
        Subject? enrolled = current_student.enrolledSubjects.Find(sub => sub == isubject);

        if (enrolled != null) {
            Console.WriteLine ("Already enrolled in course");
            return -1;
        }

        isubject.studentsEnrolled.Add(current_student);

        current_student.enrolledSubjects.Add(isubject);


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
                teacher.subjects.RemoveAll(s => s == rsubject); 
            }
        }
            
        foreach (var user in users) {
            if (user is Student student && student.enrolledSubjects != null) {
                student.enrolledSubjects.RemoveAll(s => s == rsubject);
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

        Subject? foundSub = current_student.enrolledSubjects.Find(sub => sub.id == rsubject.id);

        if (foundSub == null) {
            Console.WriteLine("You are not enrolled in such a course");
            return -1;
        }

        foreach (Subject subj in current_student.enrolledSubjects) {
            Console.WriteLine(subj.name);
        }

        current_student.enrolledSubjects.Remove(foundSub);
        rsubject.studentsEnrolled.Remove(current_student);

        foreach (Subject subj in current_student.enrolledSubjects) {
            Console.WriteLine(subj.name);
        }

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