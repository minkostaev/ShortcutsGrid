namespace ShortcutsGrid.Classes
{
    using System;
    using System.IO;

    internal static class AppValues
    {

        public static string? ExePath => Environment.ProcessPath;
        public static string? ExeDir => Path.GetDirectoryName(ExePath);
        public static string? ExeName => Path.GetFileNameWithoutExtension(ExePath);

        public static string? ListFile => Path.Combine((ExeDir == null) ? ExeName + ".csv" : ExeDir, ExeName + ".csv");
        public static string? GetSubPath(string path) => Path.Combine((ExeDir == null) ? "" : ExeDir, path);

    }
}
