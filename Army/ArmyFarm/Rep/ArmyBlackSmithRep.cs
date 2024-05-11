/*
name: Army Blacksmithing Rep
description: Farm reputation with your army. Faction: Blacksmithing
tags: army, reputation, blacksmithing
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Army/ArmyFarm/Rep/CoreArmyRep.cs
//cs_include Scripts/Story/LordsofChaos/Core13LoC.cs
//cs_include Scripts/Story/RavenlossSaga.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class ArmyBlackSmithRep
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyBlackSmithRep";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(new[] { "Creature Shard", "Monster Trophy", "Hydra Scale Piece" });

        Core.OneTimeMessage("Urgent", "Please Make sure your goto is on in ingame settings so the buttlering system works properly [turn it back off after your done armying please.]", forcedMessageBox: true);

        Core.SetOptions();

        Setup();

        Core.SetOptions(false);
    }

    public void Setup()
    {
        if (Farm.FactionRank("Blacksmithing") < 4)
            Core.Logger("Rank is < 4, Soloing *till* rank 4");

        while (!Bot.ShouldExit && Farm.FactionRank("Blacksmithing") < 4)
        {
            Core.EnsureAccept(2777);
            Core.HuntMonster("greenguardeast", "Wolf", "Furry Lost Sock", 2, log: false);
            Core.HuntMonster("greenguardwest", "Slime", "Slimy Lost Sock", 5, log: false);
            Core.EnsureComplete(2777);
        }

        Core.AddDrop("Creature Shard", "Monster Trophy", "Hydra Scale Piece");
        while (!Bot.ShouldExit && Farm.FactionRank("Blacksmithing") < 10)
        {
            Core.EnsureAccept(8736);
            Core.EquipClass(ClassType.Farm);

            Armykill("hydrachallenge", "Hydra Scale Piece", 75);
            //Army.waitForParty("maul");

            Core.EquipClass(ClassType.Solo);
            Armykill("maul", "Creature Shard");
            //Army.waitForParty("towerofdoom");

            Armykill("towerofdoom", "Monster Trophy", 15);
            //Army.waitForParty("hydrachallenge");

            Core.EnsureComplete(8736);
        }
        Core.CancelRegisteredQuests();
    }

    void Armykill(string? map = null, string? item = null, int quant = 1)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Core.CheckInventory(item, quant))
            return;

        if (item == null)
            return;

        if (!Bot.Drops.ToPickup.Contains(item) && Bot.Inventory.Items.Find(x => x.Name == item) == null)
            Core.AddDrop(item);

        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger(item, quant);

        AggroSetup(map);

        

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.JumpWait();
        Bot.Wait.ForPickup(item);
    }

    void AggroSetup(string? map)
    {
        switch (map)
        {
            case "hydrachallenge":
                Army.AggroMonCells("h75");
                Army.AggroMonStart(map);
                Army.DivideOnCells("h75");
                break;

            case "maul":
                Army.AggroMonCells("r3");
                Army.AggroMonStart(map);
                Army.DivideOnCells("r3");
                break;

            case "towerofdoom":
                Army.AggroMonCells("r10");
                Army.AggroMonStart(map);
                Army.DivideOnCells("r10");
                break;
        }
    }
}
