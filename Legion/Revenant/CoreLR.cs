/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/InfiniteLegionDarkCaster.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;

public class CoreLR
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new();
    public CoreStory Story = new();
    public CoreLegion Legion = new();
    public InfiniteLegionDC ILDC = new();
    public SeraphicWar_Story Seraph = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

    public string[] LR =
    {
        "Exalted Crown",
        "Revenant's Spellscroll",
        "Conquest Wreath",
        "Legion Revenant"
    };
    public string[] LF1 =
    {
        "Aeacus Empowered",
        "Tethered Soul",
        "Darkened Essence",
        "Dracolich Contract"
    };
    public string[] LF2 =
    {
        "Grim Cohort Conquered",
        "Ancient Cohort Conquered",
        "Pirate Cohort Conquered",
        "Battleon Cohort Conquered",
        "Mirror Cohort Conquered",
        "Darkblood Cohort Conquered",
        "Vampire Cohort Conquered",
        "Spirit Cohort Conquered",
        "Dragon Cohort Conquered",
        "Doomwood Cohort Conquered",
    };
    public string[] LF3 =
    {
        "Hooded Legion Cowl",
        "Legion Token",
        "Dage's Favor",
        "Emblem of Dage",
        "Diamond Token of Dage",
        "Dark Token"
    };

    public void GetLR(bool rankUpClass)
    {
        // Tests for IODA LR since it does not provide the badge and currently has the same ID as normal LR
        if (Core.CheckInventory("Legion Revenant") && Core.Badges.Contains("Legion Revenant"))
            return;

        Legion.JoinLegion();

        Core.AddDrop("Legion Token");
        Core.AddDrop(Legion.legionMedals);
        Core.AddDrop(LR);
        Core.AddDrop(LF1);
        Core.AddDrop(LF2);
        Core.AddDrop(LF3);

        RevenantSpellscroll();
        ConquestWreath();
        ExaltedCrown();

        //if you used insignias other quests arent unlocked(yes people have done this...)
        if (!Core.isCompletedBefore(6900))
            ExaltedCrown(1);

        Core.ChainComplete(6900);
        Bot.Wait.ForDrop("Legion Revenant", 20);
        Bot.Wait.ForPickup("Legion Revenant", 20);
        if (rankUpClass)
            Adv.RankUpClass("Legion Revenant");
    }

    //Legion Fealty 1
    public void RevenantSpellscroll(int quant = 20, bool forquest = false)
    {
        if (!forquest && Core.CheckInventory("Revenant's Spellscroll", quant))
            return;

        Legion.JoinLegion();

        Core.AddDrop("Legion Token");
        Core.AddDrop(LR);
        Core.AddDrop(LF1);

        Farm.EvilREP();

        int i = 1;
        Core.FarmingLogger("Revenant's Spellscroll", quant);
        Bot.Quests.UpdateQuest(2060);
        while (!Bot.ShouldExit && ((forquest && !Story.QuestProgression(6897)) || !Core.CheckInventory("Revenant's Spellscroll", quant)))
        {
            Core.EnsureAccept(6897);

            Core.EquipClass(ClassType.Solo);
            //Adv.BestGear(RacialGearBoost.Undead);
            Core.KillMonster("judgement", "r10a", "Left", "Ultra Aeacus", "Aeacus Empowered", 50, false, publicRoom: true);

            Core.EquipClass(ClassType.Farm);
            //Adv.BestGear(GenericGearBoost.dmgAll);
            Core.KillMonster("revenant", "r2", "Left", "*", "Tethered Soul", 300, false);
            Core.KillMonster("shadowrealmpast", "Enter", "Spawn", "*", "Darkened Essence", 500, false);
            //Adv.BestGear(RacialGearBoost.Undead);
            Core.KillMonster("necrodungeon", "r22", "Down", "*", "Dracolich Contract", 1000, false, publicRoom: true);

            Core.EnsureComplete(6897);
            Bot.Wait.ForPickup("Revenant's Spellscroll");
            Core.Logger($"Completed x{i++}");

            if (forquest)
                return;
        }
    }

    //Legion Fealty 2
    public void ConquestWreath(int quant = 6, bool forquest = false)
    {
        if (!Story.QuestProgression(6898))
            RevenantSpellscroll(1, true);

        if ((forquest && Story.QuestProgression(6898)) || Core.CheckInventory("Conquest Wreath", quant))
            return;

        Legion.JoinLegion();

        Core.AddDrop("Legion Token");
        Core.AddDrop(LR);
        Core.AddDrop(LF2);

        int i = 1;
        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger("Conquest Wreath", quant);
        Bot.Quests.UpdateQuest(4614);
        while (!Bot.ShouldExit && ((forquest && !Story.QuestProgression(6898)) || !Core.CheckInventory("Conquest Wreath", quant)))
        {
            Core.EnsureAccept(6898);
            //Adv.BestGear(RacialGearBoost.Undead);
            Core.KillMonster("mummies", "Enter", "Spawn", "*", "Ancient Cohort Conquered", 500, false);
            Core.KillMonster("doomvault", "r1", "Right", "*", "Grim Cohort Conquered", 500, false);
            //Adv.BestGear(RacialGearBoost.Human);
            Core.KillMonster("wrath", "r5", "Left", "*", "Pirate Cohort Conquered", 500, false);
            //Adv.BestGear(RacialGearBoost.Undead);
            Core.KillMonster("doomwar", "r6", "Left", "*", "Battleon Cohort Conquered", 500, false);
            Core.KillMonster("overworld", "Enter", "Spawn", "*", "Mirror Cohort Conquered", 500, false);
            Core.KillMonster("deathpits", "r1", "Left", "*", "Darkblood Cohort Conquered", 500, false);
            Core.KillMonster("maxius", "r2", "Left", "*", "Vampire Cohort Conquered", 500, false);
            Core.KillMonster("curseshore", "Enter", "Spawn", "*", "Spirit Cohort Conquered", 500, false);
            //Adv.BestGear(RacialGearBoost.Dragonkin);
            Core.KillMonster("dragonbone", "Enter", "Spawn", "*", "Dragon Cohort Conquered", 500, false);
            //Adv.BestGear(RacialGearBoost.Undead);
            Core.KillMonster("doomwood", "r6", "Right", "*", "Doomwood Cohort Conquered", 500, false);

            Core.EnsureComplete(6898);
            Bot.Wait.ForPickup("Conquest Wreath");
            Core.Logger($"Completed x{i++}");

            if (forquest)
                return;
        }
    }

    //Legion Fealty 3
    public void ExaltedCrown(int quant = 10, bool forquest = false)
    {
        if (!Story.QuestProgression(6899))
            ConquestWreath(1, true);

        if ((forquest && Story.QuestProgression(6899)) || Core.CheckInventory("Exalted Crown", quant))
            return;

        Legion.JoinLegion();
        Seraph.SeraphicWar_Questline();

        Core.AddDrop("Legion Token");
        Core.AddDrop(LR);
        Core.AddDrop(LF3);

        int i = 1;
        Core.FarmingLogger("Exalted Crown", quant);
        while (!Bot.ShouldExit && ((forquest && !Story.QuestProgression(6899)) || !Core.CheckInventory("Exalted Crown", quant)))
        {
            Core.EnsureAccept(6899);

            Adv.BuyItem("underworld", 216, "Hooded Legion Cowl");

            Legion.FarmLegionToken(4000);

            Legion.ApprovalAndFavor(0, 300);

            Legion.EmblemofDage(1);

            Legion.DiamondTokenofDage(30);

            Legion.DarkToken(100);

            Core.EnsureComplete(6899);
            Bot.Wait.ForPickup("Exalted Crown");
            Core.Logger($"Completed x{i++}");

            if (forquest)
                return;
        }
    }
}
