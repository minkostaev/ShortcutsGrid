namespace ShortcutsGrid.Models
{
    using System;
    using System.IO;

    internal static class AppValues
    {

        public static string? ExePath => Environment.ProcessPath;
        public static string? ExeDir => Path.GetDirectoryName(ExePath);
        public static string? ExeName => Path.GetFileNameWithoutExtension(ExePath);

        public static string? ListFile => Path.Combine((ExeDir == null) ? ExeName + ".csv" : ExeDir, ExeName + ".csv");
        public static string? GetSubPath(string path) => Path.Combine((ExeDir == null) ? "" : ExeDir, path);

        // base64 string
        public static string CloseImage
        {
            get
            {
                return "iVBORw0KGgoAAAANSUhEUgAAAIAAAACACAYAAADDPmHLAAAExklEQVR42u2djU0WQRCGhwq0BDrAErADOnBLsAOxA0rQDuhASsAOKEE78N5wkygf8v3tzLxzM09yCSAhe/s8kHi3d3shTWkuogfQxNIBFKcDKE4HUJwOoDgdQHE6gOJ0AMXpAIrTARSnAyhOB1CcDqA4HUBxOoDidADF6QCK0wEUhyGAd8txsxyX6+f3y/EzelBGXK3nCp7Wc/0dOaDoAD4tx91yvH/x9Qd5nqjQyZkIIofs6xdf/7Ucn5fje9TAIgOA/G9v/PujPE9Y9ggg/2E5PrzxPUOCIogKYJ98JXsEh8hXhgREEBHAofKVrBEcI18Z4hyBdwDHyleyRXCKfGWIYwSeAZwqX8kSwTnylSFOEXgFcK58hT2CGfKVIQ4ReAQwS77CGsFM+coQ4wisA5gtX2GLwEK+MsQwAssArOQrLBFYyleGGEVgFYC1fCU6Ag/5yhCDCCwC8JKvREXgKV8ZMjmC2QFgUp5k99q+Nd4RRMgHuHdwOfM8Zwfg/dv/N14RRMlXhkz8KzA7gC/Lces4GS+xjiBaPrhdjq+zftjWAgBWETDIB7dCHAAWPDx6zsZ/mB0Bi3xZxzBtwYzF/wJ+yO7ChwhmRcAkH+P4OPMHWgTANGHnRrClc3kVqwtBW5i4LZzDXiwvBWeewMxjPwrrm0EZJzLjmE/G43ZwpgnNNNYpeC0IyTCxGcY4Hc8lYcwTzDw2U7wXhTJOtBCOye3OZsSycLYIhGgs1+J8WzvqwRCmCBgIW9gS+WhYR/BM6Kqm6IdDq0cQvaQtPABQNYJw+YAhAFAtAgr5gCUAUCUCGvmAKQCw9Qio5AO2AMBWI6CTDxgDAFuLgFI+YA0AbCUCWvmAOQCQPQJq+YA9AJA1Anr5IEMAIFsEKeSDLAGALBGkkQ8yBQDYI0glH2QLALBGkE4+yBgAYIsgpXzQAcyhA3CETb6SMoJsAbDKV9JFkCkAdvlKqgiyBJBFvpImggwBZJOvpIiAPYCs8hX6CJgDyC5foY6ANYCtyFdoI2AMYGvyFcoI2ALYqnyFLgKmALYuX6GKgCWAKvIVmggYAqgmX6GIIDqAqvKV8Aj68fB4Sj4e3vL/pdQLIpjk9ytinE+STf71+jHbmDb5kihG+f2aOKeTyjDBGcY4nX5VbN6xTqFfFr2NMZ9Mvy5+e2M/it4wYtvnsJfeMqbOubxKbxq1H6YIMA7qTaN62zh7qLeN640j7bkV4o0jowPorWOPpDePPp7oCIYQbx7d28fbQr99PPD+KxB1Lz0igiETf/uB1YUgrwiil1R5RjBksnxgeSnYOoJo+YpHBEMM5APrm0FWEbDIVywjGGIkH3jcDp4dAZt8xSKCIYbygdeCkFkRsMpXZkYwxFg+8FwSdm4E7PKVGREMcZAPvBeFnhpBFvnKOREMcZIPIpaFHxtBNvnKKREMcZQPoh4MOTSCrPKVYyIY4iwfRD4ati+C7PKVQyIYEiAfRD8cigjuZPfewcNy3Eh++QoiuJfdhTK4tv9ZguSD6AAAJgeyL9fPMVHTFjyQcbWeK3haz7X04+FNMB1AcTqA4nQAxekAitMBFKcDKE4HUJwOoDgdQHE6gOJ0AMXpAIrTARSnAyhOB1CcDqA4HUBxOoDi/AFW146Q6nzUWgAAAABJRU5ErkJggg==";
            }
        }

    }
}
