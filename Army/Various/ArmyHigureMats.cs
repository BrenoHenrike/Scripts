
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;
using Skua.Core.Options;
using System.Collections.Generic;

public class ArmyHigureMats
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
        sArmy.packetDelay,
        CoreBots.Instance.SkipOptions
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        Bot.Options.SetFPS = 30;

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Army.initArmy();
        Army.setLogName(OptionsStorage);

        Army.ClearLogFile();

        Bot.Send.Packet($"%xt%zm%house%1%{Core.Username()}%");
        Bot.Wait.ForMapLoad("house");

        var items = new List<string>
        {
            "Darkon's Receipt", "La's Gratitude", "Astravian Medal", "A Melody",
            "Suki's Prestige", "Ancient Remnant", "Mourning Flower",
            "Unfinished Musical Score", "Bounty Hunter Dubloon"
        };

        var reqQuant = new Dictionary<string, int>();
        foreach (var item in items)
        {
            reqQuant[item] = 66;
        }
        reqQuant["Bounty Hunter Dubloon"] = 222;

        Core.BankingBlackList.AddRange(items);

        Core.Logger("checking req item");

        foreach (var kvp in reqQuant)
        {
            Core.AddDrop(kvp.Key);
            Army.registerMessage(kvp.Key, false);

            if (Core.CheckInventory(kvp.Key, kvp.Value))
            {
                Army.sendDone(20);
            }
        }

        Core.Join("whitemap");
        //Army.waitForPartyCell("Enter", "Spawn");
        Army.waitForSignal("armyready");

        var huntData = new[]
        {
            new {Map = "arcangrove", Cells = new[] { "LeftBack", "Left", "Back"}, Item = "Darkon's Receipt", PriorityCell = "LeftBack", QuestId = 7324, Quantity = 66},
            new {Map = "astravia", Cells = new[] {"r6", "r7", "r8"}, Item = "La's Gratitude", PriorityCell = "r8", QuestId = 8001, Quantity = 66},
            new {Map = "astraviacastle", Cells = new[] {"r3", "r6", "r11"}, Item = "Astravian Medal", PriorityCell = "r11", QuestId = 8257, Quantity = 66},
            new {Map = "astraviajudge", Cells = new[] {"r2", "r3", "r11"}, Item = "A Melody", PriorityCell = "r11", QuestId = 8396, Quantity = 66},
            new {Map = "astraviapast", Cells = new[] {"r6", "r7", "r8", "r4"}, Item = "Suki's Prestige", PriorityCell = "", QuestId = 8602, Quantity = 66},
            new {Map = "firstobservatory", Cells = new[] {"r6", "r7", "r10a"}, Item = "Ancient Remnant", PriorityCell = "r10a", QuestId = 8641, Quantity = 66},
            new {Map = "genesisgarden", Cells = new[] {"r6", "r9", "r11"}, Item = "Mourning Flower", PriorityCell = "r11", QuestId = 8688, Quantity = 66},
            new {Map = "theworld", Cells = new[] {"r9"}, Item = "Unfinished Musical Score", PriorityCell = "", QuestId = 0, Quantity = 66},
            new {Map = "hbchallenge", Cells = new[] {"r6"}, Item = "Bounty Hunter Dubloon", PriorityCell = "", QuestId = 9393, Quantity = 222}
        };

        foreach (var hunt in huntData)
        {
            ArmyHunt(hunt.Map, hunt.Cells, hunt.Item, hunt.PriorityCell, hunt.QuestId, hunt.Quantity);
        }

        Core.SetOptions(false);
    }


    void ArmyHunt(string map, string[] cells, string item, string priorityCell, int questId, int quant = 1)
    {
        Army.registerMessage(item, false);
        if (Army.isDone(20)) return;
        // Core.Equip(Bot.Config.Get<string>("SafeClass"));
        // Army.registerMessage(item);

        Core.BankingBlackList.Add(item);
        Core.AddDrop(item);
        if (map.ToLower() == "eridani")
        {
            Core.AddDrop("Tooth");
            Core.AddDrop("Wisdom Tooth");
        }

        Bot.Sleep(1000);
        // Core.Equip(Bot.Config.Get<string>("ClassToUse"));
        //Core.EquipClass(classType);
        Core.Join(map);

        //Army.waitForPartyCell("Enter", "Spawn");
        if (questId != 0)
            Core.RegisterQuests(questId);


        Army.DivideOnCellsPriority(cells, priorityCell: priorityCell, setAggro: true, log: true, equipClass: true);

        Core.FarmingLogger(item, quant);
        Core.Logger($"army: starting {quant} {item}");
        Bot.Skills.StartAdvanced("1 | 2 | 3 | 4");
        Army.AggroMonStart();
        Army.StartFarm(item, quant);

        Core.CancelRegisteredQuests();
        Army.AggroMonStop(true);
        Core.Jump(Bot.Player.Cell, Bot.Player.Pad);
        Core.ToBank(item);
        Core.Logger($"everyone have finished {quant} {item}");
    }
}
