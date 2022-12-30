namespace ShortcutsGrid.Services.Run;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Documents;

public static class RunProcess
{

    public static string Run(string commandOrPath, bool admin = false)
    {
        if (string.IsNullOrWhiteSpace(commandOrPath))
        {
            return string.Empty;
        }
        if (commandOrPath.ToLower() == "run")
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
                string[] cmdAndArgs = commandOrPath.Split(' ');
                string processCommandOrPath = string.Empty;
                string args = string.Empty;
                if (commandOrPath.Contains('\\'))
                {
                    var pathStructure = cmdAndArgs.ToList();
                    for (int i = cmdAndArgs.Length - 1; i > 0; i--)
                    {
                        pathStructure.RemoveAt(i);
                        var fileOrFolder = String.Join(" ", pathStructure.ToArray());
                        if (File.Exists(fileOrFolder))//to do case for folder if needed
                        {
                            processCommandOrPath = fileOrFolder;
                            args = commandOrPath.Replace(processCommandOrPath, "");
                            break;
                        }
                    }
                }
                else
                {
                    args = commandOrPath.Remove(0, cmdAndArgs[0].Length);
                    processCommandOrPath = cmdAndArgs[0];
                }
                ProcessStart(processCommandOrPath, admin, args);
                return string.Empty;
            }
            catch (Exception ex) { return ex.Message; }
        }
    }

    private static void ProcessStart(string commandOrPath, bool admin, string args = "")
    {
        Process process = new Process();
        process.StartInfo.UseShellExecute = true;
        //to do
        ///process.StartInfo.WorkingDirectory = "c:\\";
        process.StartInfo.FileName = commandOrPath;
        process.StartInfo.Arguments = args;
        process.StartInfo.Verb = admin ? "runas" : "";
        process.Start();
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