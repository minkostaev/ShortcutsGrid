namespace ShortcutsGrid.Controls;

using ShortcutsGrid.Services.Run;
using System.Windows.Controls;

/// <summary>
/// Interaction logic for TextLink.xaml
/// </summary>
public partial class TextLink : UserControl
{
    public TextLink()
    {
        InitializeComponent();
        hprLnk.NavigateUri = new System.Uri("");
        tbText.Text = "";
        hprLnk.Click += delegate { RunProcess.OpenLink(hprLnk.NavigateUri.ToString()); };
    }
}