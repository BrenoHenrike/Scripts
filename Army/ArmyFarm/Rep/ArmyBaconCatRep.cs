/*
name: Army BaconCat Rep
description: Farm reputation with your army. Faction: baconcat
tags: army, reputation, baconcat
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Story/ThroneofDarkness/CoreToD.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ArmyBaconCatRep
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    private CoreToD TOD = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyBaconCatRep";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6, //adjust if needed, check maps limit on wiki
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Setup();

        Core.SetOptions(false);
    }

    public void Setup()
    {
        if (Farm.FactionRank("BaconCat") >= 10)
            return;

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        TOD.BaconCatFortress();
        TOD.LaserSharkInvasion();
        
        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(5111, 5112, 5118, 5119, 5120); //Cloud Sharks! 5111, Get Those Waffle Cones Ready 5112, Save the Kittarians 5118, Bacon Cat Force Needs YOU! 5119, Ziri Is Also Tough 5120
        Farm.ToggleBoost(BoostType.Reputation);
        Army.SmartAggroMonStart("baconcatlair", "8-Bit Shark", "Cat Clothed Shark", "Cloud Shark", "Ice Cream Shark", "Sketchy Shark");
        //WaitCheck();
        while (!Bot.ShouldExit && Farm.FactionRank("BaconCat") < 10)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Reputation, false);
        Core.CancelRegisteredQuests();
    }
}
