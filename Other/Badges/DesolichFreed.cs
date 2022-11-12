//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/EtherstormWastes.cs
using Skua.Core.Interfaces;

public class DesolichFreed
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreFarms Farm = new();
    public EtherStormWastes ESW = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        Core.Logger("Gotta do the story first, will get the badge during the story don't worry :D");
        ESW.DoAll();

        Core.SetOptions(false);
    }
}
