namespace ShortcutsGrid.Services.Run
{
    using System;
    using System.Diagnostics;

    public static class RunProcess
    {

        public static string Run(string commandOrPath, bool admin = false)
        {
            if (string.IsNullOrWhiteSpace(commandOrPath)) { return string.Empty; }
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
            //todo
            ///process.StartInfo.WorkingDirectory = "c:\\";
            process.StartInfo.FileName = commandOrPath;
            process.StartInfo.Arguments = args;
            process.StartInfo.Verb = admin ? "runas" : "";
            process.Start();
        }

    }
}
