namespace ShortcutsGrid.Controls;

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

    public MenuItem? mnOpen { get; set; }
    public MenuItem? mnAdmin { get; set; }

    public MenuItem? mnFolderExe { get; set; }
    public MenuItem? mnFolderImg { get; set; }
    public MenuItem? mnFolderThis { get; set; }

    public MenuItem? mnAbout { get; set; }

    public MenuItem? mnExit { get; set; }

    private void ContextMenuInit(Shortcut shortcut)
    {
        bool showExe = File.Exists(shortcut.ExePath);
        bool showImg = File.Exists(shortcut.ImgPath);

        var contextMenu = new ContextMenu();

        var mnOpenFolder = new MenuItem();
        mnOpenFolder.Header = "Open folder of";

        mnOpen = new MenuItem();
        mnOpen.Header = "Open";
        mnAdmin = new MenuItem();
        mnAdmin.Header = "Run as admin";
        mnFolderExe = new MenuItem();
        mnFolderExe.Header = "This app";
        mnFolderExe.IsEnabled = showExe;
        mnFolderImg = new MenuItem();
        mnFolderImg.Header = "This icon";
        mnFolderImg.IsEnabled = showImg;
        mnFolderThis = new MenuItem();
        mnFolderThis.Header = "Default";
        mnAbout = new MenuItem();
        mnAbout.Header = "About";
        mnExit = new MenuItem();
        mnExit.Header = "Exit";

        mnOpenFolder.Items.Add(mnFolderExe);
        mnOpenFolder.Items.Add(mnFolderImg);
        mnOpenFolder.Items.Add(mnFolderThis);

        contextMenu.Items.Add(mnOpen);
        contextMenu.Items.Add(mnAdmin);
        contextMenu.Items.Add(new Separator());
        contextMenu.Items.Add(mnOpenFolder);
        contextMenu.Items.Add(new Separator());
        contextMenu.Items.Add(mnAbout);
        contextMenu.Items.Add(new Separator());
        contextMenu.Items.Add(mnExit);

        btn.ContextMenu = contextMenu;
    }

}