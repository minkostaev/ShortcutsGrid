namespace ShortcutsGrid;

using Models;
using Services;
using System.Windows;
using System.Windows.Input;
using Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        #region setup window
        this.Title = AppValues.ExeName;
        this.AllowsTransparency = true;
        this.WindowStyle = WindowStyle.None;
        this.Background = System.Windows.Media.Brushes.Transparent;
        this.Topmost = true;

        this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        this.ResizeMode = ResizeMode.NoResize;

        this.PreviewKeyDown += (s, e) => { if (e.Key == Key.Escape) Close(); };
        #endregion

        var shortcuts = ReadShortcuts.FileToShortcuts();

        shortcuts.Add(new Shortcut() { AppName = "Drag or Close", ImgPath = AppValues.CloseDragImage, Tag = AppValues.CloseDragId });

        foreach (var shortcut in shortcuts)
        {
            if (stkPnl1.Children.Count < 6)
            {
                stkPnl1.Children.Add(ImageButtonCreator.GetButton(this, shortcut));
            }
            else if (stkPnl2.Children.Count < 6)
            {
                stkPnl2.Children.Add(ImageButtonCreator.GetButton(this, shortcut));
            }
            else if (stkPnl3.Children.Count < 6)
            {
                stkPnl3.Children.Add(ImageButtonCreator.GetButton(this, shortcut));
            }
            else if (stkPnl4.Children.Count < 6)
            {
                stkPnl4.Children.Add(ImageButtonCreator.GetButton(this, shortcut));
            }
        }

        if (shortcuts.Count == 1)
        {
            new About().ShowDialog();
        }

    }

}
