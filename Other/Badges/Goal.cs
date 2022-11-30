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
        while (!Core.HasWebBadge(badge)) //I hope this works
        {
            Core.SendPackets("%xt%zm%ia%1%rval%btnPuntting%%");
			Bot.Sleep(15000);
            Core.Jump("Enter", "Right");
        }
    }

    private string badge = "GOAL!";
}