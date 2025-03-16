// using Assignment2.Models

namespace Assignment2.Models;

public class MainWindowModel
{
    public Subject subject { get; }
    public Teacher teacher { get; set; }
    public MainWindowModel() {
        subject = new Subject("MATH3", "It will not be taught at all");
        teacher = new Teacher();
    }

    // public login (string iuser, string ipass) {
        
    // }

    public void register (string iuser, string ipass) {
        teacher.register(iuser, ipass);
    }
}