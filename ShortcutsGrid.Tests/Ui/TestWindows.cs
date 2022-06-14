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

        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();

        if (File.Exists(testhost))
            File.Delete(testhost);

        Assert.IsInstanceOf<MainWindow>(mainWindow);
    }

    [Test]
    [Apartment(ApartmentState.STA)]
    public void TestAbout()
    {
        About about = new About();
        ///about.Show();

        Assert.IsInstanceOf<About>(about);
    }

    public string GetInputFile(string fileName)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();

        string content = string.Empty;
        var stream = assembly.GetManifestResourceStream("ShortcutsGrid.Tests.Resources." + fileName);
        if (stream != null)
        {
            var reader = new StreamReader(stream);
            content = reader.ReadToEnd();
        }

        return content;
    }

}