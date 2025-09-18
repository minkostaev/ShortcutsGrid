namespace ShortcutsGrid.Controls;

using ShortcutsGrid.Models;
using System.IO;
using System.Linq;
using System.Windows.Controls;

/// <summary>
/// Interaction logic for ImageButton.xaml
/// </summary>
public partial class ImageButton : UserControl
{
    public ImageButton(Shortcut shortcut, bool showContextMenu = true)
    {
        InitializeComponent();
        img.Source = shortcut.ImageSource;
        tb.Text = shortcut.Label;
        if (!string.IsNullOrWhiteSpace(shortcut.Description))
            btn.ToolTip = shortcut.Description;

        if (showContextMenu)
        {
            ContextMenuInit(shortcut);
        }
    }

    public MenuItem MnOpen { get; set; } = new MenuItem();
    public MenuItem MnAdmin { get; set; } = new MenuItem();

    public MenuItem MnManageList { get; set; } = new MenuItem();

    public MenuItem MnFolderExe { get; set; } = new MenuItem();
    public MenuItem MnFolderImg { get; set; } = new MenuItem();
    public MenuItem MnFolderThis { get; set; } = new MenuItem();

    public MenuItem MnAbout { get; set; } = new MenuItem();

    public MenuItem MnExit { get; set; } = new MenuItem();

    private void ContextMenuInit(Shortcut shortcut)
    {
        bool showExe = File.Exists(shortcut.ExistingExes.FirstOrDefault());
        bool showImg = File.Exists(shortcut.Image);
        bool showOpen = true;
        bool showAdmin = true;
        if (shortcut.Tag is string && shortcut.Tag.ToString() == AppValues.CloseDragId)
        {
            showOpen = false;
            showAdmin = false;
        }

        var contextMenu = new ContextMenu();

        MnManageList.Header = "Manage List";

        var mnOpenFolder = new MenuItem
        {
            Header = "Open folder of"
        };

        MnOpen.Header = "Open";
        MnOpen.IsEnabled = showOpen;
        MnAdmin.Header = "Run as admin";
        MnAdmin.IsEnabled = showAdmin;

        // to do
        // exe folder
        // icon folder
        // this folder (maybe in About window)

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
        contextMenu.Items.Add(MnManageList);
        contextMenu.Items.Add(new Separator());
        contextMenu.Items.Add(mnOpenFolder);
        contextMenu.Items.Add(new Separator());
        contextMenu.Items.Add(MnAbout);
        contextMenu.Items.Add(new Separator());
        contextMenu.Items.Add(MnExit);

        btn.ContextMenu = contextMenu;
    }

}