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
        dgrShortcuts.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        dgrShortcuts.CanUserAddRows = false;
        dgrShortcuts.Columns.Add(new DataGridTextColumn
        {
            Header = nameof(Shortcut.Label),
            Width = new DataGridLength(1, DataGridLengthUnitType.Star),
            Binding = new System.Windows.Data.Binding(nameof(Shortcut.Label))
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
                    txtName.Text = shortcut.Label;
                    txtPath.Text = shortcut.Execution;
                    txtImg.Text = shortcut.Image;
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
                    shortcut.Label = txtName.Text;
                    //shortcut.Execution = txtPath.Text;
                    shortcut.Image = txtImg.Text;
                    dgrShortcuts.Items.Refresh();
                }
            }
            FileShortcuts.ShortcutsToFile();
        };

    }

    private readonly List<string> numberRows =
    [   "11", "12", "13", "14", "15", "16",
        "21", "22", "23", "24", "25", "26",
        "31", "32", "33", "34", "35", "36",
        "41", "42", "43", "44", "45", "46" ];

}