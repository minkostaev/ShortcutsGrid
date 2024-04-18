namespace ShortcutsGrid.Tests.Ui;

using NUnit.Framework;
using Windows;
using System.IO;
using System.Reflection;
using System.Threading;

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
        mainWindow.Show();

        if (File.Exists(testhost))
            File.Delete(testhost);

        Assert.That(mainWindow, Is.InstanceOf<MainWindow>());
    }

    //[Test]
    //[Apartment(ApartmentState.STA)]
    //public void TestAbout()
    //{
    //    var about = new About();
    //    ///about.Show();

    //    Assert.IsInstanceOf<About>(about);
    //}

    public string GetInputFile(string fileName)
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