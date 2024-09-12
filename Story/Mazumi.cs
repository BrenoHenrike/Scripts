/*
name: Mazumi Story
description: This will finish the Mazumi Story.
tags: story, quest, mazumi
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Quests;

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

            // Dont use `West1` for Frogzard, as the cell contains an extra frogzard that isnt there.
            Core.KillMonster("greenguardwest", "West3", "Up", "Frogzard", "Frogzard's Lint Hoard", 2);
            
            Core.KillMonster("greenguardwest", "West12", "Up", "Big Bad Boar", "Big Bad Boar's Wallet");
            Core.EnsureComplete(91);
        }

        // Hit Job 92
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(92, "greenguardwest", new[] { "Breken the Vile", "Ogug Stoneaxe" });

    }
}
