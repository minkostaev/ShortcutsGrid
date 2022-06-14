namespace ShortcutsGrid.Tests.Services;

using Moq;
using NUnit.Framework;
using ShortcutsGrid.Models;
using ShortcutsGrid.Services;
using System.Windows;

[TestFixture]
internal class TestErrorDialog
{
    [Test]
    public void TestErrorDialog_OK()
    {
        Mock<IMessageBoxWrapper> messageBoxMock = new Mock<IMessageBoxWrapper>();
        messageBoxMock.Setup(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButton.OK, MessageBoxImage.Error))
            .Returns(MessageBoxResult.OK); //can be whatever you need, depends on test case
        MessageDialogs sut = new MessageDialogs(messageBoxMock.Object);

        bool result = sut.IsErrorDisplayed("");
        if (!result)
            result = sut.IsErrorDisplayed("error");

        Assert.IsTrue(result);
    }
    //IMessageBox test
    //https://stackoverflow.com/questions/49146068/unit-tests-for-a-function-with-messagebox
}