/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class YouMadBroBadge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (Core.HasWebBadge(badge))
        {
            Core.Logger($"Already have the {badge} badge");
            return;
        }

        Farm.AlchemyREP();

        while (!Bot.ShouldExit && !Core.HasWebBadge(badge))
        {
            Core.AddDrop("Dragon Scale", "Ice Vapor");
            Core.FarmingLogger("Dragon Scale", 30);
            Core.FarmingLogger("Ice Vapor", 30);
            while (!Core.CheckInventory(11475, 30) || !Core.CheckInventory(11478, 30))
                Core.KillMonster("lair", "Enter", "Spawn", "*", log: false);
            Adv.BuyItem("alchemy", 395, "Dragon Runestone", 100, 8844);
            Farm.AlchemyPacket("Dragon Scale", "Ice Vapor", trait: CoreFarms.AlchemyTraits.hOu, P2w: true);
        }
        Core.SellItem("Dragon Scale", all: true);
        Core.SellItem("Ice Vapor", all: true);
        Core.ToBank("Dragon Runestone");
    }
    // private string[] PotionsToSell = {"Life Potion", "Basic Crusader Elixir", "Basic Barrier Potion", "Basic Crusader Elixir", "Divine Elixir", "Barrier Potion", "Basic Barrier Potion", "Basic Divine Elixir", "Crusader Elixir"};
    private string badge = "You mad bro?";
}
