//cs_include Scripts/CoreBots.cs
using Skua.Core.Interfaces;

public class CornelisRebornbadge
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (!Core.HasAchievement(13))
            return;

        Core.EnsureAccept(1632);
        Core.HuntMonster("cornelis", "Gargoyle", "Gargoyle Horn", 100, false);
        Core.EnsureComplete(1632);
    }
}
