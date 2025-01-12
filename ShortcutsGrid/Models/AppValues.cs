﻿namespace ShortcutsGrid.Models;

using System;
using System.IO;
using System.Reflection;

internal static class AppValues
{
    public static string? LastExecuted { get; set; }
    public static long TimeToStart { get; set; }
    public static long TimeUsed { get; set; }

    public static string? ExePath => Environment.ProcessPath;
    public static string? ExeDir => Environment.CurrentDirectory;
    public static string? ExeName => Path.GetFileNameWithoutExtension(ExePath);

    public static string? AppVersion
    {
        get
        {
            try
            {
                return Assembly.GetExecutingAssembly()?.GetName()?.Version?.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

    public static string? ListCsv => Path.Combine(ExeDir ?? ExeName + ".csv", ExeName + ".csv");
    public static bool CsvExists => File.Exists(ListCsv);
    public static string? ListJson => Path.Combine(ExeDir ?? ExeName + ".json", ExeName + ".json");
    public static bool JsonExists => File.Exists(ListJson);

    public static string? GetSubPath(string path) => Path.Combine(ExeDir ?? string.Empty, path);

    public static string CloseDragImage
    {
        get
        {// base64 string
            return "iVBORw0KGgoAAAANSUhEUgAAAIAAAACACAYAAADDPmHLAAAACXBIWXMAAA7EAAAOxAGVKw4bAAAF+mlUWHRYTUw6Y29tLmFkb2JlLnhtcAAAAAAAPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz4gPHg6eG1wbWV0YSB4bWxuczp4PSJhZG9iZTpuczptZXRhLyIgeDp4bXB0az0iQWRvYmUgWE1QIENvcmUgNS42LWMxNDUgNzkuMTYzNDk5LCAyMDE4LzA4LzEzLTE2OjQwOjIyICAgICAgICAiPiA8cmRmOlJERiB4bWxuczpyZGY9Imh0dHA6Ly93d3cudzMub3JnLzE5OTkvMDIvMjItcmRmLXN5bnRheC1ucyMiPiA8cmRmOkRlc2NyaXB0aW9uIHJkZjphYm91dD0iIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyIgeG1sbnM6cGhvdG9zaG9wPSJodHRwOi8vbnMuYWRvYmUuY29tL3Bob3Rvc2hvcC8xLjAvIiB4bWxuczp4bXBNTT0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL21tLyIgeG1sbnM6c3RFdnQ9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9zVHlwZS9SZXNvdXJjZUV2ZW50IyIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ0MgMjAxOSAoV2luZG93cykiIHhtcDpDcmVhdGVEYXRlPSIyMDIyLTAyLTE0VDAwOjU3OjEzKzAyOjAwIiB4bXA6TW9kaWZ5RGF0ZT0iMjAyMi0wNC0yOVQwMTo1MjoxMCswMzowMCIgeG1wOk1ldGFkYXRhRGF0ZT0iMjAyMi0wNC0yOVQwMTo1MjoxMCswMzowMCIgZGM6Zm9ybWF0PSJpbWFnZS9wbmciIHBob3Rvc2hvcDpDb2xvck1vZGU9IjMiIHBob3Rvc2hvcDpJQ0NQcm9maWxlPSJzUkdCIElFQzYxOTY2LTIuMSIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDpkYzdlMWY4Mi1lMjcwLTBlNDctOGQ3Zi1kM2Y3NjZiNmNlNGYiIHhtcE1NOkRvY3VtZW50SUQ9ImFkb2JlOmRvY2lkOnBob3Rvc2hvcDphMzJjMWY2My00ZDU5LWMwNGUtYTg3YS1jYjczNTY4Mzc1ZmEiIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDo1ZjY2MTUwZS05Y2YzLTQ0NGEtOTk3NS01MjUzZjE5MTgxMmIiPiA8eG1wTU06SGlzdG9yeT4gPHJkZjpTZXE+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJjcmVhdGVkIiBzdEV2dDppbnN0YW5jZUlEPSJ4bXAuaWlkOjVmNjYxNTBlLTljZjMtNDQ0YS05OTc1LTUyNTNmMTkxODEyYiIgc3RFdnQ6d2hlbj0iMjAyMi0wMi0xNFQwMDo1NzoxMyswMjowMCIgc3RFdnQ6c29mdHdhcmVBZ2VudD0iQWRvYmUgUGhvdG9zaG9wIENDIDIwMTkgKFdpbmRvd3MpIi8+IDxyZGY6bGkgc3RFdnQ6YWN0aW9uPSJzYXZlZCIgc3RFdnQ6aW5zdGFuY2VJRD0ieG1wLmlpZDpkYzdlMWY4Mi1lMjcwLTBlNDctOGQ3Zi1kM2Y3NjZiNmNlNGYiIHN0RXZ0OndoZW49IjIwMjItMDQtMjlUMDE6NTI6MTArMDM6MDAiIHN0RXZ0OnNvZnR3YXJlQWdlbnQ9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgc3RFdnQ6Y2hhbmdlZD0iLyIvPiA8L3JkZjpTZXE+IDwveG1wTU06SGlzdG9yeT4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz449sJdAAAnpElEQVR4nO19eWxd13nn79z9rXyPj9ujSIqiSTqUJZmy6ESx5C1xHMd2XbdOnMbxxHbjFkU7M8A0gxkMphgMChTwADOpi0xQYFrVsesU0wSNXHfS1LXrxlq81oojyxa1kFopPr59ue/d/Z75gzxXl4+kNt7nWiB/wAWXd5d37vc73znfcr5DKKVYx9oF96/9Bdbxr4t1AqxxrBNgjWOdAGsc6wRY41gnwBrHOgHWONYJsMaxToA1jnUCrHGsE2CNY50AaxzrBFjj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";
        }
    }
    public static string CloseDragId => "CloseDragButton";

    ///public readonly static string RequestPath = $"https://localhost:7088/machinesdetails";
    public readonly static string RequestPath = $"https://www.apito.somee.com/machinesdetails";

}