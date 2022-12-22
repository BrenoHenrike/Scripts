//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;
using System.Linq;

public class ArmyLegionFealty2
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    public CoreLegion Legion = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyLegionFealty2";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        new Option<int>("armysize","Players", "Input the minimum of players to wait for", 1),
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    private string[] CoHortList = {"Grim Cohort Conquered", "Ancient Cohort Conquered",
        "Pirate Cohort Conquered", "Battleon Cohort Conquered",
        "Darkblood Cohort Conquered", "Vampire Cohort Conquered",
        "Dragon Cohort Conquered", "Doomwood Cohort Conquered", "Conquest Wreath"};

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(CoHortList);
        Core.Unbank(CoHortList);

        Core.SetOptions();
        bot.Options.RestPackets = false;

        Setup();

        Core.SetOptions(false);
    }

    public void Setup()
    {
        Legion.JoinLegion();

        Core.EquipClass(ClassType.Farm);
        Core.AddDrop(CoHortList);

        Core.Logger("Selling Cohort Conquered's to sync accs (hopefully)");
        foreach (string item in CoHortList)
            Core.SellItem(item, all: true);

        while (!Bot.ShouldExit && !Core.CheckInventory("Conquest Wreath", 6))
        {
            Core.EnsureAccept(6898);
            ArmyThing("doomvault", new[] { "Grim Soldier" }, "Grim Cohort Conquered", false, 500);
            ArmyThing("mummies", new[] { "Mummy" }, "Ancient Cohort Conquered", false, 500);
            ArmyThing("wrath", new[] { "Undead Pirate", "Mutineer", "Dark Fire", "Fishbones" }, "Pirate Cohort Conquered", false, 500);
            ArmyThing("doomwar", new[] { "Zombie", "Zombie Knight" }, "Battleon Cohort Conquered", false, 500);
            ArmyThing("overworld", new[] { "Undead Minion", "Undead Mage" }, "Mirror Cohort Conquered", false, 500);
            ArmyThing("deathpits", new[] { "Ghastly Darkblood", "Rotting Darkblood", "Sundered Darkblood" }, "Darkblood Cohort Conquered", false, 500);
            ArmyThing("maxius", new[] { "Ghoul Minion" }, "Vampire Cohort Conquered", false, 500);
            ArmyThing("curseshore", new[] { "*" }, "Spirit Cohort Conquered", false, 500);
            ArmyThing("dragonbone", new[] { "Bone Dragonling", "Dark Fire", }, "Dragon Cohort Conquered", false, 500);
            ArmyThing("doomwood", new[] { "Doomwood Soldier", "Doomwood Bonemuncher", "Doomwood Ectomancer", "Undead Paladin", "Doomwood Treeant" }, "Doomwood Cohort Conquered", false, 500);
            Core.EnsureComplete(6898);
            Bot.Wait.ForPickup("Conquest Wreath");
        }
    }

    void ArmyThing(string map = null, string[] monsters = null, string item = null, bool isTemp = false, int quant = 0)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (item == null)
            return;

        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger(item, quant);

        Core.Join(map);
        WaitCheck();

        if (Core.CheckInventory(item, quant))
            return;

        Army.SmartAggroMonStart(map, monsters);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.JumpWait();
    }

    void WaitCheck()
    {
        while (Bot.Map.PlayerCount < Bot.Config.Get<int>("armysize"))
        {
            Core.Logger($"Waiting for the squad. [{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}]");
            Bot.Sleep(5000);
        }
        Core.Logger($"Squad All Gathered [{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}]");
        Bot.Sleep(3500);
    }
}