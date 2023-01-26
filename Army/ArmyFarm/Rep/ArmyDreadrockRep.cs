/*
name: Army Dreadrock Rep
description: Farm reputation with your army. Faction: Dreadrock
tags: army, reputation, dreadrock
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ArmyDreadrockRep
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyDreadrockRep";
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
        if (Farm.FactionRank("Dreadrock") >= 10)
            return;

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.AddDrop("Ghastly Dreadrock Blade");
        Core.EquipClass(ClassType.Farm);
        Core.RegisterQuests(4863, 4862, 4865, 4868); //Endurance Tesssssst 4863, Supply Run 4862, Ghastly Blades 4865, Glub, Glub, Glub 4868
        Farm.ToggleBoost(BoostType.Reputation);
        Army.SmartAggroMonStart("dreadrock", "Fallen Hero", "Hollow Wraith", "Legion Sentinel", "Shadowknight", "Void Mercenary");
        while (!Bot.ShouldExit && Farm.FactionRank("Dreadrock") < 10)
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
        Farm.ToggleBoost(BoostType.Reputation, false);
        Core.CancelRegisteredQuests();
    }
}
