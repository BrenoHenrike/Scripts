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
//cs_include Scripts/Evil/NSoD/CoreNSOD.cs

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
//cs_include Scripts/Other/Classes/DragonOfTime.cs
//cs_include Scripts/Good/ArchPaladin.cs
//cs_include Scripts/Dailies/LordOfOrder.cs
//cs_include Scripts/Legion/SwordMaster.cs
//cs_include Scripts/Other/Classes/Daily-Classes/BlazeBinder.cs
//cs_include Scripts/Nation/Various/Archfiend.cs
//cs_include Scripts/Other/Classes/Necromancer.cs
//cs_include Scripts/Dailies/Cryomancer.cs

//cs_include Scripts/Other/Weapons/BurningBlade.cs
//cs_include Scripts/Other/Weapons/BurningBladeOfAbezeth.cs
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
//cs_include Scripts/Story/QueenofMonsters/Extra/CelestialArena.cs

//cs_include Scripts/Hollowborn/MergeShops/ShadowrealmMerge.cs
//cs_include Scripts/Other/MergeShops/SynderesMerge.cs
//cs_include Scripts/Other/MergeShops/CelestialChampMerge.cs
//cs_include Scripts/Nation/AFDL/WillpowerExtraction.cs


//cs_include Scripts/Story/AgeofRuin/CoreAOR.cs
//cs_include Scripts/Story/ShadowsOfWar/CoreSoW.cs
//cs_include Scripts/Other/MergeShops/YulgarsUndineMerge.cs
//cs_include Scripts/Hollowborn/MergeShops/DawnFortressMerge.cs
//cs_include Scripts/Story/Hollowborn/CoreHollowbornStory.cs





