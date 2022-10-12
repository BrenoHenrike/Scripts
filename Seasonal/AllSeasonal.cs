//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/AprilFools/DERP!Badge.cs
//cs_include Scripts/Seasonal/AprilFools/MeateorHunt.cs
//cs_include Scripts/Seasonal/AprilFools/SuperSLAYIN'Badge(GardenQuest).cs
//cs_include Scripts/Seasonal/AprilFools/Mmmm,Meaty(or)(MeatyShard).cs
//cs_include Scripts/Seasonal/Frostvale/Frostvale.cs
//cs_include Scripts/Seasonal/Frostvale/MountOtzi.cs
//cs_include Scripts/Seasonal/HerosHeartDay/Fezzini.cs
//cs_include Scripts/Seasonal/HerosHeartDay/LoveSpellStory.cs
//cs_include Scripts/Seasonal/HerosHeartDay/WheelOfLove.cs
//cs_include Scripts/Seasonal/LuckyDay/Pooka.cs
//cs_include Scripts/Seasonal/MayThe4th/DarkLord.cs
//cs_include Scripts/Seasonal/MayThe4th/MurderMoonStory.cs
//cs_include Scripts/Seasonal/MayThe4th/MurderMoonMerge[CyberCrystal].cs
//cs_include Scripts/Story/MemetsRealm/CoreMemet.cs
//cs_include Scripts/Seasonal/Mogloween/VampireLord.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/DageRecruit.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/Undervoid.cs
//cs_include Scripts/Seasonal/StarFestival/StarFestival.cs
//cs_include Scripts/Seasonal/SummerBreak/BeachPartyTokenItems.cs
//cs_include Scripts/Seasonal/SummerBreak/BlazingBeach.cs
//cs_include Scripts/Seasonal/SummerBreak/BlazingBeachMerge.cs
//cs_include Scripts/Seasonal/SummerBreak/BurningBeach.cs
//cs_include Scripts/Seasonal/SummerBreak/CoralBeachMerge.cs
//cs_include Scripts/Story/Summer2015AdventureMap/CoreSummer.cs
//cs_include Scripts/Seasonal/SummerBreak/LunaCoveMerge.cs
//cs_include Scripts/Seasonal/SummerBreak/SweetSummerTreats.cs
//cs_include Scripts/Seasonal/SummerBreak/Un-LifeguardQuest.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/CelestialPirateCommander[PollyRogers].cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/KaijuWar.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/HeartOfTheSeaStory.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/CetoleonWarStory.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/DragonPirateStory.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/DragonCapitalStory.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/AluteaNursery.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/BlazeBeardStory.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/LowTideStory.cs
using Skua.Core.Interfaces;

