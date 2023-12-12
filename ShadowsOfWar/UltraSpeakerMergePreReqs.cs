/*
name: Ultra Speaker Merge PreReqs
description: Gets the prerequisites for the Ultra Speaker merge.
tags: ultra speaker merge, ultra malgor merge, RGoW, goddess of war
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
using Skua.Core.Options;

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

    public string OptionsStorage = "UltraSpeakerPreReqs";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        // Options for Armors
        new Option<bool>("GetGoddessOfWar", "Get Goddess Of War armor[REQ]", "to get Goddess Of War armor or not.", true),
        new Option<bool>("GetAscendedBladeOfAwe", "Get Ascended Blade of Awe armor", "to get Ascended Blade of Awe armor or not.", false),
        // ...
        // Options for Weapons
        new Option<bool>("GetWarBladeOfCourage", "Get War Blade of Courage[REQ]", "to get War Blade of Courage or not.", true),
        new Option<bool>("GetWarBladesOfCourage", "Get War Blades of Courage", "to get War Blades of Courage or not.", false),
        new Option<bool>("GetWarBladeOfPower", "Get War Blade of Power[REQ]", "to get War Blade of Power or not.", true),
        new Option<bool>("GetWarBladesOfPower", "Get War Blades of Power", "to get War Blades of Power or not.", false),
        new Option<bool>("GetWarBladeOfSpeed", "Get War Blade of Speed[REQ]", "to get War Blade of Speed or not.", true),
        new Option<bool>("GetWarBladesOfSpeed", "Get War Blades of Speed", "to get War Blades of Speed or not.", false),
        new Option<bool>("GetWarBladeOfStrength", "Get War Blade of Strength[REQ]", "to get War Blade of Strength or not.", true),
        new Option<bool>("GetWarBladesOfStrength", "Get War Blades of Strength", "to get War Blades of Strength or not.", false),
        new Option<bool>("GetWarBladeOfWisdom", "Get War Blade of Wisdom[REQ]", "to get War Blade of Wisdom or not.", true),
        new Option<bool>("GetWarBladesOfWisdom", "Get War Blades of Wisdom", "to get War Blades of Wisdom or not.", false),
        // ...
        // Options for Gauntlets
        new Option<bool>("GetGoddessOfWarGauntlet", "Get Goddess of War Gauntlet", "to get Goddess of War Gauntlet or not.", false),
        new Option<bool>("GetGoddessOfWarGauntlets", "Get Goddess of War Gauntlets", "to get Goddess of War Gauntlets or not.", false),
        // ...
        // Options for Back Items
        new Option<bool>("GetGoddessOfWarPrestigeCloak", "Get Goddess Of War Prestige Cloak[REQ]", "to get Goddess Of War Prestige Cloak or not.", true),
        new Option<bool>("GetMalgorsHalfWing", "Get Malgor's Half-Wing", "to get Malgor's Half-Wing or not.", false),
        // ...
        // Options for Helmets
        new Option<bool>("GetDragonMalgorVisage", "Get Dragon Malgor Visage", "to get Dragon Malgor Visage or not.", false),
        new Option<bool>("GetGoddessOfWarHelm", "Get Goddess Of War Helm", "to get Goddess Of War Helm or not.", false),
        // ...
        // Options for Ground Runes
        new Option<bool>("GetGoddessOfWarBattlefield", "Get Goddess Of War Battlefield Rune", "to get Goddess Of War Battlefield Rune or not.", false),
        // ...
        
        CoreBots.Instance.SkipOptions,
    };


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        GetPrereqs();

        Core.SetOptions(false);
    }

    public void GetPrereqs()
    {
        // Check Default Options
        CheckAndSetDefaultOptions();

        // Initialize counters
        int AcquiescenceCount = 0;
        int ElementalCoreCount = 0;

        // Complete Core SoW tasks
        SoW.CompleteCoreSoW();

        // Armors
        if (Bot.Config!.Get<bool>("GetGoddessOfWar"))
        {
            if (!Core.CheckInventory("Goddess Of War", toInv: false))
            {
                Core.Logger("Getting prerequisites for 'Goddess Of War' armor...");
                // Prerequisites for acquiring "Goddess Of War" armor
                SOWM.DragonsTear();
                UBLOD.PurifiedUndeadDragonEssence(3);
                ADG.AscendedGear("Ascended Blade of Awe");
                DFO.DragonFableOriginsAll();
                Core.EquipClass(ClassType.Solo);
                Core.AddDrop("Ice Shard");
                Core.RegisterQuests(6311);
                // Ice Shard - 43712
                while (!Bot.ShouldExit && !Core.CheckInventory(43712, 50))
                    Core.HuntMonster("northmountain", "Izotz", "Ice Crystal");
                HDK.ADKFalls();
            }
        }

        // Weapons
        if (Bot.Config!.Get<bool>("GetWarBladeOfCourage"))
        {
            if (!Core.CheckInventory("War Blade of Courage", toInv: false))
            {
                Core.Logger("Getting prerequisites for 'War Blade of Courage'...");
                // Prerequisites for acquiring "War Blade of Courage"
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
        }

        // Goddess of War Gauntlets
        if (Bot.Config!.Get<bool>("GetGoddessOfWarGauntlet"))
        {
            if (!Core.CheckInventory("Goddess of War Gauntlet", toInv: false))
            {
                Core.Logger("Getting prerequisites for 'Goddess of War Gauntlet'...");
                // Prerequisites for acquiring "Goddess of War Gauntlet"
                if (Core.CheckInventory("Goddess of War Gauntlets", toInv: false))
                    Core.Logger("Buyback Goddess of War Gauntlet smh");
                else
                {
                    WFE.WarfuryEmblemFarm(50);
                }
            }
            if (!Core.CheckInventory("Goddess of War Gauntlets", toInv: false))
                ElementalCoreCount += 3;
        }

        // Back Items
        if (Bot.Config!.Get<bool>("GetGoddessOfWarPrestigeCloak") || Bot.Config!.Get<bool>("GetMalgorsHalfWing"))
        {
            Core.Logger("Getting prerequisites for Back Items...");
            if (!Core.CheckInventory("Goddess Of War Prestige Cloak") && !Core.CheckInventory("Goddess of War Cloak"))
                AcquiescenceCount += 10;
        }
        if (!Core.CheckInventory("Malgor's Half-Wing"))
            ElementalCoreCount += 3;
        if (!Core.CheckInventory("Malgor's Half-Wing and Tail "))
            ElementalCoreCount += 3;

        // Helmets
        if (Bot.Config!.Get<bool>("GetDragonMalgorVisage") || Bot.Config!.Get<bool>("GetGoddessOfWarHelm"))
        {
            Core.Logger("Getting prerequisites for Helmets...");
            if (!Core.CheckInventory("Dragon Malgor Visage"))
                ElementalCoreCount += 3;
            if (!Core.CheckInventory("Goddess Of War Helm"))
                ElementalCoreCount += 3;
        }

        // Ground Runes
        if (Bot.Config!.Get<bool>("GetGoddessOfWarBattlefield"))
        {
            Core.Logger("Getting prerequisites for Ground Runes...");
            if (!Core.CheckInventory("Goddess Of War Battlefield"))
                ElementalCoreCount += 15;
        }

        // Perform actions based on accumulated counts
        SOWM.Acquiescence(AcquiescenceCount);
        SOWM.ElementalCore(ElementalCoreCount);
    }


    // Check and set options that are turned off by default
    void CheckAndSetDefaultOptions()
    {
        if (!Bot.Config!.Get<bool>("GetGoddessOfWar"))
        {
            Core.Logger("Get Goddess Of War armor[REQ] is turned off. Turning it on to continue.");
            Bot.Config!.Set("GetGoddessOfWar", true);
        }

        if (!Bot.Config!.Get<bool>("GetWarBladeOfCourage"))
        {
            Core.Logger("Get War Blade of Courage[REQ] is turned off. Turning it on to continue.");
            Bot.Config!.Set("GetWarBladeOfCourage", true);
        }

        if (!Bot.Config!.Get<bool>("GetWarBladeOfPower"))
        {
            Core.Logger("Get War Blade of Power[REQ] is turned off. Turning it on to continue.");
            Bot.Config!.Set("GetWarBladeOfPower", true);
        }

        if (!Bot.Config!.Get<bool>("GetWarBladeOfSpeed"))
        {
            Core.Logger("Get War Blade of Speed[REQ] is turned off. Turning it on to continue.");
            Bot.Config!.Set("GetWarBladeOfSpeed", true);
        }

        if (!Bot.Config!.Get<bool>("GetWarBladeOfStrength"))
        {
            Core.Logger("Get War Blade of Strength[REQ] is turned off. Turning it on to continue.");
            Bot.Config!.Set("GetWarBladeOfStrength", true);
        }

        if (!Bot.Config!.Get<bool>("GetWarBladeOfWisdom"))
        {
            Core.Logger("Get War Blade of Wisdom[REQ] is turned off. Turning it on to continue.");
            Bot.Config!.Set("GetWarBladeOfWisdom", true);
        }

        if (!Bot.Config!.Get<bool>("GetGoddessOfWarPrestigeCloak"))
        {
            Core.Logger("Get Goddess Of War Prestige Cloak[REQ] is turned off. Turning it on to continue.");
            Bot.Config!.Set("GetGoddessOfWarPrestigeCloak", true);
        }
    }
}
