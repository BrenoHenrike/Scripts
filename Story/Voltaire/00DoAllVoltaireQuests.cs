//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/Story/Voltaire/11BattledoomStory.cs
//cs_include Scripts/Story/Voltaire/CoreVoltaire.cs
using Skua.Core.Interfaces;

public class DoAllVoltaire
{
    public IScriptInterface Bot => IScriptInterface.Instance;
    public CoreBots Core => CoreBots.Instance;
    public CoreStory Story = new();
    public Battledoom Battledoom = new();
    public CoreVoltaire CoreVoltaire = new();

    public void ScriptMain(IScriptInterface bot)
    {
        Core.SetOptions();

        CoreVoltaire.DoAll();
        
        Battledoom.StoryLine();

        Core.SetOptions(false);
    }
}