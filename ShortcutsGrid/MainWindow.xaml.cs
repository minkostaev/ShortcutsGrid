namespace ShortcutsGrid;

using Models;
using Services;
using ShortcutsGrid.Extensions;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        var startTimer = Stopwatch.StartNew();

        InitializeComponent();

        this.AttachRequest(startTimer);

        #region setup window
        this.Title = AppValues.ExeName;
        this.AllowsTransparency = true;
        this.WindowStyle = WindowStyle.None;
        this.Background = System.Windows.Media.Brushes.Transparent;
        this.Topmost = true;

        this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        this.ResizeMode = ResizeMode.NoResize;

        this.PreviewKeyDown += (s, e) => { if (e.Key == Key.Escape) Exit(); };
        #endregion

        ShowShortcuts.Load(this);
    }

    public void Exit()
    {
        ///Environment.Exit(0);
        ///App.Current.Shutdown();
        ///System.Diagnostics.Process.GetCurrentProcess().Kill();
        ///Application.Current.Shutdown();
        Hide();
        Close();
    }

}