//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Chaos/EternalDrakathSet.cs
//cs_include Scripts/Chaos/DrakathsArmor.cs
//cs_include Scripts/Good/ArchPaladin.cs
//cs_include Scripts/Good/Paladin.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Evil/SepulchuresOriginalHelm.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/HeadOfTheLegionBeast.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Other/WarFuryEmblem.cs
//cs_include Scripts/Other/MysteriousEgg.cs
//cs_include Scripts/Other/Classes/DragonslayerGeneral.cs
//cs_include Scripts/Other/Weapons/GoldenBladeOfFate.cs
//cs_include Scripts/Other/Weapons/PinkBladeofDestruction.cs
//cs_include Scripts/Other/FireChampionsArmor.cs
//cs_include Scripts/Other/Classes/DragonOfTime.cs
//cs_include Scripts/Prototypes/PrinceDarkonsPoleaxePreReqs.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
//cs_include Scripts/Story/Doomwood/DoomwoodPart3.cs
//cs_include Scripts/Story/Doomwood/AQWZombies.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/Story/SepulchureSaga/04ShadowfallRise.cs
//cs_include Scripts/Story/WarfuryTraining.cs
//cs_include Scripts/Story/Collection.cs
//cs_include Scripts/Story/Borgars.cs
//cs_include Scripts/Story/StarSinc.cs
//cs_include Scripts/Story/TowerOfDoom.cs
//cs_include Scripts/Story/XansLair.cs
using RBot;
using RBot.Options;

public class UnlockForgeEnhancements
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CoreAdvanced Adv = new();
    public CoreNation Nation = new();
    public CoreLegion Legion = new();
    public CoreDarkon Darkon = new();
    public CoreAwe Awe = new();

    public Core13LoC LOC => new();
    public CoreAstravia Astravia => new();
    public ShadowfallRise SFR = new();

    public ArchPaladin AP = new();
    public DragonOfTime DOT = new();
    public FireChampionsArmor FCA = new();
    public EternalDrakath ED = new();
    public SepulchuresOriginalHelm Seppy = new();
    public PrinceDarkonsPoleaxePreReqs PDPPR = new();
    public HeadoftheLegionBeast HOTLB = new();

    public string OptionsStorage = "Forge Ehn Unlocks";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("SkipOption", "Skip this window next time", "You will be able to return to this screen via [Options] -> [Script Options] if you wish to change anything.", false),
        new Option<ForgeQuestWeapon>("ForgeQuestWeapon", "Weapon Enhancement", "Forge Quests to unlock Weapon Enhancement, change to none to unselect", ForgeQuestWeapon.None),
        new Option<ForgeQuestCape>("ForgeQuestCape", "Cape Enhancement", "Forge Quests to unlock Cape Enhancement, change to none to unselect", ForgeQuestCape.None),
        new Option<bool>("UseGold", "Use Gold", "Speed the BlacksmithingREP grind up with Gold?", false)
    };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        ForgeUnlocks();

        Core.SetOptions(false);
    }

    public void ForgeUnlocks()
    {
        if (!Bot.Config.Get<bool>("SkipOption"))
            Bot.Config.Configure();

        if (Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape") == ForgeQuestCape.None && Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon") == ForgeQuestWeapon.None)
            Core.Logger("Both settings are set to None, no Forge Quest to do. Stopping script.", messageBox: true, stopBot: true);

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

                case "All":
                    Core.Logger("Selected to unlock all Forge Weapon Enhancements");
                    ForgeWeaponEnhancement();
                    Lacerate();
                    Smite();
                    HerosValiance();
                    ArcanasConcertoWIP();
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

                case "All":
                    Core.Logger("Selected to unlock all Forge Cape Enhancements");
                    ForgeCapeEnhancement();
                    Avarice(); //Calls on to the other functions internally
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

        FCA.GetFireChampsArmor();
        DOT.GetDoT(doExtra: false);
        ED.getSet();

        if (!Core.CheckInventory("Eternity Blade"))
        {
            Core.EnsureAccept(3485);
            Bot.Quests.UpdateQuest(3484);
            Core.HuntMonster("towerofdoom10", "Slugbutter", "Eternity Blade");
            Core.EnsureComplete(3485);
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
            Core.Logger("You must have faced Darkon the Conductor and done the weekly quest in order to unlock \"Arcana's Concerto\"", stopBot: true);
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
                Core.Logger(" x20 \"Darkon Insignia\" is Required to continue quest, our Bots cannot *currently* kill this mob Untill CoreArmy is Released and a script is made.", messageBox: true, stopBot: true);
            else Core.BuyItem("ultradarkon", 2147, "Darkon's Debris 2 (Reconstructed)");
        }

        if (!Core.CheckInventory("King Drago Insignia", 5))
            Core.Logger(" x5 \"King Drago Insignia\" is required to continue quest, our Bots cannot *currently* kill this mob untill CoreArmy is Released and a script is made.", messageBox: true, stopBot: true);
        if (!Core.CheckInventory("Darkon Insignia", 5))
            Core.Logger(" x5 \"Darkon Insignia\" is required to continue quest, our Bots cannot *currently* kill this mob untill CoreArmy is Released and a script is made.", messageBox: true, stopBot: true);

        Core.ChainComplete(8742);
        Core.Logger("Enhancement Unlocked: Arcana's Concerto");
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
}

public enum ForgeQuestWeapon
{
    ForgeWeaponEnhancement,
    Lacerate,
    Smite,
    HerosValiance,
    ArcanasConcertoWIP,
    None,
    All
};
public enum ForgeQuestCape
{
    ForgeCapeEnhancement,
    Absolution,
    Vainglory,
    Avarice,
    None,
    All
};