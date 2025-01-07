namespace ShortcutsGrid.Services;

using ShortcutsGrid.Models;
using ShortcutsGrid.Windows;

public static class ShowShortcuts
{
    
    public static void Load(MainWindow window)
    {
        window.stkPnl1.Children.Clear();
        window.stkPnl2.Children.Clear();
        window.stkPnl3.Children.Clear();
        window.stkPnl4.Children.Clear();

        var shortcuts = ReadShortcuts.FileToShortcuts();

        shortcuts.Add(new Models.Shortcut() { AppName = "Drag or Close", ImgPath = AppValues.CloseDragImage, Tag = AppValues.CloseDragId });

        foreach (var shortcut in shortcuts)
        {
            if (window.stkPnl1.Children.Count < 6)
            {
                window.stkPnl1.Children.Add(ImageButtonCreator.GetButton(shortcut, window));
            }
            else if (window.stkPnl2.Children.Count < 6)
            {
                window.stkPnl2.Children.Add(ImageButtonCreator.GetButton(shortcut, window));
            }
            else if (window.stkPnl3.Children.Count < 6)
            {
                window.stkPnl3.Children.Add(ImageButtonCreator.GetButton(shortcut, window));
            }
            else if (window.stkPnl4.Children.Count < 6)
            {
                window.stkPnl4.Children.Add(ImageButtonCreator.GetButton(shortcut, window));
            }
        }

        if (shortcuts.Count == 1)
        {
            new About().ShowDialog();
        }
    }

}