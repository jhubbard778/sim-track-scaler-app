using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shell32;

namespace MX_Simulator_Track_Scaler
{
    internal class DirectoryInfo
    {
        public static string trackFolderPath;
        public static string originalFilesDirectory;
        public static string previousFilesDirectory;
        public static string directoryToMoveFilesTo;

        public static string mxInstallDirectory = FindMXInstallDirectory();
        public static string mxAppdataDirectory = GetMXAppdataDirectory();

        // Does the user have a track folder selected
        public static bool isTrackFolderSelected = false;

        // Turns to true once and only if there is a directory that exists called "original files" and files have been moved there
        public static bool originalFilesCreated = true;

        public static bool trackFolderInInstallDirectory = false;
        public static bool trackFolderInAppdataDirectory = false;

        public static void SetFileDirectories(string path)
        {
            trackFolderPath = path;
            originalFilesDirectory = trackFolderPath + "\\original files";
            previousFilesDirectory = trackFolderPath + "\\previous files";

            trackFolderInAppdataDirectory = path.StartsWith(mxAppdataDirectory);
            trackFolderInInstallDirectory = path.StartsWith(mxInstallDirectory);

            isTrackFolderSelected = true;
        }

        public static void SetupFileDirectories()
        {
            if (!Directory.Exists(originalFilesDirectory))
            {
                Directory.CreateDirectory(originalFilesDirectory);
                originalFilesCreated = false;
            }
            else if (!Directory.Exists(previousFilesDirectory))
            {
                Directory.CreateDirectory(previousFilesDirectory);
            }
        }

        public static void SetDirectoryToMoveFilesTo()
        {
            directoryToMoveFilesTo = !originalFilesCreated ? originalFilesDirectory : previousFilesDirectory;
        }

        private static string GetMXAppdataDirectory()
        {
            string appdataDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MX Simulator");
            return Directory.Exists(appdataDirectory) ? appdataDirectory : null;
        }

        private static string FindMXInstallDirectory()
        {
            string[] potentialShortcutLocations = new string[]
            {
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar"),
                Environment.GetFolderPath(Environment.SpecialFolder.StartMenu)
            };

            Shell shell = new Shell();
            foreach (string location in potentialShortcutLocations)
            {
                if (!Directory.Exists(location)) continue;

                Folder folder = shell.NameSpace(location);
                string path = FindMXExeFromFolder(folder, shell);

                if (path != null) return path;
            }

            return null;
        }

        private static string FindMXExeFromFolder(Folder folder, Shell shell)
        {
            foreach (FolderItem item in folder.Items())
            {
                if (!item.IsLink) continue;

                ShellLinkObject lnk = (ShellLinkObject)item.GetLink;

                string targetPath = lnk.Target.Path;
                string arguments = lnk.Arguments;

                while (targetPath.EndsWith(".lnk") && !string.IsNullOrEmpty(arguments))
                {
                    ShellLinkObject linkedLnk = (ShellLinkObject)shell.NameSpace(targetPath).Items().Item().GetLink;
                    targetPath = linkedLnk.Target.Path;
                    arguments = linkedLnk.Arguments;
                }

                string finalTarget = targetPath;
                if (finalTarget.EndsWith("\\mx.exe", StringComparison.OrdinalIgnoreCase))
                {
                    string substr = finalTarget.Substring(0, finalTarget.IndexOf("\\mx.exe"));
                    return substr;
                }
            }

            return null;
        }
    }
}
