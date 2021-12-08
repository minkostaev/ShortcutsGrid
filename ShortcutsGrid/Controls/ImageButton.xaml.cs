using System.Windows.Controls;
using System.Windows.Media;

namespace ShortcutsGrid.Controls
{
    /// <summary>
    /// Interaction logic for ImageButton.xaml
    /// </summary>
    public partial class ImageButton : UserControl
    {
        public ImageButton(ImageSource? image, string label)
        {
            InitializeComponent();
            img.Source = image;
            tb.Text = label;
            btn.ToolTip = label;
        }
    }
}
