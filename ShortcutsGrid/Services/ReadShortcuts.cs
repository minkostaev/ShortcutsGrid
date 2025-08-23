namespace ShortcutsGrid.Services;

using Models;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

public static class ReadShortcuts
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
    private static string CsvDelimiter => "|";
    private static Encoding CsvEncoding => Encoding.Default;//UTF7
    private static List<Shortcut> CsvToShortcuts(string? filePath, string delimiter)
    {
        var result = new List<Shortcut>();
        if (filePath == null)
            return result;
        var stream = new StreamReader(filePath, CsvEncoding);
        while (!stream.EndOfStream)
        {
            var line = stream.ReadLine();
            string[] items = (line != null) ? line.Split(delimiter) : [""];
            //path or command | label | image path or base64
            string path = (items.Length >= 1) ? items[0] : "";
            string label = (items.Length >= 2) ? items[1] : "";
            string? img = (items.Length >= 3) ? items[2] : null;
            if (items.Length > 1 && !path.StartsWith("//"))
                result.Add(new Shortcut() { ExePath = path, AppName = label, ImgPath = img });
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
                string line = $"{shortcut.ExePath}{delimiter}{shortcut.AppName}{delimiter}{shortcut.ImgPath}";
                stream.WriteLine(line);
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