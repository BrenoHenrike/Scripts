/*
name: Army Necrotic Sword of Doom Daily
description: uses an army to farm enroaching shadows daily.
tags: nsod, nsod daily, daily, army, enroaching shadows
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class ArmyNSoDDaily
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyNSODDaily";
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
        Core.BankingBlackList.AddRange(new[] { "Glacial Pinion", "Hydra Eyeball", "Flibbitigiblets", "Void Aura" });
        Core.SetOptions(disableClassSwap: true);

        DoDaily();

        Core.SetOptions(false);
    }

    public void DoDaily()
    {
        Core.OneTimeMessage("Only for army", "This is intended for use with an army, not for solo players.");

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.AddDrop("Glacial Pinion", "Hydra Eyeball", "Flibbitigiblets", "Void Aura");
        Core.EquipClass(ClassType.Solo);
        Core.EnsureAccept(8653);

        //Army.waitForParty("Icestormarena", "Glacial Pinion");
        Army.SmartAggroMonStart("icewing", "Warlord Icewing");

        

        while (!Bot.ShouldExit && !Core.CheckInventory("Glacial Pinion"))
            Bot.Combat.Attack("*");
        Army.AggroMonStop();
        Core.JumpWait();

        //Army.waitForParty("hydrachallenge", "Hydra Eyeball");
        Army.SmartAggroMonStart("hydrachallenge", "Hydra Head 90");

        

        while (!Bot.ShouldExit && !Core.CheckInventory("Hydra Eyeball", 3))
            Bot.Combat.Attack("*");
        Army.AggroMonStop();
        Core.JumpWait();

        //Army.waitForParty("voidflibbi", "Flibbitigiblets");
        Army.SmartAggroMonStart("voidflibbi", "Flibbitiestgibbet");

        

        while (!Bot.ShouldExit && !Core.CheckInventory("Flibbitigiblets"))
            Bot.Combat.Attack("*");
        Army.AggroMonStop();
        Core.JumpWait();

        Core.EnsureComplete(8653);
        Army.AggroMonStop(true);
    }
}
