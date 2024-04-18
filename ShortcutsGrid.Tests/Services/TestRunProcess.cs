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

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestRunCommand()
    {
        string result = RunProcess.Run("explorer");

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void TestRunWrongCommand()
    {
        string result = RunProcess.Run("explorera");

        Assert.That(result, Is.Not.Empty);
    }

}