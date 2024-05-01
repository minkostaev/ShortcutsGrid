namespace ShortcutsGrid.Services;

using Forms.Wpf.Mls.Tools.Models;
using Forms.Wpf.Mls.Tools.Models.TheMachine;
using Forms.Wpf.Mls.Tools.Services;
using ShortcutsGrid.Models;
using ShortcutsGrid.Windows;
using System;
using System.ComponentModel;
using System.Text.Json;
using System.Windows.Threading;

public static class ShowShortcuts
{
    public static void WaitForResponseOnClose(MainWindow window)
    {
        var closeTime = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 250) };
        closeTime.Tick += delegate { window.Exit(); };

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
                { "Desktop-Value", AppValues.LastExecuted! },
                { "Desktop-Version", AppValues.AppVersion! }
            };
            var requestManager = new RequestManager(headers);
            string jsonString = JsonSerializer.Serialize(machine);
            response = await requestManager.SendRequest(AppValues.RequestPath, RequestMethod.POST, jsonString);
        };

        bool started = false;
        window.Closing += (sender, e) =>
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

    public static void Load(MainWindow window)
    {
        window.stkPnl1.Children.Clear();
        window.stkPnl2.Children.Clear();
        window.stkPnl3.Children.Clear();
        window.stkPnl4.Children.Clear();

        var shortcuts = ReadShortcuts.FileToShortcuts();

        shortcuts.Add(new Shortcut() { AppName = "Drag or Close", ImgPath = AppValues.CloseDragImage, Tag = AppValues.CloseDragId });

        foreach (var shortcut in shortcuts)
        {
            if (window.stkPnl1.Children.Count < 6)
            {
                window.stkPnl1.Children.Add(ImageButtonCreator.GetButton(shortcut, window));
            }
            else if (window.stkPnl2.Children.Count < 6)
            {
                window.stkPnl2.Children.Add(ImageButtonCreator.GetButton(shortcut, window));
            }
            else if (window.stkPnl3.Children.Count < 6)
            {
                window.stkPnl3.Children.Add(ImageButtonCreator.GetButton(shortcut, window));
            }
            else if (window.stkPnl4.Children.Count < 6)
            {
                window.stkPnl4.Children.Add(ImageButtonCreator.GetButton(shortcut, window));
            }
        }

        if (shortcuts.Count == 1)
        {
            new About().ShowDialog();
        }
    }

}