/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Mazumi
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        MazumiQuests();

        Core.SetOptions(false);
    }

    public void MazumiQuests()
    {
        if (Core.isCompletedBefore(92))
            return;

        Story.PreLoad(this);

        // Ninja Grudge 90
        Story.KillQuest(90, "pirates", "Fishman Soldier");

        // Without a Trace 91
        if (!Story.QuestProgression(91))
        {
            Core.EnsureAccept(91);
            Core.HuntMonster("greenguardwest", "Kittarian", "Kittarian's Wallet", 2);
            Core.HuntMonster("greenguardwest", "River Fishman", "River Fishman's Wallet", 2);
            Core.HuntMonster("greenguardwest", "Slime", "Slime-Soaked Wallet", 2);
            Core.HuntMonster("greenguardwest", "Frogzard", "Frogzard's Lint Hoard", 2);
            Core.HuntMonster("greenguardwest", "Big Bad Boar", "Big Bad Boar's Wallet");
            Core.EnsureComplete(91);
        }

        // Hit Job 92
        Story.KillQuest(92, "greenguardwest", new[] { "Breken the Vile", "Ogug Stoneaxe" });

    }
}
