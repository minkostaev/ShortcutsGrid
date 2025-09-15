namespace ShortcutsGrid.Models;

using Services.Image;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

public class Shortcut
{
    /// <summary>
    /// Path or URL or Command or Directory.
    /// It's a list to try multiple execution if it fails
    /// </summary>
    public List<string> Executions { get; set; } = [];
    /// <summary>
    /// Name bellow the icon (optional)
    /// </summary>
    public string Label { get; set; } = string.Empty;
    /// <summary>
    /// Tooltip text when hover the icon (optional)
    /// </summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// Icon path or base64String (optional)
    /// </summary>
    public string? Image { get; set; } = string.Empty;
    /// <summary>
    /// Helper property
    /// </summary>
    public object? Tag { get; set; }

    private ImageSource? _imageSource;
    public ImageSource? ImageSource
    {
        get
        {
            _imageSource ??= ImageUtilities.GetImageFromPaths(Image, Executions.FirstOrDefault());
            return _imageSource;
        }
    }
    
}