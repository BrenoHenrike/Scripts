/*
name: null
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/VHL/CoreVHL.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Evil/SDKA/CoreSDKA.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/Legion/YamiNoRonin/CoreYnR.cs
//cs_include Scripts/Darkon/CoreDarkon.cs

//cs_include Scripts/Dailies/0AllDailies.cs
//cs_include Scripts/Good/GearOfAwe/CapeOfAwe.cs
//cs_include Scripts/Good/Paladin.cs
//cs_include Scripts/Nation/AssistingCragAndBamboozle[Mem].cs
//cs_include Scripts/Other/FreeBoostsQuest(10mns).cs
//cs_include Scripts/Enhancement/InventoryEnhancer.cs
//cs_include Scripts/Nation/Various/ArchfiendDeathLord.cs
//cs_include Scripts/Story/Nation/Originul.cs

//cs_include Scripts/Other/Classes/REP-based/MasterRanger.cs
//cs_include Scripts/Other/Classes/REP-based/EternalInversionist.cs
//cs_include Scripts/Other/Classes/REP-based/GlacialBerserker.cs
//cs_include Scripts/Other/Classes/REP-based/Shaman.cs
//cs_include Scripts/Other/Classes/REP-based/StoneCrusher.cs
//cs_include Scripts/Other/Classes/REP-based/DarkbloodStormKing.cs
//cs_include Scripts/Other/Classes/ScarletSorceress.cs
//cs_include Scripts/Other/Classes/BloodSorceress.cs
//cs_include Scripts/Other/Classes/DragonShinobi.cs
//cs_include Scripts/Good/ArchPaladin.cs
//cs_include Scripts/Dailies/LordOfOrder.cs
//cs_include Scripts/Legion/SwordMaster.cs

//cs_include Scripts/Other/Weapons/BurningBlade.cs
//cs_include Scripts/Other/Weapons/DualChainSawKatanas.cs
//cs_include Scripts/Other/Weapons/EnchantedVictoryBladeWeapons.cs
//cs_include Scripts/Other/Weapons/GoldenBladeOfFate.cs
//cs_include Scripts/Other/Weapons/PinkBladeofDestruction.cs


//cs_include Scripts/Story/DragonFableOrigins.cs
//cs_include Scripts/Story/Glacera.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/Nation/CitadelRuins.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/BrightOak.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Story/BattleUnder.cs
//cs_include Scripts/Story/Tutorial.cs
//cs_include Scripts/Story/XansLair.cs
//cs_include Scripts/Story/Yokai.cs
//cs_include Scripts/Story/Friendship.cs
//cs_include Scripts/Story/Nation/Fiendshard.cs
//cs_include Scripts/Story/Doomwood/CoreDoomwood.cs
//cs_include Scripts/Story/QueenofMonsters/CoreQoM.cs
//cs_include Scripts/Story/7DeadlyDragons/Core7DD.cs
//cs_include Scripts/Other/MysteriousEgg.cs
//cs_include Scripts/Story/Summer2015AdventureMap/CoreSummer.cs
//cs_include Scripts/Story/Borgars.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Story/ElegyofMadness(Darkon)/CoreAstravia.cs

//cs_include Scripts/Hollowborn/MergeShops/ShadowrealmMerge.cs
//cs_include Scripts/Other/MergeShops/SynderesMerge.cs
//cs_include Scripts/Nation/AFDL/WillpowerExtraction.cs






using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CoreFarmerJoe
{
    //other
    public IScriptInterface Bot => IScriptInterface.Instance;
    public FreeBoosts Boosts = new();
    public FarmAllDailies FAD = new();
    public InventoryEnhancer InvEn = new();
    public SynderesMerge SM = new();
    public ArchfiendDeathLord AFDeath = new();

    //Cores
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CapeOfAwe COA = new();
    public Core13LoC LOC => new Core13LoC();
    public CoreDailies Daily = new();
    public CoreVHL VHL = new CoreVHL();
    public CoreNation Nation = new();
    public CoreYnR YNR = new();

    //Classes
    public MasterRanger MR = new();
    public Shaman Shaman = new();
    public GlacialBerserker GB = new();
    public StoneCrusher SC = new();
    public DragonShinobi DS = new();
    public ArchPaladin AP = new();
    public LordOfOrder LOO = new();
    public ScarletSorceress SS = new();
    public EternalInversionist EI = new();
    public DarkbloodStormKing DBSK = new();
    public DragonOfTime DoT = new();

    //Weapons
    public DualChainSawKatanas DCSK = new();
    public BurningBlade BB = new();
    public EnchantedVictoryBladeWeapons EVBW = new();
    public ShadowrealmMerge SRM = new();

    //Story
    public Tutorial Tutorial = new();



    public string OptionsStorage = "FarmerJoePet";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("OutFit", "Get a Pre-Made Outfit, Curtious of the Community", "We are farmers, bum ba dum bum bum bum bum", false),
        new Option<bool>("EquipOutfit", "Equip outfit at the end?", "Yay or Nay", false),
        new Option<PetChoice>("PetChoice", "Choose Your Pet", "Extra stuff to choose, if you have any suggestions -form in disc, and put it under request. or dm Tato(the retarded one on disc)", PetChoice.None),
    };

    public void ScriptMain(IScriptInterface bot) => Core.RunCore();

    public void DoAll()
    {
        Level1to30();
        Level30to75();
        Level75to100();
        EndGame();
        Outfit();
        Pets(PetChoice.HotMama);
        Pets(PetChoice.Akriloth);
    }


    public void Level1to30()
    {
        if (Bot.Player.Level >= 30)
        {
            if (Core.SoloClass == "Generic")
            {
                Core.Logger("grabbing Oracle, ranking it, then continuing");
                Core.BuyItem("classhalla", 299, "Oracle");
                Adv.RankUpClass("Oracle");
            }
            Farm.BladeofAweREP(6, true);
            Core.ToBank("Blade of Awe");
            return;
        }

        #region starting out the acc
        //starting out the acc
        Core.Logger("starting out the acc with the tutorial badges to make it a bit more convincing");
        if (Bot.Player.Level < 10)
        {
            Tutorial.Badges();

            Core.Logger("Getting Starting Levels/Equipment");

            Core.BuyItem("classhalla", 299, "Oracle");
            Core.BuyItem("classhalla", 299, "Battle Oracle Wings");
            Core.BuyItem("classhalla", 299, "Battle Oracle Battlestaff");
            Core.BuyItem("classhalla", 299, "Battle Oracle Hood");
            Core.Equip("Battle Oracle Battlestaff", "Battle Oracle Hood", "Battle Oracle Wings");

            ItemBase? DefaultWep = Bot.Inventory.Items.Find(x => x.Name.StartsWith("Default"));
            if (DefaultWep != null && Core.CheckInventory(DefaultWep.Name))
                Core.SellItem(DefaultWep.Name);

            if (Core.SoloClass == "Generic" && Bot.Player.Level < 5)
                Core.SoloClass = "Oracle";

            Core.Equip(Core.SoloClass);

            //Temporary Weapon #2
            Core.RegisterQuests(4007);
            Core.HuntMonster("oaklore", "Bone Berserker", "Venom Head", isTemp: false, log: false);
            Bot.Wait.ForPickup("Venom Head");
            Core.Equip("Venom Head");
            Bot.Wait.ForItemEquip("Venom Head", 40);
            Core.SellItem("Battle Oracle Battlestaff");

            Core.Logger("Leveling to 10 in tutorial Area");
            while (!Bot.ShouldExit && Bot.Player.Level < 10)
                Core.HuntMonster("oaklore", "Bone Berserker", log: false);
            Core.CancelRegisteredQuests();
            InvEn.EnhanceInventory(EnhancementType.Wizard);
        }

        if (Core.SoloClass == "Generic" && Bot.Player.Level < 28)
            Core.SoloClass = "Oracle";

        Core.Logger("Checking if farming quest is unlocked.");
        if (!Core.isCompletedBefore(178))
        {
            Story.KillQuest(183, "portalundead", "Skeletal Fire Mage");
            Story.KillQuest(176, "swordhavenundead", "Skeletal Soldier", false);
            Story.KillQuest(177, "swordhavenundead", "Skeletal Ice Mage", false);
        }

        if (Bot.Player.Level < 28)
        {
            InvEn.EnhanceInventory(EnhancementType.Wizard);
            Core.RegisterQuests(178);
            while (!Bot.ShouldExit && Bot.Player.Level < 28)
                Core.HuntMonster("swordhavenundead", "Undead Giant", log: false);
            Core.CancelRegisteredQuests();
        }
        InvEn.EnhanceInventory(EnhancementType.Wizard);
        Adv.RankUpClass("Oracle");
        Core.ToBank("Healer");
        Farm.BladeofAweREP(6);
        Core.Equip("Blade of Awe");

        if (Bot.Player.Level < 30)
        {
            InvEn.EnhanceInventory(EnhancementType.Wizard);
            Core.RegisterQuests(6294);
            while (!Bot.ShouldExit && Bot.Player.Level < 30)
                Core.HuntMonster("firewar", "Fire Drakel", log: false);
            Core.CancelRegisteredQuests();
            InvEn.EnhanceInventory(EnhancementType.Wizard);
        }
    }


    #endregion starting out the acc

    public void Level30to75()
    {
        #region Obtain Boost Weapons

        if (!Adv.HasMinimalBoost(GenericGearBoost.exp, 20))
        {
            //Arcane Blade of Glory / Shadow Blade of Despair (+20% xp)
            Core.Logger("Arcane Blade of Glory / Shadow Blade of Despair (+20% xp)");
            EVBW.GetWeapon(VictoryBladeStyles.Smart);
            Adv.EnhanceItem(Core.CheckInventory("Arcane Blade of Glory") ? "Arcane Blade of Glory" : "Shadow Blade of Despair", EnhancementType.Lucky);
            Core.Equip(Core.CheckInventory("Arcane Blade of Glory") ? "Arcane Blade of Glory" : "Shadow Blade of Despair");
        }

        if (!Adv.HasMinimalBoost(GenericGearBoost.dmgAll, 25))
            DCSK.GetWep();
        Adv.BestGear(GenericGearBoost.dmgAll);

        ItemBase? DefaultWep = Bot.Inventory.Items.Find(x => x.Name.StartsWith("Default"));
        if (DefaultWep != null && Core.CheckInventory(DefaultWep.Name))
            Core.SellItem(DefaultWep.Name);
        #endregion Obtain Boost Weapon

        #region Leve30 to 75
        Core.Logger("Level to 75");
        Adv.BestGear(GenericGearBoost.exp);
        Farm.ToggleBoost(BoostType.Experience);
        foreach (int Level in new int[] { 30, 45, 50, 55, 60, 65, 70, 75 })
        {
            Core.Logger($"Level Goal: {Level}");

            switch (Level)
            {
                case 30:
                    if (Bot.Player.Level >= 30)
                        break;

                    while (!Bot.ShouldExit && Bot.Player.Level < Level || !Core.CheckInventory("Master Ranger"))
                    {
                        if (Core.SoloClass == "Generic")
                            Core.SoloClass = "Oracle";
                        InvEn.EnhanceInventory();
                        MR.GetMR();
                    }
                    break;

                case 45:
                    if (Bot.Player.Level >= Level && Core.CheckInventory("Scarlet Sorceress"))
                        break;

                    while (!Bot.ShouldExit && Bot.Player.Level < Level || !Core.CheckInventory("Scarlet Sorceress"))
                    {
                        if (Core.SoloClass == "Generic")
                            Core.SoloClass = "Oracle";
                        if (Core.FarmClass == "Generic")
                            Core.FarmClass = "Master Ranger";

                        Farm.Experience(Level);
                        InvEn.EnhanceInventory();
                        SS.GetSSorc();
                    }
                    break;

                case 50:
                    if (Bot.Player.Level >= Level && Core.CheckInventory("Darkblood StormKing"))
                        break;

                    while (!Bot.ShouldExit && Bot.Player.Level < Level || !Core.CheckInventory("Darkblood StormKing"))
                    {
                        if (Core.FarmClass == "Generic")
                            Core.FarmClass = "Scarlet Sorceress";

                        DBSK.GetDSK();
                        Adv.SmartEnhance("Darkblood StormKing");
                    }
                    break;

                case 55:
                case 60:
                    if (Bot.Player.Level >= Level && Core.CheckInventory("DragonSoul Shinobi"))
                        break;

                    while (!Bot.ShouldExit && Bot.Player.Level < Level || !Core.CheckInventory("DragonSoul Shinobi"))
                    {
                        if (Core.SoloClass == "Generic")
                            Core.SoloClass = "Darkblood StormKing";
                        if (Core.FarmClass == "Generic")
                            Core.FarmClass = "Scarlet Sorceress";
                        Core.Logger("Getting DSS for DoomKittem(ArchPaladin)");
                        DS.GetDSS();
                        Farm.Experience(Level);
                        InvEn.EnhanceInventory();
                    }
                    break;

                case 65:
                    if (Bot.Player.Level >= Level && Core.CheckInventory("ArchPaladin"))
                        break;


                    while (!Bot.ShouldExit && Bot.Player.Level < Level || !Core.CheckInventory("ArchPaladin"))
                    {
                        if (Core.SoloClass == "Generic")
                            Core.SoloClass = "Darkblood StormKing";
                        if (Core.FarmClass == "Generic")
                            Core.FarmClass = "Scarlet Sorceress";

                        Farm.Experience(Level);
                        InvEn.EnhanceInventory();
                        AP.GetAP();
                    }
                    break;

                //Just Leveling
                case 70:
                case 75:
                    if (Bot.Player.Level >= Level)
                        break;

                    while (!Bot.ShouldExit && Bot.Player.Level < Level)
                    {
                        if (Core.SoloClass == "Generic")
                            Core.SoloClass = "ArchPaladin";
                        if (Core.FarmClass == "Generic")
                            Core.FarmClass = "Scarlet Sorceress";

                        AFDeath.GetArm(true, ArchfiendDeathLord.RewardChoice.Archfiend_DeathLord);
                        Farm.Experience(Level);
                        InvEn.EnhanceInventory();
                    }
                    break;
            }
        }
        Farm.ToggleBoost(BoostType.Experience, false);

        //Setting solo and farm class
        if (Core.SoloClass == "Generic" || Core.SoloClass == "Healer" || Core.SoloClass == "Oracle" || Core.SoloClass == "Darkblood StormKing")
        {
            Core.Logger("Setting Solo class.");
            Core.SoloClass = "ArchPaladin";
        }
        if (Core.FarmClass == "Generic" || Core.FarmClass == "Master Ranger")
        {
            Core.Logger("Setting Farm class.");
            Core.SoloClass = "Scarlet Sorceress";
        }
        #endregion Level to 75
    }

    public void Level75to100()
    {
        if (Core.SoloClass == "Generic")
            Core.SoloClass = "ArchPaladin";
        if (Core.FarmClass == "Generic")
            Core.FarmClass = "Scarlet Sorceress";

        InvEn.EnhanceInventory();
        #region Prepare for Lvl100
        // P1: Healer for xiang
        Core.Logger("P1: Healer for xiang, Buying & Ranking Healer\n" +
        "class to prep for xiang (Skipped if you have Dragon of Time.");

        Core.Equip(Core.SoloClass);

        ///Prep class for 13LoC
        if (!Core.CheckInventory("Dragon of Time"))
        {
            if (!Core.CheckInventory(new[] { "Healer", "Healer (Rare)" }))
            {
                if (!Core.CheckInventory("Healer (Rare)"))
                    Adv.BuyItem("classhalla", 176, "Healer");
                Adv.RankUpClass(Core.CheckInventory("Healer (Rare)") ? "Healer (Rare)" : "Healer");
            }
            Adv.RankUpClass("Dragon of Time");
        }

        //P2 Chaos Shenanagins
        Core.Logger("P2: Chaos Shenanagins");

        // if (!Core.isCompletedBefore(3765))
        LOC.Complete13LOC();
        // Farm.ChaosREP();
        // Adv.BuyItem("confrontation", 891, "Chaos Slayer Berserker");
        // Adv.RankUpClass("Chaos Slayer Berserker");
        // Core.Equip("Chaos Slayer Berserker");

        //Step 2 Solo Class:
        Core.Logger("P3: Solo Classes & Weapon");
        Core.Logger("Getting Lord of order.");
        LOO.GetLoO();
        Core.ToBank(Core.EnsureLoad(7156).Rewards.Select(i => i.Name).ToArray());

        Core.Logger("P3 - 2: Blade and Cape of Awe");
        Core.ToBank("Blade of Awe");
        Adv.BuyItem("museum", 631, "Awethur's Accoutrements");
        COA.GetCoA();
        Adv.BestGear(GenericGearBoost.dmgAll);

        Core.Logger("P3 - 3: Burning Blade");
        BB.GetBurningBlade();
        Adv.BestGear(GenericGearBoost.dmgAll);
        InvEn.EnhanceInventory();

        Core.Logger("P3 - 4: Improving Efficiency, and more Classes");
        if (Core.FarmClass == "Generic")
            Core.FarmClass = "Shaman";
        GB.GetGB();
        if (Core.SoloClass == "Generic")
            Core.SoloClass = "Glacial Berserker";
        SC.GetSC();
        if (Core.SoloClass == "Generic")
            Core.SoloClass = "StoneCrusher";
        #endregion Prepare for Lvl100
        InvEn.EnhanceInventory();

        #region Leveling to 100
        Core.Logger("P4 Leveling to 100");
        Adv.BestGear(GenericGearBoost.dmgAll);
        Farm.IcestormArena();
        InvEn.EnhanceInventory();
        #endregion Leveling to 100}
    }

    public void EndGame()
    {
        #region Ending & Extras 

        if (Core.SoloClass == "Generic")
            Core.SoloClass = "StoneCrusher";
        if (Core.FarmClass == "Generic")
            Core.FarmClass = "Shaman";

        if (Bot.Config!.Get<bool>("OutFit"))
            Outfit();

        SRM.BuyAllMerge("Hollowborn Reaper's Scythe");
        YNR.GetYnR();
        DoT.GetDoT();
        //Add more eventualy >.> please?

        #endregion Ending & Extras
    }

    public void Outfit()
    {
        //Easy Difficulty Stuff
        ShirtandHat();
        ServersAreDown();
        Adv.SmartEnhance(Bot.Player.CurrentClass?.Name ?? String.Empty);

        //Extra Stuff
        Pets(PetChoice.None);

        if (Bot.Config!.Get<bool>("EquipOutfit"))
        {
            Core.Equip(new[] { "NO BOTS Armor", "Scarecrow Hat", "The Server is Down", "Hollowborn Reaper's Scythe" });
            Core.Equip(Bot.Config.Get<PetChoice>("PetChoice").ToString());
        }

        Core.Logger("We are farmers, bum ba dum bum bum bum bum");
    }

    #region other stuff

    public void Pets(PetChoice PetChoice = PetChoice.None)
    {
        if (Bot.Config!.Get<PetChoice>("Pets") == PetChoice.None)
            return;

        if (Bot.Config.Get<PetChoice>("Pets") == PetChoice.HotMama && !Core.CheckInventory("Hot Mama"))
        {
            Core.HuntMonster("battleundere", "Hot Mama", "Hot Mama", isTemp: false, log: false);
            Bot.Wait.ForPickup("Hot Mama");
            Core.Equip("Hot Mama");
        }

        if (Bot.Config.Get<PetChoice>("Pets") == PetChoice.Akriloth && !Core.CheckInventory("Akriloth Pet"))
        {
            Core.HuntMonster("gravestrike", "Ultra Akriloth", "Akriloth Pet", isTemp: false, log: false);
            Bot.Wait.ForPickup("Akriloth Pet");
            Core.Equip("Akriloth Pet");
        }
    }

    public void ShirtandHat()
    {
        if (Core.CheckInventory("NO BOTS Armor") | Core.CheckInventory("Scarecrow Hat"))
            return;

        Core.Logger("Farming Shirt & Hat");
        SM.BuyAllMerge(buyOnlyThis: "NO BOTS Armor");
        Adv.BuyItem("yulgar", 16, "Scarecrow Hat");
    }

    public void ServersAreDown()
    {
        if (Core.CheckInventory("The Server is Down"))
            return;

        Core.Logger("Farming Servers Are Down Sign");
        Core.HuntMonster("undergroundlabb", "Rabid Server Hamster", "The Server is Down", isTemp: false, log: false);
        Bot.Wait.ForPickup("The Server is Down");
        Core.Equip("The Server is Down");
    }
    public enum PetChoice
    {
        None,
        HotMama,
        Akriloth
    };
    #endregion other stuff
}