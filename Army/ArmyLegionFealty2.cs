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
        sCore.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[]
        {"Conquest Wreath", "Grim Cohort Conquered", "Ancient Cohort Conquered",
        "Pirate Cohort Conquered", "Battleon Cohort Conquered",
        "Darkblood Cohort Conquered", "Vampire Cohort Conquered",
        "Dragon Cohort Conquered", "Doomwood Cohort Conquered"});

        Core.SetOptions();
        bot.Options.RestPackets = false;

        Setup();

        Core.SetOptions(false);
    }

    public void Setup()
    {
        Legion.JoinLegion();

        Core.EquipClass(ClassType.Farm);
        Core.AddDrop("Conquest Wreath");

        while (!Bot.ShouldExit && !Core.CheckInventory("Conquest Wreath", 6))
        {
            Core.EnsureAccept(6898);
            ArmyThing("doomvault", new[] { "Grim Soldier", "Grim Fighter", "Grim Fire Mage", "Highibiscus", "Lowtus", "Flying Spyball", "Grim Ectomancer" }, "Grim Cohort Conquered", false, 500);
            ArmyThing("mummies", new[] { "Mummy" }, "Ancient Cohort Conquered", false, 500);
            ArmyThing("wrath", new[] { "Undead Pirate", "Mutineer", "Dark Fire", "Fishbones" }, "Pirate Cohort Conquered", false, 500);
            ArmyThing("doomwar", new[] { "Zombie", "Zombie Knight" }, "Battleon Cohort Conquered", false, 500);
            ArmyThing("overworld", new[] { "Undead Minion", "Undead Mage" }, "Mirror Cohort Conquered", false, 500);
            ArmyThing("deathpits", new[] { "Ghastly Darkblood", "Rotting Darkblood", "Sundered Darkblood" }, "Darkblood Cohort Conquered", false, 500);
            ArmyThing("maxius", new[] { "Vampire Minion" }, "Vampire Cohort Conquered", false, 500);
            ArmyThing("curseshore", new[] { "*" }, "Spirit Cohort Conquered", false, 500);
            ArmyThing("dragonbone", new[] { "Bone Dragonling", "Dark Fire", }, "Dragon Cohort Conquered", false, 500);
            ArmyThing("doomwood", new[] { "Doomwood Soldier", "Doomwood Bonemuncher", "Doomwood Ectomancer", "Undead Paladin", "Doomwood Treeant" }, "Doomwood Cohort Conquered", false, 500);
            Core.EnsureComplete(6898);
        }
    }

    void ArmyThing(string map = null, string[] monsters = null, string item = null, bool isTemp = false, int quant = 0)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Core.CheckInventory(item, quant))
            return;

        if (item == null)
            return;

        Bot.Drops.Add(item);

        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger(item, quant);

        Core.Join(map);
        WaitCheck();
        Armyshit(map);

        foreach (string monster in monsters)
            Army.SmartAggroMonStart(map, monsters);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.JumpWait();
        Bot.Wait.ForPickup(item);
    }

    void WaitCheck()
    {
        while (Bot.Map.PlayerCount < Bot.Config.Get<int>("armysize"))
        {
            Core.Logger($"Waiting for the squad. [{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}]");
            Bot.Sleep(5000);
        }
            Core.Logger($"Squad All Gathered [{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}]");
    }

    void Armyshit(string map = null)
    {
        if (Bot.Map.Name == null)
            return;

        if (Bot.Map.Name == "doomvault")
        {
            Army.AggroMonCells("r1", "r2", "r3");
            Army.AggroMonStart("doomvault");
            Army.DivideOnCells("r1", "r2", "r3");
        }

        if (Bot.Map.Name == "mummies")
        {
            Army.AggroMonCells("Enter", "r2", "r3");
            Army.AggroMonStart("mummies");
            Army.DivideOnCells("Enter", "r2", "r3");
        }

        if (Bot.Map.Name == "wrath")
        {
            Army.AggroMonCells("r2", "r3", "r5");
            Army.AggroMonStart("wrath");
            Army.DivideOnCells("r2", "r3", "r5");
        }

        if (Bot.Map.Name == "doomwar")
        {
            Army.AggroMonCells("r4", "r6", "r8");
            Army.AggroMonStart("doomwar");
            Army.DivideOnCells("r4", "r6", "r8");
        }

        if (Bot.Map.Name == "overworld")
        {
            Army.AggroMonCells("Enter", "r2", "r4");
            Army.AggroMonStart("overworld");
            Army.DivideOnCells("Enter", "r2", "r4");
        }

        if (Bot.Map.Name == "deathpits")
        {
            Army.AggroMonCells("r1", "r3", "r4");
            Army.AggroMonStart("deathpits");
            Army.DivideOnCells("r1", "r3", "r4");
        }

        if (Bot.Map.Name == "maxius")
        {
            Army.AggroMonCells("r2", "r4");
            Army.AggroMonStart("maxius");
            Army.DivideOnCells("r2", "r4");
        }

        if (Bot.Map.Name == "curseshore")
        {
            Army.AggroMonCells("Enter", "r2");
            Army.AggroMonStart("curseshore");
            Army.DivideOnCells("Enter", "r2");
        }

        if (Bot.Map.Name == "dragonbone")
        {
            Army.AggroMonCells("Enter", "r2", "r3");
            Army.AggroMonStart("dragonbone");
            Army.DivideOnCells("Enter", "r2", "r3");
        }

        if (Bot.Map.Name == "doomwood")
        {
            Army.AggroMonCells("r3", "r4", "r6");
            Army.AggroMonStart("doomwood");
            Army.DivideOnCells("r3", "r4", "r6");
        }


    }
}