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
    public CoreFarms Farm = new CoreFarms();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreStory Story = new CoreStory();
    public CoreLegion Legion = new CoreLegion();
    public InfiniteLegionDC ILDC = new InfiniteLegionDC();
    public SeraphicWar_Story Seraph = new SeraphicWar_Story();

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
        if (Core.CheckInventory("Legion Revenant"))
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

        Core.ChainComplete(6900);
        Bot.Wait.ForPickup("Legion Revenant");
        if (rankUpClass)
            Adv.rankUpClass("Legion Revenant");
    }

    //Legion Fealty 1
    public void RevenantSpellscroll(int quant = 20)
    {
        if (Core.CheckInventory("Revenant's Spellscroll", quant))
            return;

        Legion.JoinLegion();

        bool hasDarkCaster = false;
        if (Core.CheckInventory(new[] { "Love Caster", "Legion Revenant" }, any: true))
            hasDarkCaster = true;
        else
        {
            List<InventoryItem> InventoryData = Bot.Inventory.Items;
            foreach (InventoryItem Item in InventoryData)
            {
                if (Item.Name.Contains("Dark Caster") && Item.Category == ItemCategory.Class)
                {
                    hasDarkCaster = true;
                    break;
                }
            }

            if (!hasDarkCaster)
            {
                List<InventoryItem> BankData = Bot.Bank.Items;
                foreach (InventoryItem Item in BankData)
                {
                    if (Item.Name.Contains("Dark Caster") && Item.Category == ItemCategory.Class)
                    {
                        hasDarkCaster = true;
                        Core.Unbank(Item.Name);
                        break;
                    }
                }
            }
        }
        if (!hasDarkCaster)
        {
            ILDC.GetILDC(false);
        }

        Core.AddDrop("Legion Token");
        Core.AddDrop(LR);
        Core.AddDrop(LF1);

        Farm.EvilREP();

        int i = 1;
        Core.Logger($"Farming {quant} Revenant's Spellscroll");
        Bot.Quests.UpdateQuest(2060);
        while (!Bot.ShouldExit && !Core.CheckInventory("Revenant's Spellscroll", quant))
        {
            Core.EnsureAccept(6897);

            Core.EquipClass(ClassType.Solo);
            Adv.BestGear(GearBoost.Undead);
            Core.KillMonster("judgement", "r10a", "Left", "Ultra Aeacus", "Aeacus Empowered", 50, false, publicRoom: true);

            Core.EquipClass(ClassType.Farm);
            Adv.BestGear(GearBoost.dmgAll);
            Core.KillMonster("revenant", "r2", "Left", "*", "Tethered Soul", 300, false);
            Core.KillMonster("shadowrealmpast", "Enter", "Spawn", "*", "Darkened Essence", 500, false);
            Adv.BestGear(GearBoost.Undead);
            Core.KillMonster("necrodungeon", "r22", "Down", "*", "Dracolich Contract", 1000, false, publicRoom: true);

            Core.EnsureComplete(6897);
            Bot.Drops.Pickup("Revenant's Spellscroll");
            Core.Logger($"Completed x{i++}");
        }
    }

    //Legion Fealty 2
    public void ConquestWreath(int quant = 6)
    {
        if (Core.CheckInventory("Conquest Wreath", quant))
            return;

        Legion.JoinLegion();

        Core.AddDrop("Legion Token");
        Core.AddDrop(LR);
        Core.AddDrop(LF2);

        int i = 1;
        Core.EquipClass(ClassType.Farm);
        Core.Logger($"Farming {quant} Conquest Wreath");
        Bot.Quests.UpdateQuest(4614);
        while (!Bot.ShouldExit && !Core.CheckInventory("Conquest Wreath", quant))
        {
            Core.EnsureAccept(6898);
            Adv.BestGear(GearBoost.Undead);
            Core.KillMonster("mummies", "Enter", "Spawn", "*", "Ancient Cohort Conquered", 500, false);
            Core.KillMonster("doomvault", "r1", "Right", "*", "Grim Cohort Conquered", 500, false);
            Adv.BestGear(GearBoost.Human);
            Core.KillMonster("wrath", "r5", "Left", "*", "Pirate Cohort Conquered", 500, false);
            Adv.BestGear(GearBoost.Undead);
            Core.KillMonster("doomwar", "r6", "Left", "*", "Battleon Cohort Conquered", 500, false);
            Core.KillMonster("overworld", "Enter", "Spawn", "*", "Mirror Cohort Conquered", 500, false);
            Core.KillMonster("deathpits", "r1", "Left", "*", "Darkblood Cohort Conquered", 500, false);
            Core.KillMonster("maxius", "r2", "Left", "*", "Vampire Cohort Conquered", 500, false);
            Core.KillMonster("curseshore", "Enter", "Spawn", "*", "Spirit Cohort Conquered", 500, false);
            Adv.BestGear(GearBoost.Dragonkin);
            Core.KillMonster("dragonbone", "Enter", "Spawn", "*", "Dragon Cohort Conquered", 500, false);
            Adv.BestGear(GearBoost.Undead);
            Core.KillMonster("doomwood", "r6", "Right", "*", "Doomwood Cohort Conquered", 500, false);

            Core.EnsureComplete(6898);
            Bot.Drops.Pickup("Conquest Wreath");
            Core.Logger($"Completed x{i++}");
        }
    }

    //Legion Fealty 3
    public void ExaltedCrown(int quant = 10)
    {
        if (Core.CheckInventory("Exalted Crown", quant))
            return;

        Legion.JoinLegion();
        Seraph.SeraphicWar_Questline();

        Core.AddDrop("Legion Token");
        Core.AddDrop(LR);
        Core.AddDrop(LF3);

        int i = 1;
        Core.Logger($"Farming {quant} Exalted Crown");
        while (!Bot.ShouldExit && !Core.CheckInventory("Exalted Crown", quant))
        {
            Core.EnsureAccept(6899);

            Farm.Gold(500000);
            Core.BuyItem("underworld", 216, "Hooded Legion Cowl");

            Legion.FarmLegionToken(4000);

            Legion.ApprovalAndFavor(0, 300);

            Legion.EmblemofDage(1);

            Legion.DiamondTokenofDage(30);

            Legion.DarkToken(100);

            Core.EnsureComplete(6899);
            Bot.Drops.Pickup("Exalted Crown");
            Core.Logger($"Completed x{i++}");
        }
    }
}
