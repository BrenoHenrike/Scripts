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
//cs_include Scripts/Other/FireChampionsArmor.cs
//cs_include Scripts/Other/Classes/DragonOfTime.cs
//cs_include Scripts/Prototypes/PrinceDarkonsPoleaxePreReqs.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
//cs_include Scripts/Story/Doomwood/DoomwoodPart3.cs
//cs_include Scripts/Story/Doomwood/AQWZombies.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/Story/SepulchureSaga/04ShadowfallRise.cs
//cs_include Scripts/Story/Summer2015AdventureMap/CoreSummer.cs
//cs_include Scripts/Story/Borgars.cs
//cs_include Scripts/Story/StarSinc.cs
//cs_include Scripts/Story/TowerOfDoom.cs
//cs_include Scripts/Story/XansLair.cs
//cs_include Scripts/Good/GearOfAwe/Awescended.cs
//cs_include Scripts/Nation/AFDL/NulgathDemandsWork.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Nation/Various/GoldenHanzoVoid.cs
//cs_include Scripts/Nation/AFDL/WillpowerExtraction.cs
//cs_include Scripts/Story/BattleUnder.cs
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
//cs_include Scripts/Legion/SwordMaster.cs
//cs_include Scripts/Dailies/LordOfOrder.cs
//cs_include Scripts/Story/Nation/CitadelRuins.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/LivingDungeon.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class UnlockForgeEnhancements
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public CoreNation Nation = new();
    public CoreLegion Legion = new();
    public CoreDarkon Darkon = new();
    public CoreAwe Awe = new();
    public Core13LoC LOC => new();
    public CoreNSOD CorNSOD = new();
    public CoreAstravia Astravia => new();
    public CoreDailies Daily = new();
    public Core7DD DD = new();
    public CoreYnR YNR = new();

    public CoreSoW SoW = new();
    public ShadowfallRise SFR = new();
    public ArchPaladin AP = new();
    public DragonOfTime DOT = new();
    public FireChampionsArmor FCA = new();
    public EternalDrakath ED = new();
    public SepulchuresOriginalHelm Seppy = new();
    public PrinceDarkonsPoleaxePreReqs PDPPR = new();
    public HeadoftheLegionBeast HOTLB = new();
    public Awescended Awescended = new();
    public NulgathDemandsWork NDW = new();
    public ThirdSpell TSS = new();
    public LordOfOrder LOO = new();

    public string OptionsStorage = "Forge Ehn Unlocks";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<ForgeQuestWeapon>("ForgeQuestWeapon", "Weapon Enhancement", "Forge Quests to unlock Weapon Enhancement, change to none to unselect", ForgeQuestWeapon.None),
        new Option<ForgeQuestCape>("ForgeQuestCape", "Cape Enhancement", "Forge Quests to unlock Cape Enhancement, change to none to unselect", ForgeQuestCape.None),
        new Option<ForgeQuestHelm>("ForgeQuestHelm", "Helm Enhancement", "Forge Quests to unlock Helm Enhancement, change to none to unselect", ForgeQuestHelm.None),
        new Option<bool>("UseGold", "Use Gold", "Speed the BlacksmithingREP grind up with Gold?", false)
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        ForgeUnlocks();

        Core.SetOptions(false);
    }

    public void ForgeUnlocks()
    {
        if (Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape") == ForgeQuestCape.None && Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon") == ForgeQuestWeapon.None && Bot.Config.Get<ForgeQuestHelm>("ForgeQuestHelm") == ForgeQuestHelm.None)
            Core.Logger("all settings are set to None, no Forge Quest to do. Stopping script.", messageBox: true, stopBot: true);

        if (Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon") != ForgeQuestWeapon.None)
        {
            if (Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon") != ForgeQuestWeapon.All)
                Core.Logger($"Selected Forge Weapon Enhancement: {Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon")}");

            switch (Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon").ToString())
            {
                case "ForgeWeaponEnhancement":
                    ForgeWeaponEnhancement();
                    break;

                case "Lacerate":
                    Lacerate();
                    break;

                case "Smite":
                    Smite();
                    break;

                case "HerosValiance":
                    HerosValiance();
                    break;

                case "ArcanasConcertoWIP":
                    ArcanasConcertoWIP();
                    break;

                case "Elysium":
                    Elysium();
                    break;


                case "Acheron":
                    Acheron();
                    break;

                case "All":
                    Core.Logger("Selected to unlock all Forge Weapon Enhancements");
                    ForgeWeaponEnhancement();
                    Lacerate();
                    Smite();
                    HerosValiance();
                    ArcanasConcertoWIP();
                    Acheron();
                    Elysium();
                    break;
            }
        }

        if (Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape") != ForgeQuestCape.None)
        {
            if (Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape") != ForgeQuestCape.All)
                Core.Logger($"Selected Forge Cape Enhancement: {Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape")}");

            switch (Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape").ToString())
            {
                case "ForgeCapeEnhancement":
                    ForgeCapeEnhancement();
                    break;

                case "Absolution":
                    Absolution();
                    break;

                case "Vainglory":
                    Vainglory();
                    break;

                case "Avarice":
                    Avarice();
                    break;

                case "Penitence":
                    Penitence();
                    break;

                case "Lament":
                    Lament();
                    break;

                case "All":
                    Core.Logger("Selected to unlock all Forge Cape Enhancements");
                    ForgeCapeEnhancement();
                    Avarice(); //Calls on to the other functions internally
                    Penitence();
                    Lament();
                    break;
            }
        }

        if (Bot.Config.Get<ForgeQuestHelm>("ForgeQuestHelm") != ForgeQuestHelm.None)
        {
            if (Bot.Config.Get<ForgeQuestHelm>("ForgeQuestHelm") != ForgeQuestHelm.All)
                Core.Logger($"Selected Forge Cape Enhancement: {Bot.Config.Get<ForgeQuestHelm>("ForgeQuestHelm")}");

            switch (Bot.Config.Get<ForgeQuestHelm>("ForgeQuestHelm").ToString())
            {
                case "ForgeHelmEnhancement":
                    ForgeHelmEnhancement();
                    break;

                case "Vim":
                    Core.Logger("Selected to unlock Vim Helm Enh");
                    Vim();
                    break;

                case "Examen":
                    Core.Logger("Selected to unlock Examen Helm Enh");
                    Examen();
                    break;

                case "Anima":
                    Core.Logger("Selected to unlock Anima Helm Enh");
                    Anima();
                    break;

                case "Pneuma":
                    Core.Logger("Selected to unlock Pneuma Helm Enh");
                    Pneuma();
                    break;

                case "All":
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

    public void ForgeWeaponEnhancement()
    {
        if (Core.isCompletedBefore(8738))
            return;

        Core.Logger("Unlocking Enhancement: Forge (Weapon)");

        LOC.Kitsune();
        Farm.Experience(30);
        Farm.BlacksmithingREP(4, Bot.Config.Get<bool>("UseGold"));

        Core.EquipClass(ClassType.Solo);
        Core.EnsureAccept(8738);

        Core.KillEscherion("1st Lord Of Chaos Helm");
        Core.KillVath("Chaos Dragonlord Helm");
        Core.HuntMonster("kitsune", "Kitsune", "Chaos Shogun Helmet", isTemp: false);
        Bot.Quests.UpdateQuest(597);
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
        Farm.BlacksmithingREP(5, Bot.Config.Get<bool>("UseGold"));

        Core.EnsureAccept(8739);

        Core.HuntMonster("graveyard", "Big Jack Sprat", "Undead Plague Spear", isTemp: false);
        Core.HuntMonster("river", "Kuro", "Kuro's Wrath", isTemp: false);

        if (!Core.CheckInventory("Massive Horc Cleaver"))
        {
            Core.AddDrop("Massive Horc Cleaver");
            Core.EnsureAccept(279);

            Core.HuntMonster("warhorc", "General Drox", "Boss Prize");

            Core.EnsureComplete(279);
            Bot.Wait.ForPickup("Massive Horc Cleaver");
        }

        if (!Core.CheckInventory("Sword in the Stone"))
        {
            Core.AddDrop("Sword in the Stone");
            Core.EnsureAccept(316);

            Core.GetMapItem(54, 7, "greenguardeast");
            Core.HuntMonster("greenguardeast", "Spider", "Tiny Sword");

            Core.EnsureComplete(316);
            Bot.Wait.ForPickup("Sword in the Stone");
        }

        if (!Core.CheckInventory("Forest Axe"))
        {
            Core.AddDrop("Forest Axe");
            Core.EnsureAccept(301);

            Core.GetMapItem(55, 4, "farm");
            Core.HuntMonster("farm", "Mosquito", "Mosquito Juice");

            Core.EnsureComplete(301);
            Bot.Wait.ForPickup("Forest Axe");
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

        SFR.StoryLine();
        Farm.Experience(60);
        Farm.BlacksmithingREP(6, Bot.Config.Get<bool>("UseGold"));

        Core.EnsureAccept(8740);

        Core.HuntMonster("shadowattack", "Death", "Death's Power", 3, isTemp: false);
        Core.KillEscherion("Chaotic Power", 7);
        Core.HuntMonster("shadowrealmpast", "*", "Empowered Essence", 50, isTemp: false);
        Core.HuntMonster("undergroundlabb", "Ultra Battle Gem", "Gem Power", 25, false);
        Adv.BuyItem("alchemyacademy", 2116, "Power Tonic", 10);

        Core.EnsureComplete(8740);
        Core.Logger("Enhancement Unlocked: Smite");
    }

    public void HerosValiance()
    {
        if (Core.isCompletedBefore(8741))
            return;

        Core.Logger("Unlocking Enhancement: Hero's Valiance");
        LOO.GetLoO();
        if (!Core.isCompletedBefore(7165))
        {
            Core.Logger("Quest Progrestion not Available For LOO (requires last quest to be complete and these are dailies)");
            return;
        }
        FCA.GetFireChampsArmor();
        DOT.GetDoT(doExtra: false);
        ED.getSet();
        if (!Core.CheckInventory(23689))
        {
            Core.AddDrop("Eternity Blade");
            Core.EnsureAccept(3485);
            Bot.Quests.UpdateQuest(3484);
            Core.HuntMonster("towerofdoom10", "Slugbutter", "Eternity Blade");
            Core.EnsureComplete(3485);
            Bot.Wait.ForPickup(23689);
        }

        Farm.Experience(100);
        Farm.BlacksmithingREP(10, Bot.Config.Get<bool>("UseGold"));

        Core.EnsureAccept(8741);

        Seppy.GravelynsDoomFireToken();
        AP.GetAP(false); //purely for the last quest "Sacred Magic: Eden"
        Adv.BuyItem("darkthronehub", 1303, "ArchPaladin Armor");

        Core.EnsureComplete(8741);
        Core.Logger("Enhancement Unlocked: Hero's Valiance");
    }

    public void ArcanasConcertoWIP()
    {
        if (Core.isCompletedBefore(8742))
            return;

        Core.Logger("Unlocking Enhancement: Arcana's Concerto (WIP)");

        Astravia.CompleteCoreAstravia();
        Farm.Experience(100);
        Farm.BlacksmithingREP(10, Bot.Config.Get<bool>("UseGold"));

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
            Darkon.MourningFlower(22);
            if (!Core.CheckInventory("Darkon Insignia", 20))
            {
                Core.Logger(" x20 \"Darkon Insignia\" is Required to continue quest, our Bots cannot *currently* kill this mob Untill CoreArmy is Released and a script is made.", messageBox: true);
                return;
            }
            else Core.BuyItem("ultradarkon", 2147, "Darkon's Debris 2 (Reconstructed)");
        }

        if (!Core.CheckInventory("King Drago Insignia", 5))
        {
            Core.Logger(" x5 \"King Drago Insignia\" is required to continue quest, our Bots cannot *currently* kill this mob untill CoreArmy is Released and a script is made.", messageBox: true);
            return;
        }
        if (!Core.CheckInventory("Darkon Insignia", 5))
        {
            Core.Logger(" x5 \"Darkon Insignia\" is required to continue quest, our Bots cannot *currently* kill this mob untill CoreArmy is Released and a script is made.", messageBox: true);
            return;
        }
        Core.ChainComplete(8742);
        Core.Logger("Enhancement Unlocked: Arcana's Concerto");
    }

    public void Acheron()
    {
        if (Core.isCompletedBefore(8820))
            return;

        Core.EnsureAccept(8820);
        VoidLodestone();
        SoW.Tyndarius();
        // have the Dark Box and Dark Key mini-saga completed 
        // Quest complete will require you to turn in the Power of Darkness, 
        Core.BuyItem(Bot.Map.Name, 1380, "The Power of Darkness");
        //20 Dark Potions,
        Daily.MonthlyTreasureChestKeys();
        if (!Core.CheckInventory(new[] { "Dark Box", "Dark Key" }))
        {
            Core.Logger("Dark Box & Key Not Found, Cannot Continue with Enh");
            return;
        }

        Core.RegisterQuests(5710);
        while (!Bot.ShouldExit && !Core.CheckInventory("Dark Potion", 20) && Core.CheckInventory(new[] { "Dark Box", "Dark Key" }))
        {
            if (Core.IsMember)
                Core.HuntMonster("darkfortress", "Dark Elemental", "Dark Gem", isTemp: false);
            else Core.HuntMonster("ruins", "Dark Elemental", "Dark Gem", isTemp: false);
        }
        Core.CancelRegisteredQuests();

        Core.HuntMonster("tercessuinotlim", "Nulgass", "The Mortal Coil", isTemp: false);
        Core.EnsureComplete(8820);
        Core.Logger("Enhancement Unlocked: Acheron");
    }

    public void Elysium()
    {
        if (Core.isCompletedBefore(8821))
            return;

        Core.EnsureAccept(8821);
        CorNSOD.BonesVoidRealm(20);
        YNR.BlademasterSwordScroll();
        NDW.NDWQuest(new[] { "Archfiend Essence Fragment" }, 3);
        Awescended.GetAwe();
        if (!Core.CheckInventory("The Divine Will"))
        {
            Core.Logger("\"Azalith\" is not Soloable, please go kill it otherwise for the Drop \"The Divine Will\", and return here and re-run the script.");
            return;
        }
        Core.EnsureComplete(8821);
        Core.Logger("Enhancement Unlocked: insert");
    }

    public void ForgeCapeEnhancement()
    {
        if (Core.isCompletedBefore(8758))
            return;

        Core.Logger("Unlocking Enhancement: Forge (Cape)");

        LOC.Escherion();
        Farm.Experience(30);
        Farm.BlacksmithingREP(3, Bot.Config.Get<bool>("UseGold"));

        Core.EquipClass(ClassType.Solo);
        Core.EnsureAccept(8758);

        Core.KillEscherion("1st Lord Of Chaos Staff");
        Core.KillVath("Chaos Dragonlord Axe");
        Core.HuntMonster("kitsune", "Kitsune", "Hanzamune Dragon Koi Blade", isTemp: false);
        Core.HuntMonster("wolfwing", "Wolfwing", "Wrath of the Werepyre", isTemp: false);

        Core.EnsureComplete(8758);
        Core.Logger($"Enhancement Unlocked: Forge (Cape)");
    }

    public void Absolution()
    {
        if (Core.isCompletedBefore(8743))
            return;

        Core.Logger("Unlocking Enhancement: Absolution");

        Farm.Experience(90);
        Farm.BlacksmithingREP(9, Bot.Config.Get<bool>("UseGold"));

        Core.EnsureAccept(8743);

        Farm.GoodREP(10);
        if (!Core.CheckInventory("Ascended Paladin"))
        {
            Core.HuntMonster("therift", "Plague Spreader", "Slimed Sigil", 200, isTemp: false);
            Core.BuyItem("therift", 1399, 39091);
            Core.BuyItem("therift", 1399, 39093);
            Core.BuyItem("therift", 1399, 39094);
        }

        Core.EnsureComplete(8743);
        Core.Logger("Enhancement Unlocked: Absolution");
    }

    public void Vainglory()
    {
        if (Core.isCompletedBefore(8744))
            return;

        Absolution();
        Core.Logger("Unlocking Enhancement: Vaingory");

        Core.EnsureAccept(8744);

        if (Core.IsMember)
        {
            if (!Core.CheckInventory("Pauldron Relic"))
            {
                Core.AddDrop("Pauldron Fragment");
                Core.EquipClass(ClassType.Solo);

                Core.RegisterQuests(4162);
                while (!Bot.ShouldExit && !Core.CheckInventory("Pauldron Fragment", 15))
                {
                    Adv.BoostHuntMonster("gravestrike", "Ultra Akriloth", "Pauldron Shard", 15, false);
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
        Awe.GetAweRelic("Gauntlet", 4169, 25, 5, "alteonbattle", "Ultra Alteon");
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

        Core.EnsureAccept(8745);

        HOTLB.Indulgence(75);
        HOTLB.Penance(75);

        Core.EnsureComplete(8745);
        Core.Logger("Enhancement Unlocked: Avarice");
    }

    public void Penitence()
    {
        Avarice();

        if (Core.isCompletedBefore(8822))
            return;

        Core.EnsureAccept(8822);
        Bot.Quests.UpdateQuest(3008);
        Core.AddDrop("Night Mare Scythe");
        while (!Bot.ShouldExit && !Core.CheckInventory("Night Mare Scythe"))
        {
            Core.EnsureAccept(3270);
            Adv.KillUltra("doomvault", "r5", "Left", "Binky", "Yulgar's Lost Scythe");
            Core.EnsureComplete(3270);
        }
        Core.HuntMonster("frozenlair", "Legion Lich Lord", "Sapphire Orb", 100, isTemp: false);
        Core.HuntMonster("icestormarena", "Warlord Icewing", "Boreal Cavalier Bardiche", isTemp: false);
        Core.HuntMonster("underlair", "ArchFiend DragonLord", "Void Scale", 13, isTemp: false);
        Core.EnsureComplete(8822);
        Core.Logger("Enhancement Unlocked: Penitence");
    }

    public void Lament()
    {
        Penitence();

        if (Core.isCompletedBefore(8823))
            return;

        Core.EnsureAccept(8823);
        Core.HuntMonster("sepulchurebattle", "Ultra Sepulchure", "Doom Heart", isTemp: false);
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

    public void ForgeHelmEnhancement()
    {
        if (Core.isCompletedBefore(8828))
            return;

        Core.EnsureAccept(8828);
        Core.HuntMonster("lostruinswar", "Diabolical Warlord", "Prismatic Celestial Wings", isTemp: false);
        Core.HuntMonster("lostruins", "Infernal Warlord", "Broken Wings", isTemp: false);
        Core.HuntMonster("infernalspire", "Azkorath", "Shadow's Wings", isTemp: false);
        Core.HuntMonster("infernalspire", "Malxas", "Wings Of Destruction", isTemp: false);
        Core.EnsureComplete(8828);
        Core.Logger("Enhancement Unlocked: ForgeHelmEnhancement");
    }


    public void Vim()
    {
        ForgeHelmEnhancement();

        if (Core.isCompletedBefore(8824))
            return;

        Core.EnsureAccept(8824);
        Core.BuyItem("Classhalla", 172, "Rogue");
        Adv.rankUpClass("Rogue");
        Bot.Quests.UpdateQuest(3484);
        Core.HuntMonster("Towerofdoom10", "*", "Ethereal Essence", 250, isTemp: false);
        Core.EnsureComplete(8824);
        Core.Logger("Enhancement Unlocked: Vim");
    }

    public void Examen()
    {
        Vim();

        if (Core.isCompletedBefore(8825))
            return;

        Core.EnsureAccept(8825);
        Core.BuyItem("Classhalla", 176, "Healer");
        Adv.rankUpClass("Healer");
        Bot.Quests.UpdateQuest(3484);
        Core.HuntMonster("Towerofdoom10", "*", "Ethereal Essence", 250, isTemp: false);
        Core.EnsureComplete(8825);
        Core.Logger("Enhancement Unlocked: Examen");
    }

    public void Anima()
    {
        Examen();

        if (Core.isCompletedBefore(8826))
            return;

        Core.EnsureAccept(8826);
        Core.BuyItem("Classhalla", 170, "Warrior");
        Adv.rankUpClass("Warrior");
        Bot.Quests.UpdateQuest(3484);
        Core.HuntMonster("Towerofdoom10", "*", "Ethereal Essence", 650, isTemp: false);
        Core.EnsureComplete(8826);
        Core.Logger("Enhancement Unlocked: Anima");
    }

    public void Pneuma()
    {
        Anima();

        if (Core.isCompletedBefore(8827))
            return;

        Core.EnsureAccept(8827);
        Core.BuyItem("Classhalla", 174, "Mage");
        Adv.rankUpClass("Mage");
        Bot.Quests.UpdateQuest(3484);
        Core.HuntMonster("Towerofdoom10", "*", "Ethereal Essence", 650, isTemp: false);
        Core.EnsureComplete(8827);
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
            if (!Core.CheckInventory(38565))
                TheDarkBox(38565);

            if (Core.CheckInventory(38565))
            {
                Core.EnsureAccept(5723);
                Core.HuntMonster("dreadfire", "Stray Mana", "Bronze Key", isTemp: false);
                Core.HuntMonster("dreadfire", "Living Brimstone", "Silver Key", isTemp: false);
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

    public void TheDarkBox(string Item = "any", int quant = 1)
    {
        Daily.MonthlyTreasureChestKeys();
        if (!Core.CheckInventory(new[] { "Dark Box", "Dark Key" }))
        {
            Core.Logger("Dark Box & Key Not Found, Cannot Continue with Enh");
            return;
        }

        Core.Logger("Pray to RNGsus for your item");
        if (!Bot.ShouldExit && !Core.CheckInventory(Item, quant) && Core.CheckInventory(new[] { "Dark Box", "Dark Key" }))
        {
            Core.EnsureAccept(5710);
            if (Core.IsMember)
                Core.HuntMonster("darkfortress", "Dark Elemental", "Dark Gem", isTemp: false);
            else Core.HuntMonster("ruins", "Dark Elemental", "Dark Gem", isTemp: false);
            Core.EnsureComplete(5710);
        }
    }

    public void TheDarkBox(int itemID, int quant = 1)
    {
        Daily.MonthlyTreasureChestKeys();
        if (!Core.CheckInventory(new[] { "Dark Box", "Dark Key" }))
        {
            Core.Logger("Dark Box & Key Not Found, Cannot Continue with Enh");
            return;
        }

        Core.Logger("Pray to RNGsus for your item");
        if (!Bot.ShouldExit && !Core.CheckInventory(itemID, quant) && Core.CheckInventory(new[] { "Dark Box", "Dark Key" }))
        {
            Core.EnsureAccept(5710);
            if (Core.IsMember)
                Core.HuntMonster("darkfortress", "Dark Elemental", "Dark Gem", isTemp: false);
            else Core.HuntMonster("ruins", "Dark Elemental", "Dark Gem", isTemp: false);
            Core.EnsureComplete(5710);
        }
    }
}

public enum ForgeQuestWeapon
{
    ForgeWeaponEnhancement,
    Lacerate,
    Smite,
    HerosValiance,
    ArcanasConcertoWIP,
    Acheron,
    Elysium,
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
