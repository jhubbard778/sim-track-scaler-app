using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MX_Simulator_Track_Scaler
{
    internal class PythonProcess
    {
        public static string pythonPath = GetPythonPath();
        public static bool pythonPathExists = pythonPath != "";

        public static bool RunPythonProcess(string scriptPath, List<string> args, TrackScalerForm ScalerForm)
        {
            if (!File.Exists(scriptPath))
            {
                UIHelper.ChangeLabel(ScalerForm.FormProgressBarLabel, Color.Red, $"Could not find script python path: {scriptPath}");
                return false;
            }

            string pythonArgs = $"\"{scriptPath}\"";
            foreach (string arg in args)
            {
                pythonArgs += $" \"{arg}\"";
            }

            // Build the process start info
            var processStartInfo = new ProcessStartInfo
            {
                FileName = pythonPath,
                Arguments = pythonArgs,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            try
            {
                // Start the Python process
                using (var process = new Process { StartInfo = processStartInfo })
                {
                    process.Start();

                    // Read output and errors (if any)
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        UIHelper.ChangeLabel(ScalerForm.FormProgressBarLabel, Color.Red, $"Unknown Error Occurred Mirroring Images");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                UIHelper.ChangeLabel(ScalerForm.FormProgressBarLabel, Color.Red, $"Error Mirroring Images: {ex.Message}");
                return false;
            }

            return true;
        }

        // https://stackoverflow.com/questions/41920032/automatically-find-the-path-of-the-python-executable
        private static string GetPythonPath(string requiredVersion = "", string maxVersion = "")
        {
            string[] possiblePythonLocations = new string[3]
            {
                @"HKLM\SOFTWARE\Python\PythonCore\",
                @"HKCU\SOFTWARE\Python\PythonCore\",
                @"HKLM\SOFTWARE\Wow6432Node\Python\PythonCore\"
            };

            //Version number, install path
            Dictionary<string, string> pythonLocations = new Dictionary<string, string>();

            foreach (string possibleLocation in possiblePythonLocations)
            {
                string regKey = possibleLocation.Substring(0, 4),
                       actualPath = possibleLocation.Substring(5);
                RegistryKey theKey = regKey == "HKLM" ? Registry.LocalMachine : Registry.CurrentUser;
                RegistryKey theValue = theKey.OpenSubKey(actualPath);

                foreach (var v in theValue.GetSubKeyNames())
                    if (theValue.OpenSubKey(v) is RegistryKey productKey)
                        try
                        {
                            string pythonExePath = productKey.OpenSubKey("InstallPath").GetValue("ExecutablePath").ToString();

                            if (pythonExePath != null && pythonExePath != "")
                            {
                                pythonLocations.Add(v.ToString(), pythonExePath);
                            }
                        }
                        catch
                        {
                            //Install path doesn't exist
                        }
            }

            if (pythonLocations.Count > 0)
            {
                System.Version desiredVersion = new Version(requiredVersion == "" ? "0.0.1" : requiredVersion);
                System.Version maxPVersion = new Version(maxVersion == "" ? "999.999.999" : maxVersion);

                string highestVersion = "", highestVersionPath = "";

                foreach (KeyValuePair<string, string> pVersion in pythonLocations)
                {
                    //TODO; if on 64-bit machine, prefer the 64 bit version over 32 and vice versa
                    int index = pVersion.Key.IndexOf("-"); //For x-32 and x-64 in version numbers
                    string formattedVersion = index > 0 ? pVersion.Key.Substring(0, index) : pVersion.Key;

                    System.Version thisVersion = new System.Version(formattedVersion);
                    int comparison = desiredVersion.CompareTo(thisVersion),
                        maxComparison = maxPVersion.CompareTo(thisVersion);

                    if (comparison <= 0)
                    {
                        //Version is greater or equal
                        if (maxComparison >= 0)
                        {
                            desiredVersion = thisVersion;

                            highestVersion = pVersion.Key;
                            highestVersionPath = pVersion.Value;
                        }
                    }
                }
                return highestVersionPath;
            }

            return "";
        }
    }
}
