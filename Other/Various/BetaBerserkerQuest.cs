/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class SecretMapQuest
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        DoQuest();

        Core.SetOptions(false);
    }

    public void DoQuest()
    {
        InventoryItem? BetaBerserker = Bot.Inventory.Items.Find(i => i.Name.ToLower().Trim() == ("Beta Berserker").ToLower().Trim() && i.Category == ItemCategory.Class);
       
        if (BetaBerserker == null || Core.CheckInventory(Core.QuestRewards(5516)))
            return;

        Core.AddDrop(Core.QuestRewards(5516));
        if (Core.CheckInventory("Beta Berserker") && !Core.CheckInventory(Core.QuestRewards(5516)))
        {
            while (BetaBerserker != null && BetaBerserker.Quantity < 1)
                Core.KillMonster("battleontown", "Enter", "Spawn", "*", log: false);

            Core.EnsureAccept(5516);
            Core.HuntMonster("nostalgiaquest", "Boss Zardman", "Secret Map");
            Core.EnsureComplete(5516);
        }
    }
}
