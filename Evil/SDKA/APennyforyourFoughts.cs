/*
name: A Penny For Your Foughts
description: This script will farm Dark Spirit Orbs using "A Penny For Your Foughts" Quest.
tags: dark spirit orbs, dso, penny, doomcoin, shadow creeper enchant, maul
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class DSOPenny
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreSDKA SDKA = new();
    
    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "DoomCoin", "Dark Spirit Orb", "Shadow Creeper Enchant" });
        Core.SetOptions();

        SDKA.Penny();

        Core.SetOptions(false);
    }
}
