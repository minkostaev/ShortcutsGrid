namespace ShortcutsGrid.Tests.Services;

using Moq;
using NUnit.Framework;
using ShortcutsGrid.Models;
using ShortcutsGrid.Services;
using ShortcutsGrid.Windows;
using System.Windows;

[TestFixture]
internal class TestErrorDialog
{
    [Test]
    public void TestErrorDialog_OK()
    {
        var messageBoxMock = new Mock<IMessageBoxWrapper>();
        messageBoxMock.Setup(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButton.OK, MessageBoxImage.Error))
            .Returns(MessageBoxResult.OK); //can be whatever you need, depends on test case
        var sut = new MessageDialogs(messageBoxMock.Object);

        bool result = sut.IsErrorDisplayed("");
        if (!result)
            result = sut.IsErrorDisplayed("error");

        Assert.That(result, Is.True);
    }
    //IMessageBox test
    //https://stackoverflow.com/questions/49146068/unit-tests-for-a-function-with-messagebox
}