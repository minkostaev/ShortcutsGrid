namespace ShortcutsGrid.Extensions;

using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

public static class StringExtensions
{

    public static byte[]? ToBase64(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return null;
        if (input.Length % 4 != 0)// Base64 strings must have a length divisible by 4
            return null;
        try
        {// Attempt to decode the string
            return Convert.FromBase64String(input);
        }
        catch (FormatException)
        {// If decoding fails, it's not a valid Base64 string
            return null;
        }
    }
    public static bool IsBase64(this string input) => ToBase64(input) != null;
    public static (bool, byte[]?) IsToBase64(this string input)
    {
        var bt = ToBase64(input);
        return (bt != null, bt);
    }

    public static ImageSource? PathToImageSource(this string path)
    {
        var bitmap = new BitmapImage();
        try
        {
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.Absolute);
            bitmap.EndInit();
        }
        catch (Exception) { return null; }
        return bitmap;
    }

}