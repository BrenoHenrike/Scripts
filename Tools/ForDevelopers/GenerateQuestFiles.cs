/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Skua.Core.Interfaces;

public class GetQuests
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        GenerateQuestFiles();
    }

    private void GenerateQuestFiles()
    {
        if (Bot.ShowMessageBox("Did you update the Quest.txt file by clicking the update button in [Tools] -> [Loader]", "Quests up to date?", true) == false)
            return;

        Core.Logger("Reading Quest.txt");
        var v = JsonConvert.DeserializeObject<dynamic[]>(File.ReadAllText(Path.Combine(CoreBots.SkuaPath, "Quests.txt")));

        List<string> r = new();
        List<string> d = new();
        d.Add("ID|Name|Once|Slot|Value|Upgrade|Gold|XP");
        Core.Logger("Placing the Quest Data in the lists.");

        foreach (var q in v)
        {
            int ID = q.ID;
            string spaces = "            ";
            foreach (var c in ID.ToString())
                spaces = spaces[..^2];

            r.Add($"`[{ID}]`{spaces}{q.Name}");
            d.Add($"{ID}|{q.Name}|{q.Once}|{q.Slot}|{q.Value}|{q.Upgrade}|{q.Gold}|{q.XP}");
        }

        Core.Logger("Writing files.");
        File.WriteAllLines("Scripts/WIP/QuestIds.txt", r);
        File.WriteAllLines("Scripts/WIP/QuestData.csv", d);

        Core.Logger("Files made:");
        Core.Logger(" - \"Scripts/WIP/QuestIds.txt\"");
        Core.Logger(" - \"Scripts/WIP/QuestData.csv\"");
    }
}
