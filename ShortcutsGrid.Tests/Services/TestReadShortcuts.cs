namespace ShortcutsGrid.Tests.Services;

using NUnit.Framework;
using ShortcutsGrid.Models;
using ShortcutsGrid.Services;
using System.IO;

[TestFixture]
internal class TestReadShortcuts
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Copy("testhost.csv", "testhostCSV.json");
    }

    [Test, Order(1)]
    public void TestFileToShortcuts_Csv()
    {
        FileShortcuts.FileToShortcuts();

        Assert.That(4, Is.EqualTo(AppValues.Shortcuts.Count));
    }

    [Test, Order(2)]
    public void TestFileToShortcuts_Json()
    {
        File.Delete("testhost.csv");

        FileShortcuts.FileToShortcuts();

        Assert.That(5, Is.EqualTo(AppValues.Shortcuts.Count));
    }

    [Test, Order(3)]
    public void TestFileToShortcuts_Break()
    {
        File.Delete("testhost.json");
        File.Move("testhostCSV.json", "testhost.json");

        FileShortcuts.FileToShortcuts();
         
        Assert.That(1, Is.EqualTo(AppValues.Shortcuts.Count));
    }

    [Test, Order(4)]
    public void TestFileToShortcuts_NoFiles()
    {
        File.Delete("testhost.csv");
        File.Delete("testhost.json");

        FileShortcuts.FileToShortcuts();

        Assert.That(1, Is.EqualTo(AppValues.Shortcuts.Count));
    }

}