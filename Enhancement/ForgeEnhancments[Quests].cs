//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/SepulchureSaga/04ShadowfallRise.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Other/Various/UltraPotions.cs
//cs_include Scripts/Other/Classes/DragonslayerGeneral.cs
//cs_include Scripts/Story/WarfuryTraining.cs
//cs_include Scripts/Other/WarFuryEmblem.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Darkon/CoreDarkon.cs
//cs_include Scripts/Other/Weapons/GoldenBladeOfFate.cs
//cs_include Scripts/Other/Weapons/PinkBladeofDestruction.cs
//cs_include Scripts/Story/Doomwood/DoomwoodPart3.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Other/MysteriousEgg.cs
//cs_include Scripts/Story/Collection.cs
//cs_include Scripts/Story/Borgars.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs
//cs_include Scripts/Chaos/DrakathsArmor.cs
//cs_include Scripts/Story/StarSinc.cs
//cs_include Scripts/Story/TowerOfDoom.cs
//cs_include Scripts/Story/Doomwood/AQWZombies.cs
//cs_include Scripts/Evil/SepulchuresOriginalHelm.cs
//cs_include Scripts/Good/Paladin.cs
//cs_include Scripts/Story/XansLair.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/DarkonGarden.cs
//cs_include Scripts/Other/FireChampionsArmor.cs
//cs_include Scripts/Other/Classes/DragonOfTime.cs
//cs_include Scripts/Chaos/EternalDrakathSet.cs
//cs_include Scripts/Good/ArchPaladin.cs
//cs_include Scripts/Prototypes/PrinceDarkonsPoleaxePreReqs.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Story/Legion/SevenCircles(War).cs
//cs_include Scripts/Legion\HeadOfTheLegionBeast.cs
using RBot;
using RBot.Options;

