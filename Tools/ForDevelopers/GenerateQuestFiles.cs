/*
name: Generate Quest Files
description: This will generate the the files needed for the QuestData sheet and the #quest-ids channel in our discord
tags: quests, developer, lists, files, spreadsheet, excel, data
*/
//cs_include Scripts/CoreBots.cs
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Skua.Core.Models.Quests;
using CommunityToolkit.Mvvm.DependencyInjection;
using Skua.Core.Models;
using Skua.Core.Interfaces;

public class GetQuests
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        GenerateQuestFiles();
    }

    private async void GenerateQuestFiles()
    {
        //if (Bot.ShowMessageBox("Did you update the Quest.txt file by clicking the update button in [Tools] -> [Loader]", "Quests up to date?", true) == false)
        //    return;

        Core.Logger("Updating Quest.txt");
        await UpdateQuests();

        Core.Logger("Reading Quest.txt");
        var v = JsonConvert.DeserializeObject<dynamic[]>(File.ReadAllText(ClientFileSources.SkuaQuestsFile))!;

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
        Core.WriteFile(Path.Combine(ClientFileSources.SkuaScriptsDIR, "WIP", "QuestIds.txt"), r);
        Core.WriteFile(Path.Combine(ClientFileSources.SkuaScriptsDIR, "WIP", "QuestData.csv"), d);
        File.Copy(ClientFileSources.SkuaQuestsFile, Path.Combine(ClientFileSources.SkuaScriptsDIR, "QuestData.json"), true);

        Core.Logger("Files made:");
        Core.Logger(" - \"Scripts/WIP/QuestIds.txt\"");
        Core.Logger(" - \"Scripts/WIP/QuestData.csv\"");
        Core.Logger(" - \"Scripts/QuestData.json\"");
    }

    private async Task UpdateQuests()
    {
        _loaderCTS = new();
        List<QuestData> questData =
            await (service ??= Ioc.Default.GetRequiredService<IQuestDataLoaderService>())
            .UpdateAsync("Quests.txt", false, null, _loaderCTS.Token);
        _loaderCTS.Dispose();
        _loaderCTS = null;
    }
    private CancellationTokenSource? _loaderCTS;
    private IQuestDataLoaderService? service;
}
