//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using Skua.Core.Interfaces;

public class MoglinPunter
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (Core.HasWebBadge(badge) || !Core.isSeasonalMapActive("punt"))
        {
            Core.Logger($"Already have the {badge} badge, or the map is not available.");
            return;
        }

        Core.Logger($"Doing quest for {badge} badge");

        Core.Join("punt");
        while (!Bot.ShouldExit && !Core.HasWebBadge(badge))
        {
            Core.Jump("Enter", "Spawn");
            Bot.Send.Packet("%xt%zm%ia%1%rval%btnPuntting%%");
        }
    }

    private string badge = "Moglin Punter";
}
