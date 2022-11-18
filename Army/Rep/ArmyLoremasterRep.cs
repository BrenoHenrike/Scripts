//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Army/CoreArmyLite.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Options;

public class ArmyLoremasterRep
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;
    private CoreFarms Farm = new();
    private CoreAdvanced Adv => new();
    private CoreArmyLite Army = new();

    private static CoreBots sCore = new();
    private static CoreArmyLite sArmy = new();

    public string OptionsStorage = "ArmyLoremasterRep";
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

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();
        bot.Options.RestPackets = false;

        Setup();

        Core.SetOptions(false);
    }

    public void Setup()
    {
        if (Farm.FactionRank("Loremaster") >= 10)
            return;

        Core.PrivateRooms = true;
        Core.PrivateRoomNumber = Army.getRoomNr();
        Core.EquipClass(ClassType.Farm);
        Farm.ToggleBoost(BoostType.Reputation);
        if (!Bot.ShouldExit && Farm.FactionRank("Loremaster") < 10)
        {
            if (Core.IsMember)
            {
                if (!Bot.Quests.IsUnlocked(3032)) //Need boat for this questsline (member only)
                {
                    Core.EnsureAccept(3029); //Rosetta Stones 3029
                    Core.HuntMonster("druids", "Void Bear", "Voidstone ", 6);
                    Core.EnsureComplete(3029);

                    Core.EnsureAccept(3030); // Cull the Foot Soldiers 3030
                    Core.HuntMonster("druids", "Void Larva", "Void Larvae Death Cry", 4);
                    Core.EnsureComplete(3030);

                    Core.EnsureAccept(3031); // Bad Vibes 3031
                    Core.HuntMonster("druids", "Void Ghast", "Ghast's Death Cry", 4);
                    Core.EnsureComplete(3031);
                }
                Core.EquipClass(ClassType.Solo);
                Core.RegisterQuests(3032); //Quite the Problem 3032
                while (!Bot.ShouldExit && Farm.FactionRank("Loremaster") < 10)
                {
                    Army.SmartAggroMonStart("druids", "Young Void Giant");
                }
                Army.AggroMonStop(true);
                Core.CancelRegisteredQuests();
            }
            else if (!Core.IsMember)
            {
                Core.EquipClass(ClassType.Farm);
                Core.RegisterQuests(7505); //Studying the Rogue 7505
                while (!Bot.ShouldExit && Farm.FactionRank("Loremaster") < 10)
                {
                    Army.SmartAggroMonStart("wardwarf", "Drow Assassin", "D'wain Jonsen");
                }
                Army.AggroMonStop(true);
                Core.CancelRegisteredQuests();
            }
        }
    }
}