//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Fireisland/CoreFireisland.cs

using Skua.Core.Interfaces;

public class Embersea
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Fireisland.Embersea();

        Core.SetOptions(false);
    }
}
