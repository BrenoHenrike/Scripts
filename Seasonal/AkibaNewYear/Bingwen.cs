/*
name: Bingwen's Quests
description: Completes Bingwen's Quests in akibacny.
tags: seasonal, yokai, akibacny, akiba new year, story, bingwen
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Bingwen
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Storyline();
        Core.SetOptions(false);
    }

    public void Storyline()
    {
        if (Core.isCompletedBefore(7339) || !Core.isSeasonalMapActive("akibacny"))
            return;

        Story.PreLoad(this);
        //Find Bingwen (7328)
        Story.MapItemQuest(7328, "akibacny", 6976);

        //Pay the Piper (7329)
        Story.KillQuest(7329, "akibacny", "Lin Kuei");

        //Pieces of Pipes (7330)
        Story.KillQuest(7330, "akibacny", new[] { "Lingzhi", "Lingzhi" });

        //Oil it up (7331)
        Story.KillQuest(7331, "akibacny", "Bamboo Treeant");

        //Get Me an Audience (7332)
        Story.MapItemQuest(7332, "akibacny", new[] { 6977, 6978, 6979, 6980, 6981, 6982 });

        //Meet Me at the Palace (7333)
        Story.MapItemQuest(7333, "akibacny", 6983);

        //Catch Some Rats (7334)
        Story.MapItemQuest(7334, "akibacny", 6984, 5);

        //Make Some Traps (7335)
        Story.KillQuest(7335, "akibacny", "Red Chicken");
        Story.MapItemQuest(7335, "akibacny", 6985, 6);

        //Those Rats are Mean (7336)
        Story.KillQuest(7336, "akibacny", "Mean Rat");

        //EEEEK (7337)
        Story.KillQuest(7337, "akibacny", "Ratassassin");

        //Smoke them Out (7338)
        Story.KillQuest(7338, "akibacny", new[] { "Lingzhi", "Fiery Lantern" });

        //Defeat Hinezumi! (7339)
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(7339, "akibacny", "Hinezumi");
    }
}
