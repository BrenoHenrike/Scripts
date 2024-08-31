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
//cs_include Scripts/Other/Armor/FireChampionsArmor.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/BeetleQuests.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiege.cs
//cs_include Scripts/Nation/Various/DragonBlade[mem].cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiegeMerge.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
//cs_include Scripts/Good/GearOfAwe/ArmorOfAwe.cs
//cs_include Scripts/Good/GearOfAwe/HelmOfAwe.cs
//cs_include Scripts/Good/SilverExaltedPaladin.cs
//cs_include Scripts/Other/Weapons/FortitudeAndHubris.cs
//cs_include Scripts/Other/Weapons/ShadowReaperOfDoom.cs
//cs_include Scripts/Story/J6Saga.cs
//cs_include Scripts/Story/Nation/Bamboozle.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Story/7DeadlyDragons/Extra/HatchTheEgg.cs
//cs_include Scripts/Other/MysteriousEgg.cs
//cs_include Scripts/Other/ShadowDragonDefender.cs
//cs_include Scripts/Evil/ADK.cs
//cs_include Scripts/Story/DjinnGate.cs
//cs_include Scripts/Other/Armor/MalgorsArmorSet.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/YamiNoRonin/CoreYnR.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/DeadLinesMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/ShadowflameFinaleMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/TimekeepMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/StreamwarMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/WorldsCoreMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/ManaCradleMerge.cs
//cs_include Scripts/Legion/SwordMaster.cs
//cs_include Scripts/Good/GearOfAwe/Awescended.cs
//cs_include Scripts/Story/Lair.cs

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
// using Skua.Core.Options;

