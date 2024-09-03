/*
name: Zoda's Quests
description: This script will complete Zoda's Quests in /zorbaspalace.
tags: zodas, zorbas, palace, seasonal, story, quests, may4th
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class Zoda
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();
    private CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Storyline();
        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(8651) || !Core.isSeasonalMapActive("zorbaspalace"))
            return;

        Story.PreLoad(this);

        // Dig Deep (8644)
        Story.KillQuest(8644, "dwarfhold", "Glow Worm");

        // The Fire Within (8645)
        Story.KillQuest(8645, "firestorm", "Living Fire");

        // Spare Parts (8646)
        Story.KillQuest(8646, "thespan", "Training Globe");

        // Diplomatic Solution (8647)
        if (!Story.QuestProgression(8647))
            AssembledSword();

        // The Path Quests (8648, 8649, 8650)
        if (!Core.isCompletedBefore(8648) && !Core.isCompletedBefore(8649) && !Core.isCompletedBefore(8650))
        {
            AssembledSword();

            if (Farm.FactionRank("Good") == 10)
            {
                Core.AddDrop("Goregold Resisted");
                Story.KillQuest(8648, "greed", "Goregold");
            }

            else if (Farm.FactionRank("Evil") == 10)
            {
                Core.AddDrop("Fifth Sepulchure Defeated");
                Story.KillQuest(8649, "murdermoon", "Fifth Sepulchure");
            }

            else
            {
                Farm.ChaosREP();
                Core.AddDrop("Ledgermayne Defeated");
                Story.KillQuest(8650, "ledgermayne", "Ledgermayne");
            }
        }

        // Search the Pit (8651)
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(8651, "zorbaspit", "ZorBLATT");
    }

    public void AssembledSword()
    {
        if (Core.CheckInventory("Assembled Sword"))
            return;

        Core.AddDrop("Assembled Sword");
        Core.EnsureAccept(8647);
        if (!Core.CheckInventory(new[] { "Wired Innards", "Sword Billet" }))
        {
            Core.EnsureAccept(8646);
            Core.HuntMonster("thespan", "Training Globe", "Spare Wires", 10);
            Core.EnsureComplete(8646);
        }
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("zorbaspalace", "Zorba the Bakk");
        Core.EnsureComplete(8647);
    }
}
