/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
using System.Diagnostics;
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class TreasureHunterGenerator
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public string OptionsStorage = "GenerateThScript";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        new Option<int>("QuestID", "QuestID", "QuestID for Treasure Hunt", 0000),
        new Option<string>("ScriptName", "Script Name", "name for generated script", ""),
        CoreBots.Instance.SkipOptions,
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GenerateTreasureHuntScript();

        Core.SetOptions(false);
    }

    public void GenerateTreasureHuntScript()
    {
        // Retrieve script name from config
        string scriptName = Bot.Config.Get<string>("ScriptName");
        if (string.IsNullOrEmpty(scriptName))
        {
            Core.Logger("Script name is not set in the config.");
            return;
        }

        // Dynamically get the path to the user's Documents folder
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        // Construct the path to the new script file within the user's Documents folder
        // Adjust the path according to your specific folder structure within Documents
        string newFilePath = Path.Combine(documentsPath, "Skua", "Scripts", "Other", "TreasureHunts", $"{scriptName}.cs");

        // Ensure the directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(newFilePath));

        // Retrieve required item names
        int questID = Bot.Config.Get<int>("QuestID");
        var requiredItems = Bot.Quests.EnsureLoad(questID).Requirements
            .Concat(Bot.Quests.EnsureLoad(questID).AcceptRequirements)
            .Where(req => req.Name != null) // Filter out null names
            .Select(req => req.Name); // Select just the name without quotes

        // Additional checks and logic before foreach statement
        string checksAndLogic = $@"
        int questID = Bot.Config.Get<int>(""QuestID"");

        #region Checks
        // Check if the treasure hunt quest is complete
        if (Core.isCompletedBefore(questID))
            return;

        // Check if the required quest item is in the inventory
        if (!Core.CheckInventory(Bot.Quests.EnsureLoad(questID).Name))
            return;

        // Ensure the required level is met
        Farm.Experience(Bot.Quests.EnsureLoad(questID).Level);

        // Log the quest name
        Core.Logger(Bot.Quests.EnsureLoad(questID).Name ?? ""Invalid Quest name"");
        #endregion Checks

        Core.EnsureAccept(questID);
    ";

        // Generate the foreach statement with switch cases
        string foreachStatement = $@"
        foreach (var itemName in new[]
        {{
{string.Join(",\n", requiredItems.Select(name => $"            \"{name}\""))}
        }})
        {{
            switch (itemName)
            {{
{string.Join("\n", requiredItems.Select(name => $"                case \"{name}\":\n                    // Logic for {name}\n                    break;"))}
                default:
                    // Logic for unknown items
                    break;
            }}
        }}
    ";

        // Generate the complete file content with proper structure
        string fileContent = $@"
    /*
    name: {scriptName}
    description: Does the Treasure Hunt: "".
    tags: treasure hunt, treasure, hunt, item1, item2
    */

    //cs_include Scripts/CoreBots.cs
    //cs_include Scripts/CoreFarms.cs
    //cs_include Scripts/CoreStory.cs
    //cs_include Scripts/CoreAdvanced.cs

    using  Skua.Core.Interfaces;
    using  System;
    using  System.Collections.Generic;
    using  System.IO;
    using  System.Linq;
    using  Skua.Core.Models.Items;
    using  Skua.Core.Options;

    public class {scriptName}
    {{
        public IScriptInterface Bot => IScriptInterface.Instance;
        public CoreBots Core => CoreBots.Instance;
        public CoreFarms Farm = new();
        public CoreAdvanced Adv = new();

        public string OptionsStorage = ""ThScriptOptions"";
        public bool DontPreconfigure = true;
        public List<IOption> Options = new()
        {{
            new Option<int>(""QuestID"", ""QuestID"", ""QuestID for Treasure Hunt"", {questID}),
            CoreBots.Instance.SkipOptions,
        }};

        public void ScriptMain(IScriptInterface bot)
        {{
            Core.SetOptions();

            GetRequirementsandAcceptRequirements();

            Core.SetOptions(false);
        }}

        public void GetRequirementsandAcceptRequirements()
        {{
            {checksAndLogic}

    {foreachStatement}

            Core.EnsureComplete(questID);
        }}
    }}
    ";

        // Write the content to the new file
        File.WriteAllText(newFilePath, fileContent);

        Core.Logger($"Generated script '{scriptName}' and saved to: {newFilePath}");

        // // Popup confirmation to open the file, explicitly using System.Windows.Forms.DialogResult
        // if (Bot.ShowMessageBox($"The file '{scriptName}.cs' has been generated. Path is {newFilePath}\n\nPress OK to open the file",
        //                                         "File Generated", "OK").Text == "OK")
        //     Process.Start("explorer", newFilePath);

        // Ask the user if they want to open the file
        if (Bot.ShowMessageBox($"File has been generated. Do you want to open it?\n\nPath: {newFilePath}", "Open Generated File", "Yes", "No").Text == "Yes")
            Process.Start("explorer", newFilePath);
    }
}
