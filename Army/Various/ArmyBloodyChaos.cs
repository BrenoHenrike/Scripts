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
        CoreBots.Instance.SkipOptions,
        sArmy.packetDelay
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.Logger("Make sure each account starts with similar amounts of Hydra Scales(max 50 difference) and no Escherions Helm/Shattered Swords", messageBox: true);
        Core.Logger("This script requires 13 inventory spaces. So, make sure you have that or get fucked x)", messageBox: true);

        Core.BankingBlackList.AddRange(Loot);

        Core.SetOptions(disableClassSwap: true);
        bot.Options.RestPackets = false;

        Setup(Bot.Config.Get<Cell>("mob"));

        Core.SetOptions(false);
    }

    public void Setup(Cell mob)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.AddDrop(Loot);
        Core.RegisterQuests(7816, 2857);
        Core.EquipClass(ClassType.Farm);
        Bot.Quests.UpdateQuest(363);

        while (!Bot.ShouldExit)
        {
            Core.Join("hydrachallenge", mob.ToString(), "Left");
            WaitCheck();
            Bot.Sleep(2000);
            Army.AggroMonCells(mob.ToString());
            Army.AggroMonStart("hydrachallenge");
            while (!Bot.ShouldExit && !Core.CheckInventory("Hydra Scale Piece", 350, false))
            {
                Bot.Combat.Attack("*");
                if (Bot.Map.PlayerCount < Bot.Config.Get<int>("armysize"))
                    break;
            }
            Army.AggroMonStop(true);
            Core.Join("stalagbite");
            Core.Jump("r2", "Right");
            WaitCheck();
            Bot.Sleep(2000);
            Army.AggroMonCells("r2");
            Army.AggroMonStart("stalagbite");
            while (!Bot.ShouldExit && !Core.CheckInventory("Shattered Legendary Sword of Dragon Control", toInv: false))
            {
                Bot.Kill.Monster("Stalagbite");
                Bot.Kill.Monster("Vath");
            }
            Army.AggroMonStop(true);
            Core.Join("escherion");
            Core.Jump("Boss", "Left");
            WaitCheck();
            Bot.Sleep(2000);
            Army.AggroMonCells("Boss");
            Army.AggroMonStart("escherion");
            while (!Bot.ShouldExit && !Core.CheckInventory("Escherion's Helm", toInv: false))
                Bot.Combat.Attack("*");
            Army.AggroMonStop(true);
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