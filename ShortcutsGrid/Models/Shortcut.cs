namespace ShortcutsGrid.Models
{
    using ShortcutsGrid.Services.Image;
    using System.Windows.Media;

    public class Shortcut
    {

        public string ExePath { get; set; } = string.Empty;
        public string AppName { get; set; } = string.Empty;
        public string? ImgPath { get; set; } = string.Empty;//ImgPath or Base64String

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

}
