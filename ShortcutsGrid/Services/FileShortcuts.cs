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
        var closeShortcut = new Shortcut();
        closeShortcut.MakeItCloseItem();
        AppValues.Shortcuts.Add(closeShortcut);
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
    private static string CsvDelimiter => "|||";
    private static Encoding CsvEncoding => Encoding.Default;//UTF7
    private static List<Shortcut> CsvToShortcuts(string? filePath, string delimiter)
    {
        var result = new List<Shortcut>();
        if (filePath == null)
            return result;
        var exes = new List<string>();
        string lbl = string.Empty;
        string dsc = string.Empty;
        int count = 0;
        var stream = new StreamReader(filePath, CsvEncoding);
        while (!stream.EndOfStream)
        {
            var line = stream.ReadLine();
            count++;
            switch (count)
            {
                case 1:
                    {
                        string[] items = (line != null) ? line.Split(delimiter) : [""];
                        lbl = (items.Length >= 1) ? items[0] : "";
                        dsc = (items.Length >= 2) ? items[1] : "";
                        break;
                    }
                case 2:
                    {
                        exes = [];
                        string[] items = (line != null) ? line.Split(delimiter) : [""];
                        foreach (var item in items)
                            exes.Add(item);
                        break;
                    }
                case 3:
                    string? icon = line;
                    result.Add(new Shortcut() { Executions = exes, Label = lbl, Image = icon, Description = dsc });
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
                if (shortcut.Id == AppValues.CloseDragId)
                    continue;
                string description = string.IsNullOrEmpty(shortcut.Description)
                    ? string.Empty : delimiter + shortcut.Description;
                stream.WriteLine(shortcut.Label + description);
                stream.WriteLine(string.Join(delimiter, shortcut.Executions.ToArray()));
                stream.WriteLine(shortcut.Image);
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