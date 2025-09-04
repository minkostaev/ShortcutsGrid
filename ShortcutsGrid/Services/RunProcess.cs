namespace ShortcutsGrid.Services;

using ShortcutsGrid.Extensions;
using ShortcutsGrid.Models;
using System;
using System.Diagnostics;
using System.IO;

public static class RunProcess
{

    public static void OpenLink(string uri)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = uri,
            UseShellExecute = true
        });
    }

    public static bool Run(string commandOrPath, bool admin = false)
    {
        if (string.IsNullOrWhiteSpace(commandOrPath))
            return false;
        try
        {
            var (executable, arguments) = ParseTarget(commandOrPath);
            ProcessStart(executable, admin, arguments);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private static (string Executable, string Arguments) ParseTarget(string line)
    {
        string[] list = line.Split(' ');
        bool isCommand = true;
        string cmdOrPath = string.Empty;
        foreach (string arg in list)
        {
            cmdOrPath = string.IsNullOrEmpty(cmdOrPath) ? arg : cmdOrPath + " " + arg;
            if (File.Exists(cmdOrPath))
            {
                isCommand = false;
                break;
            }
        }
        if (isCommand && list.Length > 0)
        {
            cmdOrPath = list[0];
        }
        string args = line.Replace(cmdOrPath, "");
        return (cmdOrPath, args);
    }

    private static void ProcessStart(string commandOrPath, bool admin, string args = "")
    {
        var process = new Process();
        process.StartInfo.FileName = commandOrPath;
        process.StartInfo.Arguments = args;
        process.StartInfo.Verb = admin ? "runas" : "";
        if (File.Exists(commandOrPath))
        {
            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(commandOrPath);
        }
        process.StartInfo.UseShellExecute = true;
        process.Start();
        AppValues.LastExecuted = commandOrPath;
        _ = WindowRequest.SendToApi();
    }

}