namespace ShortcutsGrid.Extensions;

using Forms.Wpf.Mls.Tools.Models;
using Forms.Wpf.Mls.Tools.Services;
using Mintzat.Email.Models.TheMachine;
using ShortcutsGrid.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Threading;

public static class WindowRequest
{

    public static void AttachRequest(this MainWindow window, Stopwatch startTimer)
    {
        var endTimer = Stopwatch.StartNew();
        window.ContentRendered += delegate
        {
            startTimer.Stop();
            AppValues.TimeToStart = startTimer.ElapsedMilliseconds;
        };

        var closeTime = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, 250) };
        closeTime.Tick += delegate { window.Exit(); };
        
        RequestResponse? response = null;
        var worker = new BackgroundWorker();
        worker.DoWork += async delegate
        {
            endTimer.Stop();
            AppValues.TimeUsed = endTimer.ElapsedMilliseconds;
            response = await SendToApi(true);
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

    public static async Task<RequestResponse?> SendToApi(bool exitRequest = false)
    {
        if (string.IsNullOrWhiteSpace(AppValues.LastExecuted))
            return null;
        if (AppValues.WillBeClosed && !exitRequest)
            return null;

        var machine = new TheMachine();
        var headers = new Dictionary<string, string?>
        {
            { "Desktop-Machine", machine.Hash },
            { "Desktop-Value", AppValues.LastExecuted },
            { "Desktop-Version", $"ShortcutsGrid|{AppValues.AppVersion}" }
        };
        if (AppValues.TimeUsed != 0)
            headers.Add("Desktop-Time", $"{AppValues.TimeToStart}|{AppValues.TimeUsed}");
        var requestManager = new RequestManager(headers!);
        string jsonString = JsonSerializer.Serialize(machine);
        return await requestManager.SendRequest(AppValues.RequestPath, RequestMethod.POST, jsonString);
    }

}