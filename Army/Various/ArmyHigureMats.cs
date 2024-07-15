
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;

public class ArmyPrinceDarkonsPoleaxeMats
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreArmyLite Army = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyHigureMats";
    public bool DontPreconfigure = true;
    public List<IOption> Options = new()
    {
        sArmy.player1,
        sArmy.player2,
        sArmy.player3,
        sArmy.player4,
        sArmy.player5,
        sArmy.player6,
		new Option<string>(
            "ClassToUse",
            "your class",
            "class to use",
            "classsssss"
        ),
		new Option<string>(
            "SafeClass",
            "your safe class",
            "any class that not used in this bot",
            "classsssss"
        ),
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {

        Core.SetOptions(disableClassSwap: false);

        Army.initArmy();
        Army.setLogName(OptionsStorage);
		
		// Bot.Options.SetFPS = 60;
		
		ArmyHunt("arcangrove", new[] { "Right", "LeftBack" }, "Darkon's Receipt", "", 7324, 66);
        ArmyHunt("astravia", new[] { "r6", "r7", "r8" }, "La's Gratitude", "r8", 8001, 66);
		ArmyHunt("astraviacastle", new[] { "r3", "r6", "r11" }, "Astravian Medal", "r11", 8257, 66);
        ArmyHunt("astraviajudge", new[] { "r2", "r3", "r11" }, "A Melody", "r11", 8396, 66);
		ArmyHunt("astraviapast", new[] { "r6", "r7", "r8", "r4" }, "Suki's Prestige", "", 8602, 66);
        ArmyHunt("firstobservatory", new[] { "r6", "r7", "r10a" }, "Ancient Remnant", "r10a", 8641, 66);
		ArmyHunt("genesisgarden", new[] { "r6", "r9", "r11" }, "Mourning Flower", "r11", 8688, 66);
        ArmyHunt("theworld", new[] { "r9" }, "Unfinished Musical Score", "", 0, 66);
		ArmyHunt("hbchallenge", new[] { "r6" }, "Bounty Hunter Dubloon", "", 9393, 222);

        Core.SetOptions(false);
    }


    void ArmyHunt(string map, string[] cells, string item, string priorityCell, int questId, int quant = 1)
    {
		Core.Equip(Bot.Config.Get<string>("SafeClass"));
        Army.registerMessage(item);
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Core.BankingBlackList.Add(item);
        Core.AddDrop(item);
        if (map.ToLower() == "eridani"){
            Core.AddDrop("Tooth");
            Core.AddDrop("Wisdom Tooth");
        }

		Bot.Sleep(1000);
		Core.Equip(Bot.Config.Get<string>("ClassToUse"));
        //Core.EquipClass(classType);
        Core.Join(map);
		
        Army.waitForPartyCell("Enter", "Spawn");
        if(questId != 0)
            Core.RegisterQuests(questId);
        Army.waitForSignal("imready");

        Army.DivideOnCellsPriority(cells, priorityCell: priorityCell, setAggro: true);
        Army.registerMessage(item, false);

        Core.FarmingLogger(item, quant);

        Core.Logger($"army: starting {quant} {item}");
        Army.AggroMonStart();
        Army.StartFarm(item, quant, new int[] { 1, 2, 3, 4 } );

        Core.CancelRegisteredQuests();
        Army.AggroMonStop(true);
        Core.Jump(Bot.Player.Cell, Bot.Player.Pad);
        Core.ToBank(item);
        Core.Logger($"everyone have finished {quant} {item}");
    }
}