public class ForgeEnhancments
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public Core13LoC LOC => new Core13LoC();
    public CoreStory Story = new();
    public ShadowfallRise SFR = new();
    public CoreNation Nation = new();
    public PotionBuyer Pots = new();
    public CoreAdvanced Adv = new();
    public FireChampionsArmor FCA = new();
    public DragonOfTime DOT = new();
    public EternalDrakath ED = new();
    public SepulchuresOriginalHelm Seppy = new();
    public CoreToD TOD = new();
    public ArchPaladin AP = new();
    public CoreAstravia Astravia => new CoreAstravia();
    public PrinceDarkonsPoleaxePreReqs PDPPR = new();
    public CoreAwe Awe = new CoreAwe();
    public CoreDarkon Darkon = new CoreDarkon();
    public CoreLegion Legion = new CoreLegion();
    public SevenCircles Circles = new SevenCircles();
    public HeadoftheLegionBeast HOTLB = new();


    public string OptionsStorage = "Forge Ehn Unlocks";

    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("SkipOption", "Skip this window next time", "You will be able to return to this screen via [Options] -> [Script Options] if you wish to change anything.", false),
        new Option<ForgeQuestWeapon>("ForgeQuestWeapon", "Weapon Enh", "Forge Quests to Unlock Weapon Enhs, Change to none to unselect", ForgeQuestWeapon.None),
        new Option<ForgeQuestCape>("ForgeQuestCape", "Cape Enh", "Forge Quests to Unlock Cape Enhs, Change to none to unselect", ForgeQuestCape.None),
        new Option<bool>("UseGold", "Use Gold", "Speed the BlacksmithingREP Grind up with Gold?", false)
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
            return;

        Farm.BlacksmithingREP(10, Bot.Config.Get<bool>("UseGold"));
        Farm.Experience();
        LOC.Complete13LOC();
        Astravia.CompleteCoreAstravia();


        Core.Logger($"Unlocking Wep Enh: {Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon").ToString()}");

        switch (Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon").ToString())
        {
            case "ForgeWeaponEnhancement":
                Core.Logger($"Farming {Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon")}");
                ForgeWeaponEnhancement();
                break;

            case "Lacerate":
                Core.Logger($"Farming {Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon")}");
                Lacerate();
                break;

            case "Smite":
                Core.Logger($"Farming {Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon")}");
                Smite();
                break;


            case "HerosValiance":
                Core.Logger($"Farming {Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon")}");
                HerosValiance();
                break;

            case "ArcanasConcertoWIP":
                Core.Logger($"Farming {Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon")}");
                ArcanasConcertoWIP();
                break;

            case "All":
                Core.Logger($"Farming {Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon")}");
                ForgeWeaponEnhancement();
                Lacerate();
                Smite();
                HerosValiance();
                ArcanasConcertoWIP();
                break;

            case "None":
                Core.Logger($"Farming {Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon")}");
                Core.Logger("None Selected, So Farming Nothing");
                break;
        }

        Core.Logger($"Unlocking Cape Enh: {Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape").ToString()}");

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
                ForgeCapeEnhancement();
                Absolution();
                Vainglory();
                Avarice();
                Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape").ToString();
                break;

            case "None":
                Core.Logger($"Farming {Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape")}");
                Core.Logger("None Selected, So Farming Nothing");
                break;
        }
    }


    public void ForgeCapeEnhancement()
    {
        if (!Story.QuestProgression(8758))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(8758);
            Core.KillEscherion("1st Lord Of Chaos Staff");
            Core.KillVath("Chaos Dragonlord Axe");
            Core.HuntMonster("kitsune", "Kitsune", "Hanzamune Dragon Koi Blade", isTemp: false);
            Core.HuntMonster("wolfwing", "Wolfwing", "Wrath of the Werepyre", isTemp: false);
            Core.EnsureComplete(8758);
        }
    }

    public void Absolution()
    {
        if (!Bot.Quests.IsUnlocked(8743))
        {
            Core.EnsureAccept(8743);
            Farm.GoodREP();
            if (!Core.CheckInventory("Ascended Paladin"))
            {
                Core.HuntMonster("therift", "Plague Spreader", "Slimed Sigil", 200, isTemp: false);
                Core.BuyItem("therift", 1399, 39091);
                Core.BuyItem("therift", 1399, 39093);
                Core.BuyItem("therift", 1399, 39094);
            }
            Core.EnsureComplete(8743);
        }

    }

    public void Vainglory()
    {
        if (!Bot.Quests.IsUnlocked(8744))
        {
            Core.EnsureAccept(8744);
            Awe.GetAweRelic("Pauldron", 4160, 15, 15, "gravestrike", "Ultra Akriloth");
            Awe.GetAweRelic("Breastplate", 4163, 10, 10, "aqlesson", "Carnax");
            Awe.GetAweRelic("Vambrace", 4166, 15, 15, "bloodtitan", "Ultra Blood Titan");
            Awe.GetAweRelic("Gauntlet", 4169, 25, 5, "alteonbattle", "Ultra Alteon");
            Awe.GetAweRelic("Greaves", 4172, 10, 15, "bosschallenge", "Mutated Void Dragon");
            Core.EnsureComplete(8744);
        }

    }

    public void Avarice()
    {
        if (Bot.Quests.IsUnlocked(8745))
        {
            Core.EnsureAccept(8745);
            HOTLB.Indulgence(75);
            HOTLB.Penance(75);
            Core.EnsureComplete(8745);
        }
        Core.Logger($"Enh: {Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon").ToString()} Unlocked");

    }

    public void ForgeWeaponEnhancement()
    {
        if (!Story.QuestProgression(8738))
        {
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(8738);
            Core.KillEscherion("1st Lord Of Chaos Helm");
            Core.KillVath("Chaos Dragonlord Helm");
            Core.HuntMonster("kitsune", "Kitsune", "Chaos Shogun Helmet", isTemp: false);
            Core.HuntMonster("wolfwing", "Wolfwing", "Wolfwing Mask", isTemp: false);
            Core.EnsureComplete(8738);
        }
    }

    public void Lacerate()
    {
        if (!Bot.Quests.IsUnlocked(8739))
        {
            Core.AddDrop("Massive Horc Cleaver", "Sword in the Stone", "Forest Axe");
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(8739);
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

            Core.HuntMonster("graveyard", "Big Jack Sprat", "Undead Plague Spear", isTemp: false);
            Core.HuntMonster("river", "Kuro", "Kuro's Wrath", isTemp: false);

            if (!Core.CheckInventory("Massive Horc Cleaver"))
            {
                Core.EnsureAccept(279);
                Core.HuntMonster("warhorc", "General Drox", "Boss Prize");
                Core.EnsureComplete(279);
                Bot.Wait.ForPickup("Massive Horc Cleaver");
            }
            if (!Core.CheckInventory("Sword in the Stone"))
            {
                Core.EnsureAccept(316);
                Core.GetMapItem(54, 7, "greenguardeast");
                Core.HuntMonster("greenguardeast", "Spider", "Tiny Sword");
                Core.EnsureComplete(316);
                Bot.Wait.ForPickup("Sword in the Stone");
            }
            if (!Core.CheckInventory("Forest Axe"))
            {
                Core.EnsureAccept(301);
                Core.GetMapItem(55, 4, "farm");
                Core.HuntMonster("farm", "Mosquito", "Mosquito Juice");
                Core.EnsureComplete(301);
                Bot.Wait.ForPickup("Forest Axe");
            }
            Farm.BlackKnightOrb();
            Core.EnsureComplete(8739);
        }
    }

    public void Smite()
    {
        if (!Bot.Quests.IsUnlocked(8740))
        {
            Core.EnsureAccept(8740);
            SFR.StoryLine();
            Farm.Experience(60);
            Core.HuntMonster("shadowattack", "Death", "Death's Power", 3, isTemp: false);
            Core.KillEscherion("Chaotic Power", 7);
            Core.HuntMonster("shadowrealmpast", "*", "Empowered Essence", 50, isTemp: false);
            Core.HuntMonster("undergroundlabb", "Ultra Battle Gem", "Gem Power", 25, false);
            Adv.BuyItem("alchemyacademy", 2036, "Power Tonic", 10);
            Core.EnsureComplete(8740);
        }
    }

    public void HerosValiance()
    {
        if (!Bot.Quests.IsUnlocked(8741))
        {
            LOC.Complete13LOC();
            FCA.GetFireChampsArmor();
            DOT.GetDoT();
            ED.getSet();
            if (!Core.CheckInventory("Eternity Blade"))
            {
                Core.EnsureAccept(3485);
                Bot.Quests.UpdateQuest(3484);
                Core.HuntMonster("towerofdoom10", "Slugbutter", "Eternity Blade");
                Core.EnsureComplete(3485);
            }
            Core.EnsureAccept(8741);
            Seppy.GravelynsDoomFireToken();
            AP.GetAP(false); //purely for the last quest "Sacred Magic: Eden"
            Adv.BuyItem("darkthronehub", 1303, "ArchPaladin");
            Core.EnsureComplete(8741);
        }
    }

    public void ArcanasConcertoWIP()
    {

        // Arcana's Concerto   
        if (Bot.Quests.IsUnlocked(8742))
        {
            Core.EnsureAccept(8742);
            // Must have completed the 'Darkon, the Conductor' quest.
            //8746|Darkon, the Conductor
            if (!Core.isCompletedBefore(8746))
                Core.Logger("Quest Completion is Required.", stopBot: true);
            // Must have the following items in your inventory:
            // Prince Darkon's Poleaxe
            PDPPR.FarmPreReqs();
            // Darkon's Debris 2 (Reconstructed)
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
                if (!Core.CheckInventory("Darkon Insignia Insignia", 20))
                    Core.Logger(" x5 \"Darkon Insignia Insignia\" is Required to continue quest, our Bots cannot *currently* kill this mob Untill CoreArmy is Released and a script is made.", messageBox: true, stopBot: true);
            }
            if (!Core.CheckInventory("King Drago Insignia", 5))
                Core.Logger(" x5 \"King Drago Insignia\" is Required to continue quest, our Bots cannot *currently* kill this mob Untill CoreArmy is Released and a script is made.", messageBox: true, stopBot: true);
            if (!Core.CheckInventory("Darkon Insignia Insignia", 5))
                Core.Logger(" x5 \"Darkon Insignia Insignia\" is Required to continue quest, our Bots cannot *currently* kill this mob Untill CoreArmy is Released and a script is made.", messageBox: true, stopBot: true);
            Core.EnsureComplete(8742);
        }
        Core.Logger($"Enh: {Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon").ToString()} Unlocked");
    }

    public enum ForgeQuestCape
    {
        ForgeCapeEnhancement,
        Absolution,
        Vainglory,
        Avarice,
        None,
        All
    };


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
}