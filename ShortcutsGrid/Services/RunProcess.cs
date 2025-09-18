namespace ShortcutsGrid.Services;

using Forms.Wpf.Mls.Tools.Services;
using ShortcutsGrid.Extensions;
using ShortcutsGrid.Models;
using ShortcutsGrid.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

public static class RunProcess
{
    public static bool OpenLink(string uri)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = uri,
                UseShellExecute = true
            });
            return true;
        }
        catch { return false; }
    }

    public static bool OpenExplorer(string? path)
    {
        path = TryAddSpecialFolders(path!);
        string argument;
        if (File.Exists(path))
            argument = $"/select,\"{path}\"";
        else if (Directory.Exists(path))
            argument = $"\"{path}\"";
        else
            return false;
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = argument,
                UseShellExecute = true
            });
            return true;
        }
        catch { return false; }
    }

    public static bool Run(string commandOrPath, bool skipError = false, bool admin = false)
    {
        if (string.IsNullOrWhiteSpace(commandOrPath))
            return false;
        commandOrPath = TryAddSpecialFolders(commandOrPath);
        try
        {
            var (executable, arguments) = SeparateArguments(commandOrPath);
            ProcessStart(executable, admin, arguments);
            return true;
        }
        catch (Exception)
        {
            if (!skipError)
            {
                var messageDialogs = new MessageDialogs();
                messageDialogs.SimpleError(commandOrPath);
            }
            return false;
        }
    }
    public static bool Run(List<string> commandsOrPaths, bool admin = false)
    {
        string errorMsg = string.Empty;
        foreach (string commandOrPath in commandsOrPaths)
        {
            string cmdOrPath = TryAddSpecialFolders(commandOrPath);
            errorMsg += string.IsNullOrEmpty(errorMsg) ? cmdOrPath : "\n\n" + cmdOrPath;
            if (Run(commandOrPath, true, admin))
                return true;
        }
        var messageDialogs = new MessageDialogs();
        messageDialogs.SimpleError(errorMsg);
        return false;
    }

    public static (string Executable, string Arguments) SeparateArguments(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return (string.Empty, string.Empty);
        if (Directory.Exists(input))
            return (input, string.Empty);
        string[] list = input.Split(' ');
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
        string args = input.Replace(cmdOrPath, "");
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

    public static string TryAddSpecialFolders(string path)
    {
        try
        {
            string pattern = @"<\|(.*?)\|>";// e.g. <|Desktop|>
            var matches = Regex.Matches(path, pattern);
            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1)
                {
                    string value = match.Value;
                    string id = match.Groups[1].Value;
                    string folder = SpecialFolders.NameToPath(id);
                    path = path.Replace(value, folder);
                }
            }
            return path;
        }
        catch
        {
            return path;
        }
    }

}