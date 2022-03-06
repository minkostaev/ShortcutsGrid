namespace ShortcutsGrid.Models;

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
