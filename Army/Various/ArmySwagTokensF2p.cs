/*
name: Army superfan swag token a
description: farms super-fan swag token a with f2p method
tags: "super-fan swag token a", army, f2p
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;

public class ArmySwagTokensF2p
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    public CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "CustomAggroMon";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        CoreBots.Instance.SkipOptions,
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
        sArmy.player7,
        sArmy.player8,
        sArmy.player9,
        sArmy.player10,
    };

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Setup();
        Core.SetOptions(false);
    }

    public void Setup(int quant = 100)
    {
        if (Core.CheckInventory("Super-Fan Swag Token A", quant))
            return;

        Core.AddDrop("Super-Fan Swag Token A", "Super-Fan Swag Token B", "Super-Fan Swag Token C", "Super-Fan Swag Token D");
        Core.FarmingLogger($"Super-Fan Swag Token A", quant);
        Core.EquipClass(ClassType.Farm);
        Adv.BestGear(GearBoost.dmgAll);
        Adv.SmartEnhance(Bot.Player.CurrentClass.ToString());
        Core.RegisterQuests(1304, 1307);
        while (!Bot.ShouldExit && !Core.CheckInventory("Super-Fan Swag Token A", quant))
        {
            while (!Bot.ShouldExit && !Core.CheckInventory("Super-Fan Swag Token D", 500))
            {
                // Army.RunGeneratedAggroMon(map, monNames, questIDs, classtype, drops);
                Army.AggroMonStart("terrarium");
                Army.DivideOnCells("r3", "Enter");
                Army.AggroMonIDs(701, 703);
                while (!Bot.ShouldExit && !Core.CheckInventory("Super-Fan Swag Token D", 500))
                    Bot.Combat.Attack("*");
                Army.AggroMonStop();
                Core.JumpWait();
            }


            // Core.Join("Collection", "Begin", "Spawn");
            // if (!Bot.Shops.IsLoaded)
            //     Bot.Shops.Load(325);


            //Token Buying
            Core.Logger("Buying Token C");
            Core.BuyItem("Collection", 325, "Super-Fan Swag Token C", 50);
            Core.Logger("Buying Token B");
            while (!Bot.ShouldExit && (!Core.CheckInventory("Super-Fan Swag Token B", 20) && Core.CheckInventory("Super-Fan Swag Token C", 10)))
                Bot.Shops.BuyItem("Super-Fan Swag Token B");
            Core.Logger("Buying Token A");
            while (!Bot.ShouldExit && (!Core.CheckInventory("Super-Fan Swag Token A", quant) && Core.CheckInventory("Super-Fan Swag Token B", 20)))
                Bot.Shops.BuyItem("Super-Fan Swag Token A");

        }
        Core.CancelRegisteredQuests();
    }
}
