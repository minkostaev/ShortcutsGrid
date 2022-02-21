namespace ShortcutsGrid.Windows
{
    using ShortcutsGrid.Controls;
    using ShortcutsGrid.Models;
    using ShortcutsGrid.Services.Run;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.ResizeMode = ResizeMode.NoResize;
            this.Topmost = true;
            this.Title = "About Shortcuts Grid";
            this.PreviewKeyDown += (s, e) => { if (e.Key == Key.Escape) Close(); };

            var imageButton = new ImageButton(new Shortcut().Image, "")
            {
                Height = 110,
                Width = 110
            };
            pnlImg.Children.Add(imageButton);

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
            lnkThisRepo.Click += delegate { RunProcess.OpenLink(lnkThisRepo.NavigateUri.ToString()); };

            lblMyAd.Content = "You can buy me a beer:";
            lblMyAd.FontStyle = FontStyles.Italic;
            lblMyAd.FontWeight = FontWeights.Bold;
            lnkMyPayPal.Click += delegate { RunProcess.OpenLink(lnkMyPayPal.NavigateUri.ToString()); };

        }

    }
}
