namespace ShortcutsGrid.Services.Image;

using Models;
using ShortcutsGrid.Extensions;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

internal static class ImageUtilities
{

    public static ImageSource Base64ToImage(byte[] base64Image)
    {
        var ms = new MemoryStream(base64Image);
        var img = Image.FromStream(ms);
        var bmp = new Bitmap(img);
        IntPtr btmp = bmp.GetHbitmap();
        ImageSource imgSrc = Imaging.CreateBitmapSourceFromHBitmap(
            btmp, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        return imgSrc;
    }
    public static ImageSource Base64ToImage(string base64ImageString)
    {
        var bt = Convert.FromBase64String(base64ImageString);
        return Base64ToImage(bt);
    }

    public static ImageSource? GetImageFromPaths(string? imgPath, string? exePath)
    {
        if (string.IsNullOrWhiteSpace(imgPath))
        {
            if (!string.IsNullOrWhiteSpace(exePath))
            {
                Icon icon = ExtractIcon.ExtractIconFromExecutable(exePath);
                return icon.ToImageSource();
            }
            return null;
        }
        else
        {
            var bt64 = imgPath.IsToBase64();
            if (bt64.Item1 && bt64.Item2 != null)
            {
                return Base64ToImage(bt64.Item2);
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
            else if (imgPath.StartsWith("http"))
            {
                return imgPath.PathToImageSource();
            }
            return null;
        }
    }

}