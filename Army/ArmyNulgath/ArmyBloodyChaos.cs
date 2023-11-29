/*
name: Army Bloody Chaos
description: uses an army to farm Blood Gem of the Archfiend
tags: blood gem of the archfiend, army
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
using Skua.Core.Models.Monsters;

public class ArmyBloodyChaos
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private static CoreBots Core => CoreBots.Instance;
    private readonly CoreFarms Farm = new();
    private readonly CoreArmyLite Army = new();

    private static readonly CoreBots sCore = new();
    private static readonly CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyBloodyChaos";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        new Option<Cell>("mob", "h90 or h85", "h90 for more relic turn ins, but more chance of getting stuck due to deaths - h85 for just Relics from Escherion", Cell.h90),
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Loot);
        Core.SetOptions(disableClassSwap: true);

        Setup(Bot.Config!.Get<Cell>("mob"));

        Core.SetOptions(false);
    }

    public void Setup(Cell mob, int quant = 100)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        Core.AddDrop(Loot);

        Bot.Quests.UpdateQuest(363);
        //Bloody Chaos
        Core.RegisterQuests(7816, 2857); //
        while (!Bot.ShouldExit && !Core.CheckInventory("Blood Gem of the Archfiend", quant))
        {
            ArmyHunt("hydrachallenge", Bot.Config!.Get<Cell>("mob") == Cell.h85 ? new[] { 29, 30, 31 } : new[] { 32, 33, 34 }, "Hydra Scale Piece", 200);
            ArmyHunt("stalagbite", new[] { 7, 8 }, "Shattered Legendary Sword of Dragon Control");
            ArmyHunt("escherion", new[] { 2, 3 }, "Escherion's Helm");
        }
        Core.CancelRegisteredQuests();
    }

    void ArmyHunt(string map, int[] monsters, string item, int quant = 1)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Bot.Config!.Get<bool>("sellToSync"))
            Army.SellToSync(item, quant);

        Core.AddDrop(item);

        Core.EquipClass(ClassType.Solo);
        Army.waitForParty(map, item);
        Core.FarmingLogger(item, quant);

        switch (Bot.Map.Name)
        {
            case "stalagbite":
                Army.AggroMonMIDs(7, 8);
                Army.DivideOnCells("r2");
                Army.AggroMonStart("stalagbite");

                while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
                {
                    if (Core.IsMonsterAlive("Stalagbite"))
                        Bot.Kill.Monster("Stalagbite");
                    else Bot.Combat.Attack("Vath");
                    Core.Sleep(1000);
                }
                break;

            case "escherion":
                Army.AggroMonMIDs(2, 3);
                Army.DivideOnCells("Boss");
                Army.AggroMonStart("escherion");
                while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
                {
                    if (Core.IsMonsterAlive("Staff of Inversion"))
                        Bot.Kill.Monster("Staff of Inversion");
                    else Bot.Combat.Attack("Escherion");
                    Core.Sleep(1000);
                }
                break;

            case "hydrachallenge":
                Army.AggroMonMIDs(monsters);
                Army.DivideOnCells(Bot.Config!.Get<Cell>("mob") == Cell.h85 ? "h85" : "h90");
                Army.AggroMonStart("hydrachallenge");
                while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
                    Bot.Combat.Attack(Bot.Config!.Get<Cell>("mob") == Cell.h85 ? "Hydra Head 85" : "Hydra Head 90");
                break;

            default:

                if (Bot.Player.CurrentClass?.Name == "ArchMage")
                    Bot.Options.AttackWithoutTarget = true;

                while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
                    Bot.Combat.Attack("*");
                break;
        }

        Army.AggroMonStop(true);
        Core.JumpWait();
    }



    private string[] Loot =
    {
        "Tainted Gem",
        "Dark Crystal Shard",
        "Diamond of Nulgath",
        "Voucher of Nulgath",
        "Voucher of Nulgath (non-mem)",
        "Unidentified 10",
        "Unidentified 13",
        "Gem of Nulgath",
        "Relic of Chaos",
        "Shattered Legendary Sword of Dragon Control",
        "Escherion's Helm",
        "Hydra Scale Piece",
        "Blood Gem of the Archfiend"
    };

    public enum Cell
    {
        h90 = 0,
        h85 = 1
    }
}
