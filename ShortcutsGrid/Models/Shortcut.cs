namespace ShortcutsGrid.Models;

using Services.Image;
using System.Windows.Media;

public class Shortcut : Extended.Models.Shortcut
{

    ///public string ExePath { get; set; } = string.Empty;
    ///public string AppName { get; set; } = string.Empty;
    ///public string? ImgPath { get; set; } = string.Empty;//ImgPath or Base64String
    public object? Tag { get; set; }//Helper property

    private ImageSource? _image;
    public ImageSource? Image
    {
        get
        {
            if (_image == null)
            {
                _image = ImageUtilities.GetImageFromPaths(ImgPath, ExePath);
            }
            return _image;
        }
    }

}

