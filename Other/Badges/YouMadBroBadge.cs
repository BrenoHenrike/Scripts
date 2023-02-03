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
        Core.EquipClass(ClassType.Farm);
        while (!Bot.ShouldExit && !Core.HasWebBadge(badge))
        {
            Core.AddDrop("Dragon Scale", "Ice Vapor");
            Core.FarmingLogger("Dragon Scale", 30);
            Core.FarmingLogger("Ice Vapor", 30);
            while (!Core.CheckInventory(11475, 30) || !Core.CheckInventory("Ice Vapor", 30))
                Core.KillMonster("lair", "Enter", "Spawn", "*", isTemp: false, log: false);

            Adv.BuyItem("alchemyacademy", 395, 62749, 100, 1, 8777);
            Adv.BuyItem("alchemyacademy", 395, 7132, 100, 1, 8844);
            Farm.AlchemyPacket("Dragon Scale", "Ice Vapor", trait: CoreFarms.AlchemyTraits.hOu, P2w: true);
        }
        Core.TrashCan("Dragon Scale", "Ice Vapor");
        Core.ToBank("Dragon Runestone", "Gold Voucher 100k");
    }
    // private string[] PotionsToSell = {"Life Potion", "Basic Crusader Elixir", "Basic Barrier Potion", "Basic Crusader Elixir", "Divine Elixir", "Barrier Potion", "Basic Barrier Potion", "Basic Divine Elixir", "Crusader Elixir"};
    private string badge = "You mad bro?";
}
