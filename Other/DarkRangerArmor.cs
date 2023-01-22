/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class DarkRanger
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetArmor();

        Core.SetOptions(false);
    }

    public void GetArmor()
    {
        if (Core.CheckInventory("Dark Ranger"))
            return;

        if (!Core.HasAchievement(24, "ip9"))
        {
            Core.Logger("You need to have \"8 years played\" badge in order to use this bot.");
            return;
        }

        Adv.BuyItem("whitemap", 1317, "Golden 8th Birthday Candle");
        Farm.SandseaREP();
        Core.AddDrop("Dark Ranger");

        Core.RegisterQuests(5517);
        while (!Bot.ShouldExit && !Core.CheckInventory("Dark Ranger"))
        {
            Core.HuntMonster("nostalgiaquest", "Zardman Grunt", "Grunt Leather", 6);
            Core.HuntMonster("nostalgiaquest", "Big Jack Sprat", "Black Dye", 2);
            Bot.Wait.ForPickup("Dark Ranger");
        }
        Core.CancelRegisteredQuests();
    }
}

