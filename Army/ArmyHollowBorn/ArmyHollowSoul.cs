/*
name: Army Hollow Soul
description: uses an army to farm Hollow Soul
tags: hollow soul, army
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArmyHollowSoul
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyHollowSoul";
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
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        Core.BankingBlackList.AddRange(new[] { "Hollow Soul" });
        Core.SetOptions(disableClassSwap: true);

        Setup();

        Core.SetOptions(false);
    }

    public void Setup(int quant = 2500)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        if (Core.CheckInventory("Hollow Soul", quant))
            return;

        Core.AddDrop("Hollow Soul");
        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger("Hollow Soul", quant);


        while (!Bot.ShouldExit && !Core.CheckInventory("Hollow Soul", quant))
        {
            Core.EnsureAccept(7553, 7555);

            ArmyHunt(new[] { 3, 7, 11 }, new[] { "r2", "r4", "r6" }, "shadowrealm", "Darkseed", 8);
            ArmyHunt(new[] { 4, 8, 12 }, new[] { "r2", "r4", "r6" }, "shadowrealm", "Shadow Medallion", 5);

            Core.EnsureComplete(7553);
            Core.EnsureComplete(7555);
        }

        Core.CancelRegisteredQuests();
        Army.AggroMonStop(true);
        Army.waitForParty("whitemap", "Hollow Soul");
    }

    void ArmyHunt(int[] MonsterMapIDs, string[] cells, string aggroMonStart, string itemName, int quant = 1)
    {
        Army.AggroMonCells(cells);
        Army.AggroMonStart(aggroMonStart);
        Army.DivideOnCells(cells);

        Core.FarmingLogger(itemName, quant);

        while (!Bot.ShouldExit && !Core.CheckInventory(itemName, quant))
        {
            foreach (int MonsterMapID in MonsterMapIDs)
            {
                if (Bot.Monsters.CurrentAvailableMonsters.Exists(x => x.MapID == MonsterMapID))
                    Bot.Combat.Attack(MonsterMapID);
                Core.Sleep();
            }
        }

        Army.AggroMonClear();
    }











}
