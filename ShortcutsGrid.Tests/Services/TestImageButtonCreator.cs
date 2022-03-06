namespace ShortcutsGrid.Tests.Services;

using NUnit.Framework;
using ShortcutsGrid.Services;
using System.Reflection;

[TestFixture]
internal class TestImageButtonCreator
{

    [Test]
    public void TestFolderOpeningString()
    {
        MethodInfo? methodInfo = typeof(ImageButtonCreator).
            GetMethod("FolderOpeningString", BindingFlags.Static | BindingFlags.NonPublic);
        object[] parameters = { "file.exe" };
        var result = methodInfo?.Invoke(null, parameters);

        string expected = "explorer.exe /select, \"file.exe\"";
        Assert.AreEqual(expected, result);
    }

    //reflection private
    ///MethodInfo? methodInfo = typeof(MainWindow)
    ///.GetMethod("IsErrorDisplayed", BindingFlags.NonPublic | BindingFlags.Instance);
    ///object[] parameters = { "" };
    ///var result = methodInfo?.Invoke(mainWindow, parameters);

}
