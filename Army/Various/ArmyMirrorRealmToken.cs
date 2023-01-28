/*
name:  Army Mirror Realm Tokens
description:  
tags: army, mirror realm token, token, mirror realm
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArmyMirrorRealmToken
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyMirrorRealmToken";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new List<IOption>()
    {
        new Option<Method>("Method", "Which method to farm Mirror Realm Token?", "Choose your method", Method.Kill_Weak_Mob),
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
        Core.BankingBlackList.Add("Mirror Realm Token");
        Core.SetOptions();

        Setup(Bot.Config.Get<Method>("Method"), 300);

        Core.SetOptions(false);
    }

    public void Setup(Method Method, int quant = 300)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();
        Core.AddDrop("Mirror Realm Token");

        if (Method.ToString() == "Kill_Weak_Mob")
        {
            Core.EquipClass(ClassType.Farm);
            Army.SmartAggroMonStart("overworld", "Undead Bruiser", "Undead Mage", "Undead Minion");
        }

        else if (Method.ToString() == "Kill_Boss")
        {
            Core.EquipClass(ClassType.Solo);
            Core.RegisterQuests(3188);
            Army.SmartAggroMonStart("mirrorportal", "Chaos Harpy");
        }

        while (!Bot.ShouldExit && (!Core.CheckInventory("Mirror Realm Token", 300)))
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
    }

    public enum Method
    {
        Kill_Weak_Mob = 0,
        Kill_Boss = 1,
    }
}
