//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArmySpiritOrb
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmySpiritOrb";
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
        new Option<int>("amount","Amount", "Input the amount of spirit orbs to farm", 65000),
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(Loot);
        Core.SetOptions();
        bot.Options.RestPackets = false;

        Setup(Bot.Config.Get<int>("amount"));

        Core.SetOptions(false);
    }

    public void Setup(int quant = 65000)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.AddDrop(Loot);
        Core.EquipClass(ClassType.Farm);
		Core.RegisterQuests(2082, 2083);
        Core.Logger($"Farming for {quant} bone dust");
		Army.SmartAggroMonStart("battleunderb", "Skeleton Warrior", "Skeleton Fighter", "Undead Champion");
        while (!Bot.ShouldExit && !Core.CheckInventory("Spirit Orb", quant))
            Bot.Combat.Attack("*");
        Army.AggroMonStop(true);
		Core.CancelRegisteredQuests();
    }

    private string[] Loot = { "Bone Dust", "Undead Essence", "Undead Energy", "Spirit Orb" };
}