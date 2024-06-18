/*
name: Script Main Cleaner
description: Checks all scripts we have to see if there are any that break format
tags: scriptmain, cleaner, formatter, developer
*/
//cs_include Scripts/CoreBots.cs
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using Skua.Core.Interfaces;
using Skua.Core.Models;

public class ScriptMainCleaner
{
    private IScriptInterface Bot = IScriptInterface.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Cleaner();
    }
 
#nullable enable
    private void Cleaner()
    {
        List<string> GotNull = new();
        List<string> NoSetOptions = new();
        List<string> TooManyInScriptMain = new();

        int fileCount = 0;
        int dirCount = 0;
        int lineCount = 0;
        readFiles(ClientFileSources.SkuaScriptsDIR);

        Bot.Log("---------------");
        Bot.Log("Directory Count:\t" + dirCount);
        Bot.Log("File Count:\t" + fileCount);
        Bot.Log("Line Count:\t" + lineCount);
        if (GotNull.Count > 0)
        {
            Bot.Log("---------------");
            Bot.Log($"Scriptreader got NULL [{GotNull.Count}]");
            GotNull.ForEach(x => Bot.Log("    " + x));
        }
        if (NoSetOptions.Count > 0)
        {
            Bot.Log("---------------");
            Bot.Log($"No Core.SetOptions [{NoSetOptions.Count}]");
            NoSetOptions.ForEach(x => Bot.Log("    " + x));
        }
        if (TooManyInScriptMain.Count > 0)
        {
            Bot.Log("---------------");
            Bot.Log($"Too many items in ScriptMain [{TooManyInScriptMain.Count}]");
            TooManyInScriptMain.ForEach(x => Bot.Log("    " + x));
        }

        void readFiles(string path)
        {
            string[] files = Directory.GetFiles(path);
            string[] dirs = Directory.GetDirectories(path);
            Bot.Log($"[{DateTime.Now:HH:mm:ss}] ({removeDir(path) ?? "Scripts"})  {(dirs.Length > 0 ? $"{dirs.Length} Director{(dirs.Length == 1 ? "y" : "ies")} & " : "")}{files.Length} File{(files.Length == 1 ? "" : "s")}");
            foreach (var file in files)
            {
                if (Extensions.Any(e => file.EndsWith(e)))
                    continue;

                string _file = removeDir(file)!;
                if (Files.Any(f => f.ToLower() == _file.ToLower()))
                    continue;

                string[]? __file = File.ReadAllLines(file);
                lineCount = lineCount + __file.Length;
                List<string>? inner = __file
                                .SkipWhile(l => !l.StartsWith("    public void ScriptMain"))
                                .TakeWhile(l => !l.StartsWith("    }"))
                                .ToArray()[2..].ToList();
                __file = null;

                if (inner == null)
                {
                    GotNull.Add(_file);
                    fileCount++;
                    continue;
                }
                //else foreach (string line in inner)
                //        Bot.Log(line);

                if (!inner.Any(l => l.StartsWith("        Core.SetOptions(")) &&
                    !_file.ToLower().Contains("core") &&
                    !NoSetOptionsAllowed.Any(o => o == _file))
                {
                    NoSetOptions.Add(_file);
                    fileCount++;
                    continue;
                }

                inner = inner.Where(l =>
                        !l.Contains("Core.SetOptions(") &&
                        !string.IsNullOrEmpty(l) &&
                        !string.IsNullOrWhiteSpace(l) &&
                        !l.Contains("Core.BankingBlackList") &&
                        !l.Trim().StartsWith("//") &&
                        !l.Trim().StartsWith('"')
                    ).ToList();

                if (inner.Count > 1)
                {
                    TooManyInScriptMain.Add(_file);
                    Bot.Log("---------------");
                    Bot.Log(_file);
                    inner.ForEach(x => Bot.Log(x));
                    fileCount++;
                    continue;
                }

                fileCount++;
            }

            foreach (var dir in dirs)
            {
                if (Directories.Any(d => Path.Combine(ClientFileSources.SkuaScriptsDIR, d) == dir))
                    continue;

                dirCount++;
                readFiles(dir);
            }
        }

        string? removeDir(string path)
        {
            string? toReturn = path.Replace(ClientFileSources.SkuaScriptsDIR, "");
            return toReturn.Length > 0 ? toReturn[1..] : null;
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
    };
    private string[] Files =
    {
        #region Basics
        ".gitignore",
        ".gitattributes",
        "Class1.cs",
        "z_CompiledScript.cs",
        #endregion
        #region Exceptions
        @"Tools\Butler.cs",
        @"Tools\ChooseBestGear",
        @"Evil\NSoD\0NecroticSwordOfDoom.cs",
        @"Evil\NSoD\VoidAuras\0SmartVoidAuras.cs",

        #endregion
    };
    private string[] NoSetOptionsAllowed =
    {
        @"Tools\BankAllItems.cs",
        @"Other\WheelOfDoomSpam.cs",
        @"Tools\ForDevelopers\GenerateQuestFiles.cs",
        @"Tools\GenerateQueuedScript.cs",
        @"Tools\ForDevelopers\MergeTemplateHelper.cs",
        @"Tools\ForDevelopers\ScriptMainCleaner.cs",
        @"Tools\ForDevelopers\UpdateTags.cs",
    };
    #endregion
}
