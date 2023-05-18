/*
name: Ultra Speaker Merge PreReqs
description: Gets the prerequisites for the Ultra Speaker merge.
tags: ultra speaker merge, ultra malgor merge
*/
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Chaos/AscendedDrakathGear.cs
//cs_include Scripts/Chaos/DrakathsArmor.cs
//cs_include Scripts/Evil/NSoD/CoreNSOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Evil/SepulchuresOriginalHelm.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Good/BLoD/2UltimateBlindingLightofDestiny.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Hollowborn/HollowbornDoomKnight/CoreHollowbornDoomKnight.cs
//cs_include Scripts/Hollowborn/HollowbornPaladin/CoreHollowbornPaladin.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Other/Classes/DragonslayerGeneral.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Other/FireAvatarFavorFarm.cs
//cs_include Scripts/Other/WarFuryEmblem.cs
//cs_include Scripts/ShadowsOfWar/CoreSoWMats.cs
//cs_include Scripts/Story/Artixpointe.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/ShadowsOfChaos/CoreSoC.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Story/TowerOfDoom.cs

using Skua.Core.Interfaces;

public class UltraSpeakerMergePreReqs
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private AscendedDrakathGear ADG = new();
    private CoreBLOD BLOD = new();
    private CoreHollowbornDoomKnight HDK = new();
    private CoreSoC SoC = new();
    private CoreSoW SoW = new();
    private CoreSoWMats SOWM = new();
    private DragonFableOrigins DFO = new();
    private DragonslayerGeneral DSG = new();
    private FireAvatarFavorFarm FAFF = new();
    private UltimateBLoD UBLOD = new();
    private WarfuryEmblem WFE = new();
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetPrereqs();

        Core.SetOptions(false);
    }

    public void GetPrereqs()
    {
        SoW.CompleteCoreSoW();

        int AcquiescenceCount = 0;
        int ElementalCoreCount = 0;

        // Armors
        if (!Core.CheckInventory("Goddess Of War", toInv: false))
        {
            SOWM.DragonsTear();
            UBLOD.PurifiedUndeadDragonEssence(3);
            ADG.AscendedGear("Ascended Blade of Awe");
            DFO.DragonFableOriginsAll();
            Core.EquipClass(ClassType.Solo);
            Core.RegisterQuests(6311);
            while (!Bot.ShouldExit && !Core.CheckInventory("Ice Shard", 50))
                Core.HuntMonster("northmountain", "Izotz", "Ice Crystal");
            HDK.ADKFalls();
        }

        // Weapons
        // War Blade/s of Courage
        if (!Core.CheckInventory("War Blade of Courage", toInv: false))
        {
            if (Core.CheckInventory("Goddess Of War Blades", toInv: false) || Core.CheckInventory("War Blades of Courage", toInv: false))
                Core.Logger("Buyback War Blade of Courage smh");
            else
            {
                BLOD.BrilliantAura(50);
                BLOD.BlindingAura(1);
                AcquiescenceCount += 10;
            }
        }
        if (!Core.CheckInventory("War Blades of Courage", toInv: false))
            ElementalCoreCount += 3;


        // War Blade of Power
        if (!Core.CheckInventory("War Blade of Power", toInv: false))
        {
            if (Core.CheckInventory("Goddess Of War Blades", toInv: false))
                Core.Logger("Buyback War Blade of Power smh");
            else
            {
                DSG.EnchantedScaleandClaw(250, 0);
                Core.AddDrop(11475);
                while (!Bot.ShouldExit && !Core.CheckInventory(11475, 30))
                    Core.KillMonster("lair", "Hole", "Center", "*", isTemp: false, log: false);
                Core.RemoveDrop(11475);
                AcquiescenceCount += 10;
            }
        }

        // War Blade/s of Speed
        if (!Core.CheckInventory("War Blade of Speed", toInv: false))
        {
            if (Core.CheckInventory("Goddess Of War Blades", toInv: false) || Core.CheckInventory("War Blades of Speed", toInv: false))
                Core.Logger("Buyback War Blade of Speed smh");
            else
            {
                Core.HuntMonster("shadowfallwar", "Skeletal Fire Mage", "Ultimate Darkness Gem", 50, isTemp: false);
                Core.EquipClass(ClassType.Solo);
                Core.KillMonster("shadowattack", "Boss", "Left", "Death", "Death's Oversight", 5, false);
                AcquiescenceCount += 10;
            }
        }
        if (!Core.CheckInventory("War Blades of Speed", toInv: false))
            ElementalCoreCount += 3;

        // War Blade of Strength
        if (!Core.CheckInventory("War Blade of Strength", toInv: false))
        {
            if (Core.CheckInventory("Goddess Of War Blades", toInv: false))
                Core.Logger("Buyback War Blade of Strength smh");
            else
            {
                FAFF.FAFavor(25);
                AcquiescenceCount += 10;
                ElementalCoreCount += 25;
            }
        }

        // War Blade of Wisdom
        if (!Core.CheckInventory("War Blade of Wisdom", toInv: false))
        {
            if (Core.CheckInventory("Goddess Of War Blades", toInv: false))
                Core.Logger("Buyback War Blade of Wisdom smh");
            else
            {
                Core.HuntMonster("transformation", "Queen of Monsters", "Fragment of the Queen", 13, false);

                SoW.ShadowWar();
                SoC.LagunaBeach();
                Core.EquipClass(ClassType.Farm);
                Core.RegisterQuests(7700);
                while (!Bot.ShouldExit && !Core.CheckInventory("ShadowChaos Mote", 250))
                    Core.HuntMonster("lagunabeach", "ShadowChaos Brigand", "Chaos-ShadowFlame Sample", 15);
                    AcquiescenceCount += 10;
            }
        }

        // Goddess of War Gauntlet/s
        if (!Core.CheckInventory("Goddess of War Gauntlet", toInv: false))
        {
            if (Core.CheckInventory("Goddess of War Gauntlets", toInv: false))
                Core.Logger("Buyback Goddess of War Gauntlet smh");
            else
            {
                WFE.WarfuryEmblemFarm(50);
            }
        }
        if (!Core.CheckInventory("Goddess of War Gauntlets", toInv: false))
        {
            ElementalCoreCount += 3;
        }

        // Back Items
        if (!Core.CheckInventory("Goddess Of War Prestige Cloak") && !Core.CheckInventory("Goddess of War Cloak"))
            AcquiescenceCount += 10;

        if (!Core.CheckInventory("Malgor's Half-Wing"))
            ElementalCoreCount += 3;

        if (!Core.CheckInventory("Malgor's Half-Wing and Tail "))
            ElementalCoreCount += 3;

        // Helmets
        if (!Core.CheckInventory("Dragon Malgor Visage"))
            ElementalCoreCount += 3;

        if (!Core.CheckInventory("Goddess Of War Helm"))
            ElementalCoreCount += 3;

        // Grounds
        if (!Core.CheckInventory("Goddess Of War Battlefield"))
            ElementalCoreCount += 15;

        SOWM.Acquiescence(AcquiescenceCount);
        SOWM.ElementalCore(ElementalCoreCount);
    }
}
