/*
name: VoidBattleMageSet
description: null
tags: null
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/VasalkarLairWar.cs

using Skua.Core.Interfaces;

public class VoidBattleMageSet
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new CoreFarms();
    public LairWar War = new();

    public readonly string[] Rewards =
    {
        "Void BattleMage",
        "Void BattleMage Stare",
        "Void BattleMage Male Morph",
        "Void BattleMage Male Hood",
        "Void BattleMage Locks",
        "Void BattleMage Female Morph",
        "Void BattleMage Female Hood",
        "Void BattleMage Male Crown",
        "Void BattleMage Female Crown",
        "Void BattleMage Crown",
        "Void BattleMage Runes",
        "Void BattleMage Wrap",
        "Void BattleMage Wrap Runes",
        "Void BattleMage Spear",
        "Void BattleMage Nation Staff"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        GetBattleMageSet();

        Core.SetOptions(false);
    }

    public void GetBattleMageSet()
    {
        int i = 1;
        while (!Bot.ShouldExit && !Core.CheckInventory(Rewards, toInv: false))
        {
            War.Attack();
            Core.EquipClass(ClassType.Solo);
            Core.EnsureAccept(6694);
            Core.KillMonster("lairattack", "Eggs", "Left", "Flame Dragon General", log: false);
            Core.ChainComplete(6694);
            Bot.Drops.Pickup(Rewards);
            Core.Logger($"Completed x{i++}");
        }
    }
}