public class UltraSpeakerMergePreReqs
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
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
    public FireChampionsArmor FCA = new();
    public BeetleQuests BeetleQuests = new();
    public Awescended Awescended = new();
    public CoreHollowbornPaladin CHBP = new();
    public MalgorsArmorSet MalgorsArmorSet = new();

    #region not needed anymore jsut get goddesofwar
    // public string OptionsStorage = "UltraSpeakerPreReqs";
    // public bool DontPreconfigure = true;
    // public List<IOption> Options = new()
    // {
    //     // Options for Armors
    //     new Option<bool>("GetGoddessOfWar", "Get Goddess Of War armor[REQ]", "to get Goddess Of War armor or not.", true),
    //     new Option<bool>("GetAscendedBladeOfAwe", "Get Ascended Blade of Awe armor", "to get Ascended Blade of Awe armor or not.", false),
    //     // ...
    //     // Options for Weapons
    //     new Option<bool>("GetWarBladeOfCourage", "Get War Blade of Courage[REQ]", "to get War Blade of Courage or not.", true),
    //     new Option<bool>("GetWarBladesOfCourage", "Get War Blades of Courage", "to get War Blades of Courage or not.", false),
    //     new Option<bool>("GetWarBladeOfPower", "Get War Blade of Power[REQ]", "to get War Blade of Power or not.", true),
    //     new Option<bool>("GetWarBladesOfPower", "Get War Blades of Power", "to get War Blades of Power or not.", false),
    //     new Option<bool>("GetWarBladeOfSpeed", "Get War Blade of Speed[REQ]", "to get War Blade of Speed or not.", true),
    //     new Option<bool>("GetWarBladesOfSpeed", "Get War Blades of Speed", "to get War Blades of Speed or not.", false),
    //     new Option<bool>("GetWarBladeOfStrength", "Get War Blade of Strength[REQ]", "to get War Blade of Strength or not.", true),
    //     new Option<bool>("GetWarBladesOfStrength", "Get War Blades of Strength", "to get War Blades of Strength or not.", false),
    //     new Option<bool>("GetWarBladeOfWisdom", "Get War Blade of Wisdom[REQ]", "to get War Blade of Wisdom or not.", true),
    //     new Option<bool>("GetWarBladesOfWisdom", "Get War Blades of Wisdom", "to get War Blades of Wisdom or not.", false),
    //     // ...
    //     // Options for Gauntlets
    //     new Option<bool>("GetGoddessOfWarGauntlet", "Get Goddess of War Gauntlet", "to get Goddess of War Gauntlet or not.", false),
    //     new Option<bool>("GetGoddessOfWarGauntlets", "Get Goddess of War Gauntlets", "to get Goddess of War Gauntlets or not.", false),
    //     // ...
    //     // Options for Back Items
    //     new Option<bool>("GetGoddessOfWarPrestigeCloak", "Get Goddess Of War Prestige Cloak[REQ]", "to get Goddess Of War Prestige Cloak or not.", true),
    //     new Option<bool>("GetMalgorsHalfWing", "Get Malgor's Half-Wing", "to get Malgor's Half-Wing or not.", false),
    //     // ...
    //     // Options for Helmets
    //     new Option<bool>("GetDragonMalgorVisage", "Get Dragon Malgor Visage", "to get Dragon Malgor Visage or not.", false),
    //     new Option<bool>("GetGoddessOfWarHelm", "Get Goddess Of War Helm", "to get Goddess Of War Helm or not.", false),
    //     // ...
    //     // Options for Ground Runes
    //     new Option<bool>("GetGoddessOfWarBattlefield", "Get Goddess Of War Battlefield Rune", "to get Goddess Of War Battlefield Rune or not.", false),
    //     // ...

    //     CoreBots.Instance.SkipOptions,
    // };

    #endregion not needed anymore jsut get goddesofwar

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[]
        {
            //End Goal:
            "Radiant Goddess of War",

            // Non-Bottable
            "Malgor Insignia",
            "Elemental Core",
            "Fire Avatar's Favor ",
            "Acquiescence",
            
            //last step items:
            "Goddess Of War",

            //2nd to last step items
            "Goddess Of War Prestige Cloak",

            // 2nd step items:
            "Goddess Of War Blades",
            "Goddess of War Cloak",

            //begining steps:
            "War Blade of Strength",
            "War Blade of Courage",
            "War Blade of Power",
            "War Blade of Speed",
            "War Blade of Wisdom",

            //add more here
        });

        Core.SetOptions();
        GetPrereqs();

        Core.SetOptions(false);
    }

    public void GetPrereqs()
    {

        // Initialize counters
        int AcquiescenceCount = 0;
        int ElementalCoreCount = 0;
        int InsigniasCount = 0;

        // Complete Core SoW tasks
        SoW.CompleteCoreSoW();

        #region GoddessofWar rewrote
        // Armors
        if (!Core.CheckInventory("Goddess Of War"))
        {
            Core.Logger("Getting prerequisites for 'Goddess Of War' armor...");
            // Prerequisites for acquiring "Goddess Of War" armor
            UBLOD.PurifiedUndeadDragonEssence(3);
            // Ice Shard - 43712
            if (!Core.CheckInventory(43712, 50))
            {
                Core.EquipClass(ClassType.Solo);
                Core.AddDrop("Ice Shard");
                Core.RegisterQuests(6311);
                while (!Bot.ShouldExit && !Core.CheckInventory(43712, 50))
                    Core.HuntMonster("northmountain", "Izotz", "Ice Crystal");
                Core.CancelRegisteredQuests();
            }
            SOWM.DragonsTear();
            ADG.AscendedGear("Ascended Blade of Awe");
            DFO.DragonFableOriginsAll();
            HDK.ADKFalls(true);
            GoddessOfWarPrestigeCloak();
            #region GoddessOfWarPrestigeCloak
            void GoddessOfWarPrestigeCloak()
            {
                GoddessOfWarBlades();
                GoddessofWarCloak();
                if (Core.CheckInventory(new[] { "Goddess Of War Blades", "Goddess of War Cloak" }))
                    Core.BuyItem("ultraspeaker", 2248, 72921, shopItemID: 11443);
                else Core.Logger("farmed all PreFarmable (non-insignia) items for \"GoddessOfWarPrestigeCloak\"");
            }

            if (!Core.CheckInventory("Radiant Goddess of War") && Core.CheckInventory("Goddess Of War"))
            {
                Core.AddDrop("Radiant Goddess of War");

                Core.EnsureAccept(9184);

                Farm.Experience();
                FCA.GetFireChampsArmor();
                BeetleQuests.GetBeetleWarlord();
                Awescended.GetAwe();
                CHBP.GetSpecific("Classic Hollowborn Paladin Armor");
                MalgorsArmorSet.GetSet(false, new[] { "Malgor the ShadowLord" });

                if (Core.CheckInventory(new[]
                 {
                    "Empowered Drakath Armor",
                    "Fire Champion's Armor",
                    "Void Beetle Warlord",
                    "Malgor the ShadowLord",
                    "Classic Hollowborn Paladin Armor",
                    "Awescended",
                    }))
                    Core.EnsureComplete(9184);
                else
                    foreach (string item in new[] { "Empowered Drakath Armor", "Fire Champion's Armor", "Void Beetle Warlord", "Malgor the ShadowLord", "Classic Hollowborn Paladin Armor", "Awescended" })
                        Core.Logger($"Missing {item} to complete the quest.");
                Bot.Wait.ForPickup("Radiant Goddess of War");
                if (Core.CheckInventory("Radiant Goddess of War"))
                    Core.Logger("Congrats!!!!");
            }
            void GoddessOfWarBlades()
            {
                if (Core.CheckInventory("Goddess Of War Blades"))
                {
                    Core.Logger("\"Goddess Of War Blades\" owned.");
                    return;
                }

                string[] WarBlades =
                {
                        "War Blade of Courage",
                        "War Blade of Power",
                        "War Blade of Speed",
                        "War Blade of Strength",
                        "War Blade of Wisdom"
                    };

                //Story Requirements:
                Core.Logger("Doing Story Req. for some items.");
                Core.Logger("if more quests are locked, let tato know (for this script)");
                SoC.LagunaBeach();

                foreach (string Blade in WarBlades)
                {
                    if (Core.CheckInventory(Blade))
                    {
                        // If the blade is in the inventory, skip to the next iteration
                        continue;
                    }

                    // Continue to next blade code here
                    switch (Blade)
                    {
                        case "War Blade of Courage":
                            BLOD.BrilliantAura(50);
                            BLOD.BlindingAura(1);
                            Core.Logger("Adding 7 to the Insignias Count");
                            InsigniasCount += 7;
                            Core.Logger("Adding 10 to the Acquiescence Count");
                            AcquiescenceCount += 10;
                            break;

                        case "War Blade of Power":
                            //Dragon Scale(1)
                            Core.AddDrop(11475);
                            while (!Core.CheckInventory(11475, 30))
                                Core.KillMonster("lair", "Hole", "Center", "*", isTemp: false, log: false);
                            DSG.EnchantedScaleandClaw(250, 0);

                            Core.Logger("Adding 7 to the Insignias Count");
                            InsigniasCount += 7;
                            Core.Logger("Adding 10 to the Acquiescence Count");
                            AcquiescenceCount += 10;
                            break;

                        case "War Blade of Speed":
                            Core.EquipClass(ClassType.Farm);
                            Core.HuntMonster("shadowfallwar", "Skeletal Fire Mage", "Ultimate Darkness Gem", 75, isTemp: false);
                            Core.EquipClass(ClassType.Solo);
                            Core.KillMonster("shadowattack", "Boss", "Left", "Death", "Death's Oversight", 5, false);

                            Core.Logger("Adding 7 to the Insignias Count");
                            InsigniasCount += 7;
                            Core.Logger("Adding 10 to the Acquiescence Count");
                            AcquiescenceCount += 10;
                            break;

                        case "War Blade of Strength":
                            SoW.Tyndarius();

                            Core.AddDrop("Fire Avatar's Favor");
                            Core.EquipClass(ClassType.Farm);

                            Core.RegisterQuests(8244);
                            while (!Bot.ShouldExit && !Core.CheckInventory("Fire Avatar's Favor", 25))
                            {
                                Core.KillMonster("fireavatar", "r4", "Right", "*", "Onslaught Defeated", 6);
                                Core.KillMonster("fireavatar", "r6", "Left", "*", "Elemental Defeated", 6);

                                Bot.Wait.ForPickup("Fire Avatar's Favor");
                            }
                            Core.CancelRegisteredQuests();

                            Core.Logger("Adding 7 to the Insignias Count");
                            InsigniasCount += 7;
                            Core.Logger("Adding 10 to the Acquiescence Count");
                            AcquiescenceCount += 10;
                            Core.Logger("Adding 25 to the ElementalCore Count");
                            ElementalCoreCount += 25;
                            break;

                        case "War Blade of Wisdom":
                            Core.AddDrop("Fragment of the Queen", "ShadowChaos Mote");
                            Core.EquipClass(ClassType.Solo);
                            Bot.Quests.UpdateQuest(8094);
                            Core.HuntMonster("transformation", "Queen of Monsters", "Fragment of the Queen", 13, false);

                            Core.EquipClass(ClassType.Farm);
                            Core.RegisterQuests(7700);
                            Core.HuntMonster("lagunabeach", "Flying Fisheye", "ShadowChaos Mote", 250, false);
                            Bot.Wait.ForPickup("ShadowChaos Mote");
                            Core.CancelRegisteredQuests();

                            Core.Logger("Adding 7 to the Insignias Count");
                            InsigniasCount += 7;
                            Core.Logger("Adding 10 to the Acquiescence Count");
                            AcquiescenceCount += 10;
                            break;
                    }
                }
            }
            void GoddessofWarCloak()
            {
                if (Core.CheckInventory("Goddess of War Cloak"))
                {
                    Core.Logger("\"Goddess Of War Blades\" owned.");
                    return;
                }

                Core.Logger("adding 10 to the Acquiescence Count");
                AcquiescenceCount += 10;
                Core.Logger("adding 10 to the Insignias Count");
                InsigniasCount += 10;
            }
            #endregion GoddessOfWarPrestigeCloak
            #endregion GoddessofWar rewrote

            #region RGRoW item Check
            // Core.Logger("Checking items before buying Goddess Of War");

            // // Define required items and their quantities
            // var requiredItems = new Dictionary<int, int> { { 55901, 3 }, { 43712, 50 }, { 65841, 1 }, { 72921, 1 }, { 76482, 1 } };

            // // Check if all required items are owned
            // var allItemsOwned = requiredItems.All(item =>
            // {
            //     // Find the item in the inventory
            //     ItemBase? inventoryItem = Bot.Inventory.Items.Find(x => x.ID == item.Key);

            //     // Log the inventory status of the item
            //     var isItemOwned = Core.CheckInventory(inventoryItem?.Name, item.Value);

            //     // Log the inventory status
            //     Core.Logger(!isItemOwned
            //         ? $"Missing {inventoryItem!.Name}, {item.Value - Bot.Inventory.GetQuantity(inventoryItem!.Name)}"
            //         : $"{inventoryItem!.Name} owned {Bot.Inventory.GetQuantity(inventoryItem!.Name) / item.Value}");

            //     // Return whether the item is owned in the required quantity
            //     return isItemOwned;
            // });

            SOWM.Acquiescence(AcquiescenceCount);
            SOWM.ElementalCore(ElementalCoreCount);

            // Core.Logger(allItemsOwned ? "All items owned. Attempting to buy Goddess Of War." : "Cannot buy Goddess Of War. Missing items");
            // if (!allItemsOwned)
            //     // Log the overall status based on item ownership
            //     Core.Logger($"Malgor Insignia Needed: {InsigniasCount - Bot.Inventory.GetQuantity("Malgor Insignia")}");

            // If all required items are owned, proceed to buy the specified item
            Core.BuyItem("ultraspeaker", 2248, 72921, shopItemID: 11443); //remove the else here <<

        }
        #endregion RGRoW item Check

        // #region Radiant Goddess of War quest
        // #endregion Radiant Goddess of War quest


    }

    #region Commented out


    // Check Default Options
    // CheckAndSetDefaultOptions();


    // #region WarBladeOfCourage
    // // Weapons
    // if (Bot.Config!.Get<bool>("GetWarBladeOfCourage"))
    // {
    //     if (!Core.CheckInventory("War Blade of Courage", toInv: false))
    //     {
    //         Core.Logger("Getting prerequisites for 'War Blade of Courage'...");
    //         // Prerequisites for acquiring "War Blade of Courage"
    //         if (Core.CheckInventory("Goddess Of War Blades", toInv: false) || Core.CheckInventory("War Blades of Courage", toInv: false))
    //             Core.Logger("Buyback War Blade of Courage smh");
    //         else
    //         {
    //             BLOD.BrilliantAura(50);
    //             BLOD.BlindingAura(1);
    //             Core.Logger("adding 10 to the Acquiescence Count");
    //             AcquiescenceCount += 10;
    //         }
    //     }
    //     if (!Core.CheckInventory("War Blades of Courage", toInv: false))
    //     {
    //         Core.Logger("adding 3 to the ElementalCore count");
    //         ElementalCoreCount += 3;
    //     }
    // }
    // #endregion WarBladeOfCourage

    // Goddess of War Gauntlets
    // if (Bot.Config!.Get<bool>("GetGoddessOfWarGauntlet"))
    // {
    //     if (!Core.CheckInventory("Goddess of War Gauntlet", toInv: false))
    //     {
    //         Core.Logger("Getting prerequisites for 'Goddess of War Gauntlet'...");
    //         // Prerequisites for acquiring "Goddess of War Gauntlet"
    //         if (Core.CheckInventory("Goddess of War Gauntlets", toInv: false))
    //             Core.Logger("Buyback Goddess of War Gauntlet smh");
    //         else
    //             WFE.WarfuryEmblemFarm(50);
    //     }
    //     if (!Core.CheckInventory("Goddess of War Gauntlets", toInv: false))
    //     {
    //         Core.Logger("adding 3 to the ElementalCore count");
    //         ElementalCoreCount += 3;
    //     }
    // }

    // // Helmets
    // if (Bot.Config!.Get<bool>("GetDragonMalgorVisage") || Bot.Config!.Get<bool>("GetGoddessOfWarHelm"))
    // {
    //     Core.Logger("Getting prerequisites for Helmets...");
    //     if (!Core.CheckInventory("Dragon Malgor Visage"))
    //     {
    //         Core.Logger("adding 3 to the ElementalCore count");
    //         ElementalCoreCount += 3;
    //     }
    //     if (!Core.CheckInventory("Goddess Of War Helm"))
    //     {
    //         Core.Logger("adding 3 to the ElementalCore count");
    //         ElementalCoreCount += 3;
    //     }
    // }

    // // Ground Runes
    // if (Bot.Config!.Get<bool>("GetGoddessOfWarBattlefield"))
    // {
    //     Core.Logger("Getting prerequisites for Ground Runes...");
    //     if (!Core.CheckInventory("Goddess Of War Battlefield"))
    //     {
    //         Core.Logger("adding 15 to the ElementalCore count");
    //         ElementalCoreCount += 15;
    //     }
    // }

    //split here

    // // Check and set options that are turned off by default
    // void CheckAndSetDefaultOptions()
    // {
    //     if (!Bot.Config!.Get<bool>("GetGoddessOfWar"))
    //     {
    //         Core.Logger("Get Goddess Of War armor[REQ] is turned off. Turning it on to continue.");
    //         Bot.Config!.Set("GetGoddessOfWar", true);
    //     }

    //     if (!Bot.Config!.Get<bool>("GetWarBladeOfCourage"))
    //     {
    //         Core.Logger("Get War Blade of Courage[REQ] is turned off. Turning it on to continue.");
    //         Bot.Config!.Set("GetWarBladeOfCourage", true);
    //     }

    //     if (!Bot.Config!.Get<bool>("GetWarBladeOfPower"))
    //     {
    //         Core.Logger("Get War Blade of Power[REQ] is turned off. Turning it on to continue.");
    //         Bot.Config!.Set("GetWarBladeOfPower", true);
    //     }

    //     if (!Bot.Config!.Get<bool>("GetWarBladeOfSpeed"))
    //     {
    //         Core.Logger("Get War Blade of Speed[REQ] is turned off. Turning it on to continue.");
    //         Bot.Config!.Set("GetWarBladeOfSpeed", true);
    //     }

    //     if (!Bot.Config!.Get<bool>("GetWarBladeOfStrength"))
    //     {
    //         Core.Logger("Get War Blade of Strength[REQ] is turned off. Turning it on to continue.");
    //         Bot.Config!.Set("GetWarBladeOfStrength", true);
    //     }

    //     if (!Bot.Config!.Get<bool>("GetWarBladeOfWisdom"))
    //     {
    //         Core.Logger("Get War Blade of Wisdom[REQ] is turned off. Turning it on to continue.");
    //         Bot.Config!.Set("GetWarBladeOfWisdom", true);
    //     }

    //     if (!Bot.Config!.Get<bool>("GetGoddessOfWarPrestigeCloak"))
    //     {
    //         Core.Logger("Get Goddess Of War Prestige Cloak[REQ] is turned off. Turning it on to continue.");
    //         Bot.Config!.Set("GetGoddessOfWarPrestigeCloak", true);
    //     }
    // }
    #endregion Commented out
}
