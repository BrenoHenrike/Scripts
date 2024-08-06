/*
name: Weekly Release Generator
description: Automates the process of generating quest progression checks and corresponding monster hunt commands based on specified quest ID ranges. It handles single and multiple requirements efficiently, and outputs the formatted script to a temporary file for easy integration.
tags: quest automation, monster hunting, script generation, game scripting, quest progression
*/

//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
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
        Core.OneTimeMessage("How to Use", "This Willtake the \"QuestIDRangeStart\" & \"QuestIDRangeEnd\" IDs and genreate your weekly Quests (only uses \"HuntMonsterQuest\"), and output sits to a temporary file[not stored] taht u can copypaste into a core for example.");
        int startID = Bot.Config.Get<int>("QuestIDRangeStart");
        int endID = Bot.Config.Get<int>("QuestIDRangeEnd");

        // Initialize a list to store generated lines
        List<string> generatedLines = new()
    {
        // Add initial checks and preload to generated lines
        $"if (Core.isCompletedBefore({endID}))",
        "    return;",
        string.Empty, // Add a line break after the initial checks
        "Story.PreLoad(this);",
        string.Empty // Add a line break after PreLoad
    };

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
                var huntLines = new List<string>
            {
                $"    Core.HuntMonsterQuest({q.ID}, new (string? mapName, string? monsterName, ClassType classType)[] {{"
            };

                huntLines.AddRange(q.Requirements.Select(r => $"        (\"map\", \"monster\", ClassType.Solo),"));

                // Remove the last comma and close the array
                huntLines[^1] = huntLines.Last().TrimEnd(',') + " });";

                // Add formatted hunt lines to generated lines
                generatedLines.AddRange(huntLines);
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
