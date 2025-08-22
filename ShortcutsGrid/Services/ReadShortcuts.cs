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
                AppValues.Shortcuts = CsvToShortcuts(AppValues.ListCsv, "|");
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

    private static List<Shortcut> CsvToShortcuts(string? filePath, string delimiter)
    {
        var result = new List<Shortcut>();

        if (filePath == null)
            return result;

        var stream = new StreamReader(filePath, Encoding.Default);//UTF7
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

    private static bool ShortcutsToCsv(List<Shortcut> shortcuts, string filePath, string delimiter)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return false;
        using var stream = new StreamWriter(filePath, false, Encoding.Default);
        foreach (var shortcut in shortcuts)
        {
            string line = $"{shortcut.ExePath}{delimiter}{shortcut.AppName}{delimiter}{shortcut.ImgPath}";
            stream.WriteLine(line);
        }
        return true;
    }

    private static List<Shortcut> JsonToShortcuts(string? filePath)
    {
        var shortcuts = new List<Shortcut>();
        if (string.IsNullOrWhiteSpace(filePath))
            return shortcuts;

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<Shortcut>>(json) ?? [];
    }

}