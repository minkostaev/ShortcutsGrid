namespace ShortcutsGrid.Windows;

using ShortcutsGrid.Services;
using System.Windows;
using System.Windows.Controls;

/// <summary>
/// Interaction logic for List.xaml
/// </summary>
public partial class List : Window
{
    public List()
    {
        InitializeComponent();

        this.CenterTopNoResize("Edit Shortcuts List");

        dgrShortcuts.AutoGenerateColumns = false;
        dgrShortcuts.IsReadOnly = true;
        dgrShortcuts.HeadersVisibility = DataGridHeadersVisibility.None;
        dgrShortcuts.CanUserAddRows = false;
        dgrShortcuts.Columns.Add(new DataGridTextColumn
        {
            Header = "Name",
            Width = new DataGridLength(1, DataGridLengthUnitType.Star),
            Binding = new System.Windows.Data.Binding("AppName")
        });

        dgrShortcuts.SelectionChanged += (s, e) =>
        {
            if (dgrShortcuts.SelectedItem is not null)
            {
                var shortcut = dgrShortcuts.SelectedItem as Models.Shortcut;
                if (shortcut != null)
                {
                    txtName.Text = shortcut.AppName;
                    txtPath.Text = shortcut.ExePath;
                    txtImg.Text = shortcut.ImgPath;
                }
            }
        };

        var shortcuts = ReadShortcuts.FileToShortcuts();
        dgrShortcuts.ItemsSource = shortcuts;

    }
}