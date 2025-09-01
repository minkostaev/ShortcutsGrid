namespace ShortcutsGrid.Services;

using Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

public static class FileShortcuts
{
    public static void FileToShortcuts()
    {
        try
        {
            if (AppValues.CsvExists)
            {
                AppValues.FileTypeLoaded = "csv";
                AppValues.Shortcuts = CsvToShortcuts(AppValues.ListCsv, CsvDelimiter);
            }
            else if (AppValues.JsonExists)
            {
                AppValues.FileTypeLoaded = "json";
                AppValues.Shortcuts = JsonToShortcuts(AppValues.ListJson);
            }
            else
            {
                AppValues.FileTypeLoaded = string.Empty;
                AppValues.Shortcuts = [];
            }
        }
        catch
        {
            AppValues.FileTypeLoaded = null;
            AppValues.Shortcuts = [];
        }
        AppValues.Shortcuts.Add(new Shortcut() { AppName = "Drag or Close", ImgPath = AppValues.CloseDragImage, Tag = AppValues.CloseDragId });
    }
    public static void ShortcutsToFile()
    {
        if (AppValues.FileTypeLoaded == "csv")
        {
            ShortcutsToCsv(AppValues.Shortcuts, AppValues.ListCsv, CsvDelimiter);
        }
        else if (AppValues.FileTypeLoaded == "json")
        {
            ShortcutsToJson(AppValues.Shortcuts, AppValues.ListJson);
        }
    }

    #region CSV
    private static string CsvDelimiter => "@|@";
    private static Encoding CsvEncoding => Encoding.Default;//UTF7
    private static List<Shortcut> CsvToShortcuts(string? filePath, string delimiter)
    {
        var result = new List<Shortcut>();
        if (filePath == null)
            return result;

        string lbl = "";
        string exe = "";
        int count = 0;

        var stream = new StreamReader(filePath, CsvEncoding);
        while (!stream.EndOfStream)
        {
            count++;
            var line = stream.ReadLine();
            switch (count)
            {
                case 1:
                    lbl = line ?? "";
                    break;
                case 2:
                    exe = line ?? "";
                    break;
                case 3:
                    string? icon = line;
                    result.Add(new Shortcut() { ExePath = exe, AppName = lbl, ImgPath = icon });
                    count = 0;
                    break;
            }
        }
        stream.Dispose();
        return result;
    }
    private static bool ShortcutsToCsv(List<Shortcut> shortcuts, string? filePath, string delimiter)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return false;
        try
        {
            using var stream = new StreamWriter(filePath, false, CsvEncoding);
            foreach (var shortcut in shortcuts)
            {
                if (shortcut.Tag as string == AppValues.CloseDragId)
                    continue;
                stream.WriteLine(shortcut.AppName);
                stream.WriteLine(shortcut.ExePath);
                stream.WriteLine(shortcut.ImgPath);
            }
        }
        catch { return false; }
        return true;
    }
    #endregion

    #region JSON
    private static JsonSerializerOptions JsonOptions => new() { WriteIndented = true };
    private static List<Shortcut> JsonToShortcuts(string? filePath)
    {
        var shortcuts = new List<Shortcut>();
        if (string.IsNullOrWhiteSpace(filePath))
            return shortcuts;
        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<Shortcut>>(json) ?? [];
    }
    private static bool ShortcutsToJson(List<Shortcut> shortcuts, string? filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return false;
        try
        {
            string json = JsonSerializer.Serialize(shortcuts, JsonOptions);
            File.WriteAllText(filePath, json);
        }
        catch { return false; }
        return true;
    }
    #endregion

}