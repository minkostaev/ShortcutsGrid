using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutsGrid.Classes
{
    internal static class DialogRun
    {
        [DllImport("shell32.dll", EntryPoint = "#61", CharSet = CharSet.Unicode)]
        public static extern int RunFileDlg(
            [In] IntPtr hWnd,
            [In] IntPtr icon,
            [In] string path,
            [In] string title,
            [In] string prompt,
            [In] uint flags);

        //private static void Main(string[] args)
        //{
        //    // You might also want to add title, window handle...etc.
        //    RunFileDlg(IntPtr.Zero, IntPtr.Zero, null, null, null, 0);
        //}

        //RFF_NOBROWSE = 1; // Removes the browse button.
        //RFF_NODEFAULT = 2; // No default item selected.
        //RFF_CALCDIRECTORY = 4; // Calculates the working directory from the file name.
        //RFF_NOLABEL = 8; // Removes the edit box label.
        //RFF_NOSEPARATEMEM = 14; // Removes the Separate Memory Space check box (Windows NT only).

    }
}
