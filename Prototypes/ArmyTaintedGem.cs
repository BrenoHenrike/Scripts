//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ArmyTaintedGem
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreArmyLite Army = new();
    CancellationTokenSource cts = new();

    public static CoreBots sCore = new();
    public static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyTaintedGem";
    public int q = 0;
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions,
    };

    public string[] Loot =
    {
        "Cubes",
        "Tainted Gem",
        "Receipt of Swindle"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Loot);

        Core.SetOptions();
        bot.Options.RestPackets = false;

        TaintedGem();

        Core.SetOptions(false);
    }

    public void TaintedGem(int quant = 1000)
    {
        if (Core.CheckInventory("Tainted Gem", quant))
            return;

        Core.AddDrop(Loot);
        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger($"Tainted Gem", quant);
        while (!Bot.ShouldExit && !Core.CheckInventory("Tainted Gem", quant))
        {
            Core.EnsureAccept(7817);
            boxes();
            mountfrost();
            Bot.Sleep(1500);
            Core.EnsureComplete(7817);
            q++;
            Core.Logger($"Quest completed x{q} times");
        }
    }

    public void boxes()
    {
        Core.Join("boxes");
        Army.AggroMonCells("Fort2", "Closet", "Fort1", "Boss");
        Army.AggroMonStart("boxes");
        Army.DivideOnCells("Fort2", "Closet", "Fort1", "Boss", "Boss", "Boss");

        while (!Bot.ShouldExit && (!Core.CheckInventory("Cubes", 500)))
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Core.JumpWait();
        Bot.Sleep(2000);
    }

    public void mountfrost()
    {
        Core.Join("mountfrost");
        Army.AggroMonCells("War");
        Army.AggroMonStart("mountfrost");
        Core.Jump("War", "Left");

        while (!Bot.ShouldExit && (!Core.CheckInventory("Ice Cubes", 6)))
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Core.JumpWait();
        Bot.Sleep(2000);
    }

}
