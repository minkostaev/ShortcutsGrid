namespace ShortcutsGrid.Controls;

using ShortcutsGrid.Services.Run;
using System;
using System.Windows.Controls;

/// <summary>
/// Interaction logic for TextLink.xaml
/// </summary>
public partial class TextLink : UserControl
{
    public TextLink(string uri, string text = "")
    {
        InitializeComponent();
        tbText.Text = string.IsNullOrWhiteSpace(text) ? uri : text;
        try { hprLnk.NavigateUri = new Uri(uri); }
        catch (Exception ex) { tbText.Text = ex.Message; }
        hprLnk.Click += delegate { RunProcess.OpenLink(hprLnk.NavigateUri.ToString()); };
    }
}