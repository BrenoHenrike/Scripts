/*
name: Lorebowl
description: Kills the mob in /punt for its drops
tags: LoreBowl, drops, punt, twilly
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class LoreBowl
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    string[] LoreBowlItems =
    {
   "Broncolich Pennant",
    "Chaos Pennant",
    "Cheer Lover",
    "Cheer Lover's Hair",
    "Cheer Lover's Locks",
    "Chieftains Pennant",
    "Corsairs Pennant",
    "Crows Pennant",
    "CyberHounds Pennant",
    "Diabolical Pennant",
    "Evil Pennant",
    "Good Pennant",
    "Harpies Pennant",
    "Hollowborn Pennant",
    "Hoofs Pennant",
    "Horcs Pennant",
    "Legion Pennant",
    "Loyalists Pennant",
    "Mammoths Pennant",
    "Miner Pennant",
    "Nation Pennant",
    "Saints Pennant",
    "Satyrs Pennant",
    "Tigers Pennant",
    "Treasure Chest (Misc)",
    "Void Nation Cheerleader",
};

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Drops();

        Core.SetOptions(false);
    }

    private void Drops()
    {
        if (Core.CheckInventory(LoreBowlItems, toInv: false))
            return;

        while (!Bot.ShouldExit && !Core.CheckInventory(LoreBowlItems, toInv: false))
            Core.HuntMonster("punt", "*", isTemp: false);
    }

}
