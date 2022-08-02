//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Seasonal/AprilFools/DERP!Badge.cs
//cs_include Scripts/Seasonal/AprilFools/MeateorHunt.cs
//cs_include Scripts/Seasonal/AprilFools/SuperSLAYIN'Badge(GardenQuest).cs
//cs_include Scripts/Seasonal/Frostvale/Frostvale.cs
//cs_include Scripts/Seasonal/Frostvale/MountOtzi.cs
//cs_include Scripts/Seasonal/HerosHeartDay/Fezzini.cs
//cs_include Scripts/Seasonal/HerosHeartDay/LoveSpellStory.cs
//cs_include Scripts/Seasonal/HerosHeartDay/WheelOfLove.cs
//cs_include Scripts/Seasonal/LuckyDay/Pooka.cs
//cs_include Scripts/Seasonal/MayThe4th/DarkLord.cs
//cs_include Scripts/Seasonal/MayThe4th/MurderMoonStory.cs
// cs_include Scripts/Seasonal/MayThe4th/MurderMoonMerge[CyberCrystal].cs
//cs_include Scripts/Seasonal/MayThe4th/ZorbasPalaceStory.cs
//cs_include Scripts/Seasonal/Mogloween/BloodMoonToken.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/DageRecruit.cs
//cs_include Scripts/Seasonal/StaffBirthdays/DageTheEvil/Undervoid.cs
//cs_include Scripts/Seasonal/StarFestival/StarFestival.cs
//cs_include Scripts/Seasonal/SummerBreak/BeachParty.cs
//cs_include Scripts/Seasonal/SummerBreak/BeachPartyTokenItems.cs
//cs_include Scripts/Seasonal/SummerBreak/BlazingBeach.cs
// cs_include Scripts/Seasonal/SummerBreak/BlazingBeachMerge.cs
//cs_include Scripts/Seasonal/SummerBreak/BurningBeach.cs
// cs_include Scripts/Seasonal/SummerBreak/CoralBeachMerge.cs
//cs_include Scripts/Seasonal/SummerBreak/FreakiTiki.cs
//cs_include Scripts/Seasonal/SummerBreak/LunaCove.cs
// cs_include Scripts/Seasonal/SummerBreak/LunaCoveMerge.cs
//cs_include Scripts/Seasonal/SummerBreak/SweetSummerTreats.cs
//cs_include Scripts/Seasonal/SummerBreak/Un-LifeguardQuest.cs
//cs_include Scripts/Seasonal/TalkLikeaPirateDay/CelestialPirateCommander[PollyRogers].cs
//cs_include Scripts/Seasonal/AprilFools/Mmmm,Meaty(or)(MeatyShard).cs
using RBot;

public class AllSeasonal
{
    public ScriptInterface Bot => ScriptInterface.Instance;
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
    public DarkLord DarkLord = new();
    public MurderMoon MurderMoon = new();
    public ZorbasPalace ZorbasPalace = new();
    public BloodMoonToken BMToken = new();
    public DageRecruitStory DageRecruit = new();
    public UndervoidStory Undervoid = new();
    public StarFestival StarFestival = new();

    public BeachPartyStory BeachParty = new();
    public BeachPartyTokenItems BeachPartyTokenItems = new();
    public BlazingBeachStory BlazingBeach = new();
    // public BlazingBeachMerge BlazingBeachMerge = new();
    public BurningBeachStory BurningBeach = new();
    // public CoralBeachMerge CoralBeachMerge = new();
    public FreakiTikiStory FreakiTiki = new();
    public LunaCoveStory LunaCove = new();
    // public LunaCoveMerge LunaCoveMerge = new();
    public SweetSummerTreats SweetSummerTreats = new();
    public UnLifeGuardQuest UnLifeguardQuest = new();

    public CelestialPirateCommander CelestialPirateCommander = new();
    public MmmmMeatyQuest Meaty = new();


    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Seasonals();

        Core.SetOptions(false);
    }

    public void Seasonals()
    {
        DateTime dt = System.DateTime.Now;

        foreach (int Month in dt.Month.ToString())
        {
            switch (dt.Month)
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
                    ZorbasPalace.ZorbasPalaceStory();
                    Undervoid.CompleteUnderVoid();
                    Core.Logger($"Scripts Finished for {DateTime.Now.ToString("MMMM")}");

                    break;

                case 4:
                    Core.Logger("Starting Scripts for April");
                    //insert script voids here
                    Core.Logger($"Scripts Finished for {DateTime.Now.ToString("MMMM")}");
                    Derp.GetBadge();
                    MeateorHunt.StoryLine();
                    SSB.GetBadgeANDDoStory();
                    Meaty.CompleteQuests();
                    break;

                case 5:
                    Core.Logger("Starting Scripts for May");
                    //insert script voids here
                    Core.Logger($"Scripts Finished for {DateTime.Now.ToString("MMMM")}");
                    break;

                case 6:
                    Core.Logger("Starting Scripts for June");
                    BeachParty.Storyline();
                    // BeachPartyTokenItems.TokenItems();
                    BlazingBeach.StoryLine();
                    // BlazingBeachMerge.BuyAllMerge();
                    BurningBeach.Storyline();
                    // CoralBeachMerge.BuyAllMerge();
                    FreakiTiki.Storyline();
                    LunaCove.Storyline();
                    // LunaCoveMerge.BuyAllMerge();
                    // SweetSummerTreats.GetTreats();
                    // UnLifeguardQuest.GetItems();
                    Core.Logger($"Scripts Finished for {DateTime.Now.ToString("MMMM")}");
                    break;

                case 7:
                    Core.Logger("Starting Scripts for July");
                    Frostvale.DoAll();
                    BeachParty.Storyline();
                    // BeachPartyTokenItems.TokenItems();
                    BlazingBeach.StoryLine();
                    // BlazingBeachMerge.BuyAllMerge();
                    BurningBeach.Storyline();
                    // CoralBeachMerge.BuyAllMerge();
                    FreakiTiki.Storyline();
                    LunaCove.Storyline();
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
                    Core.Logger($"Scripts Finished for {DateTime.Now.ToString("MMMM")}");
                    BMToken.BMToken();
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
}