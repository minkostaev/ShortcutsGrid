﻿namespace ShortcutsGrid.Controls;

using ShortcutsGrid.Models;
using System.IO;
using System.Windows.Controls;

/// <summary>
/// Interaction logic for ImageButton.xaml
/// </summary>
public partial class ImageButton : UserControl
{
    public ImageButton(Shortcut shortcut, bool showContextMenu = true)
    {
        InitializeComponent();
        img.Source = shortcut.Image;
        tb.Text = shortcut.AppName;
        ///btn.ToolTip = shortcut.AppName;

        if (showContextMenu)
        {
            ContextMenuInit(shortcut);
        }
    }

    public MenuItem MnOpen { get; set; } = new MenuItem();
    public MenuItem MnAdmin { get; set; } = new MenuItem();

    public MenuItem MnFolderExe { get; set; } = new MenuItem();
    public MenuItem MnFolderImg { get; set; } = new MenuItem();
    public MenuItem MnFolderThis { get; set; } = new MenuItem();

    public MenuItem MnAbout { get; set; } = new MenuItem();

    public MenuItem MnExit { get; set; } = new MenuItem();

    private void ContextMenuInit(Shortcut shortcut)
    {
        bool showExe = File.Exists(shortcut.ExePath);
        bool showImg = File.Exists(shortcut.ImgPath);
        bool showOpen = true;
        bool showAdmin = true;
        if (shortcut.Tag is string && shortcut.Tag.ToString() == AppValues.CloseDragId)
        {
            showOpen = false;
            showAdmin = false;
        }

        var contextMenu = new ContextMenu();

        var mnOpenFolder = new MenuItem();
        mnOpenFolder.Header = "Open folder of";

        MnOpen.Header = "Open";
        MnOpen.IsEnabled = showOpen;
        MnAdmin.Header = "Run as admin";
        MnAdmin.IsEnabled = showAdmin;
        MnFolderExe.Header = "This app";
        MnFolderExe.IsEnabled = showExe;
        MnFolderImg.Header = "This icon";
        MnFolderImg.IsEnabled = showImg;
        MnFolderThis.Header = "Default";
        MnAbout.Header = "About";
        MnExit.Header = "Exit";

        mnOpenFolder.Items.Add(MnFolderExe);
        mnOpenFolder.Items.Add(MnFolderImg);
        mnOpenFolder.Items.Add(MnFolderThis);

        contextMenu.Items.Add(MnOpen);
        contextMenu.Items.Add(MnAdmin);
        contextMenu.Items.Add(new Separator());
        contextMenu.Items.Add(mnOpenFolder);
        contextMenu.Items.Add(new Separator());
        contextMenu.Items.Add(MnAbout);
        contextMenu.Items.Add(new Separator());
        contextMenu.Items.Add(MnExit);

        btn.ContextMenu = contextMenu;
    }

}