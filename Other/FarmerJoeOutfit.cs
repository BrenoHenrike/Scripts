//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Hollowborn/HollowbornReapersScythe.cs
//cs_include Scripts/Enhancement/InventoryEnhancer.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
//cs_include Scripts/Good/ArchPaladin.cs
//cs_include Scripts/Story/XansLair.cs
//cs_include Scripts/Good/BLoD/CoreBLOD.cs
//cs_include Scripts/Story/Yokai.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Other/Classes/DragonShinobi.cs
//cs_include Scripts/Good/GearOfAwe/CapeOfAwe.cs
//cs_include Scripts/Dailies/LordOfOrder.cs
//cs_include Scripts/Nation/CoreNation.cs
//cs_include Scripts/Nation/AssistingCragAndBamboozle[Mem].cs
//cs_include Scripts/Nation/VHL/CoreVHL.cs
//cs_include Scripts/Dailies/0AllDailies.cs
//cs_include Scripts/Story/Nation/CitadelRuins.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/LivingDungeon.cs
//cs_include Scripts/Story/DragonFableOrigins.cs
//cs_include Scripts/Story/Glacera.cs
//cs_include Scripts/Good/GearOfAwe/CoreAwe.cs
//cs_include Scripts/Other/Weapons/BurningBlade.cs
//cs_include Scripts/Other/Classes/REP-based/EvolvedShaman.cs
//cs_include Scripts/Other/Classes/REP-based/GlacialBerserker.cs
//cs_include Scripts/Story/QueenofMonsters/Extra/BrightOak.cs
//cs_include Scripts/Other/Classes/REP-based/StoneCrusher.cs
//cs_include Scripts/Other/Classes/REP-based/EternalInversionist.cs
//cs_include Scripts/Good/Paladin.cs
//cs_include Scripts/Other\Weapons\EnchantedVictoryBladeWeapons.cs
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
    public EvolvedShaman ES = new();
    public GlacialBerserker GB = new();
    public StoneCrusher SC = new();
    public EnchantedVictoryBladeWeapons EVBW = new();

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
        StartingTheAcc();

        Core.SetOptions(false);
    }

    public void StartingTheAcc()
    {
        #region starting out the acc
        Core.BuyItem("classhalla", 176, "Healer");
        Farm.Experience(30);

        #endregion starting out the acc

        #region Obtain the Enchanted Victory Blade

        EVBW.EnchantedVictoryBlade();
        Core.Equip("Enchanted Victory Blade");
        InvEn.EnhanceInventory();
        #endregion Obtain the Silver Victory Blade

        #region Level to 75
        Farm.Experience(75);
        #endregion Level to 75

        #region Prepare for Lvl100
        //step 1 Farming Class:
        LOC.Complete13LOC(true);
        Farm.ChaosREP();
        Adv.BuyItem("Confrontation", 891, "Chaos Slayer Berserker", shopItemID: 24359);
        AP.GetAP();

        //Step 2 Solo CLass:
        LOO.GetLoO();

        //Step 3 Dailies for Classes:
        FAD.DoAllDailys();

        //Step 4 Blade and Cape of Awe:
        Farm.BladeofAweREP(6, true);
        COA.GetCoA();

        //Step 5 Burning Blade:
        Core.EquipClass(ClassType.Solo);
        BB.GetBurningBlade();

        //Step 6 Improving Efficiency:
        EI.GetEI();
        ES.GetES();
        GB.GetGB();
        SC.GetSC();
        Adv.GearStore();
        Adv.BuyItem("Classhalla", 178, "Ninja");
        Core.Equip("Ninja");
        Adv.rankUpClass("Ninja");
        Adv.GearStore(true);
        #endregion Prepare for Lvl100

        #region Leveling to 100
        Farm.Experience();
        #endregion Leveling to 100

        #region Ending & Extras 
        //Pre-Farm Enh
        Adv.EnhanceEquipped(EnhancementType.Lucky);
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
        Farm.Experience(50);
        Adv.EnhanceEquipped(EnhancementType.Lucky);

        //Medium Difficulty Stuff
        DS.GetDSS();
        COA.GetCoA();
        Farm.Experience(80);
        Adv.EnhanceEquipped(EnhancementType.Lucky);

        //Hard Difficulty Stuff
        Adv.EnhanceEquipped(EnhancementType.Lucky);
        Farm.Experience();

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
            Core.HuntMonster("battleundere", "Hot Mama", "Hot Mama", isTemp: false);
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