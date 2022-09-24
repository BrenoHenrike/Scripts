//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Legion/CoreLegion.cs
using Skua.Core.Interfaces;
using Skua.Core.Options;

public class ArmyLegionToken
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public CoreStory Story = new CoreStory();
    public CoreAdvanced Adv = new CoreAdvanced();
    public CoreArmyLite Army = new();
    public CoreLegion Legion = new();

    public static CoreBots sCore = new();
    public static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyLegionToken";
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
        new Option<Method>("Method", "Which method to get LTs?", "Choose your method", Method.Dreadrock),
        sCore.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        if (!Bot.Config.Get<bool>("SkipOption"))
            Bot.Config.Configure();

        Core.BankingBlackList.AddRange(Loot);

        Core.SetOptions(disableClassSwap: false);
        bot.Options.RestPackets = false;

        Setup(Bot.Config.Get<Method>("Method"));

        Core.SetOptions(false);
    }


    public void Setup(Method Method)
    {
        Core.EquipClass(ClassType.Farm);
        Core.AddDrop("Legion Token");
        if (Method.ToString() == "Dreadrock")
        {
            Core.Join("dreadrock");
            Core.RegisterQuests(4850);
        if (string.IsNullOrEmpty(Bot.Config.Get<string>("player5").Trim()) && string.IsNullOrEmpty(Bot.Config.Get<string>("player6").Trim()))
            Army.AggroMonCells("r3", "r4", "r5", "r6");
        else Army.AggroMonCells("r3", "r4", "r5", "r6", "r7");
        Army.AggroMonStart("dreadrock");
        Army.DivideOnCells("r3", "r4", "r5", "r6", "r7");

        while (!Bot.ShouldExit)
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
        }
        else if (Method.ToString() == "Shogun_Paragon_Pet")
        {
            Core.Join("fotia");
            Core.RegisterQuests(5755);
            Army.AggroMonCells("Enter", "r2", "r3", "r4");
        Army.AggroMonStart("fotia");
        Army.DivideOnCells("Enter", "r2", "r3", "r4");

        while (!Bot.ShouldExit)
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
        }
        else
        {
            Core.Join("legionarena");
            Core.AddDrop("Bone Sigil");
        Core.EquipClass(ClassType.Solo);
        Core.RegisterQuests(6742, 6743);
        Army.AggroMonCells("Boss");
        Army.AggroMonStart("legionarena");
        Core.Jump("Boss", "Left");

        while (!Bot.ShouldExit)
            Bot.Combat.Attack("*");

        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
        }
    }

    private string[] Loot = { "Legion Token" };
    public enum Method
    {
        Dreadrock = 0,
        Shogun_Paragon_Pet = 1,
        Legion_Arena = 2,
    }
}
