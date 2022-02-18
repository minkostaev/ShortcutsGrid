namespace ShortcutsGrid.Windows
{
    using ShortcutsGrid.Controls;
    using ShortcutsGrid.Models;
    using System.Diagnostics;
    using System.Reflection;
    using System.Windows;

    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();

            this.Title = "About Shortcuts Grid";

            var imageButton = new ImageButton(new Shortcut().Image, "")
            {
                Height = 110,
                Width = 110
            };
            pnlImg.Children.Add(imageButton);

            lblHeader.Content = "Shortcuts Grid";
            lblHeader.FontSize = 32;
            lblHeader.FontWeight = FontWeights.Bold;
            lblVersion.Content = "Version:\n" + AppVersion;
            lblVersion.FontWeight = FontWeights.Bold;

            hypeLnk.Click += delegate { OpenLink(hypeLnk.NavigateUri.ToString()); };

        }

        public string? AppVersion
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                var fileInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

                ///string filePath = Assembly.GetExecutingAssembly().Location;
                ///DateTime dt = new FileInfo(filePath).LastWriteTime;
                ///var result = fileInfo.FileVersion + " [" + dt.Year.ToString().Substring(2) + "." + dt.Month.ToString() + "." + dt.Day.ToString() + "]";

                return fileInfo.FileVersion;
            }
        }

        private void OpenLink(string uri)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = uri,
                UseShellExecute = true
            });
        }


    }
}
