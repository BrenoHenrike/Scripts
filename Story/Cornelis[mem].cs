/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Cornelis
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        StoryLine();

        Core.SetOptions(false);
    }

    public void StoryLine()
    {
        if (!Core.IsMember || Core.isCompletedBefore(1632))
            return;

        Story.PreLoad(this);

        // The Missing Vault 1625
        Story.MapItemQuest(1625, "cornelis", 856);

        // The Shattered Lens 1626
        Story.MapItemQuest(1626, "cornelis", 857, 20);

        // The Frame Up 1627
        Story.KillQuest(1627, "cornelis", "Gargoyle");

        // The Lenscrafter 1628
        if (!Story.QuestProgression(1628))
        {
            Core.EnsureAccept(1628);
            Core.BuyItem("yulgar", 366, "The Lens of Cornelis");
            Core.EnsureComplete(1628);
        }

        // The Hidden Vault 1629
        Story.MapItemQuest(1629, "cornelis", 858);

        // The Guardian 1630
        Story.KillQuest(1630, "cornelis", "Gargantugoyle");

        //The Family Jewels 1631
        Story.MapItemQuest(1631, "cornelis", 859);

        // [BADGE] Cornelis Reborn 1632
        Core.EnsureAccept(1632);
        Core.HuntMonster("cornelis", "Gargoyle", "Gargoyle Horn", 100, isTemp: false, log: false);
        Core.EnsureComplete(1632);
    }
}
