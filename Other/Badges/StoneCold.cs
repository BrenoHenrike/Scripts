//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Doomwood/Temple.cs
using Skua.Core.Interfaces;

public class StoneCold
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public Temple Temple = new();

    public void ScriptMain(IScriptInterface bot)
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

        Core.Logger($"Doing Temple story for {badge} badge");
        Temple.StoryLine();
    }

    private string badge = "Stone Cold";
}
