/*
name: Army Martial Artist
description: Uses your army to get the Martial Artist class
tags: army, martial artist, martial, artist
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class ArmyMartialArtist
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    public CoreStory Story = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyMartialArtist";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.player7,
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Loot);
        Core.SetOptions();

        Setup();

        Core.SetOptions(false);
    }

    public void Setup()
    {
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Farm.Experience(65);
        Core.AddDrop(
           "Dreadhaven General's Soul Fragment",
           "Zakhvatchik's Soul Fragment",
           "Creel's Soul Fragment",
           "Frogzard Defeated",
           "Gorillaphant Defeated",
           "Dragon Defeated"
        );

        Story.ChainQuest(9922);

        // 9923 | 500 Punches and 500 Kicks
        if (!Story.QuestProgression(9923))
        {
            Core.Logger("Quest is required, we'll stack mats via \"Deathly Slow Start [9933]\" After");
            Core.HuntMonsterQuest(9923, new (string? mapName, string? monsterName, ClassType classType)[] {
                    ("nexus", "Frogzard", ClassType.Farm),         // Frogzards Defeated (500): Join nexus, kill Frogzards
                    ("arcangrove", "Gorillaphant", ClassType.Farm), // Gorillaphants Defeated (500): Join arcangrove, kill Gorillaphants
                    ("etherwardes", "Water Dragon Warrior", ClassType.Farm),      // Dragons Defeated (500): Join etherwardes, kill dragons
                }, log: true);
        }
        Core.EquipClass(ClassType.Solo);
        List<Quest> quests = Core.EnsureLoad(Core.FromTo(9922, 9927).Append(Core.IsMember ? 9911 : 9902)?.Where(q => q > 0).ToArray());
        Core.EnsureAccept(Core.IsMember ? 9911 : 9902);

        #region Dreadhaven General's Soul Fragment

        Army.waitForParty("dreadfight", "Enter");
        Army.AggroMonMIDs(1);
        Army.AggroMonStart("dreadfight");
        Army.DivideOnCells("Enter");
        Bot.Player.SetSpawnPoint();

        while (!Bot.ShouldExit && !Core.CheckInventory("Dreadhaven General's Soul Fragment", Core.IsMember ? 200 : 400))
        {
            Bot.Combat.Attack("Dreadhaven General");
            Bot.Sleep(200);
        }
        Army.AggroMonStop(true);
        #endregion


        #region Zakhvatchik's Soul Fragment
        Army.waitForParty("hakuwar", "r10");
        Army.AggroMonMIDs(28);
        Army.AggroMonStart("hakuwar");
        Army.DivideOnCells("r10");
        Bot.Player.SetSpawnPoint();

        while (!Bot.ShouldExit && !Core.CheckInventory("Zakhvatchik's Soul Fragment", Core.IsMember ? 200 : 400))
        {
            Bot.Combat.Attack("Zakhvatchik");
            Bot.Sleep(200);
        }
        Army.AggroMonStop(true);
        #endregion


        #region Creel's Soul Fragment
        Army.waitForParty("towerofdoom5", "r10");
        Army.AggroMonMIDs(28);
        Army.AggroMonStart("towerofdoom5");
        Army.DivideOnCells("r10");
        Bot.Player.SetSpawnPoint();
        while (!Bot.ShouldExit && !Core.CheckInventory("Creel's Soul Fragment", Core.IsMember ? 200 : 400))
        {
            Bot.Combat.Attack("Creel");
            Bot.Sleep(200);
        }
        Army.AggroMonStop(true);
        #endregion

        // Wait & butler back if needed.
        Army.waitForParty("whitemap", "Enter");

        Core.EnsureCompleteMulti(9933);
        Bot.Wait.ForPickup("*");
        foreach (int Q in Core.FromTo(9923, 9927))
        {
            // 9933 | Deathly Slow Start
            // 9924 | Discount Diploma
            // 9925 | One Million Miles Searching
            // 9926 | Ughhhhhh
            // 9927 | Work Smarter, Not Harder
            Story.ChainQuest(Q);
        }

        // Ensure quest is complete before buying
        if (!Story.QuestProgression(9928))
        {
            Army.waitForParty("hakuvillage", "r2a");
            Core.EnsureAccept(9928);
            Army.AggroMonMIDs(10);
            Army.AggroMonStart("hakuvillage");
            Army.DivideOnCells("r2a");
            Bot.Player.SetSpawnPoint();

            while (!Bot.ShouldExit && !Bot.TempInv.Contains("The Master Fought"))
            {
                Bot.Combat.Attack("The Master");
                Bot.Sleep(200);
            }
            Bot.Wait.ForPickup("The Master Fought");
            Core.EnsureComplete(9928);
            Bot.Wait.ForQuestComplete(9928);
        }
        Army.waitForParty("party", "Enter");

        Core.BuyItem("hakuvillage", 2490, "Martial Artist");
        Bot.Wait.ForPickup("Martial Artist");

        Adv.RankUpClass("Martial Artist");

    }

    private string[] Loot = { "Bone Dust", "Undead Energy" };
}
