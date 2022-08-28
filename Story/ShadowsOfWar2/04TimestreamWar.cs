//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/Story/ShadowsOfWar2/CoreSoW2.cs
using Skua.Core.Interfaces;

public class TimestreamWar
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public SoW2 SoW2 = new();
    
    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        SoW2.TimestreamWar();

        Core.SetOptions(false);
    }
}
