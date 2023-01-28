/*
name:  Army Bloody Chaos
description:  uses an army to farm Blood Gem of the Archfiend
tags: blood gem of the archfiend, army
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArmyBloodyChaos
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyBloodyChaos";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        new Option<int>("armysize","Players", "Input the minimum of players to wait for", 1),
        new Option<Cell>("mob", "h90 or h85", "h90 for more relic turn ins, but more chance of getting stuck due to deaths - h85 for just Relics from Escherion", Cell.h90),
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Loot);

        Core.SetOptions(disableClassSwap: true);

        Setup(Bot.Config.Get<Cell>("mob"));

        Core.SetOptions(false);
    }

    public void Setup(Cell mob, int quant = 100)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.AddDrop(Loot);
        Core.EquipClass(ClassType.Farm);
        
        Core.RegisterQuests(2857); //Supplies to Spin the Wheel
        Bot.Quests.UpdateQuest(363);

        Core.Logger("Selling items to sync up your army");
        Core.SellItem("Hydra Scale Piece", all: true);
        Core.SellItem("Shattered Legendary Sword of Dragon Control");
        Core.SellItem("Escherion's Helm");

        while (!Bot.ShouldExit && !Core.CheckInventory("Blood Gem of the Archfiend", quant))
        {
            Core.EnsureAccept(7816); //Bloody Chaos
            Core.Join("hydrachallenge", mob.ToString(), "Left");
            WaitCheck();
            Bot.Sleep(2000);
            Army.AggroMonCells(mob.ToString());
            Army.AggroMonStart("hydrachallenge");
            while (!Bot.ShouldExit && !Core.CheckInventory("Hydra Scale Piece", 200))
                Bot.Combat.Attack("*");
            Army.AggroMonStop(true);
            Core.Join("stalagbite");
            Core.Jump("r2", "Right");
            WaitCheck();
            Bot.Sleep(2000);
            Army.AggroMonCells("r2");
            Army.AggroMonStart("stalagbite");
            while (!Bot.ShouldExit && !Core.CheckInventory("Shattered Legendary Sword of Dragon Control"))
            {
                if (Bot.Monsters.MapMonsters?.FirstOrDefault(m => m.Name == "Stalagbite")?.Alive ?? false)
                    Bot.Kill.Monster("Stalagbite");
                Bot.Combat.Attack("Vath");
            }
            Army.AggroMonStop(true);
            Core.Join("escherion");
            Core.Jump("Boss", "Left");
            WaitCheck();
            Bot.Sleep(2000);
            Army.AggroMonCells("Boss");
            Army.AggroMonStart("escherion");
            while (!Bot.ShouldExit && !Core.CheckInventory("Escherion's Helm"))
            {
                if (Bot.Monsters.MapMonsters?.FirstOrDefault(m => m.Name == "Staff of Inversion")?.Alive ?? false)
                    Bot.Kill.Monster("Staff of Inversion");
                Bot.Combat.Attack("Escherion");
            }
            Army.AggroMonStop(true);
            Core.EnsureComplete(7816);
        }

        void WaitCheck()
        {
            while (Bot.Map.PlayerCount < Bot.Config.Get<int>("armysize"))
            {
                Core.Logger($"Waiting for the squad. [{Bot.Map.PlayerNames.Count}/{Bot.Config.Get<int>("armysize")}]");
                Bot.Sleep(2000);
            }
        };
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
