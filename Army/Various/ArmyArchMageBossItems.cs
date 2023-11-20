/*
name: ArchMage Boss Items (Army)
description: Uses an army to help farm the required boss items for Archmage.
tags: arch, mage, army, boss, items, "calamitous ruin", "vital exanima", "everlight flame", "the mortal coil", "undying resolve", "the divine will", "insatiable hunger", "void essentia", "elemental binding"
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Items;

public class ArchMageMatsArmy
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    private CoreArmyLite Army = new();

    private static CoreArmyLite sArmy = new();

    public bool DontPreconfigure = true;
    public string OptionsStorage = "BossDrops";
    public List<IOption> Options = new List<IOption>()
    {
        new Option<bool>("sellToSync", "Sell to Sync", "Sell items to make sure the army stays syncronized.\nIf off, there is a higher chance your army might desyncornize", false),
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

    public string[] Drops =
    {
        "Calamitous Ruin",
        "Vital Exanima",
        "Everlight Flame",
        "The Mortal Coil",
        "Undying Resolve",
        "The Divine Will",
        "Insatiable Hunger",
        "Void Essentia",
        "Elemental Binding",
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Drops);
        Core.SetOptions(disableClassSwap: true);

        Bot.Events.PlayerAFK += PlayerAFK;
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");
        GetmBois();
        Bot.Events.PlayerAFK -= PlayerAFK;

        Core.SetOptions(false);
    }

    public void GetmBois()
    {
        Core.EquipClass(ClassType.Solo);
        Bot.Quests.UpdateQuest(8732);

        ArmyKillMonster("voidflibbi", "Enter", 5131, "Void Essentia");
        ArmyKillMonster("voidnightbane", "Enter", 5240, "Insatiable Hunger");
        ArmyKillMonster("dage", "Boss", 0, "Vital Exanima");
        ArmyKillMonster("fireavatar", "r9", 4926, "Everlight Flame");
        ArmyKillMonster("tercessuinotlim", "Boss2", 35, "The Mortal Coil");
        ArmyKillMonster("theworld", "r9", 5187, "Undying Resolve");
        ArmyKillMonster("celestialpast", "r11a", 4582, "The Divine Will");
        ArmyKillMonster("darkcarnax", "Boss", 5262, "Calamitous Ruin");
        ArmyKillMonster("archmage", "r2", 5295, "Elemental Binding", 250);

        Core.Logger("Jobs done!");
    }

    public void ArmyKillMonster(string map, string cell, int MonID, string item, int quant = 1, bool isTemp = true, bool log = true, bool publicRoom = false)
    {
        if (string.IsNullOrEmpty(item) || Core.CheckInventory(item, quant))
            return;

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (!isTemp)
            Core.AddDrop(item);

        if (Bot.Config!.Get<bool>("sellToSync"))
            Army.SellToSync(item, quant);

        if (!Core.CheckInventory(item, toInv: false))
        {
            Bot.Drops.Add(item);
            Core.FarmingLogger(item, 1);
            if (MonID != 5295)
                Army.AggroMonMIDs(MonID);
            Army.AggroMonStart(map);
            Army.DivideOnCells(cell);
        }
        else
        {
            Core.Logger($"{item} Found.");
            Army.waitForParty(map, item);
        }

        if (Bot.Map.Name == "darkcarnax")
        {
            Bot.Options.AttackWithoutTarget = true;
            Bot.Events.RunToArea += DarkCarnaxMove;

            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            {
                while (!Bot.ShouldExit && Bot.Player.Cell != cell)
                {
                    Core.Jump(cell);
                    Core.Sleep(Core.ActionDelay);
                }
                Bot.Combat.Attack(MonID);
            }

            Bot.Options.AttackWithoutTarget = false;
            Bot.Events.RunToArea -= DarkCarnaxMove;
        }
        else if (Bot.Map.Name == "archmage")
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            {
                while (!Bot.ShouldExit && Bot.Player.Cell != cell)
                {
                    Core.Jump(cell);
                    Core.Sleep(Core.ActionDelay);
                }
                foreach (int MonsterMapID in new[] { 1, 2 })
                    if (Core.IsMonsterAlive(MonsterMapID, useMapID: true))
                        Bot.Combat.Attack(MonsterMapID);
            }
        }
        else
        {
            while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            {
                while (!Bot.ShouldExit && Bot.Player.Cell != cell)
                {
                    Core.Jump(cell);
                    Core.Sleep(Core.ActionDelay);
                }
                Bot.Combat.Attack(MonID);
            }
        }
        Army.AggroMonStop(true);
        Core.ConfigureAggro(false);
        Core.JumpWait();
    }


    void DarkCarnaxMove(string zone)
    {
        Core.Sleep(Core.ActionDelay);
        switch (zone.ToLower())
        {
            case "a":
                //Move to the right
                Core.Sleep(1500);
                Bot.Player.WalkTo(Bot.Random.Next(600, 930), Bot.Random.Next(380, 475));
                break;
            case "b":
                //Move to the left
                Core.Sleep(1500);
                Bot.Player.WalkTo(Bot.Random.Next(25, 325), Bot.Random.Next(380, 475));
                break;
            default:
                //Move to the center
                Core.Sleep(1500);
                Bot.Player.WalkTo(Bot.Random.Next(325, 600), Bot.Random.Next(380, 475));
                break;
        }
    }
    public void PlayerAFK()
    {
        Core.Logger("Anti-AFK engaged");
        Core.Sleep(1500);
        Bot.Send.Packet("%xt%zm%afk%1%false%");
    }

}