/*
name: null
description: null
tags: null
*/
#region cs_include
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs

//cs_include Scripts/Good/GearOfAwe/BladeOfAwe.cs
//cs_include Scripts/Other/Classes/DragonOfTime.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Other/Weapons/GoldenBladeOfFate.cs
//cs_include Scripts/Other/Weapons/PinkBladeofDestruction.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Other/MysteriousEgg.cs
//cs_include Scripts/Story/Summer2015AdventureMap/CoreSummer.cs
//cs_include Scripts/Story/Borgars.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
//cs_include Scripts/Good/ArchPaladin.cs
//cs_include Scripts/Good/Paladin.cs
//cs_include Scripts/Story/XansLair.cs
//cs_include Scripts/Legion/Revenant/CoreLR.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/InfiniteLegionDarkCaster.cs
//cs_include Scripts/Story/Legion/SeraphicWar.cs
//cs_include Scripts/Other/Classes/ArchMage/CoreArchMage.cs
//cs_include Scripts/Farm/BuyScrolls.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/ShadowsOfWar/CoreSoWMats.cs
//cs_include Scripts/Enhancement/UnlockForgeEnhancements.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Chaos/EternalDrakathSet.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Evil/NSoD/CoreNSOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Legion/YamiNoRonin/CoreYnR.cs
//cs_include Scripts/Chaos/DrakathsArmor.cs
//cs_include Scripts/Evil/SepulchuresOriginalHelm.cs
//cs_include Scripts/Legion/HeadOfTheLegionBeast.cs
//cs_include Scripts/Other/WarFuryEmblem.cs
//cs_include Scripts/Other/Classes/DragonslayerGeneral.cs
//cs_include Scripts/Other/Armor/FireChampionsArmor.cs
//cs_include Scripts/Darkon/Various/PrinceDarkonsPoleaxePreReqs.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/Story/SepulchureSaga/CoreSepulchure.cs
//cs_include Scripts/Story/StarSinc.cs
//cs_include Scripts/Story/TowerOfDoom.cs
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
//cs_include Scripts/Other/Weapons/ExaltedApotheosisPreReqs.cs
//cs_include Scripts/Darkon/DarkonDebris2ReconstructedPrerequisites.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/DarkonGarden.cs
//cs_include Scripts/Chaos/AscendedDrakathGear.cs
//cs_include Scripts/Good/BLoD/2UltimateBlindingLightofDestiny.cs
//cs_include Scripts/Hollowborn/HollowbornDoomKnight/CoreHollowbornDoomKnight.cs
//cs_include Scripts/Hollowborn/HollowbornPaladin/CoreHollowbornPaladin.cs
//cs_include Scripts/Other/FireAvatarFavorFarm.cs
//cs_include Scripts/Story/Artixpointe.cs
//cs_include Scripts/Story/ShadowsOfChaos/CoreSoC.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/BeetleQuests.cs
//cs_include Scripts/Nation/Various/DragonBlade[mem].cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiegeMerge.cs
//cs_include Scripts/ShadowsOfWar/UltraSpeakerMergePreReqs.cs
//cs_include Scripts/Chaos/ChaosAvengerPreReqs.cs
//cs_include Scripts/Other/MergeShops/TerminaTempleMerge.cs
//cs_include Scripts/Other/Classes/VerusDoomKnight.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/DoomPirateStory.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/MergeShops/DoomPirateHaulMerge.cs
#endregion

using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Shops;
using Skua.Core.Options;
using Skua.Core.Utils;

public class CockroachMoe
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public CoreAdvanced Adv = new();
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreDailies Daily = new();
    public BladeOfAwe BoA = new(); 
    public CoreBLOD BLOD = new();
    public ArchDoomKnight ADK = new();
    public Awescended awescended = new();

    public ChaosAvengerClass CaV = new();

    public ExaltedApotheosisPreReqs EA = new();

    public PrinceDarkonsPoleaxePreReqs PDPP = new();
    
    public DarkonDebris2ReconstructedPrerequisites DDRP = new();

    public UltraSpeakerMergePreReqs US = new();

    public MalgorsArmorSet MAS = new();
    public CoreNSOD NSOD = new();
    public UnlockForgeEnhancements forgeUnlocks = new();

