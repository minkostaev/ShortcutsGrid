namespace ShortcutsGrid.Windows;

using Controls;
using Models;
using ShortcutsGrid.Services;
using System;
using System.Windows;
using System.Windows.Media;

/// <summary>
/// Interaction logic for About.xaml
/// </summary>
public partial class About : Window
{
    public About()
    {
        InitializeComponent();

        this.CenterTopNoResize("About Shortcuts Grid");

        try
        {
            var imageButton = new ImageButton(new Shortcut(), false)
            {
                Height = 128,
                Width = 128
            };
            pnlImg.Children.Add(imageButton);
        }
        catch (Exception ex) { _ = ex.Message; }

        lblHeader.Content = "Shortcuts Grid";
        lblHeader.FontSize = 32;
        lblHeader.FontWeight = FontWeights.Bold;
        lblVersion.Content = "Version:\n" + AppValues.AppVersion;
        lblVersion.FontWeight = FontWeights.Bold;

        lblDescription.Content = "It can display grid with programs paths or shortcuts," +
            " run commands, files or folders paths.\nThese 'shortcuts' should be defined" +
            " in csv or json file - which has to be placed next to\n" +
            "this exe with same file name as it." +
            " The structure you can see in my repo + examples too.";

        #region Exe name and CSV or JSON name
        string listFileName = AppValues.CsvExists ? AppValues.ExeName + ".csv" : "";
        if (string.IsNullOrWhiteSpace(listFileName))
        {
            listFileName = AppValues.JsonExists ? AppValues.ExeName + ".json" : "";
        }
        if (string.IsNullOrWhiteSpace(listFileName))
        {
            listFileName = "but does't have csv or json user file with shortcuts list!" +
                "\n!! Add '' " + AppValues.ExeName + ".csv '' or '' " + AppValues.ExeName + ".json '' !!";
            lblExeName.Foreground = Brushes.Red;
        }
        else
        {
            listFileName = "working with user file source: '' " + listFileName + " ''";
        }
        lblExeName.Content = "This exe name is: '' " + AppValues.ExeName + ".exe '' " + listFileName;
        #endregion

        lblRepo.Content = "Source code available in github on this link:";
        lblRepo.FontStyle = FontStyles.Italic;

        lblMyAd.Content = "You can buy me a beer:";
        lblMyAd.FontStyle = FontStyles.Italic;
        lblMyAd.FontWeight = FontWeights.Bold;

        #region Links
        string github = "https://github.com/minkostaev/ShortcutsGrid";
        pnlGit.Children.Add(new TextLink(github));

        string paypal = "https://www.paypal.com/paypalme/minkostaev";
        string buymeacoffee = "https://www.buymeacoffee.com/minkostaev";
        string kofi = "https://www.ko-fi.com/minkostaev";
        string revolut = "https://www.revolut.com/";
        string wise = "https://wise.com/";
        pnlDons.Children.Add(new TextLink(paypal));
        pnlDons.Children.Add(new TextLink(buymeacoffee));
        pnlDons.Children.Add(new TextLink(kofi));
        pnlDons.Children.Add(new TextLink(revolut, "Revolut: @minkostaev", "@minkostaev"));
        pnlDons.Children.Add(new TextLink(wise, "Wise: minkostaev@yahoo.com", "minkostaev@yahoo.com"));
        #endregion

    }

}