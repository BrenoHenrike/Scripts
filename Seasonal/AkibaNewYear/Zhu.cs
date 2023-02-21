/*
name: Zhu's Quests
description: Completes Zhu's Quests in zhu.
tags: seasonal, yokai, akiba new year, zhu, akibacny, story
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Zhu
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
        if (Core.isCompletedBefore(6723) || !Core.isSeasonalMapActive("zhu"))
            return;

        Story.PreLoad(this);

        //Shake a Tailfeather (6713)
        Story.KillQuest(6713, "akibacny", "Red Chicken");

        //Get Incensed (6714)
        Story.KillQuest(6714, "akibacny", "Lingzhi");

        //Smell that Smell (6715)
        Story.MapItemQuest(6715, "akibacny", 6196);

        //A Gift of Plums (6716)
        Story.KillQuest(6716, "zhu", "Plum Treeant");

        //A Taste of Truffles (6717)
        Story.KillQuest(6717, "zhu", "Heizhi");

        //Om Nom Nom (6718)
        Story.MapItemQuest(6718, "zhu", 6197, 5);
        Story.KillQuest(6718, "zhu", "BigTruffle");

        //Follow the Tracks (6719)
        Story.MapItemQuest(6719, "zhu", 6198, 3);
        Story.KillQuest(6719, "zhu", "Mogui");

        //Get out of Here! (6720)
        Story.MapItemQuest(6720, "zhu", 6199);

        //Smackdown! (6721)
        Story.KillQuest(6721, "zhu", "Xingzhi");

        //The Acorn (6722)
        Story.MapItemQuest(6722, "zhu", 6200);

        //Let Me Out! (6723)
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(6723, "zhu", "Jing");
    }
}
