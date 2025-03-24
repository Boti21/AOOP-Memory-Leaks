using Assignment2.Models;
using Assignment2.ViewModels;

namespace UnitTests.Tests;

public class MainWindowModelTests : IDisposable
{
    private readonly string actualFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Assets", "user_data.json");
    private readonly string testFilePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "Assets", "test_user_data.json");

    public MainWindowModelTests()
    {
        File.Copy(actualFilePath, testFilePath, true);
    }

    private MainWindowModel CreateModelInstance()
    {
        var Model = new MainWindowModel();

        typeof(MainWindowModel).GetField("filePath", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)?.SetValue(Model, testFilePath);

        return Model;
    }

    [Fact]
    public void RegisterExistingUser()
    {
        var Model = CreateModelInstance();
        
        int result = Model.register("Bjarne", "apparatus1234", true);

        Assert.Equal(1, result);
    }

    [Fact]
    public void LoginUnexistingUser()
    {
        var Model = CreateModelInstance();

        int result = Model.login("ShowThePainHarold", "pain");

        Assert.Equal(-1, result);
    }

    [Fact]
    public void LoginPasswordMismatch()
    {
        var Model = CreateModelInstance();

        int result = Model.login("Bjarne", "apparatus4321");

        Assert.Equal(0, result);
    }

    [Fact]
    public void CreateSubjectAlreadyExists()
    {
        var Model = CreateModelInstance();

        int result = Model.create_subject("Dynamics", "The other best course");

        Assert.Equal(-1, result);
    }

    [Fact]
    public void DeleteSubjectAsStudent()
    {
        var Model = CreateModelInstance();
        
        Model.login("Balage", "tricking2000");

        int result = Model.delete_subject("Dynamics");

        Model.logout();

        Assert.Equal(-1, result);
    }

    [Fact]
    public void EnrollUnexistingSubject()
    {
        var Model = CreateModelInstance();

        Model.login("Balage", "tricking2000");

        int result = Model.enroll_subject("MATH3");

        Assert.Equal(-1, result);
    }

    [Fact]

    public void DropSubjectAsTeacher()
    {
        var Model = CreateModelInstance();

        Model.login("Bjarne", "apparatus1234");

        int result = Model.drop_subject("MATH1");

        Assert.Equal(-1, result);
    }

    public void Dispose()
    {
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
        }
    }
}
