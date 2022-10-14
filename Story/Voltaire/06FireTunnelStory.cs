//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Voltaire/CoreVoltaire.cs
using Skua.Core.Interfaces;

public class FireTunnel
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public CoreVoltaire CoreVoltaire = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreVoltaire.FireTunnel();

        Core.SetOptions(false);
    }

}