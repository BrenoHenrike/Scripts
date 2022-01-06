//cs_include Scripts/CoreBots.cs
using RBot;

public class TowerOfDoom
{
    public ScriptInterface Bot => ScriptInterface.Instance;

    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        TowerProgress();

        Core.SetOptions(false);
    }

    public void TowerProgress(int Floor = 10)
    {
        if (Floor == 1)
        {
            Core.Logger("Tower of Doom 1");
            Core.SendPackets("{\"t\":\"xt\",\"b\":{\"r\":-1,\"o\":{\"cmd\":\"updateQuest\",\"iValue\":1,\"iIndex\":159}}}", 1, true);
            Core.EquipClass(ClassType.Solo);
            Core.SmartKillMonster(3475, "towerofdoom", "Dread Klunk", 1, true);
            return;
        }
        Bot.Player.Join($"towerofdoom{Floor}");
        if (Bot.Player.Cell == "Prison")
        {
            Core.Logger($"Tower of Doom {Floor-1} not completed yet, doing that first");
            TowerProgress(Floor-1);
        }
        Bot.Player.Join($"towerofdoom{Floor}");
        Core.Jump("r10", "Left");
        if (Bot.Player.Cell == "r10") 
        {
            Core.Logger($"Tower of Doom {Floor} already complete");
            return;
        }
        Core.Logger($"Tower of Doom {Floor}");
        Core.SendPackets($"{{\"t\":\"xt\",\"b\":{{\"r\":-1,\"o\":{{\"cmd\":\"updateQuest\",\"iValue\":{Floor},\"iIndex\":159}}}}}}", 1, true);
        Core.EquipClass(ClassType.Solo);
        Core.EnsureAccept(3474+Floor);
        Core.KillMonster($"towerofdoom{Floor}", "r10", "Left", "*");
        Core.EnsureAccept(3474+Floor);
    }
}
