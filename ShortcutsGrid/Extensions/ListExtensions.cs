namespace ShortcutsGrid.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal static class ListExtensions
{

    public static bool MoveItem<T>(this List<T> list, int oldIndex, int newIndex)
    {
        if (oldIndex < 0 || oldIndex >= list.Count || newIndex < 0 || newIndex >= list.Count)
            return false;
        T item = list[oldIndex];
        list.RemoveAt(oldIndex);
        list.Insert(newIndex, item);
        return true;
    }

}