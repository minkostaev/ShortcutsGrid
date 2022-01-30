namespace ShortcutsGrid.Tests.Ui
{
    using NUnit.Framework;
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

            //MethodInfo? methodInfo = typeof(MainWindow).GetMethod("IsErrorDisplayed", BindingFlags.NonPublic | BindingFlags.Instance);
            //object[] parameters = { "" };
            //var result = methodInfo?.Invoke(mainWindow, parameters);

            //bool? assert = true;
            //if (result is bool)
            //{
            //    assert = result as bool?;
            //}

            if (File.Exists(testhost))
                File.Delete(testhost);

            Assert.IsInstanceOf<MainWindow>(mainWindow);//assert
        }

        //IMessageBox test
        //https://stackoverflow.com/questions/49146068/unit-tests-for-a-function-with-messagebox

        public string GetInputFile(string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            var stream = assembly.GetManifestResourceStream("ShortcutsGrid.Tests.Resources." + fileName);
            var reader = new StreamReader(stream);
            string text = reader.ReadToEnd();

            return text;
        }


    }
}
