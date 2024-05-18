namespace ShortcutsGrid.Services.Run;

using ShortcutsGrid.Models;
using System;
using System.Diagnostics;
using System.IO;

public static class RunProcess
{

    public static string Run(string commandOrPath, bool admin = false)
    {
        if (string.IsNullOrWhiteSpace(commandOrPath))
        {
            return string.Empty;
        }
        if (commandOrPath.Equals("run", StringComparison.CurrentCultureIgnoreCase))
        {
            RunDialog.OpenDefault();
            return string.Empty;
        }
        try
        {
            ProcessStart(commandOrPath, admin);
            return string.Empty;
        }
        catch (Exception)
        {
            try
            {
                string[] cmdFlAndArgs = commandOrPath.Split(' ');
                string cmdOrPath = string.Empty;
                string args = string.Empty;
                bool isCommand = true;
                foreach (string arg in cmdFlAndArgs)
                {
                    cmdOrPath = string.IsNullOrEmpty(cmdOrPath) ? arg : cmdOrPath + " " + arg;
                    if (File.Exists(cmdOrPath))
                    {
                        args = commandOrPath.Replace(cmdOrPath, "");
                        isCommand = false;
                        break;
                    }
                }
                if (isCommand)
                {
                    cmdOrPath = cmdFlAndArgs[0];
                    args = commandOrPath.Replace(cmdOrPath, "");
                }
                ProcessStart(cmdOrPath, admin, args);
                return string.Empty;
            }
            catch (Exception ex) { return ex.Message; }
        }
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
        _ = ShowShortcuts.SendToApi();
    }

    public static void OpenLink(string uri)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = uri,
            UseShellExecute = true
        });
    }

}