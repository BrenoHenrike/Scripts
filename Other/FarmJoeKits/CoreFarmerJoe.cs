/*
name: null
description: null
tags: null
*/
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
//cs_include Scripts/Other/Classes/ScarletSorceress.cs
//cs_include Scripts/Other/Classes/BloodSorceress.cs
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

public class CoreFarmerJoe
{
    //other
    public IScriptInterface Bot => IScriptInterface.Instance;
    public FreeBoosts Boosts = new();
    public FarmAllDailies FAD = new();
    public InventoryEnhancer InvEn = new();


    //Cores
    public CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv = new();
    public CoreFarms Farm = new();
    public CapeOfAwe COA = new();
    public Core13LoC LOC => new Core13LoC();
    public CoreDailies Daily = new();
    public CoreVHL VHL = new CoreVHL();
    public CoreNation Nation = new();

    //Classes
    public Shaman Shaman = new();
    public GlacialBerserker GB = new();
    public StoneCrusher SC = new();
    public DragonShinobi DS = new();
    public ArchPaladin AP = new();
    public LordOfOrder LOO = new();
    public ScarletSorceress SS = new();
    public EternalInversionist EI = new();

    //Weapons
    public DualChainSawKatanas DCSK = new();
    public BurningBlade BB = new();
    public EnchantedVictoryBladeWeapons EVBW = new();
    public HollowbornScythe Scythe = new();

    //Story
    public MountDoomSkull MDS = new();
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

    public void ScriptMain(IScriptInterface bot)
    {
        Core.RunCore();
    }

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
        #region starting out the acc
        //starting out the acc
        Core.Logger("starting out the acc");
        Tutorial.Badges();

        Core.Logger("Getting Starting Cash and Levels");

