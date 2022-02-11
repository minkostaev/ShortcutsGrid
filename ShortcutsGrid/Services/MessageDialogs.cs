namespace ShortcutsGrid.Services
{
    using ShortcutsGrid.Models;
    using System.Windows;

    public class MessageDialogs
    {
        private readonly IMessageBoxWrapper _messageBox;
        public MessageDialogs(IMessageBoxWrapper messageBox)
        {
            _messageBox = messageBox;
        }

        public bool IsErrorDisplayed(string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
            {
                _messageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return true;
            }
            return false;
        }

    }
}
