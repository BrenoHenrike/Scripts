//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
//cs_include Scripts/Legion/CoreLegion.cs
//cs_include Scripts/CoreStory.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Quests;

public class ArmyLegionFealty2
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();
    public CoreLegion Legion = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyLegionFealty2";
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
        sCore.SkipOptions
    };

    public string[] LR2 =
    {
        "Conquest Wreath",
        "Grim Cohort Conquered",
        "Ancient Cohort Conquered",
        "Pirate Cohort Conquered",
        "Battleon Cohort Conquered",
        "Mirror Cohort Conquered",
        "Darkblood Cohort Conquered",
        "Vampire Cohort Conquered",
        "Spirit Cohort Conquered",
        "Dragon Cohort Conquered",
        "Doomwood Cohort Conquered"
    };

    public void ScriptMain(IScriptInterface bot)
    {
        Core.BankingBlackList.AddRange(LR2);

        Core.SetOptions(disableClassSwap: true);
        bot.Options.RestPackets = false;

        Core.SetOptions(false);
    }

    public void LegionFealty2(int quant = 50)
    {
        if (Core.CheckInventory("Conquest Wreath", quant))
            return;

        Legion.JoinLegion();

        Core.EquipClass(ClassType.Farm);
        Core.FarmingLogger($"Conquest Wreath", quant);
        while (!Bot.ShouldExit && !Core.CheckInventory("Conquest Wreath", quant))
        {
            Bot.Quests.UpdateQuest(3008);
            GetItem("doomvault", "*", 6898, "Grim Cohort Conquered", 500);
            GetItem("mummies", "*", 6898, "Ancient Cohort Conquered", 500);
            GetItem("wrath", "*", 6898, "Pirate Cohort Conquered", 500);
            GetItem("doomwar", "*", 6898, "Battleon Cohort Conquered", 500);
            GetItem("overworld", "*", 6898, "Mirror Cohort Conquered", 500);
            GetItem("deathpits", "*", 6898, "Darkblood Cohort Conquered", 500);
            GetItem("maxius", "*", 6898, "Vampire Cohort Conquered", 500);
            GetItem("curseshore", "*", 6898, "Spirit Cohort Conquered", 500);
            GetItem("dragonbone", "*", 6898, "Dragon Cohort Conquered", 500);
            GetItem("doomwood", "*", 6898, "Doomwood Cohort Conquered", 500);
            Core.EnsureComplete(6898);
        }
    }

    public void GetItem(string map = null, string Monster = null, int questID = 000, string item = null, int quant = 0)
    {
        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();

        Quest QuestData = Core.EnsureLoad(questID);
        ItemBase[] RequiredItems = QuestData.Requirements.ToArray();
        ItemBase[] QuestReward = QuestData.Rewards.ToArray();

        Core.AddDrop(item);
        Core.EquipClass(ClassType.Farm);
        if (!Bot.Quests.Active.Contains(QuestData))
            Core.EnsureAccept(questID);

        Army.SmartAggroMonStart(map, Monster);

        Core.FarmingLogger(item, quant);
        while (!Bot.ShouldExit && !Core.CheckInventory(item, quant))
            Core.HuntMonster(map, Monster, log: false);
        Army.AggroMonStop(true);
        Core.CancelRegisteredQuests();
    }
}