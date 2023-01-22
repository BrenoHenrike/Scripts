/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.ViewModels;
using System.IO;
using CommunityToolkit.Mvvm.DependencyInjection;

public class GenQueueScript
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        GenerateQueuedScript();
    }
#nullable enable

    public void GenerateQueuedScript()
    {
        IFileDialogService? _fileDialog;
        List<string[]> scripts = new();
        List<string> csIncludes = new();
        List<string> classes = new();
        List<string> options = new();
        List<string> optionNames = new();
        List<string> staticClasses = new();
        List<string> methods = new();
        List<string> scriptNames = new();

        while (!Bot.ShouldExit)
        {
            // Selecting all files
            _fileDialog = Ioc.Default.GetRequiredService<IFileDialogService>();
            string _scriptPath = _scriptPath = Path.Combine(CoreBots.SkuaPath, "Scripts");
            string? path = _fileDialog.OpenFile(_scriptPath, "Skua Script (*.cs)|*.cs");
            if (path == null)
                break;

            string includePath = Path.Combine("Scripts", path.Replace('\\', '/').Split("/Scripts/").Last()).Replace('\\', '/');
            if (includePath.ToLower().Contains("core"))
            {
                Bot.ShowMessageBox($"The bot you selected: [{includePath}] has is a Core-file.\nCore-files are not meant to be run, not even as a queued script", "Core-file selected");
                continue;
            }

            string[] file = File.ReadAllLines(Path.Combine(CoreBots.SkuaPath, includePath));
            if (file.Any(l => l.Contains("public List<IOption>")))
            {
                Bot.ShowMessageBox($"The bot you selected: [{includePath}] has \"Script Options\", these are not supported for queued scripts. It will not be added to the queue", "Contains Script Options!");
                continue;
            }

            if (csIncludes.Contains("//cs_include " + includePath))
                continue;
            csIncludes.Add("//cs_include " + includePath);

            scripts.Add(file);
            scriptNames.Add(includePath);

            if (Bot.ShowMessageBox("Do you wish to add another one?", "Script added", true) != true)
                break;
        }

        foreach (var script in scripts)
        {
            // CS Includes
            foreach (var line in script.TakeWhile(l => l.StartsWith("//cs_include")))
            {
                if (!csIncludes.Contains(line))
                {
                    csIncludes.Add(line);
                }
            }

            // Class
            string className = Array.Find(script, l => l.Contains("public class"))!.Split("public class ").Last();
            classes.Add($"    private {className} {className} = new();");

            // Script Options (I gave up)
            #region Script Options Attempt
            /*
            if (script.Any(l => l.Contains("public List<IOption>")))
            {
                string[] scriptOptions = Array.FindAll(script, l => l.Contains("List<IOption>"));
                foreach (var option in scriptOptions)
                {
                    string _name = className + $"{(option.Contains("Options =") ? "_Options" : "_" + option.Split("IOption> ").Last().Split(" =").First())}";
                    options.AddRange(new List<string> {
                        "",
                        "    public List<IOption> " + _name + " =" + option.Split('=').Last(),
                    });
                    optionNames.Add($"\"{_name}\"");

                    int scriptOptionIndex = Array.FindIndex(script, l => l == option);
                    if (scriptOptionIndex < 0)
                    {
                        Bot.Log("Failed to find scriptOptionIndex");
                        continue;
                    }

                    if (script[scriptOptionIndex + 1].Trim() != "{")
                    {
                        string staticClassName = script[scriptOptionIndex].Split("= ").Last().Split('.').First();
                        string? staticClass = Array.Find(script, l => l.Contains("static") && l.Contains(staticClassName));
                        if (staticClass == null)
                        {
                            Bot.Log("Failed to find staticClass");
                            continue;
                        }

                        if (option.Contains("MergeOptions"))
                        {
                            options.RemoveRange(options.Count() - 2, 2);
                            string merge = "    public List<IOption> Generic = " + staticClassName + ".MergeOptions;";
                            if (!options.Contains(merge))
                                options.AddRange(new List<string>() {
                                    "",
                                    merge,
                                });
                        }

                        string _staticClass = $"    private static {staticClass.Split("static ").Last()}";
                        if (!staticClasses.Contains(_staticClass))
                            staticClasses.Add(_staticClass);
                        continue;
                    }

                    string _spaces = new String(script[scriptOptionIndex].TakeWhile(c => c == ' ').ToArray()) ?? String.Empty;
                    int scriptOptionEnd = Array.IndexOf(script, _spaces + "};", scriptOptionIndex);
                    if (scriptOptionEnd < 0)
                    {
                        Bot.Log("Failed to find scriptOptionEnd");
                        continue;
                    }
                    options.Add(_spaces + "{");

                    foreach (var line in script[(scriptOptionIndex + 1)..scriptOptionEnd])
                    {
                        if (line.Contains("Option<"))
                            options.Add($"        {line.Trim()}");
                    }
                    options.Add(_spaces + "};");
                }
            }
            */
            #endregion

            // Methods
            int scriptMainIndex = Array.FindIndex(script, l => l.ToLower().Contains("public void scriptmain(iscriptinterface bot)"));
            if (scriptMainIndex < 0)
            {
                Bot.Log("Failed to find scriptMainIndex");
                continue;
            }

            string spaces = new String(script[scriptMainIndex].TakeWhile(c => c == ' ').ToArray()) ?? String.Empty;
            int scriptMainEnd = Array.IndexOf(script, spaces + "}", scriptMainIndex);
            if (scriptMainEnd < 0)
            {
                Bot.Log("Failed to find scriptMainEnd");
                continue;
            }

            foreach (var line in script[(scriptMainIndex + 1)..scriptMainEnd])
            {
                if (!lineBlackList.Any(l => line.ToLower().Contains(l)) && !String.IsNullOrEmpty(line) && !String.IsNullOrWhiteSpace(line) && !line.Trim().StartsWith("//"))
                {
                    methods.Add($"        {(line.Contains('.') ? "" : className + ".")}{line.Trim()}");
                    if (line.Contains('.'))
                    {
                        string? _otherClass = Array.Find(script, l =>
                                                    (l.Contains("private") || l.Contains("public") &&
                                                    l.Contains($" {line.Trim().Split('.').First()} =") &&
                                                    l.Contains(" new();")));
                        if (_otherClass != null && !classes.Contains(_otherClass))
                            classes.Add(_otherClass);
                    }
                }
            }
        }

        List<string> newFile = new();
        newFile.AddRange(csIncludes);
        if (options.Count() > 0)
            newFile.Add("using Skua.Core.Options;");

        InputDialogViewModel diag = new("Name the bot", "What is the name you wish to give the bot. (case-sensitive)", false);
        if (Ioc.Default.GetRequiredService<IDialogService>().ShowDialog(diag) != true)
            return;
        string botName = removeInvalidChar(diag.DialogTextInput);

        newFile.AddRange(new List<string>() {
            "using Skua.Core.Interfaces;",
            "",
            "public class Generated_" + botName,
            "{",
            "    private IScriptInterface Bot => IScriptInterface.Instance;",
            "    private CoreBots Core => CoreBots.Instance;",
        });
        newFile.AddRange(classes);
        if (staticClasses.Count() > 0)
            newFile.AddRange(staticClasses);
        if (options.Count() > 0)
        {
            newFile.AddRange(new List<string>() {
                "",
                $"    public string[] MultiOptions = {{ {String.Join(", ", optionNames)} }};",
                $"    public string OptionsStorage = \"Generated_{botName}\";"
            });
            newFile.AddRange(options);
        }
        newFile.AddRange(new List<string>() {
            "",
            "    public void ScriptMain(IScriptInterface Bot)",
            "    {",
            "        Core.SetOptions();",
            "",
        });
        newFile.AddRange(methods);
        newFile.AddRange(new List<string>() {
            "",
            "        Core.SetOptions(false);",
            "    }",
            "}"
        });

        if (!Directory.Exists(Path.Combine(CoreBots.ScriptsPath, "Generated")))
            Directory.CreateDirectory(Path.Combine(CoreBots.ScriptsPath, "Generated"));
        File.WriteAllLines(Path.Combine(CoreBots.ScriptsPath, "Generated", botName + ".cs"), newFile);

        Bot.ShowMessageBox($"File Path:\n- Scripts/Generated/{botName}.cs\n\nIt does the following bots in the same order:\n- {String.Join("\n- ", scriptNames)}", "Script is succesfully generated");
    }

    private string[] lineBlackList = {
        "{",
        "core",
        "bot",
        "}"
    };

    //private string[] optionsBlackList = {
    //    "{",
    //    "}"
    //};

    private string removeInvalidChar(string input)
    {
        string toReturn = "";
        foreach (char c in input)
            if (Char.IsLetter(c) || Char.IsNumber(c))
                toReturn += c;
        return toReturn;
    }
}
