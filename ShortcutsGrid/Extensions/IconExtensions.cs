namespace ShortcutsGrid.Extensions;

using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

internal static class IconExtensions
{

    [DllImport("gdi32.dll", SetLastError = true)]
    private static extern bool DeleteObject(nint hObject);

    public static ImageSource ToImageSource(this Icon icon)
    {
        Bitmap bitmap = icon.ToBitmap();
        nint hBitmap = bitmap.GetHbitmap();
        ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(
            hBitmap, nint.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        if (!DeleteObject(hBitmap))
            throw new Win32Exception();
        return wpfBitmap;
    }

}