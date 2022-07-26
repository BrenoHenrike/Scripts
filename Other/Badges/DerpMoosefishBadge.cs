//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
using RBot;

public class DerpMoosefishBadge
{
    public ScriptInterface Bot => ScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();

    public void ScriptMain(ScriptInterface bot)
    {
        Core.SetOptions();

        Badge();

        Core.SetOptions(false);
    }

    public void Badge()
    {
        if (Core.HasAchievement(14))
            return;

        Farm.FishingREP(11, true);
    }
}
