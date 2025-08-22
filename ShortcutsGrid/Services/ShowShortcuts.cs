namespace ShortcutsGrid.Services;

using ShortcutsGrid.Models;
using ShortcutsGrid.Windows;
using System.Windows;
using System.Windows.Input;

public static class ShowShortcuts
{
    
    public static void Load(MainWindow window)
    {
        window.stkPnl1.Children.Clear();
        window.stkPnl2.Children.Clear();
        window.stkPnl3.Children.Clear();
        window.stkPnl4.Children.Clear();

        ReadShortcuts.FileToShortcuts();

        foreach (var shortcut in AppValues.Shortcuts)
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

        if (AppValues.Shortcuts.Count == 1)
        {
            new About().ShowDialog();
        }
    }

    public static void CenterTopNoResize(this Window window, string title)
    {
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.ResizeMode = ResizeMode.NoResize;
        window.Topmost = true;
        window.Title = title;
        window.PreviewKeyDown += (s, e) => { if (e.Key == Key.Escape) window.Close(); };
    }

}