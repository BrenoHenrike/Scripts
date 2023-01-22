/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
// Must Have Completed Arcadia Storyline Up To: "Seperation Anxiety": http://aqwwiki.wikidot.com/big-daddy-s-quests#ArcadiaFarm
using Skua.Core.Interfaces;
public class FarmPaintedButterfly
{
    public IScriptInterface Bot => IScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        PaintedButterfly();

        Core.SetOptions(false);
    }

    public void PaintedButterfly()
    {
        if (!Core.isSeasonalMapActive("arcadia"))
            return;
        Core.AddDrop("Painted Butterfly");

        while (!Bot.ShouldExit && !Core.CheckInventory("Painted Butterfly", 300))
        {
            Core.EnsureAccept(8520);
            Core.HuntMonster("arcadia", "Agape", "Agape Petal", 1);
            Core.HuntMonster("arcadia", "Spirit Butterfly", "Paper Butterfly Wings", 8);
            Core.HuntMonster("arcadia", "Lightguard Wraith", "Armor Paint Residue", 8);
            Core.EnsureComplete(8520);

        }
    }
}
