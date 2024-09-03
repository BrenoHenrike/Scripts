/*
name: SkyCharged Grenadier (9years Membership)
description: This script farms the SkyCharged Grenadier class.
tags: skycharged, grenadier, 9years, member, sky charged, captain stratos
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
using Skua.Core.Interfaces;

public class SkyChargedGrenadier
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private CoreStory Story = new();
    private CoreAdvanced Adv = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetSCG();

        Core.SetOptions(false);
    }

    public void GetSCG(bool rankUpClass = true)
    {
        if (Core.CheckInventory("SkyCharged Grenadier"))
            return;

        if (!Core.HasAchievement(28, "ip14"))
        {
            Core.Logger("This bot requires you to have 9 Years Membership or more.", messageBox: true);
            return;
        }

        if (Core.isCompletedBefore(6933))
            return;

        Story.PreLoad(this);

        Core.EquipClass(ClassType.Farm);

        Core.AddDrop("SkyCharged Grenadier", "Charged Grenade");

        // Clean the Deck (6930)
        Story.KillQuest(6930, "skycharged", "Sky Pirate Draconian");

        // Draconian Delivery (6931)
        Story.KillQuest(6931, "skycharged", new[] { "Sky Pirate Draconian", "Sky Pirate Dragon" });

        // Gladius and some Jelly (6932)
        Story.KillQuest(6932, "skycharged", new[] { "Gladius", "Rehydrated Gell Oh No" });

        Core.EnsureAccept(6933);
        Core.RegisterQuests(6934);
        while (!Bot.ShouldExit && !Core.CheckInventory("Charged Grenade", 50))
        {
            Core.HuntMonster("skycharged", "Sky Pirate Draconian", "Sky Pirate Draconian Chased Off", 10);
            Bot.Wait.ForPickup("Charged Grenade");
        }

        Adv.BuyItem("skycharged", 1648, "Evolved Grenadier");
        Core.EnsureComplete(6933);
        Bot.Wait.ForPickup("SkyCharged Grenadier");

        if (rankUpClass)
            Adv.RankUpClass("SkyCharged Grenadier");
    }
}
