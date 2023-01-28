/*
name:  Army Dark Token
description:  uses an army to farm dark tokens
tags: dark token, army, seraphicwardage
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;
using System.Linq;

public class ArmyDarkToken
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    public CoreLegion Legion = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyDarkToken";
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
        Core.SetOptions();

        Setup();

        Core.SetOptions(false);
    }

    public void Setup(int quant = 600)
    {
        if (Core.CheckInventory("Dark Token", quant))
            return;

        Core.FarmingLogger("Dark Token", quant);
        Core.AddDrop("Dark Token");

        Core.EquipClass(ClassType.Farm);
        Adv.BestGear(GearBoost.Human);

        Core.RegisterQuests(6248, 6249, 6251);
        Army.SmartAggroMonStart("seraphicwardage", new[] { "Seraphic Commander, Seraphic Soldier" });
        while (!Bot.ShouldExit && !Core.CheckInventory("Dark Token", quant))
            Bot.Combat.Attack("*");
        Core.CancelRegisteredQuests();
        Army.AggroMonStop(true);
    }
}
