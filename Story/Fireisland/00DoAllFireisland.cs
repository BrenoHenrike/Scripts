//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Phoenixrise.cs
//cs_include Scripts/Story/FireIsland/CoreFireIsland.cs

using Skua.Core.Interfaces;

public class DoAllFireisland
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFireIsland CoreFireIsland = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreFireIsland.CompleteFireIsland();

        Core.SetOptions(false);
    }
}
