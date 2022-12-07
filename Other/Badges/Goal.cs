//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class GoalBadge
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (Core.HasWebBadge(badge))
        {
            Core.Logger($"Already have the {badge} badge");
            return;
        }

        Core.Logger($"Kicking some balls for the {badge}");
        Core.Join("chute");
        Core.EnsureAccept(8543);
        Bot.Map.GetMapItem(9837);
        Bot.Sleep(Core.ActionDelay);
        Core.EnsureComplete(8543);
    }

    private string badge = "GOAL!";
}