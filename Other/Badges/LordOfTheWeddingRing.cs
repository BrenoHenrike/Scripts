//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/ArtixWedding.cs
using Skua.Core.Interfaces;

public class LordOfTheWeddingRing
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public ArtixWedding AW = new();

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

        Core.Logger($"Doing Artix Wedding story for {badge} badge");
        AW.ArtixWeddingComplete();
        
    }

    private string badge = "Lord of the Wedding Ring";
}
