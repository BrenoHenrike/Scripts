using System.Dynamic;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using Skua.Core.Interfaces;
using Skua.Core.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

public class UpdateTags
{
    private IScriptInterface Bot = IScriptInterface.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Update();
    }

#nullable enable
    private void Update()
    {
        // Variables
        string scriptDir = Path.Combine(AppContext.BaseDirectory, "Scripts");
        string filePath = Path.Combine(scriptDir, "Tools", "ForDevelopers", "ScriptTags.json");
        bool shouldReturn = false;
        bool selectedFolder = false;

        // File lists
        dynamic[]? currentFile = File.Exists(filePath) ? JsonConvert.DeserializeObject<dynamic[]>(File.ReadAllText(filePath)) : null;
        List<dynamic> newFile = new();
        // Sometimes stuff is still in memory
        newFile.Clear();

        Bot.Log($"[{DateTime.Now:HH:mm:ss}] (UpdateTags)  Current ScriptTags.json holds < {currentFile?.Count()} > scripts.");

        // Allowing the user to select a folder they wish to focus on
        switch (Bot.ShowMessageBox(
            "Do you wish to select a folder to work in, or just start adding Tags and Descriptions where needed? (Auto Mode)",
            "Select mode",
             "Auto Mode", "Select Folder", "Cancel"
            ).Text)
        {
            case "Select Folder":
                // Folder selecting
                string? customFolder = Ioc.Default.GetRequiredService<IFileDialogService>().OpenFolder(scriptDir);

                if (customFolder == null)
                {
                    Bot.Log($"[{DateTime.Now:HH:mm:ss}] (UpdateTags)  No folder was selected, stopping the script.");
                    return;
                }

                selectedFolder = true;
                // Incursive function starts in the selected folder
                _UpdateTags(customFolder);
                break;

            case "Auto Mode":
                // Incursive function starts in the base Scripts Directory
                _UpdateTags(scriptDir);
                break;

            default:
                Bot.Log($"[{DateTime.Now:HH:mm:ss}] (UpdateTags)  No mode was selected, stopping the script.");
                return;
        }

        Bot.Log($"[{DateTime.Now:HH:mm:ss}] (UpdateTags)  You added < {newFile.Count()} > scripts to the ScriptTags.json");

        // In case the user ends the program early, or the user selected a custom folder, make sure to add the old data to the new file
        if ((shouldReturn || selectedFolder) && currentFile != null)
        {
            foreach (var d in currentFile)
            {
                if (!newFile.Any(v => JsonConvert.DeserializeObject(JsonConvert.SerializeObject(v))!.ToString()!.Replace("\\\\", "/") == d.ToString()))
                    newFile.Add(d);
            }
        }

        // Write all the data to the file
        File.WriteAllText(
            filePath,
            JsonConvert.DeserializeObject(
                JsonConvert.SerializeObject(newFile))!
                .ToString()!
                .Replace("\\\\", "/")
                .Replace("\"\",", "")
                .Replace("\"\"", ""));

        Bot.Log($"[{DateTime.Now:HH:mm:ss}] (UpdateTags)  ScriptTags.json now holds < {newFile.Count()} > scripts.");

        void _UpdateTags(string path)
        {
            // Gathering data for foreach and loggers
            string[] files = Directory.GetFiles(path);
            string[] dirs = Directory.GetDirectories(path);
            string _path = removeDir(path) ?? "Scripts";

            Bot.Log($"[{DateTime.Now:HH:mm:ss}] ({_path})  {(dirs.Count() > 0 ? $"{dirs.Count()} Director{(dirs.Count() == 1 ? "y" : "ies")} & " : "")}{files.Count()} File{(files.Count() == 1 ? "" : "s")}");

            // Go over every file in the directory
            foreach (var file in files)
            {
                // Skip blacklisted file-extensions and core-files
                if (Extensions.Any(e => file.EndsWith(e)) || file.Split('\\').Last().StartsWith("Core"))
                    continue;

                string _file = removeDir(file)!;
                // Skip blacklisted files
                if (Files.Any(f => f.ToLower() == _file.ToLower()))
                    continue;

                dynamic newItem = new ExpandoObject();
                // If tags are already made, use those
                if (currentFile != null && currentFile.Any(d => d.path == _file.Replace('\\', '/')))
                {
                    newItem = currentFile.First(d => d.path == _file.Replace('\\', '/'));
                }
                else
                {
                    Bot.Log($"[{DateTime.Now:HH:mm:ss}] ({_path})  {_file.Split('\\').Last()}");

                    // Filter and clean lines for the prompt
                    List<string> data = new();
                    File.ReadAllLines(file)
                                        .Where(l =>
                                            (l.Trim().StartsWith("public") ||
                                             l.Trim().StartsWith("private")) &&
                                            (l.Trim().EndsWith(")") ||
                                             l.Contains("class")) &&
                                            !l.Contains("ScriptMain") &&
                                            !l.Contains("new"))
                                        .ToList()
                                        .ForEach(l =>
                                            data.Add(
                                                new string(
                                                    l.Trim()
                                                     .TakeWhile(c => c != '(')
                                                     .ToArray()
                                        )));

                    // Script Description prompt
                    InputDialogViewModel desc = new(
                        "Script Description",
                        $"[ {_file} ]\n" +
                        "Please provide an acurate description of this script\n\n" +
                        "Methods and Classes inside file:\n·  " + String.Join("\n·  ", data),
                        false
                    );
                    if (Ioc.Default.GetRequiredService<IDialogService>().ShowDialog(desc) != true)
                    {
                        shouldReturn = true;
                        return;
                    }

                    // Script Tags prompt
                    InputDialogViewModel tags = new(
                        "Script Tags",
                        $"[ {_file} ]\n" +
                        "Please provide search-tags of this script\n\n" +
                        "Methods and Classes inside file:\n·  " + String.Join("\n·  ", data),
                        "Don't forget to use , [comma] as a divider when adding multiple tags.",
                        false
                    );
                    if (Ioc.Default.GetRequiredService<IDialogService>().ShowDialog(tags) != true)
                    {
                        shouldReturn = true;
                        return;
                    }

                    // Assigning data
                    newItem.path = _file;
                    newItem.description = desc.DialogTextInput;
                    newItem.tags = tags.DialogTextInput.Split(',', StringSplitOptions.TrimEntries);
                }
                // Adding to the database
                newFile.Add(newItem);
            }

            // Go over every directory within the directory
            foreach (var dir in dirs)
            {
                // Skip blacklisted directories
                if (Directories.Any(d => Path.Combine(scriptDir, d) == dir))
                    continue;

                // Incurisve file check
                _UpdateTags(dir);
                if (shouldReturn)
                    return;
            }
        }

        string? removeDir(string path)
        {
            string? toReturn = path.Replace(scriptDir, "");
            return toReturn.Count() > 0 ? toReturn[1..] : null;
        }
    }

    #region BlackLists
    private string[] Extensions =
    {
        ".txt",
        ".csproj",
        ".md",
        ".file",
        ".json",
    };
    private string[] Files =
    {
        ".gitignore",
        ".gitattributes",
        "Class1.cs",
        "z_CompiledScript.cs",
    };
    private string[] Directories =
    {
        ".git",
        ".shacache",
        "bin",
        "docs",
        "Generated",
        "obj",
        "plugins",
        "Skills",
        "WIP",
        "Army\\UltraBosses"
    };
    #endregion
}
