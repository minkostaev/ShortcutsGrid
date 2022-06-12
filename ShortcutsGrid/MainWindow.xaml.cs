namespace ShortcutsGrid;

using Models;
using Services;
using System.Windows;
using System.Windows.Input;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        ///var startTimer = Stopwatch.StartNew();
        ///var endTimer = Stopwatch.StartNew();

        InitializeComponent();

        this.ContentRendered += delegate
        {
            ///startTimer.Stop();
            ///var elapsedMs = startTimer.ElapsedMilliseconds;
        };
        this.Closed += delegate
        {
            ///endTimer.Stop();
            ///var elapsedMs = endTimer.ElapsedMilliseconds;
            ///AppValues.MongoShortcutsGrid.MachineAdded();
        };

        AppValues.MainWin = this;

        #region setup window
        this.Title = AppValues.ExeName;
        this.AllowsTransparency = true;
        this.WindowStyle = WindowStyle.None;
        this.Background = System.Windows.Media.Brushes.Transparent;
        this.Topmost = true;

        this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        this.ResizeMode = ResizeMode.NoResize;

        this.PreviewKeyDown += (s, e) => { if (e.Key == Key.Escape) Close(); };
        #endregion

        ShowShortcuts.Load();

    }

}