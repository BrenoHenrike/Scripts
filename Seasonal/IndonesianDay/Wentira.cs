/*
name: Wentira
description: This script will complete the storyline in /wentira.
tags: royal wentira, agung palakka, seasonal, indonesia
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;

public class Wentira
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
        if (!Core.isSeasonalMapActive("wentira") || Core.isCompletedBefore(9341))
            return;

        Story.PreLoad(this);

        // Ritual Greed (9340)
        Story.KillQuest(9340, "wentira", "Pesugihan Boar");

        // Dark Dance (9341)
        Core.EquipClass(ClassType.Solo);
        Story.KillQuest(9341, "wentira", "Kabasaran Waranei");
        Core.EquipClass(ClassType.Farm);
    }
}
