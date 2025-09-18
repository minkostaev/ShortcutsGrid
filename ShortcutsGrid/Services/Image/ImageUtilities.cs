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

    private static ImageSource? ExeToImage(string? exePath)
    {
        if (!string.IsNullOrWhiteSpace(exePath))
        {
            Icon icon = IconUtilities.ExtractIconFromExecutable(exePath);
            return icon.ToImageSource();
        }
        return null;
    }

    public static ImageSource? Base64ToImage(byte[] base64Image)
    {
        var ms = new MemoryStream(base64Image);
        Image img;
        try { img = Image.FromStream(ms); }
        catch (Exception) { return null; }
        var bmp = new Bitmap(img);
        IntPtr btmp = bmp.GetHbitmap();
        ImageSource imgSrc = Imaging.CreateBitmapSourceFromHBitmap(
            btmp, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        return imgSrc;
    }
    public static ImageSource? Base64ToImage(string base64ImageString)
    {
        var bt = Convert.FromBase64String(base64ImageString);
        return Base64ToImage(bt);
    }

    public static ImageSource? GetImageFromPaths(string? imgPath, string? exePath)
    {
        if (string.IsNullOrWhiteSpace(imgPath))
        {// this app exe icon as image
            return ExeToImage(exePath);
        }
        else
        {
            var (isBase64, byteResult) = imgPath.Base64();
            if (isBase64 && byteResult != null)
            {
                var image = Base64ToImage(byteResult);
                if (image == null)
                {
                    return ExeToImage(exePath);
                }
                return image;
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
            else
            {
                return null;
            }
        }
    }

}