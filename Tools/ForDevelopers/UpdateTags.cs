/*
name: Update Script Data
description: This bot will check all bots so that you may add the missing Name, Description and Tags where needed
tags: tags, description, name, developer, data
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
using Skua.Core.Models;
using CommunityToolkit.Mvvm.DependencyInjection;

public class UpdateTags
{
    private IScriptInterface Bot = IScriptInterface.Instance;
    private CoreBots Core = CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Update();
    }

#nullable enable
    private void Update()
    {
        // Variables
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
                string? customFolder = Ioc.Default.GetRequiredService<IFileDialogService>().OpenFolder(ClientFileSources.SkuaScriptsDIR);

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
                _UpdateTags(ClientFileSources.SkuaScriptsDIR);
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

            Bot.Log($"[{DateTime.Now:HH:mm:ss}] ({_path})  {(dirs.Length > 0 ? $"{dirs.Length} Director{(dirs.Length == 1 ? "y" : "ies")} & " : "")}{files.Length} File{(files.Length == 1 ? "" : "s")}");

            // Go over every file in the directory
            foreach (var file in files)
            {
                // Skip blacklisted file-extensions and core-files
                if (Extensions.Any(e => file.EndsWith(e)) || file.Replace('\\', '/').Split('/').Last().StartsWith("Core"))
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
                handleProp("tags");

                // Adding the comment closing tag
                newData.Add("*/");
                // Making the dataset that is to be writen
                List<string> toWrite = fileData.SkipWhile(l => !l.StartsWith("//cs_include") && !l.StartsWith("using")).ToList();
                toWrite.InsertRange(0, newData);
                // Overwriting the new file
                Core.WriteFile(file, toWrite);

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
                    if (!isCore && hasProperty(file, fileData, prop, out string _prop))
                    {
                        if (prop == "tags")
                        {
                            string[] _tags = _prop.Split(',', StringSplitOptions.TrimEntries);
                            for (int i = 0; i < _tags.Length; i++)
                                _tags[i] = _tags[i] == _tags[i].ToUpper() ? _tags[i] : _tags[i].ToLower();
                            newData.Add("tags: " + string.Join(", ", _tags));
                        }
                        else newData.Add($"{prop}: {_prop.Replace("  ", " ").Trim()}");
                    }
                    // If the user has exited, write null
                    else if (userExit || isCore)
                    {
                        newData.Add($"{prop}: {(prop == "name" ? file.Replace('\\', '/').Split('/').Last().Replace(".cs", "") : "null")}");
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
                if (Directories.Any(d => Path.Combine(ClientFileSources.SkuaScriptsDIR, d).Replace('\\', '/') == dir.Replace('\\', '/')))
                    continue;

                // Incurisve file check
                _UpdateTags(dir);
            }

            bool addProperty(string file, string prop, List<string> data, ref List<string> list)
            {
                bool tags = prop == "tags";
                InputDialogViewModel diag = new(
                    "Script " + char.ToUpper(prop[0]) + prop.Substring(1),
                    $"[ {file} ]\n" +
                    $"Please provide an acurate {prop} of this script\n\n" +
                    "Methods and Classes inside file:\n·  " + string.Join("\n·  ", data),
                    tags ? "Don't forget to use , [comma] as a divider when adding multiple tags." : string.Empty,
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
                list.Add(prop + ": " + (tags ? string.Join(", ", _tags!) : diag.DialogTextInput));
                return true;
            }
        }

        string? removeDir(string path)
        {
            string? toReturn = path.Replace(ClientFileSources.SkuaScriptsDIR, "");
            return toReturn.Length > 0 ? toReturn[1..] : null;
        }

        bool hasProperty(string file, List<string> fileData, string prop, out string propData)
        {
            var _fileData = fileData.TakeWhile(l => l != "*/");
            if (_fileData.Any(l => l.StartsWith(prop.ToLower()) &&
                l.Contains(':') &&
                !l.TrimEnd().EndsWith("null") &&
                l != "name: " + file.Replace('\\', '/').Split('/').Last().Replace(".cs", "") &&
                !string.IsNullOrWhiteSpace(l.Split(':').Last()) &&
                !string.IsNullOrEmpty(l.Split(':').Last())
                ))
            {
                propData = _fileData.First(l => l.StartsWith(prop.ToLower()) && l.Contains(':')).Split(prop.ToLower() + ':').Last();
                return true;
            }
            propData = string.Empty;
            return false;
        }
    }

    #region BlackLists
    private string[] Extensions =
    {
        ".yaml",
        ".txt",
        ".csproj",
        ".md",
        ".file",
        ".json",
        ".sln",
        ".gitignore",
        "gitattributes"
    };
    private string[] Files =
    {
        "Class1.cs",
        "z_CompiledScript.cs",
        "Army/CoreArmy.cs"
    };
    private string[] Directories =
    {
        ".git",
        ".github",
        ".shacache",
        ".vscode",
        "bin",
        "docs",
        "Generated",
        "obj",
        "plugins",
        "Skills",
        "SkuaScriptsGenerator",
        "Templates",
        "WIP",
        "Army/UltraBosses",
        "Army/Generated",
        "Prototypes",
        "SkuaScriptsGenerator",
        "Plugins",
        "WIP",
        "Tools/ForDevelopers",
        "Tools/NooneAskeforThese"
    };


    #endregion
}