using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class CoreFarmerJoe
{
    //other
    public static IScriptInterface Bot => IScriptInterface.Instance;
    public FreeBoosts Boosts = new();
    public FarmAllDailies FAD = new();
    public InventoryEnhancer InvEn = new();
    public SynderesMerge SM = new();
    public ArchfiendDeathLord AFDeath = new();

    //Cores
    public static CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public CoreFarms Farm = new();
    public CoreStory Story = new();
    public CapeOfAwe COA = new();
    public static Core13LoC LOC => new();
    public CoreDailies Daily = new();
    public CoreVHL VHL = new();
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
    public BloodSorceress BS = new();
    public BlazeBinder Bb = new();
    public ArchFiend AF = new();
    public Cryomancer Cryo = new();

    //Weapons
    public DualChainSawKatanas DCSK = new();
    public BurningBlade BB = new();
    public BurningBladeOfAbezeth BBOA = new();
    public EnchantedVictoryBladeWeapons EVBW = new();
    public ShadowrealmMerge SRM = new();

    //Story
    public Tutorial Tutorial = new();
    public CelestialArenaQuests CAQ = new();
    public GlaceraStory GS = new();



    public string OptionsStorage = "FarmerJoePet";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("OutFit", "Get a Pre-Made Outfit, Curtious of the Community", "We are farmers, bum ba dum bum bum bum bum", false),
        new Option<bool>("EquipOutfit", "Equip outfit at the end?", "Yay or Nay", false),
        new Option<PetChoice>("PetChoice", "Choose Your Pet", "Extra stuff to choose, if you have any suggestions -form in disc, and put it under request. or dm Tato(the retarded one on disc)", PetChoice.None),
    };

    public static void ScriptMain(IScriptInterface bot) => Core.RunCore();

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
        BeginnerItems();

        if (Bot.Player.Level >= 30)
            return;

        foreach (int level in new[] { 10, 15, 20, 25, 30 })
        {

            Core.Logger($"Level Goal: {level}");
            Farm.Experience(level);
            Adv.SmartEnhance(Core.FarmClass);
        }

        //safet incase it desyncs.. the relog fuction isnt exactly perfect
        Core.Logger("Class points may be desynced at Rank 9\n" +
        "if you are stuck at rank 9, please relog");
    }


    public void Level30to75()
    {
        #region InvClasses
        InventoryItem? ClassRogue = Bot.Inventory.Items.Find(i => i.Name.ToLower().Trim() == (Core.CheckInventory("Rogue (Rare)") ? "Rogue (Rare)" : "Rogue").ToLower().Trim() && i.Category == ItemCategory.Class);
        InventoryItem? ClassMage = Bot.Inventory.Items.Find(i => i.Name.ToLower().Trim() == (Core.CheckInventory("Mage (Rare)") ? "Mage(Rare)" : "Mage").ToLower().Trim() && i.Category == ItemCategory.Class);
        InventoryItem? ClassMasterRanger = Bot.Inventory.Items.Find(i => i.Name.ToLower().Trim() == ("Master Ranger").ToLower().Trim() && i.Category == ItemCategory.Class);
        InventoryItem? ClassShaman = Bot.Inventory.Items.Find(i => i.Name.ToLower().Trim() == ("Shaman").ToLower().Trim() && i.Category == ItemCategory.Class);
        InventoryItem? ClassScarletSorceress = Bot.Inventory.Items.Find(i => i.Name.ToLower().Trim() == ("Scarlet Sorceress").ToLower().Trim() && i.Category == ItemCategory.Class);
        InventoryItem? ClassBlazeBinder = Bot.Inventory.Items.Find(i => i.Name.ToLower().Trim() == ("Blaze Binder").ToLower().Trim() && i.Category == ItemCategory.Class);
        InventoryItem? ClassDragonSoulShinobi = Bot.Inventory.Items.Find(i => i.Name.ToLower().Trim() == ("DragonSoul Shinobi").ToLower().Trim() && i.Category == ItemCategory.Class);
        InventoryItem? ClassArchPaladin = Bot.Inventory.Items.Find(i => i.Name.ToLower().Trim() == ("ArchPaladin").ToLower().Trim() && i.Category == ItemCategory.Class);
        InventoryItem? ClassArchFiend = Bot.Inventory.Items.Find(i => i.Name.ToLower().Trim() == ("ArchFiend").ToLower().Trim() && i.Category == ItemCategory.Class);
        #endregion InvClasses

        #region Leve30 to 75
        // Adv.BestGear(GenericGearBoost.exp);
        Farm.ToggleBoost(BoostType.Experience);

        foreach (int Level in new int[] { 30, 45, 50, 55, 60, 65, 70, 75 })
        {
            Core.Logger($"Starting Level {Level}");

            switch (Level)
            {
                case 30:
                    if (Bot.Player.Level >= Level && Adv.HasMinimalBoost(GenericGearBoost.exp, 25) && (Core.CheckInventory("Master Ranger") && ClassMasterRanger?.Quantity == 302500))
                    {
                        Core.Logger("Items owned: \"Awethur's Accoutrements\", \"Master Ranger\" continuing");
                        continue;
                    }

                    SetClass(false, true, true);

                    ItemBase? DefaultWep = Bot.Inventory.Items.Find(x => x.Name.StartsWith("Default"));
                    if (DefaultWep != null && Core.CheckInventory(DefaultWep.Name))
                        Core.SellItem(DefaultWep.Name);
                    Core.SellItem("Battle Oracle Battlestaff");
                    Core.SellItem("Venom Head");

                    Adv.SmartEnhance(Core.FarmClass);

                    MR.GetMR(false);
                    SetClass(false, true, true);

                    //For BOA lvl 30 rogue *should* be able to kill escherion ..once in awhile :P (tested i got a few kills in an an hr... proabably horrible but w/e)
                    Farm.BladeofAweREP(6, false);
                    Adv.BuyItem("museum", 631, "Awethur's Accoutrements");
                    Core.Equip("Awethur's Accoutrements");
                    Core.Logger($"Level {Level} done");
                    continue;

                case 45:
                    if (Bot.Player.Level >= Level && (Core.CheckInventory("Shaman") && ClassShaman?.Quantity == 302500))
                    {
                        Core.Logger("Items owned: \"Shaman\" continuing");
                        continue;
                    }

                    SetClass(false, true, true);

                    Adv.SmartEnhance(Core.FarmClass);
                    Farm.Experience(Level);

                    Shaman.GetShaman(false);
                    SetClass(true, false, true);

                    Core.Logger($"Level {Level} done");
                    continue;

                case 50:
                    if (Bot.Player.Level >= Level && Core.CheckInventory("Burning Blade") && (Core.CheckInventory("Scarlet Sorceress") && ClassScarletSorceress?.Quantity == 302500))
                    {
                        Core.Logger("Items owned: \"Scarlet Sorceress\", \"Burning Blade\" continuing");
                        continue;
                    }

                    SetClass(false, true, true);

                    Adv.SmartEnhance(Core.FarmClass);

                    SS.GetSSorc(false);
                    SetClass(false, true, true);

                    BB.GetBurningBlade();
                    Adv.SmartEnhance(Core.FarmClass);
                    Core.Logger($"Level {Level} done");
                    continue;

                case 55:
                    if (Bot.Player.Level >= Level && (Core.CheckInventory("Blaze Binder") && ClassBlazeBinder?.Quantity == 302500))
                    {
                        Core.Logger("Items owned:  \"Blaze Binder\", continuing");
                        continue;
                    }

                    SetClass(true, false, true);

                    //Daily classes
                    Adv.SmartEnhance(Core.FarmClass);
                    Core.Logger("Daily Classes Check");

                    Bb.GetClass(false);
                    SetClass(false, true, true);

                    Farm.Experience(Level);
                    Adv.SmartEnhance(Core.FarmClass);
                    Core.Logger($"Level {Level} done");
                    continue;

                case 60:
                    if (Bot.Player.Level >= Level && (Core.CheckInventory("DragonSoul Shinobi") && ClassDragonSoulShinobi?.Quantity == 302500))
                    {
                        Core.Logger("Items owned:  \"DragonSoul Shinobi\", continuing");
                        continue;
                    }

                    SetClass(false, true, true);

                    Core.Logger("Getting DSS for DoomKittem(ArchPaladin)");
                    Adv.SmartEnhance(Core.FarmClass);
                    Farm.Experience(Level);
                    Adv.SmartEnhance(Core.SoloClass);

                    SetClass(true, false, true);
                    DS.GetDSS();
                    Core.Logger($"Level {Level} done");
                    continue;

                case 65:
                    if (Bot.Player.Level >= Level && (Core.CheckInventory("ArchPaladin") && ClassArchPaladin?.Quantity == 302500))
                    {
                        Core.Logger("Items owned: \"ArchPaladin\", continuing");
                        continue;
                    }

                    SetClass(false, true, true);
                    Farm.Experience(Level);

                    SetClass(true, false, true);

                    AP.GetAP(false);
                    SetClass(true, false, true);

                    Core.Logger($"Level {Level} done");
                    continue;

                case 70:
                case 75:
                    if (Bot.Player.Level >= Level && Core.CheckInventory("Archfiend DeathLord") && (Core.CheckInventory("Archfiend") && ClassArchFiend?.Quantity == 302500))
                    {
                        Core.Logger("Items owned: \"Archfiend DeathLord\", \"ArchFiend\", continuing");
                        continue;
                    }

                    SetClass(true, false, true);

                    Adv.SmartEnhance(Core.FarmClass);
                    AFDeath.GetArm(true, ArchfiendDeathLord.RewardChoice.Archfiend_DeathLord);
                    Core.Equip("Archfiend DeathLord");

                    AF.GetArchfiend(false);
                    SetClass(false, true, true);

                    Farm.Experience(Level);
                    Adv.SmartEnhance(Core.FarmClass);
                    Core.Logger($"Level {Level} done");
                    continue;
            }
        }

        Farm.ToggleBoost(BoostType.Experience, false);
        #endregion Leve 30-75
    }

    public void Level75to100()
    {
        SetClass(false, true, true);

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

        COA.GetCoA();
        Core.Equip("Cape of Awe");
        Adv.SmartEnhance(Core.FarmClass);
        LOC.Complete13LOC();

        //Step 2 Solo Class:
        Core.Logger("P3: Solo Classes & Weapon");
        Core.Logger("Getting Lord of order.");
        LOO.GetLoO();
        Core.ToBank(Core.EnsureLoad(7156).Rewards.Select(i => i.Name).ToArray());

        Core.Logger("P3 - 4: Improving Efficiency, and more Classes");
        GB.GetGB();
        SC.GetSC();

        Farm.Experience(80);
        CAQ.DoAll();
        BBOA.GetBBoA();

        #endregion Prepare for Lvl100

        InvEn.EnhanceInventory();

        #region Leveling to 100
        Core.Logger("P4 Leveling to 100");
        Farm.IcestormArena();
        InvEn.EnhanceInventory();
        #endregion Leveling to 100}
    }

    public void EndGame()
    {
        SetClass(false, true, true);

        #region Ending & Extras 

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
        SetClass(false, true, true);

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
        SetClass(true, false, true);

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
        SetClass(false, true, true);

        Core.Logger("Farming Shirt & Hat");
        SM.BuyAllMerge(buyOnlyThis: "NO BOTS Armor");
        Adv.BuyItem("yulgar", 16, "Scarecrow Hat");
    }

    public void ServersAreDown()
    {
        SetClass(false, true, true);

        if (Core.CheckInventory("The Server is Down"))
            return;

        Core.Logger("Farming Servers Are Down Sign");
        Core.HuntMonster("undergroundlabb", "Rabid Server Hamster", "The Server is Down", isTemp: false, log: false);
        Bot.Wait.ForPickup("The Server is Down");
        Core.Equip("The Server is Down");
    }

    void BeginnerItems()
    {
        if (Core.CheckInventory(Core.CheckInventory("Rogue (Rare)") ? "Rogue (Rare)" : "Rogue") && Core.CheckInventory(Core.CheckInventory("Mage (Rare)") ? "Mage (Rare)" : "Mage") && Bot.Player.Level >= 10)
        {
            Core.Logger("Acc is lvl 10+, skipping beginnger items.");
            return;
        }

        Core.Logger("Starting out acc: \n" +
        "\tGoals:  Temp weapon, Rogue class.");

        Tutorial.Badges();

        Core.Logger("Getting Started: Beginner Levels/Equipment");

        if (!Core.CheckInventory(Core.CheckInventory("Rogue (Rare)") ? "Rogue (Rare)" : "Rogue"))
            Core.BuyItem("classhalla", 172, "Rogue");
        if (!Core.CheckInventory("Mage") || Core.CheckInventory("Mage (Rare)"))
            Adv.BuyItem("classhalla", 174, 15653, shopItemID: 9845);
        foreach (string item in new[] { "Battle Oracle Wings", "Battle Oracle Battlestaff", "Battle Oracle Battlestaff" })
        {
            Core.BuyItem("classhalla", 299, item); ;
            Core.Equip(item);
        }

        ItemBase? DefaultWep = Bot.Inventory.Items.Find(x => x.Name.StartsWith("Default"));
        if (DefaultWep != null && Core.CheckInventory(DefaultWep.Name))
            Core.SellItem(DefaultWep.Name);
        SetClass(false);
        Core.EquipClass(ClassType.Solo);
        Farm.Experience(5);
    }

    public void SetClass(bool swapToSoloClass = false, bool swapToFarmClass = false, bool rankUp = true)
    {
        if (swapToSoloClass && swapToFarmClass)
        {
            Core.Logger("Both swapToSoloClass and swapToFarmClass cannot be true at the same time. Please choose only one.");
            return;
        }
        string newSoloClass = Core.SoloClass;
        string newFarmClass = Core.FarmClass;

        if (Core.SoloClass != "Generic" && Core.FarmClass != "Generic")
        {
            Core.Logger("CBO classes are set, using what you picked.");
            return;
        }

        string[] soloClassesToCheck = { "ArchPaladin", "Shaman", "Rogue (Rare)", "Rogue", "Healer (Rare)", "Healer" };
        string[] farmClassesToCheck = { "Archfiend", "Blaze Binder", "Scarlet Sorceress", "Master Ranger", "Shaman", "Mage (Rare)", "Mage" };

        Core.Logger($"Checking if CBO (Corebot Options) classes are set\n" +
            $"Solo: {string.Join(", ", soloClassesToCheck)}\n" +
            $"Farm: {string.Join(", ", farmClassesToCheck)}");

        newSoloClass = CheckAndSetClass(newSoloClass, soloClassesToCheck, "SoloClass", rankUp);
        newFarmClass = CheckAndSetClass(newFarmClass, farmClassesToCheck, "FarmClass", rankUp);

        if (swapToSoloClass)
        {
            Enum.TryParse(newSoloClass, true, out ClassType soloClassEnum);
            Core.EquipClass(soloClassEnum);
        }

        if (swapToFarmClass)
        {
            Enum.TryParse(newFarmClass, true, out ClassType farmClassEnum);
            Core.EquipClass(farmClassEnum);
        }

        Core.SoloClass = newSoloClass;
        Core.FarmClass = newFarmClass;

        Core.Logger($"Setting SoloClass to: {Core.SoloClass}.");
        Core.Logger($"Setting FarmClass to: {Core.FarmClass}.");
    }

    private string CheckAndSetClass(string classToCheck, string[] classesToCheck, string classType, bool rankUp)
    {
        if (classToCheck == "Generic" || classesToCheck.Contains(classToCheck))
        {
            return FindValidClass(classesToCheck, classType, rankUp);
        }
        else
        {
            Core.Logger($"Using predetermined {classType}: {classToCheck}");
            return classToCheck;
        }
    }

    private string FindValidClass(string[] classesToCheck, string classType, bool rankUp)
    {
        foreach (string className in classesToCheck)
        {
            if (Core.CheckInventory(className))
            {
                InventoryItem? classItem = Bot.Inventory.Items.Find(i => i.Name.ToLower().Trim() == className.ToLower().Trim() && i.Category == ItemCategory.Class);
                if (classItem != null && classItem.Quantity != 302500)
                {
                    Core.Logger($"{classType} found: {className}. Quantity: {classItem.Quantity}. {(rankUp ? "Ranking up" : "Not ranking up")} the class...");
                    if (rankUp)
                    {
                        Adv.RankUpClass(className);
                    }
                    return className;
                }
                else
                {
                    Core.Logger($"{classType} found: {className}. Quantity: {classItem?.Quantity ?? 0}. No need to rank up.");
                    return className;
                }
            }
        }

        Core.Logger($"No valid {classType} found. Using default value: Generic");
        return "Generic"; // Return "Generic" as the default value when no valid class is found.
    }


    /// <summary>
    /// Enhances the first item from the given list of items in the player's inventory, if found.
    /// </summary>
    /// <remarks>
    /// This method checks the player's inventory for the specified items and enhances the first
    /// item found using the "Adv.SmartEnhance" method.
    /// </remarks>
    public void DmgOverTimeEnh()
    {
        string[] itemsToCheck = new[]
        {
        "ShadowStalker of Time",
        "ShadowWeaver of Time",
        "ShadowWalker of Time",
        "Infinity Knight",
        "Interstellar Knight",
        "Void Highlord",
        "Dragon of Time",
        "Timeless Dark Caster",
        "Frostval Barbarian",
        "Blaze Binder",
        "DeathKnight",
        "DragonSoul Shinobi",
        "Shadow Dragon Shinobi",
        "Legion Revenant"
    };

        foreach (string item in itemsToCheck)
        {
            if (Core.CheckInventory(new[] { item }, any: true))
            {
                Adv.SmartEnhance(item);
                break; // Stops the loop once the item is found and enhanced.
            }
        }
    }




    //_____________________________________Explanation_____________________________________________________________________________________________________________//
    // 1. The SetClass() method checks if SoloClass is "Generic" and proceeds to find a valid class from the soloClassesToCheck list using the FindValidClass() method.
    // 2. If a valid class is found in the soloClassesToCheck list, it is set as the SoloClass, and the method logs a message that the class was found.
    // 3.If no valid class is found in the soloClassesToCheck list, the SoloClass remains as "Generic", and the method logs a message that no valid class was found.
    // 4. The SetClass() method then checks if FarmClass is "Generic" and proceeds to find a valid class from the farmClassesToCheck list using the FindValidClass() method.
    // 5. If a valid class is found in the farmClassesToCheck list, it is set as the FarmClass, and the method logs a message that the class was found.
    // 6. If no valid class is found in the farmClassesToCheck list, the FarmClass remains as "Generic", and the method logs a message that no valid class was found.

    // By using the break statement within the FindValidClass() method, the method returns as soon as it finds a valid class, and it doesn't continue searching for other classes in the list.
    // This approach ensures that the method sets SoloClass and FarmClass independently, allowing different classes to be chosen for each role. The first valid class found for each role is
    //  used, and the method doesn't keep searching for more valid classes after setting the first one.
    // **Curtsey  of ChatGPT**
    //_____________________________________Explanation_____________________________________________________________________________________________________________//




    public enum PetChoice
    {
        None,
        HotMama,
        Akriloth
    };
    #endregion other stuff
}