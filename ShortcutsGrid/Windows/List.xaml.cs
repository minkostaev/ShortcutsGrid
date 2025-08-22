namespace ShortcutsGrid.Windows;

using ShortcutsGrid.Models;
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

        tbNotAllowed.Visibility = Visibility.Hidden;
        dgrShortcuts.SelectionChanged += (s, e) =>
        {
            if (dgrShortcuts.SelectedItem is not null)
            {
                var shortcut = (Shortcut)dgrShortcuts.SelectedItem;
                if (shortcut != null)
                {
                    var tag = shortcut.Tag as string;
                    if (tag == AppValues.CloseDragId)
                    {
                        pnlControl.Visibility = Visibility.Hidden;
                        tbNotAllowed.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        pnlControl.Visibility = Visibility.Visible;
                        tbNotAllowed.Visibility = Visibility.Hidden;
                    }
                    txtName.Text = shortcut.AppName;
                    txtPath.Text = shortcut.ExePath;
                    txtImg.Text = shortcut.ImgPath;
                }
            }
        };

        dgrShortcuts.ItemsSource = AppValues.Shortcuts;

        btnCommit.Click += (s, e) =>
        {
            if (dgrShortcuts.SelectedItem is not null)
            {
                var shortcut = (Shortcut)dgrShortcuts.SelectedItem;
                if (shortcut != null)
                {
                    shortcut.AppName = txtName.Text;
                    shortcut.ExePath = txtPath.Text;
                    shortcut.ImgPath = txtImg.Text;
                    dgrShortcuts.Items.Refresh();
                }
            }
        };

    }
}