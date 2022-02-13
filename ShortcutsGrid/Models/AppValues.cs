﻿namespace ShortcutsGrid.Models
{
    using System;
    using System.IO;

    internal static class AppValues
    {

        public static string? ExePath => Environment.ProcessPath;
        public static string? ExeDir => Path.GetDirectoryName(ExePath);
        public static string? ExeName => Path.GetFileNameWithoutExtension(ExePath);

        public static string? ListCsv => Path.Combine((ExeDir == null) ? ExeName + ".csv" : ExeDir, ExeName + ".csv");
        public static bool CsvExists => File.Exists(ListCsv);
        public static string? ListJson => Path.Combine(ExeDir ?? ExeName + ".json", ExeName + ".json");
        public static bool JsonExists => File.Exists(ListJson);

        public static string? GetSubPath(string path) => Path.Combine((ExeDir == null) ? "" : ExeDir, path);

        // base64 string
        public static string CloseImage
        {
            get
            {
                return "iVBORw0KGgoAAAANSUhEUgAAAQAAAAEACAYAAABccqhmAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAEj6SURBVHhe7Z0HuFXVte83HQTpSO+9g4AKIgIxKoKaRFETU7wxmmiiyU3ue3lf8nJNu0ney7vx3Zt34ZpojDeWRBCNihrRRLAQsCCoVOlF6R1E2hu/dfb/OM5y79M4Za3D/H/f2LOsudaea44x/rOslgkICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgISDSaZcNMs2bNamejAQEBFYw62TBJGGYy2gTH/6TJ5ubNm9dr3bp1h3379u2xdGbUqFH1t2zZcpx4QECSYLbZzGzzCPHJkyc3GDx48NmDBg0atnTp0lVRgYShVjZMEi40+TeTjiYfmDzTuHHjfk2aNJlr4YozzjhjUKNGjWbUNdSuXbvuyy+//NKYMWPaWblDr7zyyj6L17LwpKUDAioNbdu2bWpBi61bt67v0qVLI7PLs80uO1t4hoWbGzZsOMrk/AYNGtQzUz1w7NixTz/wwAOJs8skEkAra7Qd1113Xea+++47bixap3379pkZM2ZsPeecc9pu3LjxiBHBa/Xr1x9oBPDXkydPbrB9+teqVeshi59h4UELZ1reSIuvMYLYZKRwlpHCNg5u8ToWD6OHgLww525kjn04Gz/Dgm4mtcyuVlg4xmS4SWOzr/pmg4fMXi82m+xtstuklnFAu+HDh5/Vrl27zKJFiw5ZmU1GAP3vvffeE7ZfopBEAmBasvOzn/1sMxv6Z6xBM8akmaNHj2ZatmyZqVOnTsamApk2bdpkHnrooWNGCnUp9+yzz77Xt2/f9itXrnzdFLXfjtEzSwqtLNxm4WOWN8riCyy+2sLuRg5v8IdGCs2MFPZm42EEcZrAnLuxBbXM2Q+cddZZjcwm+lu6udkHI0/s4SqTLiY4bl0T1qYuMaltZd+3sMno0aPb9O7dO2P7Zx5//PGTV199da1mzZplbMSaMcfP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fvKTtVH4ggUL6DFwbG42Ir7FhEtb9Ojk0btgOBgU3yXgqS4WtCgL+PoDo4KA5IFLCawttDFhLQmSvtxEUw0I4nWTUSZML942udQEz97CmgOdw/Lly4/aCLQe9sSaA7aEXYkUsC+ALeLwWgRElKbsjBkz9lt60Jw5c6r6du1SIakEcFOXLl1+Y2wdOT8ODPPK8QnJQ2holLRr167Mnj17GHIdnDRpUmN6dJt/0Qu8YcKCE70FTsxqOnNxHBthns7CFF/J7WDC97Ar62aagOqFJ25GmeicEcUUE+yAacbZJkznWJxkZLevZ8+e41evXs3osMHZZ599BnbXvn37aOTIiKFZs2YRIbRo0SKyR3p/rQf8/ve/P2GjgtHz589nTShxSCoB/MegQYNu5bVSrLQzl4JVaVR6cBg624t/OHLkyDY2dGdBiHk3bB59SMRkrglzRpybITzKZPjOohs0Thnm6wJt8dFQIuB0Abag+ya05sBiI18ywmm5IsEUgx6cKQW2xmjiUybYV+fevXu3W7Vq1Y7Jkye35irFc889d+L73/9+bTqklStXZjZv3vzFpUuX/sHKJg5JJYC/jBo16mIWcGwohuPSa+PY9Nj06KyeM/fjMhkO/awJDM3cnKEfwz714n7OCIKjB5QFmlIwRWCRF7vjigX3MrAuxEhBa0dcvYAUeDnCIJO3rNM620YIv7ARKV9DThySSgBzTLjN9C8mXD5jYQ12hgj+agJDswDA0B4lAL96HBBQVZDd6T4OphWsOXD7L4uRn7JpwuM2isWmA0qJi0xgWMACXPzVSgVLsQEByYPvVKMPBdb1lxMCAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAhKITOb/AwKlW+AiayEyAAAAAElFTkSuQmCC";
            }
        }

    }
}
