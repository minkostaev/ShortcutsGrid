namespace ShortcutsGrid.Windows;

using ShortcutsGrid.Controls;
using ShortcutsGrid.Models;
using ShortcutsGrid.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

/// <summary>
/// Interaction logic for List.xaml
/// </summary>
public partial class List : Window
{
    public List(string id = "")
    {
        InitializeComponent();

        this.CenterTopNoResize("Manage Shortcuts List");
        //this.ResizeMode = ResizeMode.CanResize;

        _ = new DataGridsScrollersTogether(dgrNumbers, dgrShortcuts);

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
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtImg.Text = string.Empty;
            txtCmd1.Text = string.Empty;
            txtCmd2.Text = string.Empty;
            txtCmd3.Text = string.Empty;
            txtCmd4.Text = string.Empty;
            txtCmd5.Text = string.Empty;
            txtCmd6.Text = string.Empty;
            txtCmd7.Text = string.Empty;
            txtCmd8.Text = string.Empty;
            txtCmd9.Text = string.Empty;
            pnlImg.Children.Clear();
            if (dgrShortcuts.SelectedItem is not null)
            {
                var shortcut = (Shortcut)dgrShortcuts.SelectedItem;
                if (shortcut != null)
                {
                    if (shortcut.Id == AppValues.CloseDragId)
                    {
                        pnlItem.IsEnabled = false;
                        pnlButtons.IsEnabled = false;
                    }
                    else
                    {
                        pnlItem.IsEnabled = true;
                        pnlButtons.IsEnabled = true;
                    }
                    txtName.Text = shortcut.Label;
                    txtDescription.Text = shortcut.Description;
                    for (int i = 0; i < shortcut.Executions.Count; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                txtCmd1.Text = shortcut.Executions[0];
                                break;
                            case 1:
                                txtCmd2.Text = shortcut.Executions[1];
                                break;
                            case 2:
                                txtCmd3.Text = shortcut.Executions[2];
                                break;
                            case 3:
                                txtCmd4.Text = shortcut.Executions[3];
                                break;
                            case 4:
                                txtCmd5.Text = shortcut.Executions[4];
                                break;
                            case 5:
                                txtCmd6.Text = shortcut.Executions[5];
                                break;
                            case 6:
                                txtCmd7.Text = shortcut.Executions[6];
                                break;
                            case 7:
                                txtCmd8.Text = shortcut.Executions[7];
                                break;
                            case 8:
                                txtCmd9.Text = shortcut.Executions[8];
                                break;
                            default:
                                break;
                        }
                    }
                    txtImg.Text = shortcut.Image;
                    try
                    {
                        var imageButton = new ImageButton(new Shortcut()
                        { Executions = ["exe.exe"], Image = shortcut.Image }, false)
                        {
                            Height = 128,
                            Width = 128
                        };
                        pnlImg.Children.Add(imageButton);
                    }
                    catch (Exception ex) { _ = ex.Message; }
                }
            }
        };

        dgrShortcuts.SelectedItem = AppValues.Shortcuts.Find(i => i.Id == id);

        btnCommit.Click += (s, e) =>
        {
            if (dgrShortcuts.SelectedItem is not null)
            {
                var shortcut = (Shortcut)dgrShortcuts.SelectedItem;
                if (shortcut != null)
                {
                    shortcut.Label = txtName.Text;
                    shortcut.Description = txtDescription.Text;
                    shortcut.Executions.Clear();
                    if (!string.IsNullOrEmpty(txtCmd1.Text))
                        shortcut.Executions.Add(txtCmd1.Text);
                    if (!string.IsNullOrEmpty(txtCmd2.Text))
                        shortcut.Executions.Add(txtCmd2.Text);
                    if (!string.IsNullOrEmpty(txtCmd3.Text))
                        shortcut.Executions.Add(txtCmd3.Text);
                    if (!string.IsNullOrEmpty(txtCmd4.Text))
                        shortcut.Executions.Add(txtCmd4.Text);
                    if (!string.IsNullOrEmpty(txtCmd5.Text))
                        shortcut.Executions.Add(txtCmd5.Text);
                    if (!string.IsNullOrEmpty(txtCmd6.Text))
                        shortcut.Executions.Add(txtCmd6.Text);
                    if (!string.IsNullOrEmpty(txtCmd7.Text))
                        shortcut.Executions.Add(txtCmd7.Text);
                    if (!string.IsNullOrEmpty(txtCmd8.Text))
                        shortcut.Executions.Add(txtCmd8.Text);
                    if (!string.IsNullOrEmpty(txtCmd9.Text))
                        shortcut.Executions.Add(txtCmd9.Text);
                    shortcut.Image = txtImg.Text;
                    dgrShortcuts.Items.Refresh();
                }
            }
            FileShortcuts.ShortcutsToFile();
        };

        btnRemove.Background = Brushes.LightCoral;
        btnRemove.Click += (s, e) =>
        {
            if (dgrShortcuts.SelectedItem is not null)
            {
                var shortcut = (Shortcut)dgrShortcuts.SelectedItem;
                if (shortcut != null)
                {
                    if (shortcut.Id != AppValues.CloseDragId)
                    {
                        AppValues.Shortcuts.Remove(shortcut);
                        dgrShortcuts.Items.Refresh();
                        FileShortcuts.ShortcutsToFile();
                    }
                }
            }
        };

    }

    private readonly List<string> numberRows =
    [   "11", "12", "13", "14", "15", "16",
        "21", "22", "23", "24", "25", "26",
        "31", "32", "33", "34", "35", "36",
        "41", "42", "43", "44", "45", "46" ];

}