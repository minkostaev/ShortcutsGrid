namespace ShortcutsGrid.Windows;

using System.Windows;

public interface IMessageBoxWrapper
{
    MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon);
}
public class MessageBoxWrapper : IMessageBoxWrapper
{
    public MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
    {
        return MessageBox.Show(messageBoxText, caption, button, icon);
    }
}
public class MessageDialogs(IMessageBoxWrapper? messageBox = null)
{
    private readonly IMessageBoxWrapper _messageBox = messageBox ?? new MessageBoxWrapper();
    public bool IsErrorDisplayed(string error)
    {
        if (!string.IsNullOrWhiteSpace(error))
        {
            _messageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return true;
        }
        return false;
    }
    public void SimpleError(string message, string caption = "Error")
    {
        _messageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
    }
}