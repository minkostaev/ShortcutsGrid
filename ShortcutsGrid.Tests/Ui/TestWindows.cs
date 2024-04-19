namespace ShortcutsGrid.Tests.Ui;

using NUnit.Framework;
using System.IO;
using System.Reflection;
using System.Threading;
using Windows;

[TestFixture]
internal class TestWindows
{

    [Test]
    [Apartment(ApartmentState.STA)]
    public void TestMainWindow()
    {
        string testhost = "testhost.csv";
        string csvContent = GetInputFile(testhost);
        File.WriteAllText(testhost, csvContent);

        var mainWindow = new MainWindow();

        if (File.Exists(testhost))
            File.Delete(testhost);

        Assert.That(mainWindow, Is.InstanceOf<MainWindow>());
    }

    [Test]
    [Apartment(ApartmentState.STA)]
    public void TestAbout()
    {
        var about = new About();

        Assert.That(about, Is.InstanceOf<About>());
    }

    public static string GetInputFile(string fileName)
    {
        string result = string.Empty;
        Assembly assembly = Assembly.GetExecutingAssembly();
        var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Resources." + fileName);
        if (stream != null)
        {
            var reader = new StreamReader(stream);
            result = reader.ReadToEnd();
            reader.Dispose();
        }
        return result;
    }

}