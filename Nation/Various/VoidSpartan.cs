/*
name: VoidSpartan
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Nation/CoreNation.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;

public class VoidSpartan
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreNation Nation = new();

    public readonly string[] Rewards =
    {
        "Void Spartan",
        "Void Spartan Daggers",
        "Void Spartan Blade and Shield",
        "Void Spartan Spear",
        "Void Spartan Blade",
        "Void Spartan Banners",
        "Void Spartan Cape",
        "Void Spartan Shielded Cape",
        "Void Spartan Spear and Shield",
        "Void Spartan Helm",
        "Void Spartan Helm and Scarf"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.AddRange(Rewards);
        Core.SetOptions();

        GetSpartan();

        Core.SetOptions(false);
    }

    public void GetSpartan(string? item = null)
    {
        Core.AddDrop(Nation.bagDrops.Concat(Rewards).Concat(new[] { "Zee's Red Jasper", "Fiend Cloak of Nulgath" }).ToArray());

        Quest QuestData = Core.EnsureLoad(5982);
        ItemBase? Item = Core.EnsureLoad(5982).Rewards.Find(x => x.Name == item);

        if (item == null)
            Core.Logger("Farming Void Spartan Set.");
        else Core.Logger($"Farming {item}.");

        int i = 1;
        while (!Bot.ShouldExit && (Item != null ? !Core.CheckInventory(Item!.Name) : !Core.CheckInventory(Rewards, toInv: false)))
        {
            Core.EnsureAccept(5982);

            Nation.FarmUni13(1);
            Nation.FarmBloodGem(5);
            Nation.FarmGemofNulgath(10);
            Core.HuntMonster("pyrewatch", "Flame Soldier", "Zee's Red Jasper", 1, false);
            //jumpwait jumps to same cell which is auto-aggro, spawn("Enter") isnt, so force jump there.
            Core.JumpWait();
            // Core.Jump("Enter", "Spawn");
            Farm.Gold(500000);
            Core.BuyItem("tercessuinotlim", 68, "Fiend Cloak of Nulgath");

            if (Item != null)
            {
                Core.EnsureComplete(5982, Item.ID);
                Bot.Wait.ForPickup(Item!.Name);
            }
            else
            {
                Core.EnsureCompleteChoose(5982, Core.QuestRewards(5982));
                foreach (string thing in Core.QuestRewards(5982))
                    if (Bot.Drops.Exists(thing))
                        Bot.Wait.ForPickup(thing);
            }
            Core.Logger($"Completed {QuestData.Name}[{QuestData.ID}] x{i++}");
        }
        i = 0;
    }
}
