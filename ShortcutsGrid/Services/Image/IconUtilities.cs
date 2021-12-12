namespace ShortcutsGrid.Services.Image
{
    using System;
    using System.Windows;
    using System.Drawing;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.IO;

    internal static class IconUtilities
    {
        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);

        public static ImageSource ToImageSource(this Icon icon)
        {
            Bitmap bitmap = icon.ToBitmap();
            IntPtr hBitmap = bitmap.GetHbitmap();

            ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            if (!DeleteObject(hBitmap))
            {
                throw new Win32Exception();
            }

            return wpfBitmap;
        }

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

        public static ImageSource? ImagePathToSource(string? path)
        {
            try { return new BitmapImage(new Uri((path == null) ? "" : path)); }
            catch (Exception) { return null; }
        }

    }
}
