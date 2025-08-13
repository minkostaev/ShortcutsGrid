namespace ShortcutsGrid.Windows;

using ShortcutsGrid.Services;
using System.Windows;
using System.Windows.Input;

/// <summary>
/// Interaction logic for List.xaml
/// </summary>
public partial class List : Window
{
    public List()
    {
        InitializeComponent();

        this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        this.ResizeMode = ResizeMode.NoResize;
        this.Topmost = true;
        this.Title = "Edit Shortcuts List";
        this.PreviewKeyDown += (s, e) => { if (e.Key == Key.Escape) Close(); };

        var shortcuts = ReadShortcuts.FileToShortcuts();
        dgrShortcuts.ItemsSource = shortcuts;

    }
}