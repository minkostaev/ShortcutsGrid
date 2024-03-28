namespace ShortcutsGrid;

using Forms.Wpf.Mls.Tools.Models;
using Forms.Wpf.Mls.Tools.Models.TheMachine;
using Forms.Wpf.Mls.Tools.Services;
using Models;
using Services;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

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

        ///WaitForResponseOnClose();

        this.ContentRendered += delegate
        {
            ///startTimer.Stop();
            ///var elapsedMs = startTimer.ElapsedMilliseconds;
        };
        this.Closed += delegate
        {
            ///endTimer.Stop();
            ///var elapsedMs = endTimer.ElapsedMilliseconds;
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

    private void WaitForResponseOnClose()
    {
        var closeTime = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 250) };
        closeTime.Tick += delegate { this.Close(); };
        
        //var theMachine = new TheMachine();
        
        RequestResponse? response = null;
        var worker = new BackgroundWorker();
        worker.DoWork += async delegate
        {
            var requestManager = new RequestManager();
            response = await requestManager.SendRequest("", RequestMethod.POST, "");
        };
        worker.RunWorkerCompleted += delegate { this.Close(); };

        bool started = false;
        Closing += (sender, e) =>
        {
            if (!started)
            {
                worker.RunWorkerAsync();
                started = true;
                closeTime.Start();
                e.Cancel = true;
            }
            if (response == null)
            {
                closeTime.Stop();
                e.Cancel = true;
            }
        };
    }

}