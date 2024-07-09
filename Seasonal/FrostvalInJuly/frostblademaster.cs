/*
name: Frost Blade Master
description: Does the seasonal quests of frostblademaster in /akibalight
tags: frost, blade, master, seasonal, akiba, light
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class FrostBladeMaster
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        SagaName();
        Core.SetOptions(false);
    }

    public void SagaName()
    {
        if (Core.isCompletedBefore(6992) || !Core.isSeasonalMapActive("akibalight"))
        {            
            Core.Logger("You've already completed this storyline");
            
            return;
        }
        Story.PreLoad(this);

        //Decorations - Paper Lanterns 6982
        Story.KillQuest(6982, "junkyard", "Tsukumo-Gami");

        //Decorations - Light Them Up 6983
        Story.KillQuest(6983, "greenshell", "Tsurubebi");

        //Decorations - Streamers 6984
        Story.KillQuest(6984, "yokaiboat", "Kappa Ninja");

        //Time to Decorate 6985
        Story.MapItemQuest(6985, "akibalight", 49148, 8);
        Story.MapItemQuest(6985, "akibalight", 49149, 11);

        //Nekomimis Gift 6986
        Story.KillQuest(6986, "yokaigrave", "Skello Kitty");

        //Mitsu Bishis Gift 6987
        Story.KillQuest(6987, "bamboo", new[] {"Tanuki", "Tanuki"});

        //Kunoichis Gift 6988
        Story.KillQuest(6988, "pirates", "Fishwing");

        //Kages Gift 6989
        Story.KillQuest(6989, "hachiko", "Ninja Nopperabo");

        //Dinner Time 6990
        Story.KillQuest(6990, "pirates", "Shark Bait");

        //Fireworks 6991
        Story.KillQuest(6991, "odokuro", "O-dokuro");

        //Search for Gifts 6992
        Story.KillQuest(6992, "bamboo", "Bamboo Wisp");
    }
}
