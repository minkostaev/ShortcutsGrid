namespace ShortcutsGrid.Services;

using ShortcutsGrid.Models;
using ShortcutsGrid.Windows;

public static class ShowShortcuts
{
    public static void Load()
    {
        AppValues.MainWin?.stkPnl1.Children.Clear();
        AppValues.MainWin?.stkPnl2.Children.Clear();
        AppValues.MainWin?.stkPnl3.Children.Clear();
        AppValues.MainWin?.stkPnl4.Children.Clear();

        var shortcuts = ReadShortcuts.FileToShortcuts();

        shortcuts.Add(new Shortcut() { AppName = "Drag or Close", ImgPath = AppValues.CloseDragImage, Tag = AppValues.CloseDragId });

        foreach (var shortcut in shortcuts)
        {
            if (AppValues.MainWin?.stkPnl1.Children.Count < 6)
            {
                AppValues.MainWin?.stkPnl1.Children.Add(ImageButtonCreator.GetButton(shortcut));
            }
            else if (AppValues.MainWin?.stkPnl2.Children.Count < 6)
            {
                AppValues.MainWin?.stkPnl2.Children.Add(ImageButtonCreator.GetButton(shortcut));
            }
            else if (AppValues.MainWin?.stkPnl3.Children.Count < 6)
            {
                AppValues.MainWin?.stkPnl3.Children.Add(ImageButtonCreator.GetButton(shortcut));
            }
            else if (AppValues.MainWin?.stkPnl4.Children.Count < 6)
            {
                AppValues.MainWin?.stkPnl4.Children.Add(ImageButtonCreator.GetButton(shortcut));
            }
        }

        if (shortcuts.Count == 1)
        {
            new About().ShowDialog();
        }
    }
}