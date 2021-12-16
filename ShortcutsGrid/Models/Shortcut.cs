namespace ShortcutsGrid.Models
{
    using ShortcutsGrid.Services;
    using ShortcutsGrid.Services.Image;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Media;

    public class Shortcut
    {

        public string ExePath { get; set; } = string.Empty;
        public string ExeName { get; set; } = string.Empty;
        public string? ImgPath { get; set; } = string.Empty;//ImgPath or Base64String
        public string ErrMsg { get; set; } = string.Empty;

        private ImageSource? _image;
        public ImageSource? Image
        {
            get
            {
                if (_image == null)
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace(ImgPath))
                        {
                            if (ImgPath != null)
                            {
                                Icon icon = ExtractIcon.ExtractIconFromExecutable(ExePath);
                                _image = icon.ToImageSource();
                            }
                        }
                        else
                        {
                            if (ImgPath.IsBase64())
                            {
                                _image = IconUtilities.Base64StringToImage(ImgPath);
                            }
                            else if (File.Exists(ImgPath))
                            {
                                _image = IconUtilities.ImagePathToSource(ImgPath);
                                if (_image == null)
                                {
                                    var combinedSubPath = AppValues.GetSubPath(ImgPath);
                                    _image = IconUtilities.ImagePathToSource(combinedSubPath);
                                }
                            }
                        }
                        ErrMsg = string.Empty;
                    }
                    catch (Exception ex) { ErrMsg = ex.Message; }
                }
                return _image;
            }
        }

    }
}