        if (Bot.Player.Level < 10)
        {
            Core.SoloClass = "Oracle";
            Core.FarmClass = "Mage";

            Core.RegisterQuests(4007);
            Adv.BuyItem("classhalla", 174, "Mage");
            Adv.BuyItem("classhalla", 759, "Oracle");
            Adv.rankUpClass("Mage");
            Adv.rankUpClass("Oracle");
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
            }
            Core.CancelRegisteredQuests();
        }

        if (Bot.Player.Level < 20)
        {
            Core.SoloClass = "Oracle";
            Core.FarmClass = "Mage";

            Adv.BuyItem("classhalla", 174, "Mage");
            Adv.BuyItem("classhalla", 759, "Oracle");
            Adv.BuyItem("classhalla", 174, "Mage's Hood");
            Adv.BuyItem("classhalla", 176, "White Feather Wings");
            Core.Equip("White Feather Wings");
            Core.Equip("Mage's Hood");
            Adv.rankUpClass("Mage");
            Adv.rankUpClass("Oracle");
            Farm.IcestormArena(20, true);
            Core.SoloClass = "Oracle";
            Core.FarmClass = "Mage";
            InvEn.EnhanceInventory();
        }

        if (Bot.Player.Level < 30)
        {
            Core.SoloClass = "Oracle";
            Core.FarmClass = "Mage";

            InvEn.EnhanceInventory();
            Farm.IcestormArena(30, true);
        }
    }
    #endregion starting out the acc

    public void Level30to75()
    {
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
        Adv.BuyItem("classhalla", 174, "Mage");
        Adv.rankUpClass("Mage");
        Adv.BuyItem("classhalla", 759, "Oracle");
        Adv.rankUpClass("Oracle", false);
        DCSK.GetWep();
        InvEn.EnhanceInventory();
        #endregion Dual Chainsaw Katanas

        #region Leve30 to 75
        Core.Logger("Level to 75");
        Core.EquipClass(ClassType.Farm);
        foreach (int level in new int[] { 45, 50, 55, 60, 65, 70, 75 })
        {
            if (Bot.Player.Level >= 50)
            {
                DS.GetDSS();
                Core.Equip("DragonSoul Shinobi");
                SS.GetSSorc();

                Core.SoloClass = "DragonSoul Shinobi";
                Core.FarmClass = "Scarlet Sorceress";

                Farm.Experience(level);
                InvEn.EnhanceInventory();
            }
            if (Bot.Player.Level >= 65)
            {
                InvEn.EnhanceInventory();
                AP.GetAP();
                EI.GetEI();

                Core.SoloClass = "Arch Paladin";
                Core.FarmClass = "Eternal Inversionist";

                Core.EquipClass(ClassType.Farm);
                Farm.Experience(level);
                InvEn.EnhanceInventory();
            }
            if (Bot.Player.Level < level)
            {
                Farm.Experience(level);
                InvEn.EnhanceInventory();
            }
        }
        #endregion Level to 75
    }

    public void Level75to100()
    {
        #region Prepare for Lvl100
        // P1: Healer for xiang
        Core.Logger("P1: Healer for xiang, Buying & Ranking Healer\n" +
        "class to prep for xiang (Skipped if you have Dragon of Time.");

        if (!Core.CheckInventory("Dragon of Time"))
        {
            if (!Core.CheckInventory(new[] { "Healer", "Healer (Rare)" }))
                Adv.BuyItem("classhalla", 176, "Healer");
            if (Core.CheckInventory("Healer (Rare)"))
                Adv.rankUpClass("Healer (Rare)");
            else Adv.rankUpClass("Healer");
        }

        //P2 Chaos Shenanagins
        Core.Logger("P2: Chaos Shenanagins");
        LOC.Complete13LOC();
        Farm.ChaosREP();
        Adv.BuyItem("confrontation", 891, "Chaos Slayer Berserker", shopItemID: 15402);
        Adv.rankUpClass("Chaos Slayer Berserker");
        Core.Equip("Chaos Slayer Berserker");

        //Step 2 Solo Class:
        Core.Logger("P3: Solo Classes & Weapon");
        Core.Logger("Getting Lord of order.");
        LOO.GetLoO();
        Core.ToBank(Core.EnsureLoad(7156).Rewards.Select(i => i.Name).ToArray());

        Core.Logger("P3-1: Dailies for Classes");
        FAD.DoAllDailies();

        Core.Logger("P3 - 2: Blade and Cape of Awe");
        Farm.BladeofAweREP(6, true);
        Core.ToBank("Blade of Awe");
        Adv.BuyItem("museum", 631, "Awethur's Accoutrements");
        Core.Equip("Awethur's Accoutrements");
        COA.GetCoA();
        InvEn.EnhanceInventory();

        Core.Logger("P3 - 3: Burning Blade");
        BB.GetBurningBlade();

        Core.Logger("P3 - 4: Improving Efficiency, and more Classes");
        Shaman.GetShaman();
        GB.GetGB();
        SC.GetSC();
        #endregion Prepare for Lvl100


        #region Leveling to 100
        Core.Logger("P4 Leveling to 100");
        Farm.Experience();
        InvEn.EnhanceInventory();
        #endregion Leveling to 100}
    }

    public void EndGame()
    {
        #region Ending & Extras 

        if (Bot.Config.Get<bool>("OutFit"))
            Outfit();

        Scythe.GetHBReapersScythe();
        //Add more eventualy >.> please?

        #endregion Ending & Extras
    }



    public void Outfit()
    {
        //Easy Difficulty Stuff
        RagsandHat();
        ServersAreDown();
        Adv.SmartEnhance(Bot.Player.CurrentClass.ToString());

        //Extra Stuff
        Pets(PetChoice.None);

        if (Bot.Config.Get<bool>("EquipOutfit"))
        {
            Core.Equip(new[] { "Peasant Rags", "Scarecrow Hat", "The Server is Down", "Hollowborn Reaper's Scythe" });
            Core.Equip(Bot.Config.Get<PetChoice>("PetChoice").ToString());
        }

        Core.Logger("We are farmers, bum ba dum bum bum bum bum");
    }

    #region other stuff

    public void Pets(PetChoice PetChoice = PetChoice.None)
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
