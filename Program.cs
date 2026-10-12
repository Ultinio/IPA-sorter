using System;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace IPA_Sorter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm = new MainForm();
            Application.Run(mainForm);
        }

        static MainForm mainForm;
        static string logFilename;
        static public int filesUnprocessed;
        static public int logEntries;
        static string unprocessedPrefix;
        static public bool isTaskOn = false;

        static public string[] plistPaths;

        static Dictionary<string, List<string>> undos = null;

  
        static void DisplayMessage(string msg)
        {
            MessageBox.Show(msg, mainForm.Text);
        }


        static void LogFileStart(string filename)
        {
            using (StreamWriter w = File.AppendText(logFilename))
            {
                w.WriteLine("### " + filename);
            }
        }


        static void LogFileWarning(string message)
        {
            logEntries++;
            using (StreamWriter w = File.AppendText(logFilename))
            {
                w.WriteLine("  " + message);
            }
        }


        static string CreateDeviceList(Dictionary<string, string> dict, List<object> UIDeviceFamily)
        {
            if (UIDeviceFamily == null)
            {
                UIDeviceFamily = new List<object>();
                UIDeviceFamily.Add("1");
            }

            List<string> list = new List<string>();

            foreach (object deviceIdObj in (List<object>)UIDeviceFamily)
            {
                string keyDevice = "iDevice" + deviceIdObj.ToString();
                list.Add(dict.ContainsKey(keyDevice) ? dict[keyDevice] : keyDevice);
            }

            return string.Join("-", list.ToArray());
        }


        static object GetPlistProperty(Dictionary<string, object> plistContent, string propertyNames)
        {
            object value;
            string[] explodedProps = propertyNames.Split(',');

            foreach (string propName in explodedProps)
            {
                if (plistContent.TryGetValue(propName, out value))
                {
                    return value;
                }
                else
                {
                    LogFileWarning("Plist property not found (" + propName + ")");
                }
            }

            return null;
        }


        static string GetAvailableFilename(string filename)
        {
            string folder = Path.GetDirectoryName(filename);
            string basefile = Path.GetFileNameWithoutExtension(filename);
            string extension = Path.GetExtension(filename);
            int num = 2;
            string path = Path.Combine(folder, filename);

            while (File.Exists(path))
            {
                path = Path.Combine(folder, basefile + "_" + (num++) + extension);
            }

            return path;
        }


        static void InitialiseProcess(int nbFiles)
        {
            isTaskOn = true;

            logFilename = Path.GetFileNameWithoutExtension(System.AppDomain.CurrentDomain.FriendlyName)
                + "_" + DateTime.Now.ToString("yyyy-MM-dd_HHmmss") + ".log";
            mainForm.ProgressBarInitialize(nbFiles);
            filesUnprocessed = 0;
            logEntries = 0;

            unprocessedPrefix = mainForm.GetUnprocessedPrefix();

            InitialiseUndos();
        }


        static void FinalizeProcess()
        {
            isTaskOn = false;

            mainForm.ProgressBarFinished(filesUnprocessed, logEntries);
        }


        static bool FitsMask(string sFileName, string sFileMask)
        {
            return Regex.IsMatch(sFileName, sFileMask
                                    .Replace(".", "[.]")
                                    .Replace("*", "[^/]*")
                                    .Replace("?", "."));
        }


        static Unzip.Entry getZipFileEntry(Unzip unzip, string filepath)
        {
            foreach (Unzip.Entry entry in unzip.Entries)
            {
                if (FitsMask(entry.Name, filepath))
                {
                    return entry;
                }
            }

            return null;
        }


        static void MergeDictionaries(ref Dictionary<string, object> dictFinal, Dictionary<string, object> dictAdd)
        {
            if (dictAdd == null)
                return;
            
            if (dictFinal == null)
                dictFinal = new Dictionary<string, object>();

            foreach(var item in dictAdd)
            {
                dictFinal[item.Key] = item.Value;
            }
        }


        static string[] AnalysePattern(string pattern)
        {
            if (pattern.Length == 0)
                return null;

            string[] explodedPattern = pattern.Split('#');

            if (explodedPattern.Length % 2 == 0)
                return null;
            else
                return explodedPattern;
        }


        static void AddRegex2ToDictionary(ref Dictionary<string, string> dict, Match match, string keyPrefix)
        {
            int nbCaptures = match.Groups[1].Captures.Count;

            for (int c = 0; c < nbCaptures; c++)
            {
                string key = keyPrefix + match.Groups[1].Captures[c].Value;

                if (!dict.ContainsKey(key))
                    dict.Add(key, match.Groups[2].Captures[c].Value);
            }
        }


        static Dictionary<string, string> CreatePatternDict()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            string option;


            option = mainForm.GetPatternOption("Devices");

            // UIDeviceFamily-1:iPhone|2:iPad - can be empty
            string errorMessageDevices = "Check the Pattern Option Devices (default: UIDeviceFamily_1:iPhone|2:iPad_iP--)";
            string[] exploded = option.Split('_');

            if (exploded.Length != 3)
            {
                DisplayMessage(errorMessageDevices);
                return null;
            }

            dict["deviceProp"] = exploded[0];
            dict["deviceDefault"] = exploded[2];
            Match matchDevices = Regex.Match(exploded[1], @"(?:(\d+):(\w+)\|?)*", RegexOptions.ECMAScript);

            if (matchDevices.Success)
            {
                AddRegex2ToDictionary(ref dict, matchDevices, "iDevice");
            }
            else
            {
                DisplayMessage(errorMessageDevices);
                return null;
            }


            option = mainForm.GetPatternOption("IosX");

            // X:1|4:2 - can be empty
            Match matchIos = Regex.Match(option, @"(?:(\w+):(\d+)\|?)*", RegexOptions.ECMAScript);

            if (matchIos.Success)
            {
                AddRegex2ToDictionary(ref dict, matchIos, "ios");
                if (!dict.ContainsKey("iosX"))
                    dict.Add("iosX", "1");
            }
            else
            {
                DisplayMessage("Check the Pattern Option Ios (default: X:1|4:2)");
                return null;
            }

            dict["cracker"] = mainForm.GetPatternOption("Cracker");

            return dict;
        }


        static string TranslateKey(string key)
        {
            string v = ConfigurationManager.AppSettings["replace" + key];
            return v == null ? key : v;
        }


        static string ReplacePattern(Dictionary<string, object> plistDict, string[] explodedPattern, Dictionary<string, string> patternDict)
        {
            int i = 0;
            string builtName = "";

            foreach (string patternElement in explodedPattern)
            {
                string textToAdd = patternElement;
                if (i++ % 2 == 1)
                {
                    string[] explodedElement = patternElement.Split(':');
                    string realPatternElement = explodedElement[0];

                    object propValue = null;
                    switch (realPatternElement)
                    {
                        case "_DEVICES_":
                            propValue = GetPlistProperty(plistDict, patternDict["deviceProp"]);
                            if (propValue == null)
                            {
                                textToAdd = patternDict["deviceDefault"];
                            }
                            else
                            {
                                textToAdd = CreateDeviceList(patternDict, (List<object>)propValue);
                            }
                            break;

                        case "_CRACKER_":
                            propValue = GetPlistProperty(plistDict, TranslateKey(realPatternElement));
                            if (propValue == null)
                            {
                                textToAdd = patternDict["cracker"];
                            }
                            else
                            {
                                string appleId = propValue.ToString();
                                int posAt = appleId.LastIndexOf('@');
                                textToAdd = posAt == -1 ? appleId : appleId.Substring(0, posAt);
                            }
                            break;

                        case "_IOSX_":
                            propValue = GetPlistProperty(plistDict, TranslateKey(realPatternElement));
                            if (propValue == null)
                            {
                                return null;
                            }
                            else
                            {
                                string MinimumOSVersion = propValue.ToString();
                                string[] parts = MinimumOSVersion.Split('.');

                                string ios = parts[0];
                                string realKey = "ios" + (patternDict.ContainsKey("ios" + ios) ? ios : "X");
                                int significant = Math.Min(int.Parse(patternDict[realKey]), parts.Length);

                                textToAdd = ios;
                                for (int j = 1; j < significant; j++)
                                    textToAdd += "." + parts[j];
                            }
                            break;

                        default:
                            propValue = GetPlistProperty(plistDict, TranslateKey(realPatternElement));
                            if(propValue == null)
                                return null;
                            else
                                textToAdd = propValue.ToString();

                            break;
                    }

                    if (explodedElement.Length > 1)
                    {
                        int maxLength;
                        if (int.TryParse(explodedElement[1], out maxLength))
                        {
                            if(maxLength < textToAdd.Length)
                                textToAdd = textToAdd.Substring(0, maxLength);
                        }
                    }
                }

                builtName += textToAdd;
            }

            return builtName;
        }


        static public void RunExtract(string[] files, string folderOut, string[] suffixesExploded)
        {
            InitialiseProcess(files.Length);
            
            foreach (string filename in files)
            {
                if (!isTaskOn)
                    break;

                mainForm.ProgressBarStep(filename);

                LogFileStart(filename);

                string newFolderName = folderOut == "" ? Path.GetDirectoryName(filename) : folderOut;
                string newFilename = Path.GetFileNameWithoutExtension(filename);

                string rootOutput = Path.Combine(newFolderName, newFilename);

                try
                {
                    using (Unzip unzip = new Unzip(filename))
                    {
                        if (unzip.Entries == null)
                        {
                            LogFileWarning("Corrupted IPA");
                            continue;
                        }

                        int i = 0;
                        foreach (string plistPath in plistPaths)
                        {
                            Unzip.Entry entry = getZipFileEntry(unzip, plistPath);
                            if (entry == null)
                            {
                                LogFileWarning("plist not found (" + plistPath + ")");
                                continue;
                            }

                            string filenamePlist = rootOutput + suffixesExploded[i++] + ".plist";
                            unzip.Extract(entry, filenamePlist);
                            AddUndo("plist", filenamePlist);
                        }
                    }
                }

                catch (Exception e)
                {
                    filesUnprocessed++;
                    newFilename = unprocessedPrefix + newFilename;

                    LogFileWarning(e.Message);
                }
            }

            FinalizeProcess();
        }


        static public void RunRenameSort(string[] files, string folderOut, string fileExtension, bool useExternalPlist,
            string[] suffixesExploded, bool willRename, string renamePattern, bool willSort, string sortPattern)
        {
            Dictionary<string, string> patternDict = CreatePatternDict();
            if (patternDict == null)
                return;


            string[] renamePatternExploded = null;
            if (willRename)
            {
                renamePatternExploded = AnalysePattern(renamePattern);
                if (renamePatternExploded == null)
                {
                    DisplayMessage("Check Pattern for Task Renaming");
                    return;
                }
            }

            string[] sortPatternExploded = null;
            if (willSort)
            {
                sortPatternExploded = AnalysePattern(sortPattern);
                if (sortPatternExploded == null)
                {
                    DisplayMessage("Check Pattern for Task Sorting");
                    return;
                }
            }
            

            InitialiseProcess(files.Length);


            foreach (string filename in files)
            {
                if (!isTaskOn)
                    break;

                mainForm.ProgressBarStep(filename);

                LogFileStart(filename);

                string newFolderName = folderOut == "" ? Path.GetDirectoryName(filename) : folderOut;
                string newFilename = Path.GetFileNameWithoutExtension(filename);

                Dictionary<string, object> mergedDict = null;


                // needs Plist infos
                if (useExternalPlist)
                {
                    List<Dictionary<string, object>> dicts = new List<Dictionary<string, object>>();

                    foreach (string suffix in suffixesExploded)
                    {
                        string rootFilename = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename));

                        string filenamePlist = rootFilename + suffix + ".plist";

                        if (File.Exists(filenamePlist))
                        {
                            MergeDictionaries(ref mergedDict, (Dictionary<string, object>)Plist.readPlist(filenamePlist));
                        }
                    }
                }
                else
                {
                    try
                    {
                        using (Unzip unzip = new Unzip(filename))
                        {
                            if (unzip.Entries == null)
                            {
                                LogFileWarning("Corrupted IPA");
                                continue;
                            }

                            foreach (string plistPath in plistPaths)
                            {
                                Unzip.Entry entry = getZipFileEntry(unzip, plistPath);
                                if (entry == null)
                                {
                                    LogFileWarning("plist not found (" + plistPath + ")");
                                    continue;
                                }

                                using (MemoryStream ms = new MemoryStream())
                                {
                                    unzip.Extract(entry, ms);
                                    ms.Position = 0;

                                    Dictionary<string, object> dict = (Dictionary<string, object>)Plist.readPlist(ms, plistType.Auto);
                                    if (dict == null)
                                    {
                                        LogFileWarning("Error reading plist (" + plistPath + ")");
                                    }
                                    else
                                    {
                                        MergeDictionaries(ref mergedDict, dict);
                                    }
                                }
                            }
                        }
                    }

                    catch (Exception e)
                    {
                        filesUnprocessed++;
                        newFilename = unprocessedPrefix + newFilename;

                        LogFileWarning(e.Message);
                    }
                }

                // TASK RENAME
                if (willRename && mergedDict!=null)
                {
                    string builtName = ReplacePattern(mergedDict, renamePatternExploded, patternDict);
                    if (builtName == null)
                        throw new Exception("There was a problem building new file name");
                    else
                        newFilename = builtName;
                }

                // TASK SORT
                if (willSort && mergedDict != null)
                {
                    string builtFolderName = ReplacePattern(mergedDict, sortPatternExploded, patternDict);
                    if (builtFolderName == null)
                        throw new Exception("There was a problem building new folder name");
                    else
                    {
                        newFolderName += "\\" + builtFolderName;
                        if (!Directory.Exists(newFolderName))
                        {
                            Directory.CreateDirectory(newFolderName);

                            AddUndo("dirs", newFolderName);
                        }
                    }
                }

                // MOVING OR RENAMING FILE
                string filenameOut = Path.Combine(newFolderName, newFilename + "." + fileExtension);
                if (filenameOut != filename)
                {
                    filenameOut = GetAvailableFilename(filenameOut);
                    File.Move(filename, filenameOut);

                    AddUndo("moves", filename + "?" + filenameOut);
                }
            }

            FinalizeProcess();
        }


        static void InitialiseUndos()
        {
            undos = new Dictionary<string, List<string>>()
            {
                {"plist", new List<string>()},
                {"moves", new List<string>()},
                {"dirs", new List<string>()}
            };
        }


        static void AddUndo(string type, string value)
        {
            undos[type].Add(value);
        }


        static public int Undo()
        {
            int nbActions = 0;

            foreach(string plistFile in undos["plist"])
            {
                nbActions++;
                if (File.Exists(plistFile))
                    File.Delete(plistFile);
            }

            foreach (string doubleFilename in undos["moves"])
            {
                nbActions++;
                string[] exploded = doubleFilename.Split('?');
                if (File.Exists(exploded[1]))
                    File.Move(exploded[1], exploded[0]);
            }

            foreach (string dirName in undos["dirs"])
            {
                nbActions++;
                if (Directory.Exists(dirName))
                    Directory.Delete(dirName);
            }

            return nbActions;
        }

    }
}