//Classes
    public DragonOfTime DoT = new();
    public ArchPaladin AP = new();
    public CoreLR LR = new();
    public CoreArchMage AM = new();
    public CoreVHL VHL = new();

    public VerusDoomKnightClass VDK = new();

    //TODO / Maybes: DarkCarnax, Ravenous, Arcana Invoker
    
    //Farming Route: 
    // [DOT,AP,BoA] -> BloD -> LR -> (VHL) -> (AM 1, 2) -> Lacerate -> Smite -> (AM 2,3) -> Praxis -> Val -> (AM2), ADK -> 
    // VDK -> Awescended ->  Elysium -> Helm ->(AM 5, 6) ->Cape Ench -> Ultra Prereqs -> AM -> NSoD

    public List<IOption> Options = new()
    {
        new Option<bool>("FarmAM", "Prefarm Archmage?", "Prefarm Archmage? Note: This will make the overall progression slower, but save a lot of time when you eventually start botting for AM. \nYes / No  [Yes by default]", true),
        new Option<bool>("FarmVHL", "Farm Void Highlord?", "Farm Void Highlord? Note: Due to many new classes, Void Highlord has become somewhat obsolete. It is almost never used for Ultra Bosses, and it is only useful for unlocking Valiance (which can also be done with Yami no Ronin). It is still one of the best solo play classes though, and farming it does take a bit. \nYes / No  [Yes by default]", true),
        CoreBots.Instance.SkipOptions,
    };

    public void Test()
    {
        Core.Logger("Testing Grounds");
        
    }

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        CockroachMoeCore();

        Core.SetOptions(false);
    }
    public void CockroachMoeCore()
    {
        //Test();

        Core.Logger("Checking for Essentials: Dragon of Time, ArchPaladin, Blade of Awe.");
        CheckEssentials();

        Core.Logger("Essentials complete.");


        Core.Logger("Blinding Light of Destiny found, continuing.");
        if (!Core.CheckInventory("Blinding Light of Destiny")){
            Core.Logger("Blinding Light of Destiny not found, initiating the Blinding Light of Destiny farm now.");
            BLoDCheckProgress();
        }
        else{
            Core.Logger("Blinding Light of Destiny found, continuing.");
        }

        if (!Core.CheckInventory("Legion Revenant")){
            Core.Logger("Legion Revenant not found, initiating the Legion Revenant farm now.");
            LR.GetLR(true);
        }
        Core.Logger("Legion Revenant found, continuing.");

        // Put VHL in here as optional
        if(Bot.Config!.Get<bool>("FarmAM") && !Core.CheckInventory("Void Highlord"))
        {
            Core.Logger("Void Highlord not found, checking if all dailies are done to commit to the farm.");
 
            if (Daily.CheckDaily(802))
            {
                Daily.EldersBlood();
            }

            if(Core.CheckInventory("Elders' Blood", 17))
            {
                Core.Logger("All dailies completed, commencing the Void Highlord farm now.");
                VHL.GetVHL();
            }
            else{
                Core.Logger("17x Elder's Blood not found, skipping Void Highlord farm.");
            }
        }

        //Farm AM Quest 1
        //Accept AM Quest 2 and get it's drops from monsters (Chaotic Ether, Mortal Ether), but start farming Forge Weapon
        if(Bot.Config!.Get<bool>("FarmAM") && !Core.CheckInventory("ArchMage"))
        {
            Core.Logger("Completing the first ArchMage quest (MysticScribingKit) and accepting the second one (PrismaticEther) to prefarm");            
            AM.MysticScribingKit(1);
            Core.EnsureAccept(8910);
        }

        if (!Core.isCompletedBefore(8740))
        {
            Core.Logger("Smite not unlocked, initiating the Smite farm now.");
            if(Bot.Config!.Get<bool>("FarmAM") && !Core.CheckInventory("ArchMage"))
            {
                Core.Logger("Mortal Ether will be collected for ArchMage as well.");
                Core.AddDrop("Mortal Ether");
            }
            forgeUnlocks.Smite();
        }

        //Finish AM Quest 2, Accept AM Quest 3 
        if(Bot.Config!.Get<bool>("FarmAM") && !Core.CheckInventory("ArchMage"))
        {
            Core.Logger("Accepting ArchMage quest to prefarm.");
            AM.PrismaticEther(1);
            Core.EnsureAccept(8911);

            Core.Logger("Earth Locus will be collected for ArchMage.");
            Core.AddDrop("Earth Locus");
        }

        //Farm Praxis, But:
        // - Whilst farming for Dragon Rogue, accept Earth Locus from Extorax
        if (!Core.isCompletedBefore(9171))
        {
            Core.Logger("Praxis not unlocked, initiating the Smite farm now.");
            forgeUnlocks.Praxis();
        }

        //If has VHL or YNR, might as well get Valiance (need Void Scale x13 from Archfiend DragonLord)
        if(Core.CheckInventory("Yami no Ronin") || Core.CheckInventory("Void Highlord"))
        {
            forgeUnlocks.HerosValiance();
            Core.Logger("Valiance unlocked.");
        }
        else{
            Core.Logger("Solo Class for Archfiend DragonLord not found (Yami no Ronin / Void Highlord), skipping Valiance.");
        }
        Core.Logger("Valiance unlocked or skipped (if the message above says that you have no Solo Class, then it's skipped).");

        //Accept AM Quest 2, farm Arch DoomKnight, accept Mortal Ether along the way
        if (!Core.CheckInventory("Arch DoomKnight"))
        {
            Core.Logger("Arch DoomKnight not found, initiating the Arch DoomKnight farm now.");

            if(Bot.Config!.Get<bool>("FarmAM") && !Core.CheckInventory("ArchMage"))
            {
                Core.Logger("Mortal Ether will be collected for ArchMage as well.");
                Core.EnsureAccept(8910);
                Core.AddDrop("Mortal Ether");
            }          

            ADK.DoAll(true);
        }
        Core.Logger("Arch DoomKnight found, continuing.");

        if(VDKPreReqs()){
            Core.Logger("Verus DoomKnight Prerequisites are fulfilled, commencing the farm.");
            VDK.GetClass();
        }


        //Farm Awe-scended
        if (!Core.CheckInventory("Awescended")){
            Core.Logger("Awescended not found, initiating the Awescended farm now.");
            awescended.GetAwe();
        }
        Core.Logger("Awescended found, continuing.");

        //Farm Elysium, but accept Uni 13, 19, 25, 26, 27
        if (!Core.isCompletedBefore(8821))
        {
            Core.AddDrop("Unidentified 13");
            Core.AddDrop("Unidentified 19");
            Core.AddDrop("Unidentified 25");
            Core.AddDrop("Unidentified 26");
            Core.AddDrop("Unidentified 27");
            
            Core.Logger("Elysium not unlocked, initiating the Praxis farm now.");
            Core.Logger("Unidentified 13 is needed in general, and Unidentified 25 is used for Hollowborn Paladin (which is used for Goddess of War)");
            forgeUnlocks.Elysium();
        }
        Core.Logger("Elysium unlocked, continuing.");

        //Farm Helm Enchs bc why not at this point
        if (!Core.isCompletedBefore(8827))
        {
            Core.Logger("All Helm Enchantments not unlocked, initiating the Helm Enchantments farm now.");
            forgeUnlocks.Pneuma();
        }
        Core.Logger("All Helm Enchantments are unlocked, continuing.");     


        //Farm Cape Enchs, But:
        // - during Penitence, when fighting Icewing, accept Rimeblossom, Temporal Floe, and Starlit Frost for AM
        if (!Core.isCompletedBefore(8823))
        {
            Core.Logger("All Cape Forge Enchantments not unlocked, initiating the Cape Forge Enchantments farm now.");

            if(Bot.Config!.Get<bool>("FarmAM") && !Core.CheckInventory("ArchMage"))
            {
            Core.AddDrop("Rimeblossom");
            Core.AddDrop("Temporal Floe");
            Core.AddDrop("Starlit Frost");
            
            Core.Logger("Rimeblossom, Temporal Floe, and Starlit Frost will be accepted for ArchMage.");
            } 

            forgeUnlocks.Lament();
        }    
        Core.Logger("All Cape Enchantments are unlocked, continuing.");

        //Prereq Ultra bosses
        UBPreReqs();

        //Finish ArchMage finally
        if(Bot.Config!.Get<bool>("FarmAM") && !Core.CheckInventory("ArchMage"))
        {
            Core.Logger("ArchMage not found, initiating the ArchMage farm now.");
            AM.BookOfIce(); 
            AM.BookOfAether(); 
            AM.BookOfArcana(); 

            //Safety net if else condition
            if(Core.CheckInventory("Elemental Binding", 250))
            {
                AM.GetAM();
            }
        }

        //start NSoD I guess
        Core.Logger("Congratulations, you have everything you need for endgame.", messageBox: true);

        if(!Core.CheckInventory("Necrotic Sword of Doom"))
        {
            Core.Logger("Just kidding.", messageBox: true);
            Core.Logger("There is still the Necrotic Sword of Doom (+51% DMG) to get. WARNING: Farming this item without membership is stupidly long. I would recommend botting something else unless you got really nothing else to do or farm.", messageBox: true);
            NSOD.GetNSOD();
        }

        //WIP: Continue here if needed
    }

    public void CheckEssentials()
    {
        if (!Core.CheckInventory("Blade of Awe")){
            Core.Logger("Blade of Awe not found, initiating the Blade of Awe farm now.");
            BoA.GetBoA();
        }
        Core.Logger("Blade of Awe found, continuing.");

        if (!Core.CheckInventory("Dragon of Time")){
            Core.Logger("Dragon of Time not found, initiating the Dragon of Time farm now.");
            DoT.GetDoT();
        }
        Core.Logger("Dragon of Time found, continuing.");

        Core.Logger("ArchPaladin found, continuing.");
        if (!Core.CheckInventory("ArchPaladin")){
            Core.Logger("ArchPaladin not found, initiating the ArchPaladin farm now.");
            AP.GetAP();
        }
        Core.Logger("ArchPaladin found, continuing.");
    }


    public void BLoDCheckProgress()
    {
        //check if player has daggers, if not check if has almighty aluminium, if not 
        if (!Core.CheckInventory("Blinding Daggers of Destiny")){
            Core.Logger("Blinding Daggers of Destiny not found, checking if it can be farmed today.");

            if (!Core.CheckInventory("Aluminum") && !Core.CheckInventory("Almighty Aluminum") && !Core.CheckInventory("Almighty Aluminum of Destiny")){
                if (Daily.CheckDaily(2091))
                {
                    Daily.MineCrafting(new[] { "Aluminum" }, 1);
                }
                else
                {
                    Core.Logger("'Mine Crafting' has already been completed today, skipping the Blinding Light of Destiny farm.");
                    return;
                }
            }
            BLOD.GetBlindingWeapon(WeaponOfDestiny.Daggers);

        }
        Core.Logger("Blinding Daggers of Destiny found, continuing.");

        if (!Core.CheckInventory("Blinding Mace of Destiny")){
            Core.Logger("Blinding Mace of Destiny not found, checking if it can be farmed today.");

            if (!Core.CheckInventory("Copper") && !Core.CheckInventory("Celestial Copper") && !Core.CheckInventory("Celestial Copper of Destiny")){
                if (Daily.CheckDaily(2091))
                {
                    Daily.MineCrafting(new[] { "Copper" }, 1);
                }
                else
                {
                    Core.Logger("'Mine Crafting' has already been completed today, skipping the Blinding Light of Destiny farm.");
                    return;
                }
            }
            BLOD.GetBlindingWeapon(WeaponOfDestiny.Mace);
            

        }
        Core.Logger("Blinding Mace of Destiny found, farming BLoD now.");
        BLOD.BlindingLightOfDestiny();
    }

    public bool VDKPreReqs()
    {
        bool ready = true;

        if(!Core.CheckInventory("Xyfrag's Slimy Tooth", 5))
        {
            ready = false;
        }
        
        if(!Core.CheckInventory("Nerfkitten's Fang", 3))
        {
            ready = false;
        }
        if(!Core.CheckInventory("Doomkitten's Molar", 20))
        {
            ready = false;
        }
        if(!Core.CheckInventory("Xyfrag's Slimy Tooth", 5))
        {
            ready = false;
        }
        if(!Core.CheckInventory("Deadly Duo's Decayed Denture", 10))
        {
            ready = false;
        }
        
        if (!ready)
        {
            Core.Logger("Verus DoomKnight Prerequisites are missing, farm will be skipped.");
        }
        
        return ready;
    }
    public void UBPreReqs()
    {
        //Prereq Exalted Apotheosis
        Core.Logger("Checking if the prerequirements for Exalted Apotheosis have been fulfilled.");
        EA.PreReqs();

        //Prereq CaV
        Core.Logger("Checking if the prerequirements for Chaos Avenger have been fulfilled.");
        CaV.GetClass();

        //Prereq PrinceDarkonsPoleaxe
        Core.Logger("Checking if the prerequirements for Prince Darkons Poleaxe have been fulfilled.");
        PDPP.FarmPreReqs();

        //Prereq DarkonDebris2Reconstructed
        Core.Logger("Checking if the prerequirements for Darkon's Debris 2 (Reconstructed) have been fulfilled.");
        DDRP.FarmAll();

        //Malgor's Armor first
        Core.Logger(!Core.CheckInventory("Malgor the ShadowLord") ? "Malgor the ShadowLord not found, initiating the Malgor the ShadowLord farm now." : "Malgor the ShadowLord found, continuing.");
        MAS.GetSet();

        //Prereq Ultra Speaker
        Core.Logger("Checking if the prerequirements for Ultra Speaker have been fulfilled.");
        US.GetPrereqs();

    }


}