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
        string Map = Floor == 1 ? "towerofdoom" : "towerofdoom"+Floor.ToString();
        Bot.SendPacket($"%xt%zm%cmd%{Bot.Map.RoomID}%tfer%{Bot.Player.Username}%{Map}%");
        Bot.Wait.ForMapLoad($"towerofdoom{Floor}");
        if (Bot.Player.Cell == "Prison")
        {
            Core.Logger($"Tower of Doom {Floor-1} not completed yet, doing that first");
            TowerProgress(Floor-1);
        }
        if (Bot.Map.Name != Map)
        {
            Bot.SendPacket($"%xt%zm%cmd%{Bot.Map.RoomID}%tfer%{Bot.Player.Username}%{Map}%");
            Bot.Wait.ForMapLoad($"towerofdoom{Floor}");
        }
        Bot.Player.Jump("r10", "Left");
        if (Bot.Player.Cell == "r10") 
        {
            Core.Logger($"Tower of Doom {Floor} already complete");
            return;
        }
        Core.Logger($"Tower of Doom {Floor}");
        Bot.SendClientPacket($"{{\"t\":\"xt\",\"b\":{{\"r\":-1,\"o\":{{\"cmd\":\"updateQuest\",\"iValue\":{Floor},\"iIndex\":159}}}}}}", "json");
        Core.EquipClass(ClassType.Solo);
        Core.EnsureAccept(3474+Floor);
        Core.KillMonster(Map, "r10", "Left", "*", publicRoom : true);
        Core.EnsureComplete(3474+Floor);
    }
}
