using System;
using System.IO;

namespace OpenInApp.Common.Helpers
{
    public class GeneralOptionsHelper
    {
        public static string GetActualPathToExe(string appFolderName, string executableFileToBrowseFor)
        {
            return GetActualPathToExe(appFolderName, null, executableFileToBrowseFor);
        }

        public static string GetActualPathToExe(string appFolderName, string appSubFolderName, string executableFileToBrowseFor)
        {
            var programFiles = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
            var programFilesFolders = programFiles.Parent.GetDirectories(programFiles.Name.Replace(" (x86)", string.Empty) + "*");

            foreach (DirectoryInfo programFilesFolder in programFilesFolders)
            {
                var appParentFolderPaths = programFilesFolder.GetDirectories(appFolderName);
                foreach (DirectoryInfo appParentFolderPath in appParentFolderPaths)
                {
                    if (string.IsNullOrEmpty(appSubFolderName))
                    {
                        var path = Path.Combine(appParentFolderPath.FullName, executableFileToBrowseFor);
                        if (File.Exists(path))
                        {
                            return path;
                        }
                    }
                    else
                    {
                        var appFolderPaths = appParentFolderPath.GetDirectories(appSubFolderName + "*");
                        foreach (DirectoryInfo appFolderPath in appFolderPaths)
                        {
                            var path = Path.Combine(appFolderPath.FullName, executableFileToBrowseFor);
                            if (File.Exists(path))
                            {
                                return path;
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
