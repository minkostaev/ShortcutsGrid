namespace ShortcutsGrid.Controls;

using ShortcutsGrid.Services.Run;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

/// <summary>
/// Interaction logic for TextLink.xaml
/// </summary>
public partial class TextLink : UserControl
{
    public TextLink(string uri, string text = "", string copyText = "")
    {
        InitializeComponent();
        tbText.Text = string.IsNullOrWhiteSpace(text) ? uri : text;
        try { hprLnk.NavigateUri = new Uri(uri); }
        catch (Exception ex) { tbText.Text = ex.Message; }
        hprLnk.Click += delegate { RunProcess.OpenLink(hprLnk.NavigateUri.ToString()); };

        if (!string.IsNullOrWhiteSpace(copyText))
        {
            CntxtMn = new ContextMenu();
            var mnCopy = new MenuItem();
            mnCopy.Header = "Copy";
            mnCopy.Click += delegate { Clipboard.SetText(copyText); };
            CntxtMn.Items.Add(mnCopy);
            tbText.ContextMenu = CntxtMn;
            tbText.MouseEnter += TbText_MouseEnter;
        }
    }

    private ContextMenu? CntxtMn { get; set; }

    private void TbText_MouseEnter(object sender, MouseEventArgs e)
    {
        ShowCntxtMn();
    }

    private void ShowCntxtMn()
    {
        if (CntxtMn != null)
        {
            CntxtMn.IsOpen = true;
            tbText.MouseEnter -= TbText_MouseEnter;
        }
    }

}