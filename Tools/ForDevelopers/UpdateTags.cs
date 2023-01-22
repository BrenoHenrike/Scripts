/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
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
        string scriptDir = CoreBots.ScriptsPath;
        bool userExit = false;

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
                if (Extensions.Any(e => file.EndsWith(e)))
                    continue;

                string _file = removeDir(file)!.Replace('\\', '/');
                // Skip blacklisted files
                if (Files.Any(f => f.ToLower().Replace('\\', '/') == _file.ToLower()))
                    continue;

                // Reading file
                List<string> fileData = File.ReadAllLines(file).ToList();
                // Starting on writing the new data for the file
                List<string> newData = new() { "/*" };

                List<string> scriptData = new();
                bool hasLogged = false;
                bool isCore = _file.StartsWith("Core");

                handleProp("name");
                handleProp("description");

                // If tags are already made, use it
                if (!isCore && hasProperty(fileData, "tags", out string tags))
                {
                    string[] _tags = tags.Split(',', StringSplitOptions.TrimEntries);
                    for (int i = 0; i < _tags.Length; i++)
                        _tags[i] = _tags[i] == _tags[i].ToUpper() ? _tags[i] : _tags[i].ToLower();
                    newData.Add("tags: " + String.Join(", ", _tags));
                }
                // If the user has exited, write null
                else if (userExit || isCore)
                {
                    newData.Add("tags: null");
                }
                // Otherwise, ask for the tags
                else
                {
                    logOnce();
                    addProperty(_file, "tags", scriptData, ref newData);
                }

                // Adding the comment closing tag
                newData.Add("*/");
                // Making the dataset that is to be writen
                List<string> toWrite = fileData.SkipWhile(l => !l.StartsWith("//cs_include") && !l.StartsWith("using")).ToList();
                toWrite.InsertRange(0, newData);
                // Overwriting the new file
                File.WriteAllLines(file, toWrite);

                void logOnce()
                {
                    if (hasLogged)
                        return;

                    Bot.Log($"[{DateTime.Now:HH:mm:ss}] ({_path})  {_file.Split('/').Last()}");
                    hasLogged = true;

                    fileData.Where(l =>
                        (l.Trim().StartsWith("public") ||
                         l.Trim().StartsWith("private")) &&
                        (l.Trim().EndsWith(")") ||
                         l.Contains("class")) &&
                        !l.Contains("ScriptMain") &&
                        !l.Contains("new"))
                        .ToList()
                        .ForEach(l =>
                            scriptData.Add(
                                new string(
                                    l.Trim()
                                    .TakeWhile(c => c != '(')
                                    .ToArray()
                    )));
                }
                void handleProp(string prop)
                {
                    // If prop is already made, use it
                    if (!isCore && hasProperty(fileData, prop, out string _prop))
                        newData.Add($"{prop}: {_prop.Replace("  ", " ")}");
                    // If the user has exited, write null
                    else if (userExit || isCore)
                    {
                        newData.Add(prop + ": null");
                    }
                    // Otherwise, ask for the prop
                    else
                    {
                        logOnce();
                        addProperty(_file, prop, scriptData, ref newData);
                    }
                }
            }

            // Go over every directory within the directory
            foreach (var dir in dirs)
            {
                // Skip blacklisted directories
                if (Directories.Any(d => Path.Combine(scriptDir, d).Replace('\\', '/') == dir.Replace('\\', '/')))
                    continue;

                // Incurisve file check
                _UpdateTags(dir);
            }

            bool addProperty(string file, string prop, List<string> data, ref List<string> list)
            {
                bool tags = prop == "tags";
                InputDialogViewModel diag = new(
                    "Script " + Char.ToUpper(prop[0]) + prop.Substring(1),
                    $"[ {file} ]\n" +
                    $"Please provide an acurate {prop} of this script\n\n" +
                    "Methods and Classes inside file:\n·  " + String.Join("\n·  ", data),
                    tags ? "Don't forget to use , [comma] as a divider when adding multiple tags." : String.Empty,
                    false
                );
                if (Ioc.Default.GetRequiredService<IDialogService>().ShowDialog(diag) != true)
                {
                    userExit = true;
                    Bot.Log($"[{DateTime.Now:HH:mm:ss}] (UpdateTags)  You have exited the tool, please wait a moment whilst it wraps things up.");
                    list.Add(prop + ": null");
                    return false;
                }
                string[]? _tags = null;
                if (tags)
                {
                    _tags = diag.DialogTextInput.Split(',', StringSplitOptions.TrimEntries);
                    for (int i = 0; i < _tags.Length; i++)
                        _tags[i] = _tags[i] == _tags[i].ToUpper() ? _tags[i] : _tags[i].ToLower();
                }
                list.Add(prop + ": " + (tags ? String.Join(", ", _tags!) : diag.DialogTextInput));
                return true;
            }
        }

        string? removeDir(string path)
        {
            string? toReturn = path.Replace(scriptDir, "");
            return toReturn.Count() > 0 ? toReturn[1..] : null;
        }

        bool hasProperty(List<string> file, string prop, out string propData)
        {
            var _file = file.TakeWhile(l => l != "*/");
            if (_file.Any(l => l.StartsWith(prop.ToLower()) && l.Contains(':') && !l.TrimEnd().EndsWith("null")))
            {
                propData = _file.First(l => l.StartsWith(prop.ToLower()) && l.Contains(':')).Split(':').Last();
                return true;
            }
            propData = String.Empty;
            return false;
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
        "Army/CoreArmy.cs"
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
        "Army/UltraBosses"
    };
    #endregion
}
