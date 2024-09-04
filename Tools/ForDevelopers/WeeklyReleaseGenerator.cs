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
        if (Bot.Config == null)
        {
            Core.Logger("Config is not set properly.");
            return;
        }

        int startID = Bot.Config.Get<int>("QuestIDRangeStart");
        int endID = Bot.Config.Get<int>("QuestIDRangeEnd");
        string mapName = Bot.Config.Get<string>("MapName") ?? string.Empty;

        if (string.IsNullOrEmpty(mapName))
        {
            Core.Logger("MapName is not set properly.");
            return;
        }

        // Join the map first
        Core.Join(mapName);
        Bot.Wait.ForMapLoad(mapName);

        // Initialize a list to store generated lines
        List<string> generatedLines = new()
    {
        $"if (Core.isCompletedBefore({endID}))",
        "    return;",
        string.Empty, // Add a line break after the initial checks
        "Story.PreLoad(this);",
        string.Empty // Add a line break after PreLoad
    };

        // Collect monster names from the map and ensure uniqueness
        List<string> monsterNames = Bot.Monsters.MapMonsters
            .Select(m => m.Name)
            .Distinct()
            .ToList();

        // Add Useable Monsters section to generated lines
        generatedLines.Add("#region Useable Monsters");
        generatedLines.Add("string[] UseableMonsters = new[]");
        generatedLines.Add("{");
        generatedLines.AddRange(monsterNames.Select((name, index) =>
            $"\t\"{name}\", // UseableMonsters[{index}]{(index < monsterNames.Count - 1 ? "," : "")}"));
        generatedLines.Add("};");
        generatedLines.Add("#endregion Useable Monsters");



        // Ensure quests are processed in numerical order
        var sortedQuests = Core.EnsureLoad(Core.FromTo(startID, endID))
                                .OrderBy(q => q.ID)
                                .ToList();

        foreach (Quest q in sortedQuests)
        {
            // Add a blank line for separation
            generatedLines.Add(string.Empty);

            // Log the quest ID and name
            var questLogMessage = $"// {q.ID} | {q.Name}";
            generatedLines.Add(questLogMessage);

            // Start the QuestProgression check for the current quest
            generatedLines.Add($"if (!Story.QuestProgression({q.ID}))");
            generatedLines.Add("{");

            if (q.Requirements.Count == 0)
            {
                // Handle case where there are no requirements
                generatedLines.Add($"    Core.ChainQuest({q.ID});");
            }
            else
            {
                // Handle cases with requirements
                generatedLines.Add($"    Core.HuntMonsterQuest({q.ID}, new (string? mapName, string? monsterName, ClassType classType)[] {{");

                foreach (var req in q.Requirements.Select((r, index) => new { r, index }))
                {
                    string monsterName = $"UseableMonsters[{req.index}]";
                    generatedLines.Add($"        (\"{mapName}\", {monsterName}, ClassType.Solo),");
                }

                // Remove the last comma and close the array
                generatedLines[^1] = generatedLines.Last().TrimEnd(',') + " });";
            }

            // Close the QuestProgression check block
            generatedLines.Add("}");

            // Add an extra new line for readability between different quests
            generatedLines.Add(string.Empty);
        }

        // Write generated lines to a temporary file
        string tempFilePath = Path.GetTempFileName();
        File.WriteAllLines(tempFilePath, generatedLines);

        // Open the file automatically
        System.Diagnostics.Process.Start("notepad.exe", tempFilePath);
    }



}
