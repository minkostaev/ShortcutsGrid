namespace ShortcutsGrid.Models;

using Services.Image;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

public class Shortcut
{
    public List<string> Executions { get; set; } = [];// to do
    //public string Execution { get; set; } = string.Empty;//Path or URL or Command or Directory
    public string Execution => Executions.FirstOrDefault();
    public string Label { get; set; } = string.Empty;//Name
    public string Description { get; set; } = string.Empty;//Tooltip
    public string? Image { get; set; } = string.Empty;//Path or Base64String
    public object? Tag { get; set; }//Helper property

    private ImageSource? _imageSource;
    public ImageSource? ImageSource
    {
        get
        {
            _imageSource ??= ImageUtilities.GetImageFromPaths(Image, Execution);
            return _imageSource;
        }
    }
    
}