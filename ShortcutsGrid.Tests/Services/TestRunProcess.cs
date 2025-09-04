namespace ShortcutsGrid.Tests.Services;

using NUnit.Framework;
using ShortcutsGrid.Services;

[TestFixture]
internal class TestRunProcess
{

    [Test]
    public void TestRunEmpty()
    {
        bool result = RunProcess.Run("");

        Assert.That(result, Is.False);
    }

    [Test]
    public void TestRunCommand()
    {
        bool result = RunProcess.Run("explorer");

        Assert.That(result, Is.True);
    }

    [Test]
    public void TestRunWrongCommand()
    {
        bool result = RunProcess.Run("explorera");

        Assert.That(result, Is.False);
    }

}