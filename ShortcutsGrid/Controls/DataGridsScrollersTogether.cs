namespace ShortcutsGrid.Controls;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

public class DataGridsScrollersTogether
{
    private readonly DataGrid _leftGrid;
    private readonly DataGrid _rightGrid;
    private ScrollViewer? scrollLeft;
    private ScrollViewer? scrollRight;
    public DataGridsScrollersTogether(DataGrid gridLeft, DataGrid gridRigh)
    {
        _leftGrid = gridLeft;
        _rightGrid = gridRigh;
        _leftGrid.Loaded += DataGrid_Loaded;
        _rightGrid.Loaded += DataGrid_Loaded;
    }
    private void DataGrid_Loaded(object sender, RoutedEventArgs e)
    {
        scrollLeft ??= GetScrollViewer(_leftGrid);
        scrollRight ??= GetScrollViewer(_rightGrid);
        if (scrollLeft != null && scrollRight != null)
        {
            scrollLeft.ScrollChanged += ScrollViewer_ScrollChanged;
            scrollRight.ScrollChanged += ScrollViewer_ScrollChanged;
        }
    }
    private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        if (sender == scrollLeft && e.VerticalChange != 0)
            scrollRight?.ScrollToVerticalOffset(e.VerticalOffset);
        else if (sender == scrollRight && e.VerticalChange != 0)
            scrollLeft?.ScrollToVerticalOffset(e.VerticalOffset);
    }
    private static ScrollViewer? GetScrollViewer(DependencyObject depObj)
    {
        if (depObj is ScrollViewer viewer)
            return viewer;
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
        {
            var child = VisualTreeHelper.GetChild(depObj, i);
            var result = GetScrollViewer(child);
            if (result != null) return result;
        }
        return null;
    }
}