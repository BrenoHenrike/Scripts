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
    public CoreDarkon Darkon = new CoreDarkon();


    public bool DontPreconfigure = false;

    public string OptionsStorage = "Forge Ehn Unlocks";

    public List<IOption> Options = new List<IOption>()
    {
        new Option<ForgeQuestWeapon>("ForgeQuestWeapon", "Weapon Enh", "Forge Quests to Unlock Weapon Enhs, Change to none to unselect", ForgeQuestWeapon.ForgeWeaponEnhancement),
        new Option<ForgeQuestCape>("ForgeQuestWeapon", "Cape Enh", "Forge Quests to Unlock Cape Enhs, Change to none to unselect", ForgeQuestCape.ForgeCapeEnhancement),
        new Option<bool>("UseGold", "Use Gold", "Speed the BlacksmithingREP Grind up with Gold?", false)
    };

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        CapeQuests();
        WeaponQuests();

        Core.SetOptions(false);
    }

    public void CapeQuests()
    {
        if (Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape") == ForgeQuestCape.None)
            return;

        if (Core.isCompletedBefore(8745))
            return;

        Core.Logger($"Unlocking Enh: {Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape").ToString()}");

        // foreach (char quest in Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon").ToString())
        // {

        // }

        Farm.BlacksmithingREP(10, Bot.Config.Get<bool>("UseGold"));
        Farm.Experience(90);
        LOC.Complete13LOC();

        if (Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape") == ForgeQuestCape.ForgeCapeEnhancement || Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape") == ForgeQuestCape.All)
        {
            // Forge Cape Enhancement
            if (!Story.QuestProgression(8758))
            {
                Core.EnsureAccept(8758);
                Core.KillEscherion("1st Lord Of Chaos Helm");
                Core.KillVath("Chaos Dragonlord Helm");
                Core.HuntMonster("kitsune", "Kitsune", "Chaos Shogun Helmet", isTemp: false);
                Core.HuntMonster("wolfwing", "Wolfwing", "Wolfwing Mask", isTemp: false);
                Core.EnsureComplete(8758);
            }
        }

        if (Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape") == ForgeQuestCape.Absolution || Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape") == ForgeQuestCape.All)
        {
            // Absolution
            if (!Story.QuestProgression(8743))
            {
                Core.EnsureAccept(8743);
                // Ascended Paladin
                // Ascended Paladin Staff
                // Ascended Paladin Sword            
                Core.EnsureComplete(8743);
            }
        }

        if (Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape") == ForgeQuestCape.Vainglory || Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape") == ForgeQuestCape.All)
        {
            // Vainglory
            if (!Story.QuestProgression(8744))
            {
                Core.EnsureAccept(8744);
                // Pauldron Relic
                // Breastplate Relic
                // Vambrace Relic
                // Gauntlet Relic
                // Greaves Relic            
                Core.EnsureComplete(8744);
            }
        }

        if (Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape") == ForgeQuestCape.Avarice || Bot.Config.Get<ForgeQuestCape>("ForgeQuestCape") == ForgeQuestCape.All)
        {
            // Avarice      
            if (!Story.QuestProgression(8745))
            {
                Core.EnsureAccept(8745);
                // Indulgence x75 
                // Penance x75 
                Core.EnsureComplete(8745);
            }
        }
        Core.Logger($"Enh: {Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon").ToString()} Unlocked");

    }

    public void WeaponQuests()
    {
        if (Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon") == ForgeQuestWeapon.None)
            return;

        if (Core.isCompletedBefore(8742))
            return;

        Core.Logger($"Unlocking Enh: {Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon").ToString()}");

        Farm.BlacksmithingREP(10, Bot.Config.Get<bool>("UseGold"));
        Farm.Experience();
        LOC.Complete13LOC();
        Astravia.CompleteCoreAstravia();


        if (Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestCape") == ForgeQuestWeapon.ForgeWeaponEnhancement || Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestCape") == ForgeQuestWeapon.All)
        {
            // Forge Weapon Enhancement
            if (!Story.QuestProgression(8738))
            {
                Core.EnsureAccept(8738);
                Core.KillEscherion("1st Lord Of Chaos Helm");
                Core.KillVath("Chaos Dragonlord Helm");
                Core.HuntMonster("kitsune", "Kitsune", "Chaos Shogun Helmet", isTemp: false);
                Core.HuntMonster("wolfwing", "Wolfwing", "Wolfwing Mask", isTemp: false);
                Core.EnsureComplete(8738);
            }

            if (Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon") == ForgeQuestWeapon.Lacerate || Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon") == ForgeQuestWeapon.All)
            {
                // Lacerate
                if (!Story.QuestProgression(8739))
                {
                    Core.EnsureAccept(8739);
                    if (Story.QuestProgression(92))
                    {
                        // Ninja Grudge
                        Story.KillQuest(90, "pirates", "Shark Bait");
                        // Without a Trace
                        if (Story.QuestProgression(91))
                        {
                            Story.KillQuest(91, "greenguardwest", "Kittarian");
                            Story.KillQuest(91, "river", "River Fishman");
                            Story.KillQuest(91, "swordhavenbridge", "Slime ");
                            Story.KillQuest(91, "greenguardwest", new[] { "Frogzard", "Big Bad Boar" });
                        }
                        // Hit Job   
                        Story.KillQuest(92, "greenguardwest", new[] { "Breken the Vile", "Ogug Stoneaxe" });
                    }
                    Core.HuntMonster("graveyard", "Big Jack Sprat", "Undead Plague Spear", isTemp: false);
                    Core.HuntMonster("river", "Kuro", "Kuro's Wrath", isTemp: false);

                    if (!Core.CheckInventory("Massive Horc Cleaver"))
                    {
                        Core.EnsureAccept(279);
                        Core.HuntMonster("warhorc", "Horc Master", isTemp: false);
                        Core.EnsureComplete(279);
                        Bot.Wait.ForPickup("Massive Horc Cleaver");
                    }
                    if (!Core.CheckInventory("Sword in the Stone"))
                    {
                        // (Reward from the 'Landing Swords!' quest)
                        Core.EnsureAccept(316);
                        Core.GetMapItem(54, 7, "greenguardeast");
                        Core.HuntMonster("greenguardeast", "Spider", "Tiny Sword", isTemp: false);
                        Core.EnsureComplete(316);
                        Bot.Wait.ForPickup("Sword in the Stone");
                    }
                    if (!Core.CheckInventory("Forest Axe"))
                    {
                        // (Reward from the 'warm MILK!' quest)
                        Core.EnsureAccept(301);
                        Core.GetMapItem(55, 4, "farm");
                        Core.HuntMonster("farm", "Mosquito", "Mosquito Juice", isTemp: false);
                        Core.EnsureComplete(301);
                        Bot.Wait.ForPickup("Forest Axe");
                    }
                    Farm.BlackKnightOrb();
                    Core.EnsureComplete(8739);
                }
            }

            if (Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon") == ForgeQuestWeapon.Smite || Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon") == ForgeQuestWeapon.All)
            {
                // Smite
                if (!Story.QuestProgression(8740))
                {
                    Core.EnsureAccept(8740);
                    SFR.StoryLine();
                    Farm.Experience(60);
                    Core.HuntMonster("shadowattack", "Death", "Death's Power", 3, isTemp: false);
                    Core.KillEscherion("Chaotic Power", 7);
                    Core.HuntMonster("map", "mob", "Empowered Essence", 50, isTemp: false);
                    Core.HuntMonster("undergroundlabb", "Ultra Battle Gem", "Gem Power", 25, false);
                    // Power Tonic x10           
                    Adv.BuyItem("alchemyacademy", 2036, "Power Tonic", 10);
                    Core.EnsureComplete(8740);
                }
            }

            if (Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon") == ForgeQuestWeapon.HerosValiance || Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon") == ForgeQuestWeapon.All)
            {
                // Hero's Valiance
                if (!Story.QuestProgression(8741))
                {
                    Core.EnsureAccept(8741);
                    //         Must have completed the 'The Final Challenge' quest.
                    LOC.Complete13LOC();
                    // Must have the following items in your inventory:
                    // Fire Champion's Armor
                    FCA.GetFireChampsArmor();
                    // Dragon of Time
                    DOT.GetDoT();
                    // Drakath the Eternal
                    ED.getSet();
                    // Eternity Blade
                    if (!Core.CheckInventory("Eternity Blade"))
                    {
                        Core.EnsureAccept(3485);
                        Bot.Quests.UpdateQuest(3484);
                        Core.HuntMonster("towerofdoom10", "Slugbutter", "Eternity Blade");
                        Core.EnsureComplete(3485);
                    }
                    // Gravelyn's DoomFire Token 
                    Seppy.GravelynsDoomFireToken();
                    // ArchPaladin Armor      
                    // TOD.CompleteToD();
                    AP.GetAP(false); //purely for the last quest "Sacred Magic: Eden"
                    Adv.BuyItem("darkthronehub", 1303, "ArchPaladin");
                    Core.EnsureComplete(8741);
                }
            }

            if (Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon") == ForgeQuestWeapon.ArcanasConcerto || Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon") == ForgeQuestWeapon.All)
            {
                // Arcana's Concerto   
                if (!Story.QuestProgression(8742))
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
                            Core.Logger(" x5 is Required to continue quest", stopBot: true);
                    }
                    if (!Core.CheckInventory("King Drago Insignia", 5))
                        Core.Logger(" x5 is Required to continue quest", stopBot: true);
                    if (!Core.CheckInventory("Darkon Insignia Insignia", 5))
                        Core.Logger(" x5 is Required to continue quest", stopBot: true);
                    Core.EnsureComplete(8742);
                }
            }
            Core.Logger($"Enh: {Bot.Config.Get<ForgeQuestWeapon>("ForgeQuestWeapon").ToString()} Unlocked");
        }
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
        ArcanasConcerto,
        None,
        All
    };

}
