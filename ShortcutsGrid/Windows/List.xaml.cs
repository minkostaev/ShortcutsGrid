namespace ShortcutsGrid.Windows;

using ShortcutsGrid.Controls;
using ShortcutsGrid.Models;
using ShortcutsGrid.Services;
using System.Collections.Generic;
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

        this.CenterTopNoResize("Manage Shortcuts List");
        
        _ = new DataGridsScrollersTogether(dgrNumbers, dgrShortcuts);

        tbNotAllowed.Visibility = Visibility.Hidden;
        btnUp.IsEnabled = false;
        btnDown.IsEnabled = false;

        dgrNumbers.AutoGenerateColumns = false;
        dgrNumbers.IsEnabled = false;
        dgrNumbers.IsReadOnly = true;
        dgrNumbers.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        dgrNumbers.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
        dgrNumbers.HeadersVisibility = DataGridHeadersVisibility.None;
        dgrNumbers.CanUserAddRows = false;
        dgrNumbers.Columns.Add(new DataGridTextColumn
        {
            Header = "#",
            Width = new DataGridLength(1, DataGridLengthUnitType.Star),
            Binding = new System.Windows.Data.Binding(".")// Bind to the string itself
        });
        dgrNumbers.ItemsSource = numberRows;

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
        dgrShortcuts.ItemsSource = AppValues.Shortcuts;

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
            ReadShortcuts.ShortcutsToFile();
        };

    }

    private readonly List<string> numberRows =
    [   "1", "2", "3", "4", "5", "6",
        "1", "2", "3", "4", "5", "6",
        "1", "2", "3", "4", "5", "6",
        "1", "2", "3", "4", "5", "6" ];

}