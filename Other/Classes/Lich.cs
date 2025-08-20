/*
name: Lich Class
description: Gets the Lich Class and its pre-requisites.
tags: Lich, class, pre-requisites, necronomicon, nictos, klaatu, verata, necromicon, shadow lich, frozen lair, legion
*/

#region Includes Area
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Seasonal/Friday13th/MergeShops/ColossalWaresMerge.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Evil/VordredsArmor.cs
//cs_include Scripts/Farm/REP/GrimskullTrollingRep.cs
//cs_include Scripts/Prototypes/MoreSkullsWorldBoss.cs
//cs_include Scripts/Chaos/DrakathsArmor.cs
//cs_include Scripts/Chaos/EternalDrakathSet.cs
//cs_include Scripts/Dailies/LordOfOrder.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Darkon/Various/PrinceDarkonsPoleaxePreReqs.cs
//cs_include Scripts/Enhancement/UnlockForgeEnhancements.cs
//cs_include Scripts/Evil/ADK.cs
//cs_include Scripts/Evil/NSoD/CoreNSOD.cs
//cs_include Scripts/Evil/SepulchuresOriginalHelm.cs
//cs_include Scripts/Good/ArchPaladin.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Good/GearOfAwe/ArmorOfAwe.cs
//cs_include Scripts/Good/GearOfAwe/Awescended.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
//cs_include Scripts/Good/GearOfAwe/HelmOfAwe.cs
//cs_include Scripts/Good/Paladin.cs
//cs_include Scripts/Good/SilverExaltedPaladin.cs
//cs_include Scripts/Hollowborn/CoreHollowborn.cs
//cs_include Scripts/Hollowborn/TradingandStuff(single).cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/HeadOfTheLegionBeast.cs
//cs_include Scripts/Legion/SwordMaster.cs
//cs_include Scripts/Legion/YamiNoRonin/CoreYnR.cs
//cs_include Scripts/Nation/AFDL/NulgathDemandsWork.cs
//cs_include Scripts/Nation/AFDL/WillpowerExtraction.cs
//cs_include Scripts/Nation/AssistingCragAndBamboozle[Mem].cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/EmpoweringItems.cs
//cs_include Scripts/Nation/MergeShops/DilligasMerge.cs
//cs_include Scripts/Nation/MergeShops/DirtlickersMerge.cs
//cs_include Scripts/Nation/MergeShops/NationMerge.cs
//cs_include Scripts/Nation/MergeShops/NulgathDiamondMerge.cs
//cs_include Scripts/Nation/MergeShops/VoidChasmMerge.cs
//cs_include Scripts/Nation/MergeShops/VoidRefugeMerge.cs
//cs_include Scripts/Nation/NationLoyaltyRewarded.cs
//cs_include Scripts/Nation/VHL/CoreVHL.cs
//cs_include Scripts/Nation/Various/ArchfiendDeathLord.cs
//cs_include Scripts/Nation/Various/DragonBlade[mem].cs
//cs_include Scripts/Nation/Various/GoldenHanzoVoid.cs
//cs_include Scripts/Nation/Various/JuggernautItems.cs
//cs_include Scripts/Nation/Various/PrimeFiendShard.cs
//cs_include Scripts/Nation/Various/PurifiedClaymoreOfDestiny.cs
//cs_include Scripts/Nation/Various/SwirlingTheAbyss.cs
//cs_include Scripts/Nation/Various/TarosManslayer.cs
//cs_include Scripts/Nation/Various/TheLeeryContract[Member].cs
//cs_include Scripts/Nation/Various/VoidPaladin.cs
//cs_include Scripts/Nation/Various/VoidSpartan.cs
//cs_include Scripts/Other/Armor/FireChampionsArmor.cs
//cs_include Scripts/Other/Armor/MalgorsArmorSet.cs
//cs_include Scripts/Other/Classes/DragonOfTime.cs
//cs_include Scripts/Other/Classes/DragonslayerGeneral.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Other/MergeShops/InfernalArenaMerge.cs
//cs_include Scripts/Other/MysteriousEgg.cs
//cs_include Scripts/Other/ShadowDragonDefender.cs
//cs_include Scripts/Other/WarFuryEmblem.cs
//cs_include Scripts/Other/Weapons/FortitudeAndHubris.cs
//cs_include Scripts/Other/Weapons/GoldenBladeOfFate.cs
//cs_include Scripts/Other/Weapons/PinkBladeofDestruction.cs
//cs_include Scripts/Other/Weapons/ShadowReaperOfDoom.cs
//cs_include Scripts/Other/Weapons/VoidAvengerScythe.cs
//cs_include Scripts/Other/Weapons/WrathofNulgath.cs
//cs_include Scripts/ShadowsOfWar/CoreSoWMats.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/DeadLinesMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/ManaCradleMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/ShadowflameFinaleMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/StreamwarMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/TimekeepMerge.cs
//cs_include Scripts/ShadowsOfWar/MergeShops/WorldsCoreMerge.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleDelve.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleDelveMerge.cs
//cs_include Scripts/Seasonal/StaffBirthdays/Nulgath/TempleSiege.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Story/7DeadlyDragons/Extra/HatchTheEgg.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Story/Borgars.cs
//cs_include Scripts/Story/DjinnGate.cs
//cs_include Scripts/Story/DoomVault.cs
//cs_include Scripts/Story/DoomVaultB.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
//cs_include Scripts/Story/J6Saga.cs
//cs_include Scripts/Story/Lair.cs
//cs_include Scripts/Story/Legion/DarkWarLegionandNation.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/Nation/Bamboozle.cs
//cs_include Scripts/Story/Nation/CitadelRuins.cs
//cs_include Scripts/Story/Nation/Fiendshard.cs
//cs_include Scripts/Story/Nation/Originul.cs
//cs_include Scripts/Story/Nation/VoidChasm.cs
//cs_include Scripts/Story/Nation/VoidRefuge.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/CelestialArena.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/InfernalArena.cs
//cs_include Scripts/Story/SepulchureSaga/CoreSepulchure.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/StarSinc.cs
//cs_include Scripts/Story/Summer2015AdventureMap/CoreSummer.cs
//cs_include Scripts/Story/ThirdSpell.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Story/TowerOfDoom.cs
//cs_include Scripts/Story/XansLair.cs
//cs_include Scripts/Story/Yokai.cs
//cs_include Scripts/Prototypes/Grimgaol.cs
#endregion

