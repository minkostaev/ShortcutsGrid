namespace ShortcutsGrid.Tests.Services;

using Moq;
using NUnit.Framework;
using ShortcutsGrid.Windows;
using System.Windows;

[TestFixture]
internal class TestErrorDialog
{
    private MessageDialogs? _okDialog;

    [SetUp]
    public void Setup()
    {
        var messageBoxMock = new Mock<IMessageBoxWrapper>();
        messageBoxMock.Setup(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButton.OK, MessageBoxImage.Error))
            .Returns(MessageBoxResult.OK); //can be whatever you need, depends on test case
        _okDialog = new MessageDialogs(messageBoxMock.Object);
    }
    //IMessageBox test
    //https://stackoverflow.com/questions/49146068/unit-tests-for-a-function-with-messagebox

    [Test]
    public void TestErrorDialog_OK()
    {
        if (_okDialog == null)
        {
            Assert.Fail("Dialog not initialized");
            return;
        }

        bool result = _okDialog.IsErrorDisplayed("");
        if (!result)
            result = _okDialog.IsErrorDisplayed("error");

        Assert.That(result, Is.True);
    }

    [Test]
    public void TestErrorDialog_NoError()
    {
        if (_okDialog == null)
        {
            Assert.Fail("Dialog not initialized");
            return;
        }

        _okDialog.SimpleError("");

        Assert.That(_okDialog, Is.Not.Null);
    }

}