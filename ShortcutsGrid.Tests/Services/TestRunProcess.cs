namespace ShortcutsGrid.Tests.Services;

using NUnit.Framework;
using ShortcutsGrid.Services.Run;

[TestFixture]
internal class TestRunProcess
{

    [Test]
    public void TestRunEmpty()
    {
        string result = RunProcess.Run("");
        
        Assert.IsEmpty(result);
    }

    [Test]
    public void TestRunCommand()
    {
        string result = RunProcess.Run("explorer");

        Assert.IsEmpty(result);
    }

    [Test]
    public void TestRunWrongCommand()
    {
        string result = RunProcess.Run("explorera");

        Assert.IsNotEmpty(result);
    }

}
