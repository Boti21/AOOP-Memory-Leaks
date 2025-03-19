using Assignment2.Models;

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

    public void Dispose()
    {
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
        }
    }
}
