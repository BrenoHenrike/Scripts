/*
name: Army DreadRock LT
description: Uses an army to farm legion tokens at dreadrock
tags: army, legion, dreadrock, legion tokens
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;

public class ArmyDreadRockLT
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyDreadrockLT";
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
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.Add("Legion Token");
        Core.SetOptions(disableClassSwap: false);

        Setup();

        Core.SetOptions(false);
    }

    public void Setup(int quant = 25000)
    {
        if (Core.CheckInventory("Legion Token", quant))
            return;
        Core.AddDrop("Legion Token");
        Core.Logger($"Farming {quant} Legion Tokens");
        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(4849);
        Army.SmartAggroMonStart("dreadrock", new[] { "Fallen Hero", "Hollow Wraith", "Legion Sentinel", "Shadowknight", "Void Mercenary" });
        while (!Bot.ShouldExit && !Core.CheckInventory("Legion Token", quant))
            Bot.Combat.Attack("*");
        Core.CancelRegisteredQuests();
        Army.AggroMonStop(true);
    }
}
