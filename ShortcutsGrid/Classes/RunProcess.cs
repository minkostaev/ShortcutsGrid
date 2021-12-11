namespace ShortcutsGrid.Classes
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public static class RunProcess
    {

        public static string Run(string commandOrPath, bool admin = false)
        {
            if (string.IsNullOrWhiteSpace(commandOrPath)) { return string.Empty; }
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
                    string args = commandOrPath.Remove(0, cmdAndArgs[0].Length);
                    ProcessStart(cmdAndArgs[0], admin, args);
                    return string.Empty;
                }
                catch (Exception ex) { return ex.Message; }
            }
        }

        private static void ProcessStart(string commandOrPath, bool admin, string args = "")
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            ///process.StartInfo.WorkingDirectory = "c:\\";
            process.StartInfo.FileName = commandOrPath;
            process.StartInfo.Arguments = args;
            process.StartInfo.Verb = admin ? "runas" : "";
            process.Start();
        }

        public static string GetShortcutsFile
        {
            get
            {
                ///var exePath = Process.GetCurrentProcess().MainModule.FileName;
                var exePath = Environment.ProcessPath;
                var exeDir = Path.GetDirectoryName(exePath);
                var exeName = Path.GetFileNameWithoutExtension(exePath);
                var fileWithList = Path.Combine(exeDir == null ? "" : exeDir, exeName + ".csv");
                return fileWithList;
            }
        }

    }

}
