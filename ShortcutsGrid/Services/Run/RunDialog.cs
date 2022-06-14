namespace ShortcutsGrid.Services.Run;

using System;
using System.Runtime.InteropServices;

public static class RunDialog
{
    [DllImport("shell32.dll", EntryPoint = "#61", CharSet = CharSet.Unicode)]
    private static extern int RunFileDlg(
        [In] IntPtr hWnd,
        [In] IntPtr icon,
        [In] string? path,
        [In] string? title,
        [In] string? prompt,
        [In] uint flags);

    //RFF_NOBROWSE = 1; // Removes the browse button.
    //RFF_NODEFAULT = 2; // No default item selected.
    //RFF_CALCDIRECTORY = 4; // Calculates the working directory from the file name.
    //RFF_NOLABEL = 8; // Removes the edit box label.
    //RFF_NOSEPARATEMEM = 14; // Removes the Separate Memory Space check box (Windows NT only).

    public static int OpenDefault()
    {
        return RunFileDlg(IntPtr.Zero, IntPtr.Zero, null, null, null, 0);
    }

    //Add more version of this dialog

}