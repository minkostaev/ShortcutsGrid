namespace ShortcutsGrid.Services.Image;

using ShortcutsGrid.Models;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

internal static class ImageUtilities
{
    public static ImageSource Base64StringToImage(string base64ImageString)
    {
        byte[] b;
        b = Convert.FromBase64String(base64ImageString);
        var ms = new MemoryStream(b);
        var img = Image.FromStream(ms);

        var bmp = new Bitmap(img);
        IntPtr hBitmap = bmp.GetHbitmap();
        ImageSource imageSource = Imaging.CreateBitmapSourceFromHBitmap(
            hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

        return imageSource;
    }

    public static ImageSource? GetImageFromPaths(string? imgPath, string exePath)
    {
        if (string.IsNullOrWhiteSpace(imgPath))
        {
            if (imgPath != null)
            {
                Icon icon = ExtractIcon.ExtractIconFromExecutable(exePath);
                return icon.ToImageSource();
            }
            return null;
        }
        else
        {
            if (imgPath.IsBase64())
            {
                return ImageUtilities.Base64StringToImage(imgPath);
            }
            else if (File.Exists(imgPath))
            {
                var _image = imgPath.PathToImageSource();
                if (_image == null)
                {
                    var combinedSubPath = AppValues.GetSubPath(imgPath);
                    return combinedSubPath?.PathToImageSource();
                }
                return _image;
            }
            return null;
        }
    }

}
