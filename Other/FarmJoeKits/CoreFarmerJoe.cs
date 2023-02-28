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

//cs_include Scripts/Dailies/0AllDailies.cs
//cs_include Scripts/Good/GearOfAwe/CapeOfAwe.cs
//cs_include Scripts/Good/Paladin.cs
//cs_include Scripts/Nation/AssistingCragAndBamboozle[Mem].cs
//cs_include Scripts/Other/FreeBoostsQuest(10mns).cs
//cs_include Scripts/Enhancement/InventoryEnhancer.cs

//cs_include Scripts/Other/Classes/REP-based/MasterRanger.cs
//cs_include Scripts/Other/Classes/REP-based/EternalInversionist.cs
//cs_include Scripts/Other/Classes/REP-based/GlacialBerserker.cs
//cs_include Scripts/Other/Classes/REP-based/Shaman.cs
//cs_include Scripts/Other/Classes/REP-based/StoneCrusher.cs
//cs_include Scripts/Other/Classes/ScarletSorceress.cs
//cs_include Scripts/Other/Classes/BloodSorceress.cs
//cs_include Scripts/Other/Classes/DragonShinobi.cs
//cs_include Scripts/Good/ArchPaladin.cs
//cs_include Scripts/Dailies/LordOfOrder.cs

//cs_include Scripts/Other/Weapons/BurningBlade.cs
//cs_include Scripts/Other/Weapons/DualChainSawKatanas.cs
//cs_include Scripts/Other/Weapons/EnchantedVictoryBladeWeapons.cs
//cs_include Scripts/Hollowborn/HollowbornReapersScythe.cs

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
//cs_include Scripts/Story/Friendship.cs

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
    public CoreStory Story = new();
    public CapeOfAwe COA = new();
    public Core13LoC LOC => new Core13LoC();
    public CoreDailies Daily = new();
    public CoreVHL VHL = new CoreVHL();
    public CoreNation Nation = new();

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
        //Farm and Solo class holders for those doing this on non-starter accs
        string SoloClassHolder = Core.SoloClass;
        string FarmClassHolder = Core.FarmClass;

        Level1to30();
        Level30to75();
        Level75to100();
        EndGame();
        Outfit();
        Pets(PetChoice.HotMama);
        Pets(PetChoice.Akriloth);

        //Restore (is this needed?)
        Core.SoloClass = SoloClassHolder;
        Core.SoloClass = FarmClassHolder;
    }


    public void Level1to30()
    {
        if (Bot.Player.Level >= 30)
        {
            Core.Logger("Level is 30+, grabbing oracle, ranking it, then continuing");
            Core.BuyItem("classhalla", 299, "Oracle");
            Adv.rankUpClass("Oracle");
            return;
        }

        #region starting out the acc
        //starting out the acc
        Core.Logger("starting out the acc with the tutorial badges to make it a bit more convincing");
        Tutorial.Badges();

        Core.Logger("Getting Starting Levels/Equipment");

        Core.BuyItem("classhalla", 299, "Oracle");
        Core.BuyItem("classhalla", 299, "Battle Oracle Wings");
        Core.BuyItem("classhalla", 299, "Battle Oracle Battlestaff");
        Core.BuyItem("classhalla", 299, "Battle Oracle Hood");
        Core.Equip("Battle Oracle Battlestaff", "Battle Oracle Hood", "Battle Oracle Wings");

        Core.SoloClass = "Oracle";
        Core.Equip(Core.SoloClass);

        Core.RegisterQuests(4007);
        while (!Bot.ShouldExit && Bot.Player.Level < 10)
            Core.HuntMonster("oaklore", "Bone Berserker");
        Core.CancelRegisteredQuests();

        Story.KillQuest(176, "swordhavenundead", "Skeletal Soldier", false);
        Story.KillQuest(177, "swordhavenundead", "Skeletal Ice Mage", false);

        Core.RegisterQuests(178);
        while (!Bot.ShouldExit && Bot.Player.Level < 28)
            Core.HuntMonster("swordhavenundead", "Undead Giant");
        Core.CancelRegisteredQuests();


        Core.RegisterQuests(6294);
        while (!Bot.ShouldExit && Bot.Player.Level < 30)
            Core.HuntMonster("firewar", "Fire Drakel");
        Core.CancelRegisteredQuests();
        Adv.rankUpClass("Oracle");
    }


    #endregion starting out the acc

    public void Level30to75()
    {

        #region Obtain the Enchanted Victory Blade
        //Arcane Blade of Glory / Shadow Blade of Dispair (+20% xp)
        Core.Logger("Arcane Blade of Glory / Shadow Blade of Dispair (+20% xp)");
        EVBW.GetWeapon(VictoryBladeStyles.Smart);

        string wep = "";
        if (Core.CheckInventory("Arcane Blade of Glory"))
            wep = "Arcane Blade of Glory";
        else if (Core.CheckInventory("Shadow Blade of Dispair"))
            wep = "Shadow Blade of Dispair";
        Core.Equip(wep);
        #endregion Obtain the Silver Victory Blade

        Core.SellItem("Battle Oracle Battlestaff");

        #region Dual Chainsaw Katanas
        DCSK.GetWep();
        #endregion Dual Chainsaw Katanas

        #region Leve30 to 75
        Core.Logger("Level to 75");
        Core.SoloClass = "Oracle";
        Core.FarmClass = "Oracle";
        Core.Equip(Core.FarmClass);
        Adv.BestGear(GearBoost.dmgAll);

        foreach (int Level in new int[] { 30, 45, 50, 55, 60, 65, 70, 75 })
        {
            while (!Bot.ShouldExit && Bot.Player.Level < Level)
            {
                Core.Logger($"Level Goal: {Level}");
                Core.Equip(Core.FarmClass);
                Adv.SmartEnhance(Bot.Player.CurrentClass.Name);
                switch (Level)
                {
                    case 30:
                    case 45:
                    case 70:
                    case 75:
                        if (Bot.Player.Level > 45)
                            MR.GetMR();
                        Core.FarmClass = "Master Ranger";
                        Farm.IcestormArena(Level);
                        break;
                    case 50:
                    case 55:
                        while (!Bot.ShouldExit && Bot.Player.Level < Level)
                            Core.KillMonster("underlair", "r5", "Left", "Void Draconian", log: false);
                        break;
                    case 60:
                        Farm.IcestormArena(Level);
                        DS.GetDSS();
                        Core.Equip("DragonSoul Shinobi");
                        Core.SoloClass = "DragonSoul Shinobi";
                        SS.GetSSorc();
                        Core.FarmClass = "Scarlet Sorceress";

                        Core.Equip(Core.FarmClass);
                        Adv.SmartEnhance(Core.FarmClass);
                        break;
                    case 65:
                        Farm.IcestormArena(Level);
                        AP.GetAP();
                        Core.SoloClass = "ArchPaladin";
                        Core.FarmClass = "ArchPaladin";
                        EI.GetEI();
                        Farm.BladeofAweREP(6, true);
                        Core.ToBank("Blade of Awe");

                        Core.SoloClass = "ArchPaladin";
                        Core.FarmClass = "Eternal Inversionist";

                        Core.Equip(Core.FarmClass);
                        break;
                }
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

        Core.Equip(Core.SoloClass);

        ///Prep class for 13LoC
        if (!Core.CheckInventory("Dragon of Time"))
        {
            if (!Core.CheckInventory(new[] { "Healer", "Healer (Rare)" }))
                Adv.BuyItem("classhalla", 176, "Healer");
            if (Core.CheckInventory("Healer (Rare)"))
                Adv.rankUpClass("Healer (Rare)");
            else Adv.rankUpClass("Healer");
        }

        Core.SoloClass = "ArchPaladin";
        Core.FarmClass = "Eternal Inversionist";

        Core.Equip(Core.SoloClass);

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
        Core.FarmClass = "Shaman";
        GB.GetGB();
        Core.SoloClass = "Glacial Berserker";
        SC.GetSC();
        Core.SoloClass = "StoneCrusher";
        #endregion Prepare for Lvl100


        #region Leveling to 100
        Core.Logger("P4 Leveling to 100");
        Farm.IcestormArena(75);
        InvEn.EnhanceInventory();
        Farm.IcestormArena();
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
        Adv.SmartEnhance(Bot.Player.CurrentClass.Name);

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