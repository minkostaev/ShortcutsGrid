namespace ShortcutsGrid.Classes
{

    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    internal static class ReadShortcuts
    {

        public static List<Shortcut> FileToShortcuts(string filePath)
        {
            try
            {
                var result = new List<Shortcut>();
                
                var stream = new StreamReader(filePath, Encoding.Default);//UTF7
                while (!stream.EndOfStream)
                {
                    var line = stream.ReadLine();
                    string[] items = (line != null) ? line.Split('|') : new string[] { "", "", "" };
                    //path or command | label | image path
                    string path = (items.Length >= 1) ? items[0] : "";
                    string label = (items.Length >= 2) ? items[1] : "";
                    string img = (items.Length >= 3) ? items[2] : "";
                    if (!string.IsNullOrWhiteSpace(path) && !string.IsNullOrWhiteSpace(label) && !string.IsNullOrWhiteSpace(img))
                    {
                        result.Add(new Shortcut() { ExePath = path, ExeName = label, ImgPath = img });
                    }
                }
                stream.Close();
                return result;
            }
            catch
            {
                return new List<Shortcut>();
            }
        }

    }

}
