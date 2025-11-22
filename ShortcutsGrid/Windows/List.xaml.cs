namespace ShortcutsGrid.Windows;

using ShortcutsGrid.Controls;
using ShortcutsGrid.Extensions;
using ShortcutsGrid.Models;
using ShortcutsGrid.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            pnlImg1.Children.Clear();
            pnlImg2.Children.Clear();
            pnlImg3.Children.Clear();
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
                    stkCmds.Children.Clear();
                    for (int i = 0; i < shortcut.Executions.Count; i++)
                    {
                        Grid pnlTxt = new()
                        {
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            Margin = new Thickness(10, 0, 10, 2)
                        };
                        stkCmds.Children.Add(pnlTxt);
                        TextBox txt = new()
                        {
                            Text = shortcut.Executions[i],
                            TextWrapping = TextWrapping.Wrap,
                            Margin = new Thickness(0, 0, 28, 0)
                        };
                        txt.ContextMenu = CmdTxtContextMenu(txt);
                        pnlTxt.Children.Add(txt);
                        Button btnDel = new()
                        {
                            Content = "X",
                            Width = 24,
                            HorizontalAlignment = HorizontalAlignment.Right,
                            Tag = i,
                            IsEnabled = i != 0,
                            Margin = new Thickness(10, 0, 0, 2)
                        };
                        btnDel.Click += (s, e) =>
                        {
                            int id = -1;
                            try
                            {
                                id = (int)((Button)s).Tag;
                                shortcut.Executions.RemoveAt(id);
                            }
                            catch { }
                            ReSelectItem(shortcut);
                        };
                        pnlTxt.Children.Add(btnDel);
                    }
                    Button btnAdd = new()
                    {
                        Content = "+",
                        Width = 24
                    };
                    btnAdd.Click += (s, e) =>
                    {
                        shortcut.Executions.Add(string.Empty);
                        ReSelectItem(shortcut);
                    };
                    stkCmds.Children.Add(btnAdd);

                    txtImg.Text = shortcut.Image;
                    try
                    {
                        pnlImg1.Children.Add(ImageButtonOnly(shortcut.Image));
                        pnlImg2.Children.Add(ImageButtonOnly(shortcut.Image));
                        pnlImg3.Children.Add(ImageButtonOnly(shortcut.Image));
                    }
                    catch (Exception ex) { _ = ex.Message; }
                }
            }
            else
            {
                pnlItem.IsEnabled = false;
                pnlButtons.IsEnabled = false;
            }
        };
        dgrShortcuts.SelectedCellsChanged += (s, e) =>
        {
            if (dgrShortcuts.SelectedIndex == 0)
                btnUp.IsEnabled = false;
            else
                btnUp.IsEnabled = true;
            if (dgrShortcuts.SelectedIndex == AppValues.Shortcuts.Count - 2)
                btnDown.IsEnabled = false;
            else
                btnDown.IsEnabled = true;
        };

        dgrShortcuts.SelectedItem = AppValues.Shortcuts.Find(i => i.Id == id);

        // Buttons

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
                    foreach (var child in stkCmds.Children)
                    {
                        if (child is Grid pnlTxt)
                        {
                            foreach (var pnlChild in pnlTxt.Children)
                            {
                                if (pnlChild is TextBox txt)
                                {
                                    shortcut.Executions.Add(txt.Text);
                                }
                            }
                        }
                    }
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

        btnNew.Click += (s, e) =>
        {
            if (AppValues.Shortcuts.Count > 23)
            {
                var messageDialogs = new MessageDialogs();
                messageDialogs.SimpleError("Max mumber of items reached!", "Action not allowed");
                return;
            }
            int index = (dgrShortcuts.SelectedIndex == -1) ? 0 : dgrShortcuts.SelectedIndex;
            var newShortcut = new Shortcut();
            AppValues.Shortcuts.Insert(index, newShortcut);
            dgrShortcuts.SelectedItem = newShortcut;
            dgrShortcuts.Items.Refresh();
            FileShortcuts.ShortcutsToFile();
        };

        btnUp.IsEnabled = false;
        btnDown.IsEnabled = false;

        btnUp.Click += (s, e) =>
        {
            AppValues.Shortcuts.MoveItem(dgrShortcuts.SelectedIndex, dgrShortcuts.SelectedIndex - 1);
            dgrShortcuts.Items.Refresh();
            FileShortcuts.ShortcutsToFile();
        };
        btnDown.Click += (s, e) =>
        {
            AppValues.Shortcuts.MoveItem(dgrShortcuts.SelectedIndex, dgrShortcuts.SelectedIndex + 1);
            dgrShortcuts.Items.Refresh();
            FileShortcuts.ShortcutsToFile();
        };

    }

    private readonly List<string> numberRows =
    [   "11", "12", "13", "14", "15", "16",
        "21", "22", "23", "24", "25", "26",
        "31", "32", "33", "34", "35", "36",
        "41", "42", "43", "44", "45", "46" ];

    private void ReSelectItem(Shortcut shortcut)
    {
        dgrShortcuts.SelectedItem = null;
        dgrShortcuts.SelectedItem = shortcut;
    }

    private ImageButton ImageButtonOnly(string? image)
    {
        var btn = new ImageButton(new Shortcut()
        { Executions = ["exe.exe"], Image = image }, false)
        {
            Height = 128,
            Width = 128
        };
        return btn;
    }

    private ContextMenu CmdTxtContextMenu(TextBox txt)
    {
        ContextMenu menu = new();

        MenuItem cutItem = new()
        {
            Header = "Cut",
            Command = ApplicationCommands.Cut
        };
        menu.Items.Add(cutItem);
        MenuItem copyItem = new()
        {
            Header = "Copy",
            Command = ApplicationCommands.Copy
        };
        menu.Items.Add(copyItem);
        MenuItem pasteItem = new()
        {
            Header = "Paste",
            Command = ApplicationCommands.Paste
        };
        menu.Items.Add(pasteItem);

        menu.Items.Add(new Separator());

        MenuItem insertItem = new() { Header = "Path code" };
        menu.Items.Add(insertItem);

        insertItem.Items.Add(CmdTxtContextItem(txt, "Windows", "<|Windows|>"));
        insertItem.Items.Add(CmdTxtContextItem(txt, "Windows System", "<|WindowsSystem|>"));
        insertItem.Items.Add(CmdTxtContextItem(txt, "Program Files", "<|ProgramFiles|>"));
        insertItem.Items.Add(CmdTxtContextItem(txt, "Program Files(x86)", "<|ProgramFilesX86|>"));
        insertItem.Items.Add(CmdTxtContextItem(txt, "Program Data", "<|ProgramData|>"));
        insertItem.Items.Add(CmdTxtContextItem(txt, "User Profile", "<|UserProfile|>"));
        insertItem.Items.Add(CmdTxtContextItem(txt, "Local AppData", "<|LocalAppData|>"));
        insertItem.Items.Add(CmdTxtContextItem(txt, "Roaming AppData", "<|RoamingAppData|>"));
        insertItem.Items.Add(CmdTxtContextItem(txt, "Temp", "<|Temp|>"));
        insertItem.Items.Add(CmdTxtContextItem(txt, "Desktop", "<|Desktop|>"));
        insertItem.Items.Add(CmdTxtContextItem(txt, "Documents", "<|Documents|>"));
        insertItem.Items.Add(CmdTxtContextItem(txt, "Pictures", "<|Pictures|>"));
        insertItem.Items.Add(CmdTxtContextItem(txt, "Music", "<|Music|>"));
        insertItem.Items.Add(CmdTxtContextItem(txt, "Videos", "<|Videos|>"));

        MenuItem gotoItem = new() { Header = "Go to" };
        menu.Items.Add(gotoItem);

        gotoItem.Items.Add(CmdTxtContextItem("Windows", "<|Windows|>"));
        gotoItem.Items.Add(CmdTxtContextItem("Windows System", "<|WindowsSystem|>"));
        gotoItem.Items.Add(CmdTxtContextItem("Program Files", "<|ProgramFiles|>"));
        gotoItem.Items.Add(CmdTxtContextItem("Program Files(x86)", "<|ProgramFilesX86|>"));
        gotoItem.Items.Add(CmdTxtContextItem("Program Data", "<|ProgramData|>"));
        gotoItem.Items.Add(CmdTxtContextItem("User Profile", "<|UserProfile|>"));
        gotoItem.Items.Add(CmdTxtContextItem("Local AppData", "<|LocalAppData|>"));
        gotoItem.Items.Add(CmdTxtContextItem("Roaming AppData", "<|RoamingAppData|>"));
        gotoItem.Items.Add(CmdTxtContextItem("Temp", "<|Temp|>"));
        gotoItem.Items.Add(CmdTxtContextItem("Desktop", "<|Desktop|>"));
        gotoItem.Items.Add(CmdTxtContextItem("Documents", "<|Documents|>"));
        gotoItem.Items.Add(CmdTxtContextItem("Pictures", "<|Pictures|>"));
        gotoItem.Items.Add(CmdTxtContextItem("Music", "<|Music|>"));
        gotoItem.Items.Add(CmdTxtContextItem("Videos", "<|Videos|>"));

        return menu;
    }
    private MenuItem CmdTxtContextItem(TextBox txt, string header, string place)
    {
        MenuItem mnItm = new() { Header = header, };
        mnItm.Click += (s, e) =>
        {
            int caretIndex = txt.CaretIndex;
            txt.Text = txt.Text.Insert(caretIndex, place);
            txt.CaretIndex = caretIndex + place.Length;
        };
        return mnItm;
    }
    private MenuItem CmdTxtContextItem(string header, string place)
    {
        MenuItem mnItm = new() { Header = header, };
        mnItm.Click += (s, e) => { RunProcess.OpenExplorer(place); };
        return mnItm;
    }

}