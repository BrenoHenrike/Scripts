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

        Setup(Bot.Config.Get<Cell>("mob"));

        Core.SetOptions(false);
    }

    public void Setup(Cell mob, int quant = 100)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        Core.AddDrop(Loot);
        Core.EquipClass(ClassType.Farm);

        Core.RegisterQuests(2857); //Supplies to Spin the Wheel
        Bot.Quests.UpdateQuest(363);

        Army.SellToSync("Hydra Scale Piece", Bot.Inventory.GetQuantity("Hydra Scale Piece"));
        Army.SellToSync("Shattered Legendary Sword of Dragon Control", 1);
        Army.SellToSync("Escherion's Helm", 1);

        //Bloody Chaos
        Core.RegisterQuests(7816);
        while (!Bot.ShouldExit && !Core.CheckInventory("Blood Gem of the Archfiend", quant))
        {
            ArmyHunt("hydrachallenge", new[] { mob.ToString() }, "Hydra Scale Piece", ClassType.Solo, true, 200);
            ArmyHunt("stalagbite", new[] { "vath" }, "Shattered Legendary Sword of Dragon Control", ClassType.Solo, false);
            ArmyHunt("escherion", new[] { "Escherion" }, "Escherion's Helm", ClassType.Solo, false);
        }
        Core.CancelRegisteredQuests();
    }


    void ArmyHunt(string map = null, string[] monsters = null, string item = null, ClassType classType = ClassType.Solo, bool isTemp = false, int quant = 1)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Bot.Config.Get<bool>("sellToSync"))
            Army.SellToSync(item, quant);

        Core.AddDrop(item);

        Core.EquipClass(classType);
        Army.waitForParty(map, item);
        Core.FarmingLogger(item, quant);

        Army.SmartAggroMonStart(map, monsters);

        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
        {
            if (Bot.Map.Name == "Vath")
            {
                if (Bot.Monsters.MapMonsters?.FirstOrDefault(m => m.Name == "Stalagbite")?.Alive ?? false)
                {
                    Bot.Kill.Monster("Stalagbite");
                    Bot.Combat.Attack("Vath");
                }
            }
            else if (Bot.Map.Name == "escherion")
            {
                while (!Bot.ShouldExit && !Core.CheckInventory("Escherion's Helm"))
                {
                    if (Bot.Monsters.MapMonsters?.FirstOrDefault(m => m.Name == "Staff of Inversion")?.Alive ?? false)
                        Bot.Kill.Monster("Staff of Inversion");
                    Bot.Combat.Attack("Escherion");
                }
            }
            else Bot.Combat.Attack("*");
        }
        Army.AggroMonStop(true);
        Core.JumpWait();
    }


    public void PlayerAFK()
    {
        Core.Logger("Anti-AFK engaged");
        Bot.Sleep(1500);
        Bot.Send.Packet("%xt%zm%afk%1%false%");
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