using System.Threading.Tasks;
using Skua.Core.Interfaces;
using Skua.Core.Models.Players;
using Skua.Core.Options;
using Newtonsoft.Json.Linq;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Items;
using Skua.Core.Models.Auras;

public class Lich
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    private CoreAdvanced Adv = new();
    private CoreFarms Farm = new();
    private CoreStory Story = new();
    private CoreDailies Daily = new();
    private ColossalWaresMerge ColossalWaresMerge = new();
    private GrimskullTrollingRep GrimskullTrollingRep = new();
    public static Grimgaol GRunOptions = new();
    public Grimgaol GrimGaolRun = new();


    public bool DontPreconfigure = true;
    public string OptionsStorage = GRunOptions.OptionsStorage;
    public List<IOption> Options = GRunOptions.Options;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Example();

        Core.SetOptions(false);
    }

    public void Example(bool rankUpClass = true)
    {
        if (Core.CheckInventory("Lich"))
        {
            if (rankUpClass)
                Adv.RankUpClass("Lich");
            return;
        }
        // Verata's Necronomicon
        // Ensure you have the required items for Grimskull Trolling Rep
        if (!Core.CheckInventory("Verata's Necronomicon"))
        {
            GrimskullTrollingRep.DoGrimskullTrollingRep();
            Core.BuyItem("gaolcell", 2362, "Verata's Necronomicon");
            Bot.Wait.ForPickup("Verata's Necronomicon");
        }

        // Nictos Necronomicon
        if (!Core.CheckInventory("Nicto's Necronomicon"))
        {
            if (!Core.IsMember)
            {
                Core.Logger("You need to be a member to buy Nicto's Necronomicon.");
                return;
            }
            ColossalWaresMerge.BuyAllMerge("Nicto's Necronomicon");
            Bot.Wait.ForPickup("Nicto's Necronomicon");
        }

        // Klaatu's Necronomicon
        Core.AddDrop("Klaatu's Necronomicon");
        if (!Core.CheckInventory("Klaatu's Necronomicon"))
        {
            Core.EnsureAccept(10338);
            Core.EquipClass(ClassType.Farm);
            Farm.BattleUnderB(quant: 6666);
            Farm.EvilREP();
            Adv.BuyItem("Shadowfall", 89, "Shadow Lich");
            Core.EquipClass(ClassType.Solo);
            Core.HuntMonster("DarkoviaForest", "Lich of the Stone", "Lich Of The Stone", isTemp: false);
            Core.HuntMonster("frozenlair", "Legion Lich Lord", "Sapphire Orb", 113, isTemp: false);
            Core.GetMapItem(14740, 1, "necroproject");
            Core.EnsureComplete(10338);
            Bot.Wait.ForPickup("Klaatu's Necronomicon");
        }

        // Lich Class
        if (!Core.CheckInventory("Lich Class"))
        {
            Core.AddDrop(94824 /* Lich class item ID */);
            Core.ChainComplete(10339);
            Bot.Wait.ForPickup("Lich");


        }

        if (rankUpClass)
            Adv.RankUpClass("Lich");
    }

    // Create a void for each necropnomicon:
    public void KlaatuNecropnomicon()
    {
        if (Core.CheckInventory("Klaatu's Necronomicon"))
            return;

        Core.EnsureAccept(10338);
        Core.EquipClass(ClassType.Farm);
        Farm.BattleUnderB(quant: 6666);
        Farm.EvilREP();
        Adv.BuyItem("Shadowfall", 89, "Shadow Lich");
        Core.EquipClass(ClassType.Solo);
        Core.HuntMonster("DarkoviaForest", "Lich of the Stone", "Lich Of The Stone", isTemp: false);
        Core.HuntMonster("frozenlair", "Legion Lich Lord", "Sapphire Orb", 113, isTemp: false);
        Core.GetMapItem(14740, 1, "necroproject");
        Core.EnsureComplete(10338);
        Bot.Wait.ForPickup("Klaatu's Necronomicon");
    }
    public void NictosNecropnomicon()
    {
        if (Core.CheckInventory("Nicto's Necronomicon"))
            return;

        ColossalWaresMerge.BuyAllMerge("Nicto's Necronomicon");
        Bot.Wait.ForPickup("Nicto's Necronomicon");
    }
    public void VeratasNecropnomicon()
    {
        if (Core.CheckInventory("Verata's Necromicon"))
            return;

        if (Farm.FactionRank("Grimskull Trolling") < 10)
        {
            Core.Logger("You need to have rank 10 Grimskull Trolling Rep to buy Verata's Necromicon.");
            GrimGaolRun.DoGrimGaol();
        }
        Core.BuyItem("gaolcell", 2362, "Verata's Necromicon");
        Bot.Wait.ForPickup("Verata's Necromicon");
    }

    public void LichClass(bool rankup = true)
    {
        if (Core.CheckInventory("Lich"))
        {
            if (rankup && Core.CheckClassRank(false, "Lich") < 10)
            {
                Core.Logger("You have the Lich Class, but not at max rank. Rank up to 10.");
                Adv.GearStore();
                Adv.SmartEnhance("Lich");
                Farm.Experience(rankUpClass: true);
                Adv.GearStore(true);
            }
            return;
        }

        // Ensure you have the required necropnomicons
        KlaatuNecropnomicon();
        NictosNecropnomicon();
        VeratasNecropnomicon();

        // Complete the quest to get the Lich Class
        {
            if (Core.CheckInventory(94824 /* Lich class item ID */))
            {

                return;
            }
            Core.AddDrop(94824 /* Lich class item ID */);
            Core.ChainComplete(10339);
            Bot.Wait.ForPickup("Lich");

            if (rankup && Core.CheckClassRank(false, "Lich") < 10)
            {
                Core.Logger("Ranking up Lich Class.");
                Adv.GearStore();
                Adv.SmartEnhance("Lich");
                Farm.Experience(rankUpClass: true);
                Adv.GearStore(true);
            }
        }
    }
}

