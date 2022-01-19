namespace ShortcutsGrid.Tests.Ui
{
    using NUnit.Framework;
    using System.Reflection;
    using System.Threading;

    [TestFixture]
    internal class TestWindows
    {
        
        [Test]
        [Apartment(ApartmentState.STA)]
        public void TestMainWindow()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            MethodInfo? methodInfo = typeof(MainWindow).GetMethod("IsErrorDisplayed", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] parameters = { "" };
            var result = methodInfo?.Invoke(mainWindow, parameters);

            bool? assert = true;
            if (result is bool)
            {
                assert = result as bool?;
            }

            Assert.IsFalse(assert);
        }

        //IMessageBox test
        //https://stackoverflow.com/questions/49146068/unit-tests-for-a-function-with-messagebox

    }
}
