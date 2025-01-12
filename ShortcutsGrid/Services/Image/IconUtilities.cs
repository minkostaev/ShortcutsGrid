namespace ShortcutsGrid.Services.Image;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

internal static class IconUtilities
{
    [UnmanagedFunctionPointer(CallingConvention.Winapi, SetLastError = true, CharSet = CharSet.Unicode)]
    [SuppressUnmanagedCodeSecurity]
    internal delegate bool ENUMRESNAMEPROC(IntPtr hModule, IntPtr lpszType, IntPtr lpszName, IntPtr lParam);
    [DllImport(KERNEL32_DLL, SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, uint dwFlags);

    [DllImport(KERNEL32_DLL, SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern IntPtr FindResource(IntPtr hModule, IntPtr lpName, IntPtr lpType);

    [DllImport(KERNEL32_DLL, SetLastError = true)]
    public static extern IntPtr LoadResource(IntPtr hModule, IntPtr hResInfo);

    [DllImport(KERNEL32_DLL, SetLastError = true)]
    public static extern IntPtr LockResource(IntPtr hResData);

    [DllImport(KERNEL32_DLL, SetLastError = true)]
    public static extern uint SizeofResource(IntPtr hModule, IntPtr hResInfo);

    [DllImport(KERNEL32_DLL, SetLastError = true, CharSet = CharSet.Unicode)]
    [SuppressUnmanagedCodeSecurity]
    public static extern bool EnumResourceNames(IntPtr hModule, IntPtr lpszType, ENUMRESNAMEPROC lpEnumFunc, IntPtr lParam);

    private const string KERNEL32_DLL = "kernel32.dll";
    private const uint LOAD_LIBRARY_AS_DATAFILE = 0x00000002;
    private readonly static IntPtr RT_ICON = (IntPtr)3;
    private readonly static IntPtr RT_GROUP_ICON = (IntPtr)14;

    public static Icon ExtractIconFromExecutable(string path)
    {
        IntPtr hModule = LoadLibraryEx(path, IntPtr.Zero, LOAD_LIBRARY_AS_DATAFILE);
        var tmpData = new List<byte[]>();
        bool callback(nint h, nint t, nint name, nint l)
        {
            var dir = GetDataFromResource(hModule, RT_GROUP_ICON, name);
            // Calculate the size of an entire .icon file.
            int count = BitConverter.ToUInt16(dir, 4);  // GRPICONDIR.idCount
            int len = 6 + 16 * count;                   // sizeof(ICONDIR) + sizeof(ICONDIRENTRY) * count
            for (int i = 0; i < count; ++i)
                len += BitConverter.ToInt32(dir, 6 + 14 * i + 8); // GRPICONDIRENTRY.dwBytesInRes
            using var dst = new BinaryWriter(new MemoryStream(len));
            dst.Write(dir, 0, 6);// Copy GRPICONDIR to ICONDIR.
            int picOffset = 6 + 16 * count; // sizeof(ICONDIR) + sizeof(ICONDIRENTRY) * count
            for (int i = 0; i < count; ++i)
            {// Load the picture.
                ushort id = BitConverter.ToUInt16(dir, 6 + 14 * i + 12); // GRPICONDIRENTRY.nID
                var pic = GetDataFromResource(hModule, RT_ICON, (IntPtr)id);
                // Copy GRPICONDIRENTRY to ICONDIRENTRY.
                dst.Seek(6 + 16 * i, 0);
                dst.Write(dir, 6 + 14 * i, 8);  // First 8bytes are identical.
                dst.Write(pic.Length);          // ICONDIRENTRY.dwBytesInRes
                dst.Write(picOffset);           // ICONDIRENTRY.dwImageOffset
                dst.Seek(picOffset, 0);
                dst.Write(pic, 0, pic.Length);
                picOffset += pic.Length;
            }
            tmpData.Add(((MemoryStream)dst.BaseStream).ToArray());
            return true;
        }
        EnumResourceNames(hModule, RT_GROUP_ICON, callback, IntPtr.Zero);
        byte[][] iconData = [.. tmpData];
        using var ms = new MemoryStream(iconData[0]);
        return new Icon(ms);
    }
    private static byte[] GetDataFromResource(IntPtr hModule, IntPtr type, IntPtr name)
    {// Load the binary data from the specified resource.
        IntPtr hResInfo = FindResource(hModule, name, type);
        IntPtr hResData = LoadResource(hModule, hResInfo);
        IntPtr pResData = LockResource(hResData);
        uint size = SizeofResource(hModule, hResInfo);
        byte[] buf = new byte[size];
        Marshal.Copy(pResData, buf, 0, buf.Length);
        return buf;
    }

}