public class AllSeasonal
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public DERPBadge Derp = new();
    public MeateorHunt MeateorHunt = new();
    public SuperSLAYINBadge SSB = new();
    public Frostvale Frostvale = new();
    public MountOtzi MountOtzi = new();
    public FezziniStory Fezzini = new();
    public LoveSpell LoveSpell = new();
    public WheeleOfLove WheeleOfLove = new();
    public PookaStory Pooka = new();
    public MmmmMeatyQuest Meaty = new();
    public DarkLord DarkLord = new();
    public MurderMoon MurderMoon = new();

    public VampireLord VPL = new();
    public DageRecruitStory DageRecruit = new();
    public UndervoidStory Undervoid = new();
    public StarFestival StarFestival = new();
    public BeachPartyTokenItems BeachPartyTokenItems = new();
    public BlazingBeachStory BlazingBeach = new();
    // public BlazingBeachMerge BlazingBeachMerge = new();
    public BurningBeachStory BurningBeach = new();
    // public CoralBeachMerge CoralBeachMerge = new();
    public CoreSummer LunaCove = new();
    // public LunaCoveMerge LunaCoveMerge = new();
    public SweetSummerTreats SweetSummerTreats = new();
    public UnLifeGuardQuest UnLifeguardQuest = new();
    public CelestialPirateCommander CelestialPirateCommander = new();
    public KaijuWar KaijuWar = new();
    public HeartOfTheSeaStory HeartOfTheSeaStory = new();
    public CetoleonWarStory CetoleonWarStory = new();
    public DragonPirateStory DragonPirateStory = new();
    public DragonCapitalStory DragonCapitalStory = new();
    public LowTideStory LowTideStory = new();
    public AluteaNursery AluteaNursery = new();
    public BlazeBeard BlazeBeard = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Seasonals();

        Core.SetOptions(false);
    }

    public void Seasonals()
    {
        switch (DateTime.Now.Month)
        {
            default:
                DageRecruit.CompleteDageRecruit();
                if (Bot.Quests.IsAvailable(7713))
                    CelestialPirateCommander.GetCPC(true);
                break;

            case 1:
                Core.Logger("Starting Scripts for January");
                //insert script voids here
                Core.Logger($"Scripts Finished for {DateTime.Now.ToString("MMMM")}");
                break;

            case 2:
                Core.Logger("Starting Scripts for Febuary");
                //insert script voids here
                Fezzini.FezziniScript();
                LoveSpell.LoveSpellScript();
                WheeleOfLove.DoWheeleOfLove();
                Core.Logger($"Scripts Finished for {DateTime.Now.ToString("MMMM")}");
                break;

            case 3:
                Core.Logger("Starting Scripts for March");
                //insert script voids here
                Pooka.CompletePooka();
                DarkLord.GetDL();
                MurderMoon.MurderMoonStory();
                Undervoid.CompleteUnderVoid();
                Core.Logger($"Scripts Finished for {DateTime.Now.ToString("MMMM")}");

                break;

            case 4:
                Core.Logger("Starting Scripts for April");
                //insert script voids here
                Derp.GetBadge();
                MeateorHunt.StoryLine();
                SSB.GetBadgeANDDoStory();
                Meaty.CompleteQuests();
                Core.Logger($"Scripts Finished for {DateTime.Now.ToString("MMMM")}");
                break;

            case 5:
                Core.Logger("Starting Scripts for May");
                //insert script voids here
                Core.Logger($"Scripts Finished for {DateTime.Now.ToString("MMMM")}");
                break;

            case 6:
                Core.Logger("Starting Scripts for June");
                // BeachPartyTokenItems.TokenItems();
                BlazingBeach.StoryLine();
                // BlazingBeachMerge.BuyAllMerge();
                BurningBeach.Storyline();
                // CoralBeachMerge.BuyAllMerge();
                LunaCove.LunaCove();
                // LunaCoveMerge.BuyAllMerge();
                // SweetSummerTreats.GetTreats();
                // UnLifeguardQuest.GetItems();
                Core.Logger($"Scripts Finished for {DateTime.Now.ToString("MMMM")}");
                break;

            case 7:
                Core.Logger("Starting Scripts for July");
                Frostvale.DoAll();
                // BeachPartyTokenItems.TokenItems();
                BlazingBeach.StoryLine();
                // BlazingBeachMerge.BuyAllMerge();
                BurningBeach.Storyline();
                // CoralBeachMerge.BuyAllMerge();
                LunaCove.LunaCove();
                // LunaCoveMerge.BuyAllMerge();
                // SweetSummerTreats.GetTreats();
                // UnLifeguardQuest.GetItems();
                StarFestival.StoryLine();
                MountOtzi.MountOtziQuests();
                Core.Logger($"Scripts Finished for {DateTime.Now.ToString("MMMM")}");
                break;

            case 8:
                Core.Logger("Starting Scripts for August");
                //insert script voids here
                Frostvale.DoAll();
                Core.Logger($"Scripts Finished for {DateTime.Now.ToString("MMMM")}");
                break;

            case 9:
                Core.Logger("Starting Scripts for September");
                //insert script voids here
                CelestialPirateCommander.GetCPC(true);
                KaijuWar.KaijuItems();
                HeartOfTheSeaStory.HeartOfTheSea();
                CetoleonWarStory.CetoleonWar();
                DragonPirateStory.DragonPirate();
                DragonCapitalStory.DragonCapital();
                LowTideStory.Storyline();
                AluteaNursery.DoAll();
                BlazeBeard.TokenQuests();
                Core.Logger($"Scripts Finished for {DateTime.Now.ToString("MMMM")}");
                break;

            case 10:
                Core.Logger("Starting Scripts for October");
                //insert script voids here
                Core.Logger($"Scripts Finished for {DateTime.Now.ToString("MMMM")}");
                break;

            case 11:
                Core.Logger("Starting Scripts for November");
                //insert script voids here
                VPL.GetClass(false);
                Core.Logger($"Scripts Finished for {DateTime.Now.ToString("MMMM")}");
                break;

            case 12:
                Core.Logger("Starting Scripts for December");
                //insert script voids here
                // Frostvale.DoAll();
                Core.Logger($"Scripts Finished for {DateTime.Now.ToString("MMMM")}");
                break;
        }
    }
}