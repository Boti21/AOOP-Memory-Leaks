namespace Assignment2.Models;

using System.Text.Json.Serialization;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(Teacher), "teacher")]
[JsonDerivedType(typeof(Student), "student")]

public abstract class User {
    public uint id { get; set; }
    public string name { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string salt { get; set;}
}