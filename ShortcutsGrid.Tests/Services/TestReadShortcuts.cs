namespace ShortcutsGrid.Tests.Services;

using NUnit.Framework;
using ShortcutsGrid.Services;
using System.IO;

[TestFixture]
internal class TestReadShortcuts
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        if (!File.Exists("testhostCSV.json"))
            File.Copy("testhost.csv", "testhostCSV.json");
    }

    [Test, Order(1)]
    public void TestFileToShortcuts_Csv()
    {
        var result = ReadShortcuts.FileToShortcuts();

        Assert.AreEqual(3, result.Count);
    }

    [Test, Order(2)]
    public void TestFileToShortcuts_Json()
    {
        File.Delete("testhost.csv");

        var result = ReadShortcuts.FileToShortcuts();

        Assert.AreEqual(4, result.Count);
    }

    [Test, Order(3)]
    public void TestFileToShortcuts_Break()
    {
        File.Delete("testhost.json");
        File.Move("testhostCSV.json", "testhost.json");

        var result = ReadShortcuts.FileToShortcuts();

        Assert.AreEqual(0, result.Count);
    }

    [Test, Order(4)]
    public void TestFileToShortcuts_NoFiles()
    {
        File.Delete("testhost.csv");
        File.Delete("testhost.json");

        var result = ReadShortcuts.FileToShortcuts();

        Assert.AreEqual(0, result.Count);
    }

}