//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Seasonal/Frostvale/Frostvale.cs
using Skua.Core.Interfaces;

public class FrostvaleBadges
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public Frostvale Frostvale = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Badges();

        Core.SetOptions(false);
    }

    public void Badges()
    {
        if (Core.HasWebBadge(badge1) && Core.HasWebBadge(badge2))
        {
            Core.Logger($"Already have the Frostvale badges");
            return;
        }

        if (!Core.HasWebBadge(badge1))
            Core.Logger("World Savior: Reward from; \"Face Kezeroth\" Quest.");

        if (!Core.HasWebBadge(badge2))
            Core.Logger("Frost Defanged: Reward from; \"Defeat Frostfang (Evil/Good)\" ");

        Core.Logger("Doing Frostvale story for badges");
        Frostvale.DoAll();
    }

    private string badge1 = "World Savior";
    private string badge2 = "Frost Defanged";
}
