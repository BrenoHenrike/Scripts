//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Dailies/0AllDailies.cs
//cs_include Scripts/Dailies/LordOfOrder.cs
//cs_include Scripts/Enhancement/InventoryEnhancer.cs
//cs_include Scripts/Good/ArchPaladin.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Good/GearOfAwe/CapeOfAwe.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
//cs_include Scripts/Good/Paladin.cs
//cs_include Scripts/Hollowborn/HollowbornReapersScythe.cs
//cs_include Scripts/Nation/AssistingCragAndBamboozle[Mem].cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/VHL/CoreVHL.cs
//cs_include Scripts/Other/Classes/DragonShinobi.cs
//cs_include Scripts/Other/Classes/REP-based/EternalInversionist.cs
//cs_include Scripts/Other/Classes/REP-based/GlacialBerserker.cs
//cs_include Scripts/Other/Classes/REP-based/Shaman.cs
//cs_include Scripts/Other/Classes/REP-based/StoneCrusher.cs
//cs_include Scripts/Other/FreeBoostsQuest(10mns).cs
//cs_include Scripts/Other/Weapons/BurningBlade.cs
//cs_include Scripts/Other/Weapons/DualChainSawKatanas.cs
//cs_include Scripts/Other/Weapons/EnchantedVictoryBladeWeapons.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
//cs_include Scripts/Story/Glacera.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/LordsofChaos/MountDoomSkull.cs
//cs_include Scripts/Story/Nation/CitadelRuins.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/BrightOak.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/LivingDungeon.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Story/Tutorial.cs
//cs_include Scripts/Story/XansLair.cs
//cs_include Scripts/Story/Yokai.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class FarmerJoeStartingTheAcc
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public CoreFarms Farm = new();
    public HollowbornScythe Scythe = new();
    public EternalInversionist EI = new();
    public InventoryEnhancer InvEn = new();
    public ArchPaladin AP = new();
    public DragonShinobi DS = new();
    public CapeOfAwe COA = new();
    public Core13LoC LOC => new Core13LoC();
    public CoreDailies Daily = new();
    public LordOfOrder LOO = new();
    public CoreVHL VHL = new CoreVHL();
    public CoreNation Nation = new();
    public FarmAllDailys FAD = new();
    public BurningBlade BB = new();
    public Shaman Shaman = new();
    public GlacialBerserker GB = new();
    public StoneCrusher SC = new();
    public EnchantedVictoryBladeWeapons EVBW = new();
    public Tutorial Tutorial = new();
    public DualChainSawKatanas DCSK = new();
    public FreeBoosts Boosts = new();
    public MountDoomSkull MDS = new();

    public string OptionsStorage = "FarmerJoePet";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        CoreBots.Instance.SkipOptions,
        new Option<bool>("OutFit", "Get a Pre-Made Outfit, Curtious of the Community", "We are farmers, bum ba dum bum bum bum bum", false),
        new Option<bool>("EquipOutfit", "Equip outfit at the end?", "Yay or Nay", false),
        new Option<PetChoice>("PetChoice", "Choose Your Pet", "Extra stuff to choose, if you have any suggestions -form in disc, and put it under request. or dm Tato(the retarded one on disc)", PetChoice.None),
    };


    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Core.BankingBlackList.AddRange(Nation.bagDrops);
        Core.BankingBlackList.Add("Lord of Order");
        StartingTheAcc();

        Core.SetOptions(false);
    }

    public void StartingTheAcc()
    {
        #region starting out the acc
        //starting out the acc
        Core.Logger("starting out the acc");
        Tutorial.Badges();

        Core.Logger("Getting Starting Cash and Levels");

        if (Bot.Player.Level < 10)
        {
            Core.RegisterQuests(4007);
            Core.EquipClass(ClassType.Solo);
            while (!Bot.ShouldExit && Bot.Player.Level < 10)
            {
                if (Bot.Player.Level >= 5 && !Core.CheckInventory(new[] { "Healer's Staff", "Greenguard Knight", "Greenguard Knight Sheathed Blades" }))
                {
                    Bot.Drops.Add("Greenguard Knight", "Greenguard Knight Sheathed Blades");
                    Core.BuyItem("oaklore", 1576, "Healer's Staff");
                    Core.Equip("Healer's Staff");
                    Core.EnsureAccept(6257);
                    Core.KillMonster("oaklore", "r4Up", "Right", "*", "Training Monster Slain", 5);
                    Core.EnsureComplete(6257);
                    Core.Equip("Greenguard Knight", "Greenguard Knight Sheathed Blades");
                }
                Core.HuntMonster("oaklore", "Bone Berserker");
                Core.CancelRegisteredQuests();
            }
        }

        if (Bot.Player.Level < 20)
        {
            Adv.BuyItem("classhalla", 176, "Healer");
            Adv.BuyItem("classhalla", 174, "Mage's Hood");
            Adv.BuyItem("classhalla", 176, "White Feather Wings");
            Core.Equip("White Feather Wings");
            Core.Equip("Mage's Hood");
            Core.Equip("Healer");
            InvEn.EnhanceInventory();
            Farm.IcestormArena(20, true);
        }

        if (Bot.Player.Level < 30)
        {
            InvEn.EnhanceInventory();
            Farm.IcestormArena(30, true);
        }

        #endregion starting out the acc

        #region Obtain the Enchanted Victory Blade
        //Arcane Blade of Glory / Shadow Blade of Dispair (+20% xp)
        Core.Logger("Arcane Blade of Glory / Shadow Blade of Dispair (+20% xp)");
        EVBW.GetWeapon(VictoryBladeStyles.Smart);

        string wep = "";
        if (Core.CheckInventory("Enchanted Victory Blade"))
            wep = "Enchanted Victory Blade";
        else if (Core.CheckInventory("Arcane Blade of Glory"))
            wep = "Arcane Blade of Glory";
        else if (Core.CheckInventory("Shadow Blade of Dispair"))
            wep = "Shadow Blade of Dispair";
        Core.Equip(wep);
        InvEn.EnhanceInventory();
        #endregion Obtain the Silver Victory Blade

        #region Dual Chainsaw Katanas
        Adv.GearStore();
        Adv.BuyItem("classhalla", 174, "Mage");
        Adv.rankUpClass("Mage");
        Adv.GearStore(true);
        InvEn.EnhanceInventory();
        DCSK.GetWep();
        #endregion Dual Chainsaw Katanas

        #region Level to 75
        Core.Logger("Level to 75");
        Core.EquipClass(ClassType.Farm);
        foreach (int level in new int[] { 45, 50, 55, 60, 65, 70, 75 })
        {
            if (Bot.Player.Level < level)
            {
                Farm.Experience(level);
                InvEn.EnhanceInventory();
            }
        }
        #endregion Level to 75


        #region Prepare for Lvl100
        Core.Logger("step 1 Farming Classes");
        // P1: Healer LOC
        Core.Equip("Healer");
        Adv.rankUpClass("Healer");
        LOC.Complete13LOC();

        // P2: Mage Chaos Rep
        Core.Equip("Mage");
        Core.EquipClass(ClassType.Farm);
        Farm.ChaosREP();

        // P3: Chaos Slayer
        Adv.BuyItem("confrontation", 891, "Chaos Slayer Berserker", shopItemID: 15402);
        Adv.rankUpClass("Chaos Slayer Berserker");
        Core.Equip("Chaos Slayer Berserker");

        // P4: DragonSoul Shinobi for Doomkitten
        DS.GetDSS();

        // P5: Farm ArchPaladin
        Core.Logger("This will stall at Ultra Alteon, it is your job to unblock it");
        Core.Equip("Chaos Slayer Berserker");
        AP.GetAP();
        Core.Equip("ArchPaladin");

        //Step 2 Solo Class:
        Core.Logger("step 2 LOO Class Daily");
        LOO.GetLoO();
        Core.ToBank(Core.EnsureLoad(7156).Rewards.Select(i => i.Name).ToArray());

        Core.Logger("Step 3 Dailies for Classes");
        FAD.DoAllDailys();

        Core.Logger("Step 4 Blade and Cape of Awe");
        Farm.BladeofAweREP(6, true);
        Core.ToBank("Blade of Awe");
        Adv.BuyItem("museum", 631, "Awethur's Accoutrements");
        Core.Equip("Awethur's Accoutrements");
        COA.GetCoA();
        InvEn.EnhanceInventory();

        Core.Logger("Step 5 Burning Blade");
        Core.Equip("ArchPaladin");
        BB.GetBurningBlade();
        InvEn.EnhanceInventory();

        Core.Logger("Step 6 Improving Efficiency, and more Classes");
        EI.GetEI();
        Core.Equip("Eternal Inversionist");
        Shaman.GetShaman();
        GB.GetGB();
        SC.GetSC();
        Adv.BuyItem("Classhalla", 178, "Ninja");
        Adv.rankUpClass("Ninja");
        #endregion Prepare for Lvl100


        #region Leveling to 100
        Core.Logger("Leveling to 100");
        Farm.Experience();
        InvEn.EnhanceInventory();
        #endregion Leveling to 100


        #region Ending & Extras 
        Scythe.GetHBReapersScythe();
        InvEn.EnhanceInventory();
        #endregion Ending & Extras


        if (Bot.Config.Get<bool>("OutFit"))
            Outfit();

    }

    public void Outfit()
    {
        //Easy Difficulty Stuff
        RagsandHat();
        ServersAreDown();
        Adv.EnhanceEquipped(EnhancementType.Lucky);

        //Extra Stuff
        Pets();

        if (Bot.Config.Get<bool>("EquipOutfit"))
        {
            Core.Equip(new[] { "Peasant Rags", "Scarecrow Hat", "The Server is Down", "Hollowborn Reaper's Scythe" });
            Core.Equip(Bot.Config.Get<PetChoice>("PetChoice").ToString());
        }

        Core.Logger("We are farmers, bum ba dum bum bum bum bum");
    }

    #region other stuff

    public void Pets()
    {
        if (Bot.Config.Get<PetChoice>("Pets") == PetChoice.None)
            return;

        if (Bot.Config.Get<PetChoice>("Pets") == PetChoice.HotMama && !Core.CheckInventory("Hot Mama"))
        {
            Core.HuntMonster("battleundere", "Hot Mama", "Hot Mama", isTemp: false, log: false);
            Bot.Wait.ForPickup("Hot Mama");
            Core.Equip("Hot Mama");
        }

        if (Bot.Config.Get<PetChoice>("Pets") == PetChoice.Akriloth && !Core.CheckInventory("Akriloth Pet"))
        {
            Core.HuntMonster("gravestrike", "Ultra Akriloth", "Akriloth Pet", isTemp: false);
            Bot.Wait.ForPickup("Akriloth Pet");
            Core.Equip("Akriloth Pet");
        }
    }

    public void RagsandHat()
    {
        if (Core.CheckInventory("Peasant Rags") | Core.CheckInventory("Scarecrow Hat"))
            return;

        Core.Logger("Farming Rags & Hat");

        Adv.BuyItem("yulgar", 41, "Peasant Rags");
        Bot.Wait.ForPickup("Peasant Rags");
        Core.Equip("Peasant Rags");
        Adv.BuyItem("yulgar", 16, "Scarecrow Hat");
        Bot.Wait.ForPickup("Scarecrow Hat");
        Core.Equip("Scarecrow Hat");
    }

    public void ServersAreDown()
    {
        if (Core.CheckInventory("The Server is Down"))
            return;

        Core.Logger("Farming Servers Are Down Sign");
        Core.HuntMonster("undergroundlabb", "Rabid Server Hamster", "The Server is Down", isTemp: false);
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