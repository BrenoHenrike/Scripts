/*
name: Unlock Forge Enhancements
description: This script will farm all forge enhancements.
tags: lacerate, smite, herosvaliance, arcanasconcerto, elysium, acheron, absolution, vainglory, avarice, penitence, lament, vim, examen, anima, pneuma, dauntless, praxis, ravenous
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Chaos/EternalDrakathSet.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Evil/NSoD/CoreNSOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Legion/YamiNoRonin/CoreYnR.cs

//cs_include Scripts/Chaos/DrakathsArmor.cs
//cs_include Scripts/Good/ArchPaladin.cs
//cs_include Scripts/Good/Paladin.cs
//cs_include Scripts/Evil/SepulchuresOriginalHelm.cs
//cs_include Scripts/Legion/HeadOfTheLegionBeast.cs
//cs_include Scripts/Other/WarFuryEmblem.cs
//cs_include Scripts/Other/MysteriousEgg.cs
//cs_include Scripts/Other/Classes/DragonslayerGeneral.cs
//cs_include Scripts/Other/Weapons/GoldenBladeOfFate.cs
//cs_include Scripts/Other/Weapons/PinkBladeofDestruction.cs
//cs_include Scripts/Other/Armor/FireChampionsArmor.cs
//cs_include Scripts/Other/Classes/DragonOfTime.cs
//cs_include Scripts/Darkon/Various/PrinceDarkonsPoleaxePreReqs.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/Story/SepulchureSaga/CoreSepulchure.cs
//cs_include Scripts/Story/Summer2015AdventureMap/CoreSummer.cs
//cs_include Scripts/Story/Borgars.cs
//cs_include Scripts/Story/StarSinc.cs
//cs_include Scripts/Story/TowerOfDoom.cs
//cs_include Scripts/Story/XansLair.cs
//cs_include Scripts/Story/Lair.cs
//cs_include Scripts/Good/GearOfAwe/Awescended.cs
//cs_include Scripts/Nation/AFDL/NulgathDemandsWork.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Nation/Various/GoldenHanzoVoid.cs
//cs_include Scripts/Nation/AFDL/WillpowerExtraction.cs
//cs_include Scripts/Good/GearOfAwe/ArmorOfAwe.cs
//cs_include Scripts/Good/GearOfAwe/HelmOfAwe.cs
//cs_include Scripts/Good/SilverExaltedPaladin.cs
//cs_include Scripts/Other/Weapons/FortitudeAndHubris.cs
//cs_include Scripts/Other/Weapons/ShadowReaperOfDoom.cs
//cs_include Scripts/Story/J6Saga.cs
//cs_include Scripts/Story/Nation/Bamboozle.cs
//cs_include Scripts/Story/7DeadlyDragons/Extra/HatchTheEgg.cs
//cs_include Scripts/Other/ShadowDragonDefender.cs
//cs_include Scripts/Evil/ADK.cs
//cs_include Scripts/Story/DjinnGate.cs
//cs_include Scripts/Story/ThirdSpell.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/Yokai.cs
//cs_include Scripts/Legion/SwordMaster.cs
//cs_include Scripts/Dailies/LordOfOrder.cs
//cs_include Scripts/Story/Nation/CitadelRuins.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
//cs_include Scripts/Other/Armor/MalgorsArmorSet.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/DeadLinesMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/ShadowflameFinaleMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/TimekeepMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/StreamwarMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/WorldsCoreMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/ManaCradleMerge.cs
//cs_include Scripts/ShadowsOfWar/CoreSoWMats.cs
//cs_include Scripts/Story/Nation/Fiendshard.cs

//cs_include Scripts/Nation/VHL/CoreVHL.cs
//cs_include Scripts/Story/Nation/VoidRefuge.cs
//cs_include Scripts/Nation/MergeShops/VoidRefugeMerge.cs
//cs_include Scripts/Nation/AssistingCragAndBamboozle[Mem].cs
//cs_include Scripts/Story/Nation/Originul.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleDelve.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiege.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleDelveMerge.cs
//cs_include Scripts/Nation/Various/TheLeeryContract[Member].cs
//cs_include Scripts/Nation/Various/JuggernautItems.cs
//cs_include Scripts/Nation/Various/VoidPaladin.cs
//cs_include Scripts/Nation/MergeShops/NulgathDiamondMerge.cs
//cs_include Scripts/Nation/Various/TarosManslayer.cs
//cs_include Scripts/Nation/Various/PurifiedClaymoreOfDestiny.cs
//cs_include Scripts/Nation/Various/VoidSpartan.cs
//cs_include Scripts/Nation/Various/SwirlingTheAbyss.cs
//cs_include Scripts/Hollowborn/TradingandStuff(single).cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Nation/EmpoweringItems.cs
//cs_include Scripts/Other/Weapons/VoidAvengerScythe.cs
//cs_include Scripts/Nation/MergeShops/DilligasMerge.cs
//cs_include Scripts/Nation/MergeShops/DirtlickersMerge.cs
//cs_include Scripts/Other/Weapons/WrathofNulgath.cs
//cs_include Scripts/Story/Legion/DarkWarLegionandNation.cs
//cs_include Scripts/Nation/Various/PrimeFiendShard.cs
//cs_include Scripts/Nation/Various/ArchfiendDeathLord.cs
//cs_include Scripts/Nation/MergeShops/VoidChasmMerge.cs
//cs_include Scripts/Story/Nation/VoidChasm.cs
//cs_include Scripts/Nation/MergeShops/NationMerge.cs
//cs_include Scripts/Nation/NationLoyaltyRewarded.cs


using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Skills;
using Skua.Core.Options;

public class UnlockForgeEnhancements
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    private CoreFarms Farm = new();
    private CoreStory Story = new();
    private CoreAdvanced Adv = new();

    private CoreNation Nation = new();
    private CoreLegion Legion = new();
    private CoreDarkon Darkon = new();
    private CoreSoWMats SOWM = new();
    private CoreAwe Awe = new();

    private Core13LoC LOC => new();
    private CoreNSOD CorNSOD = new();
    private CoreAstravia Astravia => new();
    private CoreDailies Daily = new();
    private Core7DD DD = new();
    private CoreYnR YNR = new();

    private CoreSoW SoW = new();
    private CoreSepulchure CoreSS = new();
    private ArchPaladin AP = new();
    private DragonOfTime DOT = new();
    private FireChampionsArmor FCA = new();
    private EternalDrakath ED = new();
    private SepulchuresOriginalHelm Seppy = new();
    private PrinceDarkonsPoleaxePreReqs PDPPR = new();
    private HeadoftheLegionBeast HOTLB = new();
    private Awescended Awescended = new();
    private NulgathDemandsWork NDW = new();
    private ThirdSpell TSS = new();
    private LordOfOrder LOO = new();
    private SevenCircles Circles = new();
    private YokaiQuests Yokai = new();
    private MalgorsArmorSet MAS = new();
    private PrimeFiendShard PFS = new();


    public string OptionsStorage = "Forge Ehn Unlocks";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        new Option<ForgeQuestWeapon>("ForgeQuestWeapon", "Weapon Enhancement", "Forge Quests to unlock Weapon Enhancement, change to none to unselect", ForgeQuestWeapon.None),
        new Option<ForgeQuestCape>("ForgeQuestCape", "Cape Enhancement", "Forge Quests to unlock Cape Enhancement, change to none to unselect", ForgeQuestCape.None),
        new Option<ForgeQuestHelm>("ForgeQuestHelm", "Helm Enhancement", "Forge Quests to unlock Helm Enhancement, change to none to unselect", ForgeQuestHelm.None),
        new Option<bool>("UseGold", "Use Gold", "Speed the BlacksmithingREP grind up with Gold?", false),
        new Option<bool>("BulkFarmGold", "Pre-Farm Gold(BlackSmithRep)", "Bulk Turnin after farming 100m Gold. (turns in x10 as long as u have 5m gold)", false),
        new Option<bool>("SellQuestClass", "Sell quest classes", "sell the classes backa after the Anima, Pneuma, Examen, and Vim quests", false),
        new Option<bool>("CanSolo", "Can solo", "Solo Sluggbutter"),
        new Option<bool>("UseInsignOnDaunt", "Use Insignia for dauntless", "Use your Insignia to buy the `Malgor's ShadowFlame Blade`[Malgor] & `Infernal Flame Pyromancer`[Avatar Tyndarius]", false)
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        ForgeUnlocks();

        Core.SetOptions(false);
    }

    public void ForgeUnlocks()
    {
        ForgeQuestCape selectedCapeEnhancements = Bot.Config!.Get<ForgeQuestCape>("ForgeQuestCape");
        ForgeQuestWeapon selectedWeaponEnhancements = Bot.Config!.Get<ForgeQuestWeapon>("ForgeQuestWeapon");
        ForgeQuestHelm selectedHelmEnhancements = Bot.Config!.Get<ForgeQuestHelm>("ForgeQuestHelm");

        if (selectedCapeEnhancements == ForgeQuestCape.None &&
            selectedWeaponEnhancements == ForgeQuestWeapon.None &&
            selectedHelmEnhancements == ForgeQuestHelm.None)
            Core.Logger("All settings are set to None, no Forge Quest to do. Stopping script.", messageBox: true, stopBot: true);

        if (selectedWeaponEnhancements != ForgeQuestWeapon.None)
        {
            if (selectedWeaponEnhancements != ForgeQuestWeapon.All)
                Core.Logger($"Selected Forge Weapon Enhancement: {Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon")}");

            switch (selectedWeaponEnhancements)
            {
                case ForgeQuestWeapon.ForgeWeaponEnhancement:
                    ForgeWeaponEnhancement();
                    break;

                case ForgeQuestWeapon.Lacerate:
                    Lacerate();
                    break;

                case ForgeQuestWeapon.Smite:
                    Smite();
                    break;

                case ForgeQuestWeapon.Praxis:
                    Praxis();
                    break;

                case ForgeQuestWeapon.HerosValiance:
                    HerosValiance();
                    break;

                case ForgeQuestWeapon.ArcanasConcerto:
                    ArcanasConcerto();
                    break;

                case ForgeQuestWeapon.Elysium:
                    Elysium();
                    break;

                case ForgeQuestWeapon.Acheron:
                    Acheron();
                    break;

                case ForgeQuestWeapon.DauntLess:
                    DauntLess();
                    break;

                case ForgeQuestWeapon.Ravenous:
                    Ravenous();
                    break;

                case ForgeQuestWeapon.All:
                    Core.Logger("Selected to unlock all Forge Weapon Enhancements");
                    ForgeWeaponEnhancement();
                    Lacerate();
                    Smite();
                    Praxis();
                    Acheron();
                    HerosValiance();
                    Elysium();
                    ArcanasConcerto();
                    DauntLess();
                    Ravenous();
                    break;
            }
        }

        if (selectedCapeEnhancements != ForgeQuestCape.None)
        {
            if (selectedCapeEnhancements != ForgeQuestCape.All)
                Core.Logger($"Selected Forge Cape Enhancement: {selectedCapeEnhancements}");

            switch (selectedCapeEnhancements)
            {
                case ForgeQuestCape.ForgeCapeEnhancement:
                    ForgeCapeEnhancement();
                    break;

                case ForgeQuestCape.Absolution:
                    Absolution();
                    break;

                case ForgeQuestCape.Vainglory:
                    Vainglory();
                    break;

                case ForgeQuestCape.Avarice:
                    Avarice();
                    break;

                case ForgeQuestCape.Penitence:
                    Penitence();
                    break;

                case ForgeQuestCape.Lament:
                    Lament();
                    break;

                case ForgeQuestCape.All:
                    Core.Logger("Selected to unlock all Forge Cape Enhancements");
                    ForgeCapeEnhancement();
                    Avarice(); //Calls on to the other functions internally
                    Penitence();
                    Lament();
                    break;
            }
        }

        if (selectedHelmEnhancements != ForgeQuestHelm.None)
        {
            if (selectedHelmEnhancements != ForgeQuestHelm.All)
                Core.Logger($"Selected Forge Cape Enhancement: {selectedHelmEnhancements}");

            switch (selectedHelmEnhancements)
            {
                case ForgeQuestHelm.ForgeHelmEnhancement:
                    ForgeHelmEnhancement();
                    break;

                case ForgeQuestHelm.Vim:
                    Core.Logger("Selected to unlock Vim Helm Enh");
                    Vim();
                    break;

                case ForgeQuestHelm.Examen:
                    Core.Logger("Selected to unlock Examen Helm Enh");
                    Examen();
                    break;

                case ForgeQuestHelm.Anima:
                    Core.Logger("Selected to unlock Anima Helm Enh");
                    Anima();
                    break;

                case ForgeQuestHelm.Pneuma:
                    Core.Logger("Selected to unlock Pneuma Helm Enh");
                    Pneuma();
                    break;

                case ForgeQuestHelm.All:
                    Core.Logger("Selected to unlock all Forge Helm Enhancements");
                    ForgeHelmEnhancement();
                    Vim();
                    Examen();
                    Anima();
                    Pneuma();
                    break;
            }
        }
    }

    #region Weapon Enhancements

    public void ForgeWeaponEnhancement()
    {
        if (Core.isCompletedBefore(8738))
            return;

        Core.Logger("Unlocking Enhancement: Forge (Weapon)");

        LOC.Escherion();
        Farm.Experience(30);
        Farm.BlacksmithingREP(4, Bot.Config!.Get<bool>("UseGold"), Bot.Config!.Get<bool>("UseGold"));

        Core.EquipClass(ClassType.Solo);
        Core.EnsureAccept(8738);

        Core.KillEscherion("1st Lord Of Chaos Helm");
        Core.KillVath("Chaos Dragonlord Helm");
        Core.KillKitsune("Chaos Shogun Helmet");
        Core.HuntMonster("wolfwing", "Wolfwing", "Wolfwing Mask", isTemp: false);

        Core.EnsureComplete(8738);
        Core.Logger("Enhancement Unlocked: Forge (Weapon)");
    }

    public void Lacerate()
    {
        if (Core.isCompletedBefore(8739))
            return;

        Core.Logger("Unlocking Enhancement: Lacerate");

        if (!Bot.Quests.IsUnlocked(8739))
        {
            Core.EquipClass(ClassType.Solo);
            if (!Bot.Quests.IsUnlocked(92))
            {
                if (!Bot.Quests.IsUnlocked(91))
                {
                    Core.EnsureAccept(90);
                    Core.HuntMonster("pirates", "Shark Bait", "Pirate Pegleg", 5);
                    Core.EnsureComplete(90);
                }
                Core.EnsureAccept(91);
                Core.KillMonster("greenguardwest", "West1", "Left", "Kittarian", "Kittarian's Wallet", 2);
                Core.KillMonster("greenguardwest", "West9", "Left", "River Fishman", "River Fishman's Wallet", 2);
                Core.KillMonster("greenguardwest", "West10", "Left", "Slime", "Slime-Soaked Wallet", 2);
                Core.KillMonster("greenguardwest", "West3", "Left", "Frogzard", "Frogzard's Lint Hoard", 2);
                Core.KillMonster("greenguardwest", "West12", "Up", "Big Bad Boar", "Big Bad Boar's Wallet");
                Core.EnsureComplete(91);
            }
            Story.KillQuest(92, "greenguardwest", new[] { "Breken the Vile", "Ogug Stoneaxe" });
        }

        Farm.Experience(40);
        Farm.BlacksmithingREP(5, Bot.Config!.Get<bool>("UseGold"), Bot.Config!.Get<bool>("UseGold"));

        Core.EnsureAccept(8739);

        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("graveyard", "Big Jack Sprat", "Undead Plague Spear", isTemp: false);
        Core.HuntMonster("river", "Kuro", "Kuro's Wrath", isTemp: false);

        if (!Core.CheckInventory("Massive Horc Cleaver"))
        {
            Core.AddDrop("Massive Horc Cleaver");
            Core.EnsureAccept(279);
            Core.HuntMonster("warhorc", "Horc Master", "Boss Prize");
            Core.EnsureComplete(279);
        }
        if (!Core.CheckInventory("Sword in the Stone"))
        {
            Core.AddDrop("Sword in the Stone");
            Core.EnsureAccept(316);
            Core.GetMapItem(54, 7, "greenguardeast");
            Core.HuntMonster("greenguardeast", "Spider", "Tiny Sword");
            Core.EnsureComplete(316);
        }
        if (!Core.CheckInventory("Forest Axe"))
        {
            Core.AddDrop("Forest Axe");
            Core.EnsureAccept(301);
            Core.GetMapItem(55, 4, "farm");
            Core.HuntMonster("farm", "Mosquito", "Mosquito Juice");
            Core.EnsureComplete(301);
        }

        Farm.BlackKnightOrb();

        Core.EnsureComplete(8739);
        Core.Logger("Enhancement Unlocked: Lacerate");
    }

    public void Smite()
    {
        if (Core.isCompletedBefore(8740))
            return;

        Core.Logger("Unlocking Enhancement: Smite");

        Farm.Experience(60);
        Farm.BlacksmithingREP(6, Bot.Config!.Get<bool>("UseGold"), Bot.Config!.Get<bool>("UseGold"));
        CoreSS.ShadowfallRise();

        Core.EnsureAccept(8740);
        Core.EquipClass(ClassType.Solo);

        Core.HuntMonster("shadowattack", "Death", "Death's Power", 3, isTemp: false);
        Core.KillEscherion("Chaotic Power", 7);
        Core.HuntMonster("shadowrealmpast", "Pure Shadowscythe", "Empowered Essence", 50, isTemp: false);
        Core.HuntMonster("undergroundlabb", "Ultra Battle Gem", "Gem Power", 25, false);
        Adv.BuyItem("alchemyacademy", 2116, "Power Tonic", 10);

        Core.EnsureComplete(8740);
        Core.Logger("Enhancement Unlocked: Smite");
    }

    public void Praxis()
    {
        if (Core.isCompletedBefore(9171))
            return;

        Core.Logger("Unlocking Enhancement: Praxis");

        Farm.BlacksmithingREP(6, Bot.Config!.Get<bool>("UseGold"), Bot.Config!.Get<bool>("UseGold"));

        Adv.BuyItem("thespan", 439, "Thief of Hours Armor");
        Adv.BuyItem("yulgar", 69, "Hashashin Armor");
        Adv.BuyItem("dragonkoiz", 95, "Imperial Chunin Clone");

        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("ectocave", "Ektorax", "Dragon Rogue", isTemp: false);
        YNR.Yami(3);

        Core.EquipClass(ClassType.Farm);
        Yokai.Quests();
        Core.AddDrop("Dragon Shinobi Token");

        Core.RegisterQuests(7924);
        while (!Bot.ShouldExit && !Core.CheckInventory("Dragon Shinobi Token", 100))
            Core.HuntMonster("shadowfortress", "1st Head of Orochi", "Perfect Orochi Scales", 10, isTemp: false);
        Core.CancelRegisteredQuests();

        Adv.BuyItem("shadowfortress", 1968, 59465, shopItemID: 8079);

        Core.ChainComplete(9171);
        Core.Logger("Enhancement Unlocked: Praxis");
    }

    public void HerosValiance()
    {
        if (Core.isCompletedBefore(8741))
            return;

        Core.Logger("Unlocking Enhancement: Hero's Valiance");

        Farm.Experience(100);
        Farm.BlacksmithingREP(10, Bot.Config!.Get<bool>("UseGold"), Bot.Config!.Get<bool>("UseGold"));

        FCA.GetFireChampsArmor();
        DOT.GetDoT(doExtra: false);
        ED.getSet(true, "Drakath the Eternal");
        if (!Core.CheckInventory("Drakath the Eternal"))
            Core.Logger("Cannot \"Finish\" `Heros Valiance quest. We'll continue farming it though...\n" +
            $"\"Drakath Armor\": x{Bot.Inventory.GetQuantity("Drakath Armor")}\n" +
            $"\"Dage's Scroll Fragment\" x{Bot.Inventory.GetQuantity("Dage's Scroll Fragment")}\n" +
            $"\"Drakath the Eternal\" x{Bot.Inventory.GetQuantity("Drakath the Eternal")}"); ;
        LOO.GetLoO();

        if (!Core.CheckInventory(23689))
        {
            Core.EquipClass(ClassType.Solo);
            Core.AddDrop("Eternity Blade");
            Core.EnsureAccept(3485);
            Core.HuntMonster("towerofdoom10", "Slugbutter", "Eternity Blade");
            Core.EnsureComplete(3485);
            Bot.Wait.ForPickup(23689);
        }

        Core.EnsureAccept(8741);

        Seppy.GravelynsDoomFireToken();
        AP.GetAP(false); //purely for the last quest "Sacred Magic: Eden"

        if (!Core.isCompletedBefore(7165))
        {
            Core.Logger("Quest Progrestion not available For LoO (requires last quest to be complete and these are dailies)");
            return;
        }

        Adv.BuyItem("darkthronehub", 1303, "ArchPaladin Armor");

        if (Core.CheckInventory("Drakath the Eternal"))
        {
            Core.EnsureComplete(8741);
            Core.Logger("Enhancement Unlocked: Hero's Valiance");
        }
        else Core.Logger("Could not complete \"Hero's Valiance\", Try again tomarrow after then \"Dage's Scroll Fragment\" Daily.");
    }

    public void ArcanasConcerto()
    {
        if (Core.isCompletedBefore(8742))
            return;

        Core.Logger("Unlocking Enhancement: Arcana's Concerto");

        Astravia.CompleteCoreAstravia();
        Farm.Experience(100);
        Farm.BlacksmithingREP(10, Bot.Config!.Get<bool>("UseGold"), Bot.Config!.Get<bool>("UseGold"));

        if (!Core.isCompletedBefore(8746))
        {
            Core.Logger("You must have faced Darkon the Conductor and done the weekly quest in order to unlock \"Arcana's Concerto\"", messageBox: true);
            return;
        }
        PDPPR.FarmPreReqs();

        if (!Core.CheckInventory("Darkon's Debris 2 (Reconstructed)"))
        {
            if (!Core.CheckInventory("Darkon's Debris 2 (Recovered)"))
            {
                Darkon.UnfinishedMusicalScore(22);
                Adv.BuyItem("theworld", 2141, "Darkon's Debris 2 (Recovered)");
            }
            Darkon.BanditsCorrespondence(22);
            Darkon.SukisPrestiege(22);
            Darkon.AncientRemnant(22);
            Darkon.WheelofFortune(22, 0);
            if (!Core.CheckInventory("Darkon Insignia", 20))
            {
                Core.Logger(" x20 \"Darkon Insignia\" is Required to continue quest, our Bots cannot *currently* kill this mob, use Grim (different client) & @InsertNameHere's ultra bot", messageBox: true);
                return;
            }
            else Core.BuyItem("ultradarkon", 2147, "Darkon's Debris 2 (Reconstructed)");
        }

        if (!Core.CheckInventory("King Drago Insignia", 5))
        {
            Core.Logger(" x5 \"King Drago Insignia\" is required to continue quest, our Bots cannot *currently* kill this mob, use Grim (different client) & @InsertNameHere's ultra bot", messageBox: true);
            return;
        }
        if (!Core.CheckInventory("Darkon Insignia", 5))
        {
            Core.Logger(" x5 \"Darkon Insignia\" is required to continue quest, our Bots cannot *currently* kill this mob, use Grim (different client) & @InsertNameHere's ultra bot", messageBox: true);
            return;
        }
        Core.ChainComplete(8742);
        Core.Logger("Enhancement Unlocked: Arcana's Concerto");
    }

    public void Acheron()
    {
        if (Core.isCompletedBefore(8820) || !Core.CheckInventory(new[] { 38566, 38567 }, toInv: false))
            return;

        Core.Logger("Unlocking Enhancement: Acheron");

        Farm.BlacksmithingREP(8, Bot.Config!.Get<bool>("UseGold"), Bot.Config!.Get<bool>("UseGold"));
        Core.EnsureAccept(8820);

        VoidLodestone();

        SoW.Tyndarius();

        // have the Dark Box and Dark Key mini-saga completed 
        // Quest complete will require you to turn in the Power of Darkness, 
        Core.BuyItem(Bot.Map.Name, 1380, "The Power of Darkness");

        //20 Dark Potions,
        Core.RegisterQuests(5710);
        while (!Bot.ShouldExit && !Core.CheckInventory("Dark Potion", 20))
        {
            if (Core.IsMember)
                Core.HuntMonster("darkfortress", "Dark Elemental", "Dark Gem", isTemp: false);
            else Core.HuntMonster("ruins", "Dark Elemental", "Dark Gem", isTemp: false);
        }
        Core.CancelRegisteredQuests();

        Adv.GearStore();
        Core.DodgeClass();
        Core.HuntMonster("tercessuinotlim", "Nulgath", "The Mortal Coil", isTemp: false);
        Adv.GearStore(true);
        Core.EnsureComplete(8820);
        Core.Logger("Enhancement Unlocked: Acheron");
    }

    public void Elysium()
    {
        if (Core.isCompletedBefore(8821))
            return;

        Core.Logger("Unlocking Enhancement: Elysium");

        Farm.BlacksmithingREP(10, Bot.Config!.Get<bool>("UseGold"), Bot.Config!.Get<bool>("UseGold"));

        Core.EnsureAccept(8821);
        CorNSOD.BonesVoidRealm(15);
        YNR.BlademasterSwordScroll();
        NDW.NDWQuest(new[] { "Archfiend Essence Fragment" }, 3);
        Awescended.GetAwe();
        if (!Core.CheckInventory("The Divine Will"))
        {
            Core.Logger("\"Azalith\" is not Soloable, please go kill it otherwise for the Drop \"The Divine Will\", and return here and re-run the script.");
            return;
        }
        Core.EnsureComplete(8821);
        Core.Logger("Enhancement Unlocked: Elysium");
    }

    public void DauntLess()
    {
        if (Core.isCompletedBefore(9172))
            return;

        Core.Logger("Unlocking Enhancement: Dauntless");

        Farm.BlacksmithingREP(10, Bot.Config!.Get<bool>("UseGold"), Bot.Config!.Get<bool>("UseGold"));

        string[] DauntlessItems = { "ShadowLord's Helm", "Malgor the ShadowLord", "Malgor's ShadowFlame Blade", "Infernal Flame Pyromancer" };
        Core.Logger($"The only items the bot can get are: \"Malgor the ShadowLord\" and \"ShadowLord's Helm\". Unless `UseInsignOnDaunt` is enabled: {Bot.Config.Get<bool>("UseInsignOnDaunt")}");

        // Base Quest Req. for dauntless
        int Malgorinsig = 5;
        int AvatarTyndInsig = 10;

        Core.EnsureAccept(9172);
        if (!Core.CheckInventory(DauntlessItems))
        {

            //Story
            SoW.CompleteCoreSoW();

            // Items: "ShadowLord's Helm", "Malgor the ShadowLord"
            MAS.GetSet();

            #region  [Prep] Malgor's ShadowFlame Blade
            Adv.GearStore();
            Core.BossClass();
            SOWM.ElementalCore(20);
            Adv.GearStore(true);
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("shadowgrove", "Titan Shadow Dragonlord", "ShadowFlame Dragon Blade", isTemp: false);
            #endregion

            #region  [Prep] Infernal Flame Pyromancer
            SoW.Tyndarius();

            Core.AddDrop("Fire Avatar's Favor");
            Core.EquipClass(ClassType.Farm);
            Core.FarmingLogger("Fire Avatar's Favor", 75);
            Core.RegisterQuests(8244);
            while (!Bot.ShouldExit && !Core.CheckInventory("Fire Avatar's Favor", 75))
            {
                Core.KillMonster("fireavatar", "r4", "Right", "*", "Onslaught Defeated", 6);
                Core.KillMonster("fireavatar", "r6", "Left", "*", "Elemental Defeated", 6);

                Bot.Wait.ForPickup("Fire Avatar's Favor");
            }
            Core.CancelRegisteredQuests();

            #endregion

            if (Bot.Config.Get<bool>("UseInsignOnDaunt"))
            {
                //ensure in inv
                Core.Unbank("Malgor Insignia", "Avatar Tyndarius Insignia");

                if (!Core.CheckInventory("Malgor's ShadowFlame Blade") && Core.CheckInventory("Malgor Insignia", 20))
                    Adv.BuyItem("ultraspeaker", 2248, "Malgor's ShadowFlame Blade");
                else
                    Malgorinsig += 20;

                if (!Core.CheckInventory("Infernal Flame Pyromancer") && Core.CheckInventory("Avatar Tyndarius Insignia", 20))
                    Adv.BuyItem("fireavatar", 2038, "Infernal Flame Pyromancer");
                else
                    AvatarTyndInsig += 20;
            }

            // totals:
            // 25 Malgor Insignia
            // 30 Avatar Tyndarius Insignia

        }

        if (Core.CheckInventory(DauntlessItems) && Core.CheckInventory("Malgor Insignia", 5) && Core.CheckInventory("Avatar Tyndarius Insignia", 10))
        {
            Core.ChainComplete(9172);
            Core.Logger("Enhancement Unlocked: Dauntless");
        }
        else
        {

            Core.Logger("Items still needed(the bot cannot farm these):");
            foreach (string item in DauntlessItems.Where(item => !Core.CheckInventory(item)))
                Core.Logger($"Missing \"{item}\" x1");

            Core.Logger($"Missing \"Malgor Insignia\" x {Malgorinsig}");
            Core.Logger($"Missing \"Avatar Tyndarius Insignia\" x {AvatarTyndInsig}");
            Core.AbandonQuest(9172);
        }
    }

    public void Ravenous()
    {
        if (Core.isCompletedBefore(9560))
            return;

        Core.Logger("Unlocking Enhancement: Ravenous");

        Farm.Experience();
        PFS.Storyline();

        Adv.BuyItem(Bot.Map.Name, 2411, "Gluttonous Maw");
        Core.ChainComplete(9560);
    }

    #endregion
    #region Forge Enhancements

    public void ForgeCapeEnhancement()
    {
        if (Core.isCompletedBefore(8758))
            return;

        Core.Logger("Unlocking Enhancement: Forge (Cape)");

        LOC.Kitsune();
        Farm.Experience(30);
        Farm.BlacksmithingREP(4, Bot.Config!.Get<bool>("UseGold"), Bot.Config!.Get<bool>("UseGold"));

        Core.EquipClass(ClassType.Solo);
        Core.EnsureAccept(8758);

        Core.HuntMonster("lostruinswar", "Diabolical Warlord", "Prismatic Celestial Wings", isTemp: false);
        Core.HuntMonster("lostruins", "Infernal Warlord", "Broken Wings", isTemp: false);
        Core.HuntMonster("infernalspire", "Azkorath", "Shadow's Wings", isTemp: false);
        Core.HuntMonster("infernalspire", "Malxas", "Wings Of Destruction", isTemp: false);

        Core.EnsureComplete(8758);
        Core.Logger($"Enhancement Unlocked: Forge (Cape)");
    }

    public void Absolution()
    {
        if (Core.isCompletedBefore(8743))
            return;

        Core.Logger("Unlocking Enhancement: Absolution");

        Farm.Experience(90);
        Farm.GoodREP(10);
        Farm.BlacksmithingREP(9, Bot.Config!.Get<bool>("UseGold"), Bot.Config!.Get<bool>("UseGold"));

        int SlimeSigil = 200;

        // Define items and their corresponding deduction values
        Dictionary<int, int> itemsToCheck = new()
        {
            { 39091, 100 },  // Ascended Paladin
            // { 39093, 50 },   // Ascended Paladin Staff
            { 39094, 50 }    // Ascended Paladin Sword
        };

        // Check each item in the dictionary and deduct points if not found
        foreach (var item in itemsToCheck)
        {
            if (Core.CheckInventory(item.Key))
            {
                SlimeSigil -= item.Value;
            }
        }

        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("therift", "r9", "Left", "Plague Spreader", "Slimed Sigil", SlimeSigil, isTemp: false);
        Adv.BuyItem("therift", 1399, "Ascended Paladin", shopItemID: 5244);
        // Adv.BuyItem("therift", 1399, "Ascended Paladin Staff", shopItemID: 5246);
        Adv.BuyItem("therift", 1399, "Ascended Paladin Sword", shopItemID: 5247);

        Core.ChainComplete(8743);
        Core.Logger("Enhancement Unlocked: Absolution");
    }

    public void Vainglory()
    {
        if (Core.isCompletedBefore(8744))
            return;

        Absolution();

        Core.Logger("Unlocking Enhancement: Vainglory");
        Core.EnsureAccept(8744);

        Farm.BlacksmithingREP(9, Bot.Config!.Get<bool>("UseGold"), Bot.Config!.Get<bool>("UseGold"));
        Core.EquipClass(ClassType.Solo);

        if (Core.IsMember)
        {
            if (!Core.CheckInventory("Pauldron Relic"))
            {
                Farm.BladeofAweREP(10, false);
                Farm.Experience(55);
                Core.BuyItem("museum", 1130, "Armor of Awe Pass");
                Core.AddDrop("Pauldron Fragment");
                Core.RegisterQuests(4162);
                while (!Bot.ShouldExit && !Core.CheckInventory("Pauldron Fragment", 15))
                {
                    Core.HuntMonster("gravestrike", "Ultra Akriloth", "Pauldron Shard", 15, false);
                    Bot.Wait.ForPickup("Pauldron Fragment");
                }
                Core.CancelRegisteredQuests();

                Core.BuyItem("museum", 1129, "Pauldron Relic");
            }
        }
        else
            Awe.GetAweRelic("Pauldron", 4160, 15, 15, "gravestrike", "Ultra Akriloth");
        Awe.GetAweRelic("Breastplate", 4163, 10, 10, "aqlesson", "Carnax");
        Awe.GetAweRelic("Vambrace", 4166, 15, 15, "bloodtitan", "Ultra Blood Titan");
        Awe.GetAweRelic("Gauntlet", 4169, 25, 5, "alteonbattle", "ULTRA Alteon");
        Awe.GetAweRelic("Greaves", 4172, 10, 15, "bosschallenge", "Mutated Void Dragon");

        Core.EnsureComplete(8744);
        Core.Logger("Enhancement Unlocked: Vainglory");
    }

    public void Avarice()
    {
        if (Core.isCompletedBefore(8745))
            return;

        Vainglory();
        Core.Logger("Unlocking Enhancement: Avarice");

        Farm.BlacksmithingREP(9, Bot.Config!.Get<bool>("UseGold"), Bot.Config!.Get<bool>("UseGold"));

        Core.EnsureAccept(8745);

        Circles.CirclesWar();
        HOTLB.Indulgence(50);
        HOTLB.Penance(50);

        Core.EnsureComplete(8745);
        Core.Logger("Enhancement Unlocked: Avarice");
    }

    public void Penitence()
    {
        if (Core.isCompletedBefore(8822))
            return;

        Avarice();
        Core.Logger("Unlocking Enhancement: Penitence");

        Farm.BlacksmithingREP(9, Bot.Config!.Get<bool>("UseGold"), Bot.Config!.Get<bool>("UseGold"));

        Core.EnsureAccept(8822);
        Core.EquipClass(ClassType.Solo);

        while (!Bot.ShouldExit && !Core.CheckInventory("Night Mare Scythe"))
        {
            Core.AddDrop("Night Mare Scythe");
            Core.EnsureAccept(3270);
            Core.KillMonster("doomvault", "r5", "Left", "Binky", "Yulgar's Lost Scythe");
            Core.EnsureComplete(3270);
        }
        Core.HuntMonster("frozenlair", "Legion Lich Lord", "Sapphire Orb", 100, isTemp: false);
        Core.HuntMonster("icewing", "Warlord Icewing", "Boreal Cavalier Bardiche", isTemp: false);
        Core.HuntMonster("underlair", "ArchFiend DragonLord", "Void Scale", 13, isTemp: false);

        Core.EnsureComplete(8822);
        Core.Logger("Enhancement Unlocked: Penitence");
    }

    public void Lament()
    {
        if (Core.isCompletedBefore(8823))
            return;

        Penitence();
        Core.Logger("Unlocking Enhancement: Lament");

        Farm.BlacksmithingREP(9, Bot.Config!.Get<bool>("UseGold"), Bot.Config!.Get<bool>("UseGold"));

        Core.EquipClass(ClassType.Solo);
        Core.EnsureAccept(8823);

        Core.HuntMonster("sepulchurebattle", "ULTRA Sepulchure", "Doom Heart", isTemp: false);
        if (!Core.CheckInventory("Heart of the Sun"))
            TSS.StoryLine(true); //sun heart thing
        Core.HuntMonster("ashfallcamp", "Smoldur", "Flame Heart", 10, isTemp: false);
        DD.Sloth();
        Adv.GearStore();
        DD.HazMatSuit();
        Core.HuntMonster("sloth", "Mutated Plague", "Bloodless Heart", 3, isTemp: false);
        Adv.GearStore(true);

        Core.EnsureComplete(8823);
        Core.Logger("Enhancement Unlocked: Lament");
    }

    #endregion
    #region Helm Enhancements

    public void ForgeHelmEnhancement()
    {
        if (Core.isCompletedBefore(8828))
            return;

        Core.Logger("Unlocking Enhancement: Forge (Helm)");
        Farm.Experience(30);
        Farm.BlacksmithingREP(4, Bot.Config!.Get<bool>("UseGold"), Bot.Config!.Get<bool>("UseGold"));

        Core.EquipClass(ClassType.Solo);
        Core.EnsureAccept(8828);

        Core.KillEscherion("1st Lord Of Chaos Staff");
        Core.KillVath("Chaos Dragonlord Axe");
        Core.KillKitsune("Hanzamune Dragon Koi Blade");
        Core.HuntMonster("wolfwing", "Wolfwing", "Wrath of the Werepyre", isTemp: false);

        Core.EnsureComplete(8828);
        Core.Logger("Enhancement Unlocked: Forge (Helm)");
    }

    public void Vim()
    {
        if (Core.isCompletedBefore(8824))
            return;

        ForgeHelmEnhancement();
        Core.Logger("Unlocking Enhancement: Vim");

        Farm.BlacksmithingREP(7, Bot.Config!.Get<bool>("UseGold"), Bot.Config!.Get<bool>("UseGold"));
        Adv.GearStore();
        Core.EnsureAccept(8824);

        Core.BuyItem("Classhalla", 172, "Rogue");
        Adv.RankUpClass("Rogue");
        Adv.GearStore(true);

        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("Towerofdoom10", "Enter", "Spawn", "*", "Ethereal Essence", 250, isTemp: false);

        Core.EnsureComplete(8824);
        if (Bot.Config!.Get<bool>("SellQuestClass"))
            Core.SellItem("Rogue");
        Core.Logger("Enhancement Unlocked: Vim");
    }

    public void Examen()
    {
        if (Core.isCompletedBefore(8825))
            return;

        Vim();
        Core.Logger("Unlocking Enhancement: Examen");

        Adv.GearStore();
        Core.EnsureAccept(8825);
        Core.BuyItem("Classhalla", 176, "Healer");
        Adv.RankUpClass("Healer");
        Adv.GearStore(true);

        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("Towerofdoom10", "Enter", "Spawn", "*", "Ethereal Essence", 250, isTemp: false);

        Core.EnsureComplete(8825);
        if (Bot.Config!.Get<bool>("SellQuestClass"))
            Core.SellItem("Healer");
        Core.Logger("Enhancement Unlocked: Examen");
    }

    public void Anima()
    {
        if (Core.isCompletedBefore(8826))
            return;

        Examen();
        Core.Logger("Unlocking Enhancement: Anima");

        Adv.GearStore();
        Core.EnsureAccept(8826);

        Core.BuyItem("Classhalla", 170, "Warrior");
        Adv.RankUpClass("Warrior");
        Adv.GearStore(true);

        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("Towerofdoom10", "Enter", "Spawn", "*", "Ethereal Essence", 650, isTemp: false);

        Core.EnsureComplete(8826);
        if (Bot.Config!.Get<bool>("SellQuestClass"))
            Core.SellItem("Warrior");
        Core.Logger("Enhancement Unlocked: Anima");
    }

    public void Pneuma()
    {
        if (Core.isCompletedBefore(8827))
            return;

        Anima();
        Core.Logger("Unlocking Enhancement: Pneuma");

        Adv.GearStore();
        Core.EnsureAccept(8827);

        Core.BuyItem("Classhalla", 174, "Mage");
        Adv.RankUpClass("Mage");
        Adv.GearStore(true);

        Core.EquipClass(ClassType.Farm);
        Core.KillMonster("Towerofdoom10", "Enter", "Spawn", "*", "Ethereal Essence", 650, isTemp: false);

        Core.EnsureComplete(8827);
        if (Bot.Config!.Get<bool>("SellQuestClass"))
            Core.SellItem("Mage");
        Core.Logger("Enhancement Unlocked: Pneuma");
    }

    public void VoidLodestone()
    {
        if (Core.CheckInventory("Void Lodestone"))
            return;

        // Arcane Lodestone
        if (!Core.CheckInventory("Arcane Lodestone"))
        {
            //(Reward from the 'Open Ebony Chest' quest
            //Requires: ???(38565) to acess quest
            TheDarkBox(38565);

            if (Core.CheckInventory(38565))
            {
                Core.EnsureAccept(5723);
                Core.HuntMonster("dreadfire", "Stray Mana", "Bronze Key", isTemp: false);
                Core.HuntMonster("dreadfire", "Living Brimstone", "Silver Key", isTemp: false);
                Core.Logger("Going to your house to load the shop.\n" +
                "[there may be a delay]");
                Core.SendPackets($"%xt%zm%house%1%{Bot.Player.Username}%");
                Core.Sleep(5000);
                Core.BuyItem(Bot.Map.Name, 336, "Golden Key");
                Core.EnsureComplete(5723);
            }
            else
            {
                Core.Logger("Cannot Accept Quest Without Item \"???\"");
                return;
            }
        }

        // Mercury Elixir
        if (!Core.CheckInventory("Mercury Elixir"))
        {
            //Reward from the 'Mercury Elixir' quest
            Core.EnsureAccept(5757);
            Core.HuntMonster("Battleunderb", "The Lost", "Mercury Elixir");
            Core.EnsureComplete(5757);
        }

        Core.BuyItem("doomwood", 1381, "Void Lodestone");
    }

    public void TheDarkBox(int itemID, int quant = 1)
    {
        ItemBase? Reward = Core.EnsureLoad(5710).Rewards.Find(x => x.ID == itemID);
        if (Reward == null)
        {
            Core.Logger($"ERROR: itemID {itemID} was not found in quest 5710");
            return;
        }

        Core.AddDrop("Dark Potion", Reward.Name);

        Daily.MonthlyTreasureChestKeys();
        if (!Core.CheckInventory(new[] { "Dark Box", "Dark Key" }))
        {
            Core.Logger("Dark Box & Key Not Found, Cannot Continue with Enh");
            return;
        }

        Core.Logger("Pray to RNGsus for your item");
        while (!Bot.ShouldExit && !Core.CheckInventory(Reward.ID, quant))
        {
            Core.EnsureAccept(5710);
            if (Core.IsMember)
                Core.HuntMonster("ruins", "Dark Elemental", "Dark Gem", isTemp: false);
            else Core.HuntMonster("darkfortress", "Dark Elemental", "Dark Gem", isTemp: false);
            Core.EnsureComplete(5710);
            Bot.Wait.ForPickup(Reward.ID);
        }
    }

    #endregion
}

public enum ForgeQuestWeapon
{
    ForgeWeaponEnhancement,
    Lacerate,
    Smite,
    Praxis,
    Acheron,
    HerosValiance,
    Elysium,
    ArcanasConcerto,
    DauntLess,
    Ravenous,
    None,
    All
};

public enum ForgeQuestCape
{
    ForgeCapeEnhancement,
    Absolution,
    Vainglory,
    Avarice,
    Penitence,
    Lament,
    None,
    All
};

public enum ForgeQuestHelm
{
    ForgeHelmEnhancement,
    Vim,
    Examen,
    Anima,
    Pneuma,
    None,
    All
};
