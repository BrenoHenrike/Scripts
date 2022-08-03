//cs_include Scripts/CoreBots.cs
using RBot;

public class CornelisRebornbadge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (Core.HasAchievement(13))
            return;

        Core.EnsureAccept(1632);
        Core.HuntMonster("cornelis", "Gargoyle", "Gargoyle Horn", 100, false);
        Core.EnsureComplete(1632);
    }
}
