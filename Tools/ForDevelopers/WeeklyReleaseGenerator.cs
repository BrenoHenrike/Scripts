/*
name: Weekly Release Generator
description: Automates the process of generating quest progression checks and corresponding monster hunt commands based on specified quest ID ranges. It handles single and multiple requirements efficiently, and outputs the formatted script to a temporary file for easy integration.
tags: quest automation, monster hunting, script generation, game scripting, quest progression
*/

//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Quests;
using Skua.Core.Options;
using System.Collections.Generic;
using System.IO;

public class WeeklyReleaseGenerator
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public string OptionsStorage = "QuestIDRange";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<int>("QuestIDRangeStart", "QuestIDRangeStart", "First QuestID in the story"),
        new Option<int>("QuestIDRangeEnd", "QuestIDRangeEnd", "Last QuestID in the story"),
        new Option<string>("MapName", "MapName", "Default map name for the hunt"),
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();
        Generator();
        Core.SetOptions(false);
    }

    public void Generator()
    {
        Core.Logger("Generating script...");

        int startID = Bot.Config.Get<int>("QuestIDRangeStart");
        int endID = Bot.Config.Get<int>("QuestIDRangeEnd");
        string mapName = Bot.Config.Get<string>("MapName");

        // Join the map and wait for it to load
        Core.Join(mapName);
        Bot.Wait.ForMapLoad(mapName);

        // Initialize a list to store generated lines
        List<string> generatedLines = new()
        {
            $"if (Core.isCompletedBefore({endID}))",
            "    return;",
            string.Empty,
            "Story.PreLoad(this);",
            string.Empty,
            $"#region Method Dictionary",
            $"// Example Method Calls:",
            $"// Story.KillQuest(000, \"{mapName}\", \"MonsterName\");",
            $"// Story.KillQuest(000, \"{mapName}\", new[] {{ \"Monstername\", \"Monstername\" }});",
            $"// Story.MapItemQuest(000, \"{mapName}\", 1, 1);",
            $"// Story.MapItemQuest(000, \"{mapName}\", new[] {{ 000, 000, 000 }});",
            $"// Story.ChainQuest(000);",
            $"#endregion Method Dictionary"
        };

        // Ensure quests are processed in numerical order
        var sortedQuests = Core.EnsureLoad(Core.FromTo(startID, endID))
                                .OrderBy(q => q.ID)
                                .ToList();

        foreach (Quest q in sortedQuests)
        {
            generatedLines.Add(string.Empty);

            // Log the quest ID and name
            generatedLines.Add($"// {q.ID} | {q.Name}");

            // Start the QuestProgression check for the current quest
            generatedLines.Add($"if (!Story.QuestProgression({q.ID}))");
            generatedLines.Add("{");

            if (q.Requirements.Count == 0)
            {
                generatedLines.Add($"    Core.ChainQuest({q.ID});");
            }
            else
            {
                // Handle cases with requirements
                var huntLines = new List<string>
                {
                    $"    Core.HuntMonsterQuest({q.ID}, new (string? mapName, string? monsterName, ClassType classType)[] {{"
                };

                huntLines.AddRange(q.Requirements.Select(r => $"        (\"{mapName}\", \"monster\", ClassType.Solo),"));

                // Remove the last comma and close the array
                huntLines[^1] = huntLines.Last().TrimEnd(',') + " });";

                // Add formatted hunt lines to generated lines
                generatedLines.AddRange(huntLines);
            }

            // Close the QuestProgression check block
            generatedLines.Add("}");
            generatedLines.Add(string.Empty);
        }

        // Collect and remove duplicate monster names from the map
        List<Monster> mapMonsters = Bot.Monsters.MapMonsters.Distinct().ToList();
        List<string> monsterNames = mapMonsters.Select(m => m.Name).Distinct().ToList();

        // Add Useable Monsters section to generated lines
        generatedLines.Add($"#region Useable Monsters");
        generatedLines.Add($"string[] UseableMonsters = new[] {{ {string.Join(", ", monsterNames.Select(name => $"\"{name}\""))} }};");
        generatedLines.Add($"#endregion Useable Monsters");

        // Write generated lines to a temporary file
        string tempFilePath = Path.GetTempFileName();
        File.WriteAllLines(tempFilePath, generatedLines);

        // Open the file automatically
        System.Diagnostics.Process.Start("notepad.exe", tempFilePath);

        Core.Logger("Script generation complete.");
    }
}
