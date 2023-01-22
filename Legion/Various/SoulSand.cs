/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
using Skua.Core.Interfaces;

public class AnotherOneBitesTheDust
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreLegion Legion = new CoreLegion();
    public CoreAdvanced Adv = new CoreAdvanced();
    public SeraphicWar_Story SeraphicWar = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        SoulSand();

        Core.SetOptions(false);
    }

    public void SoulSand(int quant = 50)
    {
        if (Core.CheckInventory("Soul Sand", quant))
            return;

        Farm.Experience(65);
        SeraphicWar.SeraphicWar_Questline();

        Core.AddDrop("Soul Sand");
        Core.FarmingLogger("Soul Sand", quant);
        while (!Bot.ShouldExit && !Core.CheckInventory("Soul Sand", quant))
        {
            Core.EnsureAccept(7991);
            Farm.BattleUnderB("Bone Dust", 333);
            Legion.ApprovalAndFavor(0, 400);
            Legion.DarkToken(80);
            Core.EnsureComplete(7991);
        }
    }
}
