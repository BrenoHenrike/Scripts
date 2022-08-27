//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Phoenixrise.cs
//cs_include Scripts/Story/FireIsland/CoreFireIsland.cs


using Skua.Core.Interfaces;

public class Phoenixrise
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public PhoenixriseStory PhoenixriseStory = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        PhoenixriseStory.Phoenixrise();

        Core.SetOptions(false);
    }
}
