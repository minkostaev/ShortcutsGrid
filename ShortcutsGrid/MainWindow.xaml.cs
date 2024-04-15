namespace ShortcutsGrid;

using Forms.Wpf.Mls.Tools.Models;
using Forms.Wpf.Mls.Tools.Models.TheMachine;
using Forms.Wpf.Mls.Tools.Services;
using Models;
using Services;
using System;
using System.ComponentModel;
using System.Text.Json;
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

        WaitForResponseOnClose();
        
        ContentRendered += delegate
        {
            ///startTimer.Stop();
            ///var elapsedMs = startTimer.ElapsedMilliseconds;
        };
        Closed += delegate
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

        this.PreviewKeyDown += (s, e) => { if (e.Key == Key.Escape) AppValues.Exit(); };
        #endregion

        ShowShortcuts.Load();
    }

    private void WaitForResponseOnClose()
    {
        var closeTime = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 250) };
        closeTime.Tick += delegate { AppValues.Exit(); };
        
        RequestResponse? response = null;
        var worker = new BackgroundWorker();
        worker.DoWork += async delegate
        {
            if (AppValues.LastExecuted == null)
                return;
            var machine = new TheMachine();
            var headers = new System.Collections.Generic.Dictionary<string, string>
            {
                { "Desktop-Machine", machine.Hash! },
                { "Desktop-Value", AppValues.LastExecuted! }
            };
            var requestManager = new RequestManager(headers);
            string jsonString = JsonSerializer.Serialize(machine);
            response = await requestManager.SendRequest(AppValues.RequestPath, RequestMethod.POST, jsonString);
        };

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
                e.Cancel = true;
            }
        };
    }

}