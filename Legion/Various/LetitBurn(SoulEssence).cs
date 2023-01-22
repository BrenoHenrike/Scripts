/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/Various/SoulSand.cs
//cs_include Scripts/Legion/Various/LegionBonfire.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class LetItBurn
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public CoreLegion Legion = new CoreLegion();
    public CoreAdvanced Adv = new CoreAdvanced();
    public AnotherOneBitesTheDust SSand = new AnotherOneBitesTheDust();
    public LegionBonfire Bon = new LegionBonfire();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        SoulEssence();

        Core.SetOptions(false);
    }

    private string[] rewards = { "Legion Undead Spawn", "Legion Undead Visor", "Legion Forge Banisher", "Legion Spawn Bonker" };

    public void SoulEssence(int quant = 50)
    {
        if (Core.CheckInventory("Soul Essence", quant))
            return;

        Core.AddDrop("Soul Essence");
        Core.AddDrop(rewards);
        int i = 1;

        Farm.Experience(65);
        Bon.GetLegionBonfire();

        Core.Logger($"Farming {quant} Soul Essence");
        Core.EquipClass(ClassType.Solo);
        while (!Bot.ShouldExit && !Core.CheckInventory("Soul Essence", quant))
        {
            Core.EnsureAccept(7992);
            Core.HuntMonster("dagefortress", "Grrrberus", "Grrberus' Flame");
            SSand.SoulSand(3);
            if (!Core.CheckInventory(rewards))
                Core.EnsureCompleteChoose(7992, rewards);
            else Core.EnsureComplete(7992);
            Core.Logger($"Completed x{i++}");
        }
    }
}
