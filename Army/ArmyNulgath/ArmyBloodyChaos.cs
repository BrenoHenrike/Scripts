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
using Skua.Core.Models.Monsters;
using Skua.Core.Options;

public class ArmyBloodyChaos
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private static CoreBots Core => CoreBots.Instance;
    private readonly CoreArmyLite Army = new();

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
            ArmyHunt("hydrachallenge", mob == Cell.h85 ? new[] { 29, 30, 31 } : new[] { 32, 33, 34 }, "Hydra Scale Piece", 200);
            ArmyHunt("stalagbite", new[] { 7, 8 }, "Shattered Legendary Sword of Dragon Control");
            ArmyHunt("escherion", new[] { 2, 3 }, "Escherion's Helm");
            Bot.Wait.ForQuestComplete(7816);
        }
        Core.CancelRegisteredQuests();
    }

    void ArmyHunt(string map, int[] monsters, string item, int quant = 1)
    {
        if (Core.CheckInventory(item, quant))
        {
            return;
        }

        Army.waitForParty(map);

        Core.AddDrop(item);

        //Army.waitForParty(map, item);
        Core.FarmingLogger(item, quant);

        switch (map)
        {
            case "stalagbite":
                Core.EquipClass(ClassType.Solo);
                bool PrekillVath = false;
                Core.Join("stalagbite", "r2", "Left");

                Monster? Vath = Bot.Monsters.MapMonsters.FirstOrDefault(x => x.MapID == 7);
                Monster? Stalagbite = Bot.Monsters.MapMonsters.FirstOrDefault(x => x.MapID == 8);

                Army.AggroMonMIDs(7, 8);
                Army.DivideOnCells("r2");
                Army.AggroMonStart("stalagbite");

                while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
                {
                    // Initialize combat (to set hp)
                    if (!PrekillVath)
                    {
                        Bot.Kill.Monster(Stalagbite!.MapID);
                        Bot.Wait.ForMonsterSpawn(Stalagbite!.MapID);
                        PrekillVath = true;
                    }

                    Stalagbite = Bot.Monsters.MapMonsters.FirstOrDefault(x => x.MapID == 8);
                    Vath = Bot.Monsters.MapMonsters.FirstOrDefault(x => x.MapID == 7);

                    if (Stalagbite?.State == 1 || Stalagbite?.State == 2)
                    {
                        Bot.Kill.Monster(Stalagbite!.MapID);
                        Bot.Combat.CancelTarget();
                    }
                    else if (Stalagbite?.State == 0)
                    {
                        Bot.Combat.Attack(Vath!.MapID);
                        Core.Sleep();
                    }
                }
                Army.AggroMonStop(true);
                Core.JumpWait();
                break;

            case "escherion":
                Core.Join("escherion", "Boss", "Left");
                Core.EquipClass(ClassType.Solo);
                Monster? Staff = Bot.Monsters.MapMonsters.FirstOrDefault(x => x.MapID == 2);
                Monster? Escherion = Bot.Monsters.MapMonsters.FirstOrDefault(x => x.MapID == 3);
                bool PrekillEscherion = false;

                Army.AggroMonMIDs(2, 3);
                Army.DivideOnCells("Boss");
                Army.AggroMonStart("escherion");

                while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
                {
                    if (Bot.Player.Cell != "Boss")
                    {
                        Core.Jump("Boss", "Left");
                        Core.Sleep();
                    }

                    // Initialize combat (to set hp)
                    if (!PrekillEscherion)
                    {
                        Bot.Kill.Monster(Staff!.MapID);
                        Bot.Wait.ForMonsterSpawn(Staff.MapID);
                        PrekillEscherion = true;
                    }

                    Staff = Bot.Monsters.MapMonsters.FirstOrDefault(x => x.MapID == 2);
                    Escherion = Bot.Monsters.MapMonsters.FirstOrDefault(x => x.MapID == 3);

                    if (Staff?.State == 1 || Staff!.State == 2)
                    {
                        Bot.Kill.Monster(Staff!.MapID);
                        Bot.Combat.CancelTarget();
                    }
                    else if (Staff?.State == 0)
                    {
                        Bot.Combat.Attack(Escherion!.MapID);
                        Core.Sleep();
                    }
                }

                Army.AggroMonStop(true);
                Core.JumpWait();
                break;

            case "hydrachallenge":
                Army.AggroMonMIDs(monsters);
                Army.AggroMonStart("hydrachallenge");
                Army.DivideOnCells(Bot.Config!.Get<Cell>("mob") == Cell.h85 ? "h85" : "h90");

                while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
                {
                    Bot.Combat.Attack("*");
                    Core.Sleep();
                }

                Army.AggroMonStop(true);
                Core.JumpWait();
                break;

            default:
                while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
                    Bot.Combat.Attack("*");
                break;
        }

        Army.AggroMonStop(true);
        Core.JumpWait();
    }



    private readonly string[] Loot =
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
